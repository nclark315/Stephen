using UnityEngine;
using System.Collections.Generic;

public class PhonicsGridManager : MonoBehaviour
{
    public GameObject letterBlockPrefab;
    public int gridSize = 3; // Can be increased per level
    public float spacing = 0.5f;

    public List<string> currentLetters; // Filled from word list or manually

    public Transform gridParent;

    public void GenerateGrid(List<string> letters, List<Sprite> imgs)
    {
        ClearGrid();
        currentLetters = letters;

        int index = 0;
        for (int x = gridSize-1; x >= 0; x--)
        {
            for (int z = gridSize-1; z >=0; z--)
            {
                Vector3 pos = new Vector3(z*spacing, x*spacing, 0);
                GameObject block = Instantiate(letterBlockPrefab);
                block.transform.parent = gridParent;
                block.transform.localPosition = pos;
                block.name = "Block_" + letters[index];
                block.GetComponent<LetterBlock>().SetLetter(letters[index], imgs[index]);
                index++;
            }
        }
    }

    private void ClearGrid()
    {
        foreach (Transform child in gridParent)
        {
            Destroy(child.gameObject);
        }
    }
}
