using UnityEngine;
using System.Collections.Generic;

public class PhonicsLevelManager : MonoBehaviour
{
    public PhonicsGridManager gridManager;

    private int currentLevel = 0;
    private List<List<string>> levelData = new List<List<string>>()
    {
        new List<string>{"C", "A", "T", "X", "Y", "Z", "B", "O", "G"},
        new List<string>{"D", "O", "G", "L", "M", "N", "P", "Q", "R"}
        // Add more levels
    };

    private void Start()
    {
        LoadNextLevel(); // 🔴 Without this, nothing gets created
    }

    public void LoadNextLevel()
    {
        if (currentLevel < levelData.Count)
        {
            gridManager.GenerateGrid(levelData[currentLevel]);
            currentLevel++;
        }
    }
}
