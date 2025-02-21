using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] public Text _movesText;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _losePanel;
    [SerializeField] private Text _bestResultText; 

    // Для тестовой инициализации
    public bool IsGameOver => _losePanel.activeSelf;

    public void UpdateMoves(int current, int max)
    {
        _movesText.text = $"Ходы: {current}/{max}";
    }

    public void ShowLevelComplete()
    {
        _winPanel.SetActive(true);
    }

    public void ShowGameOver()
    {
        _losePanel.SetActive(true);
    }

    public void ShowBestResult(int bestMoves)
    {
        if (bestMoves == -1)
        {
            _bestResultText.text = "Лучший результат: не установлен";
        }
        else
        {
            _bestResultText.text = $"Лучший результат: {bestMoves} ходов";
        }
    }
}