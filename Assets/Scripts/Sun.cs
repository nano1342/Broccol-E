using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    public Transform anchorSunSet;
    public Transform anchorSunRise;
    public float moveDuration = 10f;

    public void Rise()
    {
        StartCoroutine(MoveCubeSmoothly(this.transform, anchorSunSet.position, anchorSunRise.position, moveDuration));
    }

    private IEnumerator MoveCubeSmoothly(Transform target, Vector3 startPos, Vector3 endPos, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            target.position = Vector3.Lerp(startPos, endPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        target.position = endPos; // Ensure it reaches the exact end position
    }
}
