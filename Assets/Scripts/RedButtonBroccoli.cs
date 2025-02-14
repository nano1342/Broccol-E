using System.Collections;
using UnityEngine;

public class RedButtonBroccoli : MonoBehaviour
{
    public GameObject sphere;
    public GameObject objectToMove;
    public GameObject targetObject;

    // Sphere
    private float angleValue = -135f;
    private bool isRotating = false;

    // Broccoli
    public float jumpHeight = 1f;
    public float moveDuration = 1f;
    public float rotationSpeed = 90f;


    public void OnMouseDown()
    {
        if (!isRotating && sphere != null && objectToMove != null)
        {
            StartCoroutine(RotateSequence());
        }
    }

    public void OnTriggerPressed()
    {
        if (!isRotating && sphere != null && objectToMove != null)
        {
            StartCoroutine(RotateSequence());
        }
    }

    IEnumerator RotateSequence()
    {
        isRotating = true; 

        yield return RotateSmoothly(angleValue); // Rotation initiale

        yield return ThrowObject(targetObject.transform.position); // Broccoli jumping out

        yield return RotateSmoothly(-angleValue); // Rotation inverse

        isRotating = false;
    }

    IEnumerator RotateSmoothly(float targetAngle)
    {
        Quaternion startRotation = sphere.transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 0, targetAngle);
        float duration = 0.6f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            sphere.transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        sphere.transform.rotation = endRotation;
    }
    IEnumerator ThrowObject(Vector3 target)
    {
        Vector3 startPos = objectToMove.transform.position;
        Quaternion startRotation = objectToMove.transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 0, rotationSpeed);

        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            float t = elapsed / moveDuration; // Normalisation du temps (0 à 1)

            // Interpolation de la position horizontale
            Vector3 horizontalPos = Vector3.Lerp(startPos, target, t);

            // Calcul de la hauteur (courbe parabolique)
            float height = Mathf.Sin(t * Mathf.PI) * jumpHeight;

            // Application de la position combinée
            objectToMove.transform.position = new Vector3(horizontalPos.x, horizontalPos.y + height, horizontalPos.z);

            // Application de la rotation
            objectToMove.transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        objectToMove.transform.position = target; // Assure la position finale
        objectToMove.transform.rotation = endRotation; // Assurer la rotation finale
    }
}
