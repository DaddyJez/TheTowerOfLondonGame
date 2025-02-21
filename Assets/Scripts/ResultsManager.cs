using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ResultsManager : MonoBehaviour
{
    private const string SavedLevelsKey = "SavedLevels";

    public void SaveResult(int moves)
    {
        string levelName = SceneManager.GetActiveScene().name;

        int previousResult = PlayerPrefs.GetInt(levelName, -1);

        if (previousResult == -1 || moves < previousResult)
        {
            PlayerPrefs.SetInt(levelName, moves);

            string savedLevels = PlayerPrefs.GetString(SavedLevelsKey, "");
            if (!savedLevels.Contains(levelName))
            {
                savedLevels += (savedLevels.Length > 0 ? "," : "") + levelName;
                PlayerPrefs.SetString(SavedLevelsKey, savedLevels);
            }

            PlayerPrefs.Save();
        }
    }

    public int GetBestResultForCurrentLevel()
    {
        string levelName = SceneManager.GetActiveScene().name;
        return PlayerPrefs.GetInt(levelName, -1); // -1 означает, что результат не найден
    }

    public Dictionary<string, int> GetAllResults()
    {
        Dictionary<string, int> results = new Dictionary<string, int>();

        string savedLevels = PlayerPrefs.GetString(SavedLevelsKey, "");
        string[] levelNames = savedLevels.Split(',');

        foreach (string levelName in levelNames)
        {
            if (!string.IsNullOrEmpty(levelName))
            {
                int moves = PlayerPrefs.GetInt(levelName, -1);
                if (moves != -1)
                {
                    results[levelName] = moves;
                }
            }
        }

        return results;
    }

    // Очищаем все сохраненные результаты (для тестирования)
    public void ClearAllResults()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}