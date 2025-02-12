using UnityEngine;
using System.Collections;

public class PipeDoor : MonoBehaviour
{
    public bool doorOpened = false; // Indique si la porte est fermée
    public float rotationAngle = 150f; // Angle d'ouverture
    public float rotationSpeed = 3f; // Vitesse de rotation
    public Console console;

    public float pivotOffsetAmount = 0.04f;

    private Vector3 pivotOffset; // Stocke dynamiquement l'offset du pivot

    void Start()
    {
        float halfWidth = transform.localScale.x * 0.5f;  // Distance au bord
        pivotOffset = new Vector3(-halfWidth - pivotOffsetAmount, 0, 0);
    }

    public void Open()
    {
        Transform doorToOpen = transform;
        if (transform.parent != null && transform.parent.name.StartsWith("Door"))
        {
            doorToOpen = transform.parent;
        }

        PipeDoor doorScript = doorToOpen.GetComponent<PipeDoor>();
        PipeGame pipeGame = FindObjectOfType<PipeGame>();
        if (doorScript != null && !doorScript.doorOpened && pipeGame.pipeGameLaunched)
        {
            doorScript.StartCoroutine(doorScript.RotateDoor(doorToOpen));
        }
    }

    private IEnumerator RotateDoor(Transform door)
    {
        doorOpened = true;
        Vector3 pivot = door.position + door.right * (pivotOffset.x - pivotOffsetAmount); // Décalage supplémentaire
        float currentAngle = 0f;

        while (currentAngle < rotationAngle)
        {
            float step = rotationSpeed * Time.deltaTime * 90f;
            if (currentAngle + step > rotationAngle)
                step = rotationAngle - currentAngle;

            door.RotateAround(pivot, door.up, step);
            currentAngle += step;

            yield return null;
        }
    }

    public IEnumerator RotateDoorBack(Transform door)
    {
        doorOpened = false;
        Vector3 pivot = door.position + door.right * (pivotOffset.x - pivotOffsetAmount); // Décalage supplémentaire
        float currentAngle = 0f;

        while (currentAngle < rotationAngle)
        {
            float step = rotationSpeed * Time.deltaTime * 90f;
            if (currentAngle + step > rotationAngle)
                step = rotationAngle - currentAngle;

            door.RotateAround(pivot, door.up, -step);
            currentAngle += step;

            yield return null;
        }
    }
}
