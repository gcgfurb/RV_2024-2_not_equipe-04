using TMPro;
using UnityEngine;
using UnityEngine.UI;  // Usado para UI
using Vuforia;
using System.Collections;
using Unity.VisualScripting;

public class ImageTargetInteractionManager : MonoBehaviour
{
    public TextMeshProUGUI infoText;

    private static string primaryTargetName;
    private static string secondaryTargetName;
    private string plusInformation = secondaryTargetName;

    public void OnPrimaryTargetFound(string imageTargetName)
    {
        Debug.Log("OnPrimaryTargetFound: " + imageTargetName);
        primaryTargetName = imageTargetName;
        UpdateInfoText(primaryTargetName);
    }
    public void OnSecondaryTargetFound(string imageTargetName)
    {
        Debug.Log("OnSecondaryTargetFound primary: " + primaryTargetName);
        
        if (primaryTargetName == "Terra")
        {
            UpdateInfoText("A Terra é o único planeta do Sistema Solar com vida identificada");
        } else if (primaryTargetName == "Jupiter") {
            UpdateInfoText("Jupiter é o maior planeta do Sistema Solar");
        }
    }

    public void OnPrimaryTargetLost()
    {
        Debug.Log("OnPrimaryTargetLost");
    }

    public void OnSecondaryTargetLost()
    {
        Debug.Log("OnSecondaryTargetLost");
    }

    private void UpdateInfoText(string message)
    {
        if (infoText != null)
        {
            infoText.text = message;
        }
        else
        {
            Debug.LogWarning("InfoText não está configurado no Inspector!");
        }
    }
}