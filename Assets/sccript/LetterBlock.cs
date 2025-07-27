using UnityEngine;
using TMPro;

public class LetterBlock : MonoBehaviour
{
    public string letter;

    [Header("Visuals")]
    public Material normalMaterial;
    public Material highlightedMaterial;
    public Renderer rend;

    [Header("Text")]
    public TextMeshPro letterText; // Assign in inspector

    [Header("Audio")]
    public AudioClip tapSound;
    private AudioSource audioSource;

    private Vector3 originalScale;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        originalScale = transform.localScale;

        // Auto-assign renderer if not set
        if (rend == null)
            rend = GetComponent<Renderer>();
    }

    public void SetLetter(string l)
    {
        letter = l.ToUpper();

        if (letterText != null)
            letterText.text = letter;
    }

    public void OnSelected()
    {
        if (rend != null && highlightedMaterial != null)
            rend.material = highlightedMaterial;

        transform.localScale = originalScale * 1.2f;

        if (tapSound && audioSource)
            audioSource.PlayOneShot(tapSound);
    }

    public void ResetBlock()
    {
        if (rend != null && normalMaterial != null)
            rend.material = normalMaterial;

        transform.localScale = originalScale;
    }

    public void Shake()
    {
        // Optional: Quick shake animation using iTween / LeanTween / DOTween
        // For now, we'll just wiggle manually
        StartCoroutine(ShakeCoroutine());
    }

    private System.Collections.IEnumerator ShakeCoroutine()
    {
        Vector3 originalPos = transform.position;
        float duration = 0.2f;
        float strength = 0.1f;
        float time = 0;

        while (time < duration)
        {
            transform.position = originalPos + Random.insideUnitSphere * strength;
            time += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPos;
    }
}
