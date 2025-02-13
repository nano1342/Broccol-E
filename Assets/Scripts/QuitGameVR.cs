using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class QuitGameVR : MonoBehaviour
{
    public void Quit()
    {
        Debug.Log("Fermeture du jeu dans 3 secondes...");
        StartCoroutine(QuitWithDelay(3f));
    }

    private IEnumerator QuitWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("Quitter le jeu maintenant !");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}