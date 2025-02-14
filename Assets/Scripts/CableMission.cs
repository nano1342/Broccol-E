using UnityEngine;

public class CableMission : MonoBehaviour
{
    public GameObject[] cables;  // Tableau pour stocker les objets Cable (sph�re et cylindre)
    public GameObject[] targetCube;  // Tableau pour stocker les objets Cable (sph�re et cylindre)
    public Transform[] targetPositions; // Positions pour les target cubes

    private Color[] colors = new Color[] { Color.blue, Color.green, Color.red, Color.yellow };  // Liste des couleurs

    void Start()
    {
        AssignColorsToCables(); // Initialisation des couleurs des c�bles et des cubes target
        PositionTargetsRandomly(); // Positionner les cubes target al�atoirement
    }

    void AssignColorsToCables()
    {
        int[] randomIndices = { 0, 1, 2, 3 };
        RandomizeArray(randomIndices);

        // Applique les couleurs aux c�bles
        for (int i = 0; i < cables.Length; i++)
        {
            // Assigner une couleur al�atoire � chaque c�ble
            GameObject currentCable = cables[i];

            // Trouver le cylindre, la sph�re et le cube target dans le c�ble en utilisant les tags
            Renderer cylinderRenderer = FindObjectWithTagInChild(currentCable, "cylinder");
            Renderer sphereRenderer = FindObjectWithTagInChild(currentCable, "sphere");
            GameObject targetCube = FindObjectWithTagInChild(currentCable, "target")?.gameObject;  // Le cube target dans chaque c�ble

            // Appliquer les couleurs � la sph�re, au cylindre et au cube
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

    // M�lange un tableau d'entiers pour obtenir des indices al�atoires
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

    // M�thode pour changer la couleur du cube target
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


    // Recherche un objet avec un tag sp�cifique dans les enfants du parent
    private Renderer FindObjectWithTagInChild(GameObject parent, string tag)
    {
        Transform[] children = parent.GetComponentsInChildren<Transform>();

        foreach (var child in children)
        {
            if (child.CompareTag(tag))  // V�rifie si l'objet enfant a le bon tag
            {
                return child.GetComponent<Renderer>();  // Retourne le Renderer de l'enfant
            }
        }

        return null;  // Si aucun objet avec ce tag n'est trouv�
    }

    // Positionner les cubes target al�atoirement parmi les TargetPosition
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
