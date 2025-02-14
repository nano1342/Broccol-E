using UnityEngine;
using System.Collections;
using TMPro;

public class BlinkingText : MonoBehaviour
{
    public Console console;


    void Start()
    {
        gameObject.SetActive(false);
    }
     
    public void StartBlinking()
    {
        gameObject.SetActive(true);
        console.AddLine("Set to true");
    }

    public void StopBlinking()
    {
        gameObject.SetActive(false);
        console.AddLine("Set to false");
    }

    //private IEnumerator Wait()
    //{
    //    console.AddLine("Start wait");
    //    yield return new WaitForSeconds(0.5f);
    //    console.AddLine("End wait");
    //}
}