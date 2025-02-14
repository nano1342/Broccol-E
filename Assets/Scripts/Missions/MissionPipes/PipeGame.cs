using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

struct Case
{
    public int pieceType;
    public int pieceRotation;

    public Case(int info1, int info2)
    {
        pieceType = info1;      //  0 is empty    1 is i    2 is L    3 is |    4 is T    5 is +
        pieceRotation = info2;  //  0 is i  L  |  T  +      Then 1 rotates 90° clockwise
    }
}
public class PipeGame : MonoBehaviour
{
    public GameObject pipeStraightPrefab; // Pipei
    public GameObject pipeCornerPrefab;  // PipeL
    public GameObject pipeVerticalPrefab; // Pipe-
    public GameObject pipeTeePrefab;     // PipeT
    public GameObject pipeCrossPrefab;   // Pipe+

    public Transform posCenter;
    public Console console;
    public bool pipeGameLaunched = false;
    public MissionPipes scriptMission;


    int selectedGrid;
    Case[,] plateau = new Case[5, 5];
    int[,,] gridList = new int[3,5,5];
    // Start is called before the first frame update
    public void StartGame()
    {
        if (pipeGameLaunched) return;
        pipeGameLaunched = true;
        Dictionary<int, GameObject> pipePrefabs = new Dictionary<int, GameObject>
        {
            { 1, pipeStraightPrefab },  // Pipei
            { 2, pipeCornerPrefab },    // PipeL
            { 3, pipeVerticalPrefab },  // Pipe-
            { 4, pipeTeePrefab },       // PipeT
            { 5, pipeCrossPrefab }      // Pipe+
        };

        gridList = new int[,,]
        {
            // Grille 0
            {
                {0, 0, 0, 0, 0},
                {0, 1, 0, 1, 0},
                {0, 3, 0, 3, 0},
                {0, 1, 0, 1, 0},
                {0, 0, 0, 0, 0}
            },
            // Grille 1
            {
                {0, 0, 0, 0, 0},
                {0, 1, 0, 1, 0},
                {0, 4, 3, 4, 0},
                {0, 1, 0, 1, 0},
                {0, 0, 0, 0, 0}
            },
            // Grille 2
            {
                {0, 0, 1, 0, 1},
                {0, 0, 3, 2, 2},
                {1, 2, 5, 4, 0},
                {2, 2, 3, 2, 2},
                {0, 0, 1, 0, 1}
            }
        };

        System.Random rnd = new System.Random();
        selectedGrid = rnd.Next(3);
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                plateau[i, j] = new Case(gridList[selectedGrid,i,j], rnd.Next(4));
            }
        }
        GenerateGrid();
    }

    void GenerateGrid()
    {
        float tileSize = 0.05f;
        float hoverHeight = 0.05f; // Hauteur à laquelle les tuyaux doivent flotter

        // Récupération du Cube
        Transform cubeTransform = transform.Find("Cube");
        if (cubeTransform == null)
        {
            Debug.LogError("Cube introuvable sous PipeGameController !");
            return;
        }

        // Déterminer l'axe normal au Cube (axe perpendiculaire)
        Vector3 normal = cubeTransform.TransformDirection(Vector3.back); // Par défaut, "devant" le Cube

        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.name.StartsWith("GamePipe_"))
            {
                Destroy(obj);
            }
        }

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                int pieceType = plateau[i, j].pieceType;
                int pieceRotation = plateau[i, j].pieceRotation;

                GameObject prefab = GetPrefabForPieceType(pieceType);

                if (prefab != null)
                {
                    // Position relative dans le plan local du Cube
                    Vector3 localPipePosition = new Vector3(j * tileSize * 4 - (float)0.4, i * tileSize * 4 - (float)0.4, 0);

                    // Convertir la position locale en position globale
                    Vector3 worldPipePosition = cubeTransform.TransformPoint(localPipePosition);

                    // Décaler la position le long de la normale pour que le pipe flotte au-dessus du Cube
                    worldPipePosition += normal * hoverHeight;

                    // Appliquer la rotation du Cube à la pièce
                    Quaternion worldRotation = cubeTransform.rotation * Quaternion.Euler(0, 0, pieceRotation * 90);

                    // Instanciation du pipe
                    GameObject pipe = Instantiate(prefab, worldPipePosition, worldRotation);

                    pipe.name = $"GamePipe_{i}_{j}";

                    RotatePipe rotatePipe = pipe.GetComponent<RotatePipe>();
                    if (rotatePipe != null)
                    {
                        rotatePipe.rotationIndex = pieceRotation;
                        rotatePipe.pipeGame = this;
                    }
                }
            }
        }
    }
    GameObject GetPrefabForPieceType(int pieceType)
    {
        switch (pieceType)
        {
            case 1: return pipeStraightPrefab;
            case 2: return pipeCornerPrefab;
            case 3: return pipeVerticalPrefab;
            case 4: return pipeTeePrefab;
            case 5: return pipeCrossPrefab;
            default: return null;
        }
    }
    public void UpdatePlateau(int i, int j, int newRotation)
    {
        if (i >= 0 && i < 5 && j >= 0 && j < 5)
        {
            plateau[i, j].pieceRotation = newRotation;
        }
    }
    public void CheckSolution()
    {
        if (checkGrid())
        {
            scriptMission.completeMission();
            pipeGameLaunched = false;
            console.AddLine("PipeGame Finished");
            PipeDoor doorScript = GameObject.Find("Door")?.GetComponent<PipeDoor>();
            if (doorScript != null && doorScript.doorOpened)
            {
                doorScript.StartCoroutine(doorScript.RotateDoorBack(doorScript.transform));
            }
        }
    }
    public bool checkGrid()
    {
        switch (selectedGrid)
        {
            case 0:
                if (plateau[1, 1].pieceRotation == 2 && plateau[1, 3].pieceRotation == 2 && plateau[3, 1].pieceRotation == 0 && plateau[3, 3].pieceRotation == 0)
                {
                    if ((plateau[2, 1].pieceRotation == 0 || plateau[2, 1].pieceRotation == 2) && (plateau[2, 3].pieceRotation == 0 || plateau[2, 3].pieceRotation == 2)) return true;
                }
                return false;
            case 1:
                if (plateau[1, 1].pieceRotation == 2 && plateau[1, 3].pieceRotation == 2 && plateau[3, 1].pieceRotation == 0 && plateau[3, 3].pieceRotation == 0)
                {
                    if (plateau[2, 1].pieceRotation == 0 && plateau[2, 3].pieceRotation == 2)
                    {
                        if (plateau[2,2].pieceRotation == 1 || plateau[2, 2].pieceRotation == 3) return true;
                    }
                }
                return false;
            case 2:
                if (plateau[2, 0].pieceRotation == 2 && plateau[0, 2].pieceRotation == 2 && plateau[4, 2].pieceRotation == 0 && plateau[0, 4].pieceRotation == 2 && plateau[4, 4].pieceRotation == 0)
                {
                    if ((plateau[1, 2].pieceRotation == 2 || plateau[1, 2].pieceRotation == 0) && (plateau[3, 2].pieceRotation == 2 || plateau[3, 2].pieceRotation == 0))
                    {
                        if (plateau[3, 0].pieceRotation == 0 && plateau[3, 1].pieceRotation == 3 && plateau[2, 1].pieceRotation == 1 &&
                            plateau[1, 3].pieceRotation == 1 && plateau[1, 4].pieceRotation == 3 && plateau[3, 3].pieceRotation == 0 && plateau[3, 4].pieceRotation == 2)
                        {
                            if (plateau[3, 2].pieceRotation == 2) return true;
                        }
                    }
                }
                return false;
            default:
                return false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
