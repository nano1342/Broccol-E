using UnityEngine;

public class CableMission : MonoBehaviour
{
    public GameObject[] cables;  // Tableau pour stocker les objets Cable (sphère et cylindre)
    public GameObject[] targetCube;  // Tableau pour stocker les objets Cable (sphère et cylindre)
    public Transform[] targetPositions; // Positions pour les target cubes

    private Color[] colors = new Color[] { Color.blue, Color.green, Color.red, Color.yellow };  // Liste des couleurs

    void Start()
    {
        AssignColorsToCables(); // Initialisation des couleurs des câbles et des cubes target
        PositionTargetsRandomly(); // Positionner les cubes target aléatoirement
    }

    void AssignColorsToCables()
    {
        int[] randomIndices = { 0, 1, 2, 3 };
        RandomizeArray(randomIndices);

        // Applique les couleurs aux câbles
        for (int i = 0; i < cables.Length; i++)
        {
            // Assigner une couleur aléatoire à chaque câble
            GameObject currentCable = cables[i];

            // Trouver le cylindre, la sphère et le cube target dans le câble en utilisant les tags
            Renderer cylinderRenderer = FindObjectWithTagInChild(currentCable, "cylinder");
            Renderer sphereRenderer = FindObjectWithTagInChild(currentCable, "sphere");
            GameObject targetCube = FindObjectWithTagInChild(currentCable, "target")?.gameObject;  // Le cube target dans chaque câble

            // Appliquer les couleurs à la sphère, au cylindre et au cube
            if (cylinderRenderer != null)
            {
                cylinderRenderer.material.color = colors[randomIndices[i]];
            }
            if (sphereRenderer != null)
            {
                sphereRenderer.material.color = colors[randomIndices[i]];
            }
            if (targetCube != null)
            {
                // Initialiser le cube target en blanc
                targetCube.GetComponent<Renderer>().material.color = Color.white;
            }
        }
    }

    // Mélange un tableau d'entiers pour obtenir des indices aléatoires
    void RandomizeArray(int[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int temp = array[i];
            int randomIndex = Random.Range(0, i + 1);
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }

    // Méthode pour changer la couleur du cube target
    public void ChangeTargetColor(GameObject target, Color newColor)
    {
        if (target != null)
        {
            Renderer targetRenderer = target.GetComponent<Renderer>();
            if (targetRenderer != null)
            {
                targetRenderer.material.color = newColor;

                // Notifie le GameManager du changement de couleur
                FindObjectOfType<CableWinCondition>().OnTargetColored();
            }
        }
    }


    // Recherche un objet avec un tag spécifique dans les enfants du parent
    private Renderer FindObjectWithTagInChild(GameObject parent, string tag)
    {
        Transform[] children = parent.GetComponentsInChildren<Transform>();

        foreach (var child in children)
        {
            if (child.CompareTag(tag))  // Vérifie si l'objet enfant a le bon tag
            {
                return child.GetComponent<Renderer>();  // Retourne le Renderer de l'enfant
            }
        }

        return null;  // Si aucun objet avec ce tag n'est trouvé
    }

    // Positionner les cubes target aléatoirement parmi les TargetPosition
    void PositionTargetsRandomly()
    {
        int[] randomIndices = { 0, 1, 2, 3 };
        RandomizeArray(randomIndices);

        for (int i = 0; i < cables.Length; i++)
        {
            if (targetCube != null && i < targetPositions.Length)
            {
                targetCube[i].transform.position = targetPositions[randomIndices[i]].position;
            }
        }
    }
}
