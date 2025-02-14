using UnityEngine;

public class CableWinCondition : MonoBehaviour
{
    private GameObject[] targetCubes;  // Tableau pour stocker les cubes target
    private int coloredCubes = 0;  // Compteur de cubes colorés

    void Start()
    {
        // Trouver tous les cubes avec le tag "target"
        targetCubes = GameObject.FindGameObjectsWithTag("target");
    }

    // Méthode appelée lorsqu'un cube change de couleur
    public void OnTargetColored()
    {
        coloredCubes = 0;

        foreach (GameObject cube in targetCubes)
        {
            Color cubeColor = cube.GetComponent<Renderer>().material.color;

            // Vérifie si le cube n'est plus blanc
            if (cubeColor != Color.white)
            {
                coloredCubes++;
            }
        }

        // Si les 4 cubes sont colorés, déclencher la victoire
        if (coloredCubes == 4)
        {
            Debug.Log("🎉 Tous les cubes sont colorés ! Mission accomplie !");
            WinGame();
        }
    }

    void WinGame()
    {
        // Ici, tu peux ajouter des actions (changement de scène, effets visuels, etc.)
        Debug.Log("🚀 Tu peux ajouter une animation de victoire !");
    }
}
