using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Настройки")]
    [SerializeField] private LevelConfig _currentLevel;

    [Header("Ссылки")]
    [SerializeField] private RodManager _rodManager;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private ResultsManager _resultsManager;

    private int _currentMoves;
    private Rod[] _allRods;

    private void Start()
    {
        _allRods = FindObjectsByType<Rod>(FindObjectsSortMode.None);
        _currentMoves = 0;
        _uiManager.UpdateMoves(_currentMoves, _currentLevel.maxMoves);
        
        if (_resultsManager == null)
            _resultsManager = FindFirstObjectByType<ResultsManager>();

        if (_uiManager == null)
            _uiManager = FindFirstObjectByType<UIManager>();
    }

    // Для тестовой инициализации
    public void SetTestRods(Rod[] rods) => _allRods = rods;
    public UIManager UIManager => _uiManager;

    public void HandleDragEnd(GameObject ringObject, GameObject targetRod)
    {
        _currentMoves++;
        _uiManager.UpdateMoves(_currentMoves, _currentLevel.maxMoves);

        Ring ring = ringObject.GetComponent<Ring>();
        if (ring == null)
        {
            Logger.Error("Кольцо не найдено!");
            return;
        }

        Rod rod = targetRod?.GetComponent<Rod>();
        if (rod != null && rod.TryAddRing(ring))
        {
            Logger.Info($"Кольцо {ring.name} перемещено на {targetRod.name}");
            if (_currentMoves > _currentLevel.maxMoves)
            {
                _uiManager.ShowGameOver();
                return;
            }

            if (CheckLevelComplete())
            {
                _resultsManager.SaveResult(_currentMoves);
                _uiManager.ShowLevelComplete();

                int bestResult = _resultsManager.GetBestResultForCurrentLevel();
                _uiManager.ShowBestResult(bestResult);
            }
            return;
        }

        if (ring.CurrentRod != null)
        {
            ring.CurrentRod.TryAddRing(ring);
            Logger.Warn($"Кольцо {ring.name} возвращено на исходный стержень");
        }
        else
        {
            ring.transform.position = ring.OriginalPosition;
            Logger.Error($"Кольцо {ring.name} возвращено в стартовую позицию");
        }
    }

    // тут я использую public для последующего тестирования
    public bool CheckLevelComplete()
    {
        foreach (var target in _currentLevel.targetRods)
        {
            Rod rod = System.Array.Find(_allRods, r => r.Index == target.rodIndex);
            if (rod == null)
            {
                Logger.Error($"Стержень с индексом {target.rodIndex} не найден!");
                return false;
            }

            if (!rod.CheckConfiguration(target.ringSizes))
                return false;
        }
        return true;
    }
}