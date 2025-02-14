using TMPro;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public TextMeshProUGUI screenTextMessage;
    public TextMeshProUGUI screenTextCode;
    public Console console;
    public DigicodeManager digicodeManager;
    public string generatedCode;

    public void GenerateRandomCode()
    {
        if (digicodeManager.GetGameRunning()) return;
        digicodeManager.GameStart();
        int codeLength = Random.Range(3, 6); //3 to 5 digits
        generatedCode = "";
        for (int i = 0; i < codeLength; i++) generatedCode += Random.Range(0, 10).ToString() + " ";
        console.AddLine("Digicode Code : " + generatedCode);
        digicodeManager.correctCode = generatedCode;
        if (screenTextCode != null) screenTextCode.text = generatedCode;
        else console.AddLine("Couldn't find screenTextCode for ScreenManager");
        if (screenTextMessage != null) screenTextMessage.text = "Incoming message :";
        else console.AddLine("Couldn't find screenTextMessage for ScreenManager");
    }
}