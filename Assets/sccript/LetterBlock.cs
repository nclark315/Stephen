using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Collections;

public class LetterBlock : MonoBehaviour
{
    public string letter;
    private bool isSelected = false;
    private bool isMovingDone = false;
    private bool isRotating = false;
    private bool rotationStarted = false;


    private float hoverAmplitude = 0.2f;
    private float hoverSpeed = 2f;
    private float hoverStartY;
    private float rotationDelay = 2f;


    private float smoothSpeed = 5f; // Smoothness speed
    private float threshold = 0.1f;
    private Vector3 targetScale;
    private Vector3 targetPosition;
    private Quaternion targetRotation;

    [Header("Visuals")]
    public Material normalMaterial;
    public Material highlightedMaterial;
    public Material doneMaterial;
    public Renderer rend;

    [Header("Text")]
    public TextMeshPro[] letterText; // Assign in inspector
    public SpriteRenderer[] spImgs;

    [Header("Audio")]
    public AudioClip tapSound;
    private AudioSource audioSource;

    private Vector3 originalScale;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    bool clickable = true;


    void Start()
    {
        originalScale = transform.localScale;
        originalPosition = transform.localPosition;
        originalRotation = transform.localRotation;
    }
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        originalScale = transform.localScale;

        // Auto-assign renderer if not set
        if (rend == null)
            rend = GetComponent<Renderer>();
    }

    public void SetLetter(string l, Sprite blockImg)
    {
        letter = l.ToUpper();

        for (int i = 0; i < letterText.Length; i++)
        {
            if (letterText[i] != null)
                letterText[i].text = letter;

            if (spImgs[i] != null)
                spImgs[i].sprite = blockImg;
        }

    }


    public void OnSelected()
    {
        if (isSelected) return;

        rend.material = highlightedMaterial;
        isSelected = true;
        isMovingDone = false;

        // Set the target scale and position
        targetScale = originalScale * 1.2f;  // Smooth scale up
        targetPosition = new Vector3(originalPosition.x, originalPosition.y+0.3f, 2f); // Smooth Z movement
        targetRotation = Quaternion.Euler(0, -180f, 0);
        StartCoroutine(StartRotationAfterDelay());
    }
    private IEnumerator StartRotationAfterDelay()
    {
        rotationStarted = true;
        yield return new WaitForSeconds(rotationDelay);
        isRotating = true;
    }
    void Update()
    {
        if (!isSelected) return;

        if (!isMovingDone)
        {
            // Smoothly scale
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * smoothSpeed);

            // Smoothly move only Z position (X, Y remain the same)
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * smoothSpeed);

            bool scaleDone = Vector3.Distance(transform.localScale, targetScale) <= threshold;
            bool positionDone = Vector3.Distance(transform.position, targetPosition) <= threshold;

            if (scaleDone && positionDone)
            {
                isMovingDone = true;
                Debug.Log("moving  done");
            }
        }
        if (isRotating)
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * smoothSpeed);

            // stop when close enough and snap
            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.5f)
            {
                isRotating = false;
                transform.rotation = targetRotation;
            }
        }


    }
    public void ResetBlock()
    {

        isSelected = false;
        transform.localScale = originalScale;
    }

    public void Shake()
    {
        // Optional: Quick shake animation using iTween / LeanTween / DOTween
        // For now, we'll just wiggle manually
        StartCoroutine(ShakeCoroutine());
    }
    public void Disable(Vector3 targetPosN)
    {
        clickable = false;
        isMovingDone = false;
        rend.material = doneMaterial;
        targetScale = originalScale;

        originalScale = transform.localScale;

        originalPosition = transform.localPosition;
        targetPosition = targetPosN;
        

    }
    public bool GetStatus()
    {
        return clickable;
    }
    private System.Collections.IEnumerator ShakeCoroutine()
    {
        Vector3 originalPos = transform.position;
        float duration = 0.2f;
        float strength = 0.1f;
        float time = 0;

        while (time < duration)
        {
            transform.position = originalPos + UnityEngine.Random.insideUnitSphere * strength;
            time += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPos;
    }


}
