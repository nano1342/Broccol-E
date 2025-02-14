using UnityEngine;

public class CableController : MonoBehaviour
{
    public Transform fixedEnd;  // Le point fixe du c�ble
    public Transform sphere;    // La sph�re mobile
    public float stopDistance = 0.1f;  // Distance d'arr�t du c�ble avant la sph�re
    public float maxLength = 2f; // Longueur maximale du c�ble

    void Update()
    {
        if (fixedEnd == null || sphere == null)
            return;

        // Calculer la direction entre FixedEnd et la sph�re
        Vector3 direction = sphere.position - fixedEnd.position;

        // Calculer la distance entre FixedEnd et la sph�re
        float distance = direction.magnitude - stopDistance;

        // Limiter la distance maximale
        distance = Mathf.Clamp(distance, 0, maxLength);

        // Positionner le c�ble au milieu entre FixedEnd et la sph�re
        Vector3 midPoint = fixedEnd.position + direction.normalized * (distance / 2f + stopDistance / 2f);
        transform.position = midPoint;

        // Faire en sorte que le cylindre s'oriente vers la sph�re
        transform.up = direction.normalized;

        // Ajuster la longueur du c�ble
        transform.localScale = new Vector3(transform.localScale.x, distance / 2f, transform.localScale.z);

        // Maintenir la sph�re � 1 unit� du bout du cylindre
        sphere.position = fixedEnd.position + direction.normalized * (distance + stopDistance);
    }
}
