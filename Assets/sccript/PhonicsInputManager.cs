using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PhonicsInputManager : MonoBehaviour
{
    private List<LetterBlock> selectedBlocks = new List<LetterBlock>();
    public int wordLength = 3;

    [Header("Camera Reference")]
    public Camera mainCamera; // Assign your Cinemachine TPS Camera here

    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main; // Fallback
    }

    void Update()
    {
        if (Pointer.current != null && Pointer.current.press.wasPressedThisFrame)
        {
            Vector2 screenPosition = Pointer.current.position.ReadValue();
            Ray ray = mainCamera.ScreenPointToRay(screenPosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                LetterBlock block = hit.collider.GetComponent<LetterBlock>();
                if (block != null && !selectedBlocks.Contains(block))
                {
                    block.OnSelected();
                    selectedBlocks.Add(block);

                    if (selectedBlocks.Count == wordLength)
                        ValidateWord();
                }
            }
        }
    }

    void ValidateWord()
    {
        string formedWord = "";
        foreach (var block in selectedBlocks)
            formedWord += block.letter;

        if (WordValidator.IsCorrectWord(formedWord))
            FeedbackManager.Instance.OnCorrectWord(selectedBlocks);
        else
            FeedbackManager.Instance.OnWrongWord(selectedBlocks);

        selectedBlocks.Clear();
    }
}
