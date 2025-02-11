using System.Collections;
using UnityEngine;

public class ButtonLED : MonoBehaviour
{
    public GameObject cube1;
    public GameObject cube2;
    public Transform anchor;

    public float moveDistance = 0.01f; // Distance to move
    public float moveDuration = 0.1f; // Duration of the movement

    public void push()
    {
        //StartCoroutine(MoveCubeSmoothly(cube2.transform, cube2.transform.position, cube2.transform.position + new Vector3(0, 0, -moveDistance), moveDuration));
        StartCoroutine(MoveCubeSmoothly(cube2.transform, cube2.transform.position, anchor.position, moveDuration));
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


        // Smoothly move back to the original position
        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            target.position = Vector3.Lerp(endPos, startPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        target.position = startPos; // Ensure it reaches the exact start position
    }
}
