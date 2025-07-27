using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class FeedbackManager : MonoBehaviour
{
    public static FeedbackManager Instance;
    public GameObject confettiPrefab;
    public GameObject goodJobText;
    public GameObject nextPrompt;

    private void Awake()
    {
        Instance = this;
    }

    public void OnCorrectWord(List<LetterBlock> blocks)
    {
        foreach (var block in blocks)
        {
            Instantiate(confettiPrefab, block.transform.position + Vector3.up * 0.5f, Quaternion.identity);
        }

        goodJobText.SetActive(true);
        nextPrompt.SetActive(true);
    }

    public void OnWrongWord(List<LetterBlock> blocks)
    {
        foreach (var block in blocks)
        {
            block.Shake(); // Optional animation
            block.ResetBlock();
        }
    }
}
