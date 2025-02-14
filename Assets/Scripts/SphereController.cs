using UnityEngine;

public class SphereController : MonoBehaviour
{
    public GameObject targetObject; // L'objet cible à toucher
    private Color originalColor;    // Couleur d'origine du cube
    private bool isDragging = false;
    private Vector3 offset;

    private CableMission cableMission; // Référence au script CableMission

    void Start()
    {
        // Récupérer la référence au script CableMission
        cableMission = FindObjectOfType<CableMission>();  // Recherche du script CableMission dans la scène

        // Stocke la couleur d'origine du cube
        if (targetObject != null)
        {
            Renderer targetRenderer = targetObject.GetComponent<Renderer>();
            if (targetRenderer != null)
            {
                originalColor = targetRenderer.material.color;
            }
        }
    }

    void OnMouseDown()
    {
        isDragging = true;
        offset = transform.position - GetMouseWorldPosition();
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            transform.position = GetMouseWorldPosition() + offset;
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    public void OnTriggerPressed()
    {
        isDragging = true;
        offset = transform.position - GetMouseWorldPosition();
        if (isDragging)
        {
            transform.position = GetMouseWorldPosition() + offset;
        }
    }

    public void OnTriggerDrag()
    {
        if (isDragging)
        {
            transform.position = GetMouseWorldPosition() + offset;
        }
    }

    public void OnTriggerUp()
    {
        isDragging = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == targetObject) // Vérifie la collision avec le cube
        {
            // Changer la couleur du cube pour correspondre à la couleur de la sphère
            if (cableMission != null)
            {
                Color sphereColor = GetComponent<Renderer>().material.color;
                cableMission.ChangeTargetColor(targetObject, sphereColor);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == targetObject) // Lorsque la sphère quitte le cube
        {
            // Remet la couleur d'origine du cube via CableMission
            if (cableMission != null)
            {
                cableMission.ChangeTargetColor(targetObject, originalColor);  // Restaure la couleur d'origine
            }
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z; // Profondeur actuelle
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }
}
