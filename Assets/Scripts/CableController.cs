using UnityEngine;

public class CableController : MonoBehaviour
{
    public Transform fixedEnd;  // Le point fixe du câble
    public Transform sphere;    // La sphère mobile
    public float stopDistance = 0.1f;  // Distance d'arrêt du câble avant la sphère
    public float maxLength = 2f; // Longueur maximale du câble

    void Update()
    {
        if (fixedEnd == null || sphere == null)
            return;

        // Calculer la direction entre FixedEnd et la sphère
        Vector3 direction = sphere.position - fixedEnd.position;

        // Calculer la distance entre FixedEnd et la sphère
        float distance = direction.magnitude - stopDistance;

        // Limiter la distance maximale
        distance = Mathf.Clamp(distance, 0, maxLength);

        // Positionner le câble au milieu entre FixedEnd et la sphère
        Vector3 midPoint = fixedEnd.position + direction.normalized * (distance / 2f + stopDistance / 2f);
        transform.position = midPoint;

        // Faire en sorte que le cylindre s'oriente vers la sphère
        transform.up = direction.normalized;

        // Ajuster la longueur du câble
        transform.localScale = new Vector3(transform.localScale.x, distance / 2f, transform.localScale.z);

        // Maintenir la sphère à 1 unité du bout du cylindre
        sphere.position = fixedEnd.position + direction.normalized * (distance + stopDistance);
    }
}
