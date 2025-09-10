using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    [Header("Zoom Settings")]
    public float zoomDistance = 3f;            // Distance closer to target during zoom
    public float moveSpeed = 2f;               // Speed of movement
    public float rotateSpeed = 2f;             // Speed of rotation

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private Coroutine currentRoutine;

    void Start()
    {
        // Store initial camera position and rotation
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    /// <summary>
    /// Smoothly moves and rotates camera to look toward the target with a zoom effect.
    /// </summary>
    /// <param name="target">Target Transform to focus on.</param>
    public void LookAtTarget(Transform target)
    {
        if (currentRoutine != null) StopCoroutine(currentRoutine);
        currentRoutine = StartCoroutine(LookAtRoutine(target));
    }

    /// <summary>
    /// Smoothly returns camera to its original position and rotation.
    /// </summary>
    public void GoBack()
    {
        if (currentRoutine != null) StopCoroutine(currentRoutine);
        currentRoutine = StartCoroutine(GoBackRoutine());
    }

    private IEnumerator LookAtRoutine(Transform target)
    {
        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;

        // Calculate target position slightly closer to the target
        Vector3 direction = (transform.position - target.position).normalized;
        Vector3 targetPos = target.position + direction * zoomDistance;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * moveSpeed;

            // Smooth position transition
            transform.position = Vector3.Lerp(startPos, targetPos, t);

            // Smooth rotation transition to look at the target
            Quaternion targetRot = Quaternion.LookRotation(target.position - transform.position);
            transform.rotation = Quaternion.Slerp(startRot, targetRot, t * rotateSpeed);

            yield return null;
        }

        // Final snap to exact position and rotation
        transform.position = targetPos;
        transform.rotation = Quaternion.LookRotation(target.position - transform.position);
    }

    private IEnumerator GoBackRoutine()
    {
        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * moveSpeed;

            transform.position = Vector3.Lerp(startPos, originalPosition, t);
            transform.rotation = Quaternion.Slerp(startRot, originalRotation, t * rotateSpeed);

            yield return null;
        }
        
        transform.position = originalPosition;
        transform.rotation = originalRotation;
    }
}
