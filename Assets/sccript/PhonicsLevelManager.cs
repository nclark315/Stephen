using UnityEngine;
using System.Collections.Generic;

public class PhonicsLevelManager : MonoBehaviour
{
    public PhonicsGridManager gridManager;

    [SerializeField] List<string> levelData = new List<string>();
    [SerializeField] List<Sprite> objectImgs = new List<Sprite>();

    private void Start()
    {
        LoadNextLevel(); // 🔴 Without this, nothing gets created
    }

    public void LoadNextLevel()
    {
            gridManager.GenerateGrid(levelData, objectImgs);
    }
}
