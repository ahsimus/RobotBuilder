using System.Collections;
using UnityEngine;

public class Animation : MonoBehaviour
{
    [SerializeField] private Transform target;

    [Header("Jump")]
    [SerializeField] private float jumpHeight = 1f;
    [SerializeField] private float jumpDuration = 0.5f;

    [Header("Squash & Stretch")]
    [SerializeField] private float squashFactor = 0.7f;
    [SerializeField] private float stretchFactor = 1.3f;
    [SerializeField] private float squashDuration = 0.1f;

    private Coroutine currentAnimation;

    private Vector3 startPosition;
    private Vector3 startScale;

    private void Awake()
    {
        if (target == null)
            target = transform;

        startPosition = target.localPosition;
        startScale = target.localScale;
    }

    public void Play()
    {
        if (currentAnimation != null)
            StopCoroutine(currentAnimation);

        currentAnimation = StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        // Squash
        Vector3 squashScale = new(
            startScale.x * 1.2f,
            startScale.y * squashFactor,
            startScale.z * 1.2f);

        yield return LerpScale(startScale, squashScale, squashDuration);

        // Jump + Stretch
        Vector3 stretchScale = new(
            startScale.x * 0.9f,
            startScale.y * stretchFactor,
            startScale.z * 0.9f);

        float elapsed = 0f;

        while (elapsed < jumpDuration)
        {
            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(elapsed / jumpDuration);

            // Парабола
            float yOffset = 4f * jumpHeight * t * (1f - t);

            target.localPosition = startPosition + Vector3.up * yOffset;

            if (t < 0.3f)
            {
                target.localScale = Vector3.Lerp(
                    squashScale,
                    stretchScale,
                    t / 0.3f);
            }
            else
            {
                target.localScale = Vector3.Lerp(
                    stretchScale,
                    startScale,
                    (t - 0.3f) / 0.7f);
            }

            yield return null;
        }

        target.localPosition = startPosition;
        target.localScale = startScale;

        currentAnimation = null;
    }

    private IEnumerator LerpScale(
        Vector3 from,
        Vector3 to,
        float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(elapsed / duration);

            target.localScale = Vector3.Lerp(from, to, t);

            yield return null;
        }

        target.localScale = to;
    }
}