using UnityEngine;
using UnityEngine.UI;

public class RodExpectedSizes : MonoBehaviour
{
    [SerializeField] private Text _targetText; 
    [SerializeField] private Rod _rod; 
    [SerializeField] private LevelConfig _levelConfig; 

    private void Start()
    {
        if (_targetText == null)
        {
            Debug.LogError("Текстовый элемент не назначен!");
            return;
        }

        if (_rod == null)
        {
            Debug.LogError("Стержень не назначен!");
            return;
        }

        if (_levelConfig == null)
        {
            Debug.LogError("Конфигурация уровня не назначена!");
            return;
        }

        int[] targetSizes = GetTargetSizesForRod(_rod.Index);
        if (targetSizes != null)
        {
            _targetText.text = "Ожидается: " + string.Join(", ", targetSizes);
        }
        else
        {
            _targetText.text = "Нет целевых размеров";
        }
    }

    private int[] GetTargetSizesForRod(int rodIndex)
    {
        foreach (var targetRod in _levelConfig.targetRods)
        {
            if (targetRod.rodIndex == rodIndex)
            {
                return targetRod.ringSizes;
            }
        }
        return null; 
    }
}