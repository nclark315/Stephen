using UnityEngine;
using System.Collections.Generic;

public class PhonicsGridManager : MonoBehaviour
{
    public GameObject letterBlockPrefab;
    public int gridSize = 3; // Can be increased per level
    public float spacing = 0.5f;

    public List<string> currentLetters; // Filled from word list or manually

    public Transform gridParent;

    public void GenerateGrid(List<string> letters)
    {
        ClearGrid();
        currentLetters = letters;

        int index = 0;
        for (int x = 0; x < gridSize; x++)
        {
            for (int z = 0; z < gridSize; z++)
            {
                Vector3 pos = new Vector3(x *spacing, 0, z * spacing);
                GameObject block = Instantiate(letterBlockPrefab);
                block.transform.parent = gridParent;
                block.transform.localPosition = pos;
                block.name = "Block_" + letters[index];
                block.GetComponent<LetterBlock>().SetLetter(letters[index]);
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
