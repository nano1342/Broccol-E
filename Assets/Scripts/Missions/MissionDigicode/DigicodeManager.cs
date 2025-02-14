using UnityEngine;
using TMPro;
using System.Collections;
using static UnityEngine.Rendering.DebugUI;

public class DigicodeManager : MonoBehaviour
{
    public TextMeshProUGUI digicodeText;
    public TextMeshProUGUI screenTextMessage;
    public TextMeshProUGUI screenTextCode;
    private string enteredCode = "";
    public string correctCode = "";
    public Console console;
    private Color[] colors = { Color.red, Color.green };
    public GameObject digicodeBackground;
    public ScreenManager screenManager;
    private bool gameRunning = false;

    public void OnButtonPress(string value)
    {
        if (digicodeText == null) console.AddLine("Couldn't find digicodeText for Digicode");
        if (screenTextMessage == null) console.AddLine("Couldn't find screenTextMessage for Digicode");
        if (screenTextCode == null) console.AddLine("Couldn't find screenTextCode for Digicode");
        if (value == "X")
        {
            enteredCode = "";
            digicodeText.text = enteredCode;
        }
        else if (value == "OK")
        {
            if (enteredCode == correctCode)
            {
                console.AddLine("Code correct !");
                screenTextMessage.text = "Welcome aboard, Captain";
                enteredCode = "";
                gameRunning = false;
                screenTextCode.text = enteredCode;
                digicodeText.text = enteredCode;
            }
            else
            {
                console.AddLine("Code incorrect !");
                enteredCode = "";
                ChangeTargetColor();
                digicodeText.text = enteredCode;
            }
        }
        else
        {
            console.AddLine("Entering numer : " + value);
            if (enteredCode.Length < 10)
            {
                enteredCode += value + " ";
                digicodeText.text = enteredCode;
            }
        } 
    }

    public void ChangeTargetColor()
    {
        console.AddLine("Changing color entry");
        if (digicodeBackground != null)
        {
            StartCoroutine(BlinkColor());
        }
        console.AddLine("Changing color exit");
    }

    public void GameStart()
    {
        gameRunning = true;
    }

    public bool GetGameRunning()
    {
        return gameRunning;
    }
    private IEnumerator BlinkColor()
    {
        Renderer targetRenderer = digicodeBackground.GetComponent<Renderer>();
        if (targetRenderer != null)
        {
            Color originalColor = targetRenderer.material.color;
            targetRenderer.material.color = Color.red;
            yield return new WaitForSeconds(0.25f);
            targetRenderer.material.color = originalColor;
        }
    }
}
