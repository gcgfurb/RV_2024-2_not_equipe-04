using TMPro;
using UnityEngine;

public class MainScript : MonoBehaviour
{
    public TextMeshProUGUI infoText; // Arraste o componente TextMeshProUGUI aqui

    public void UpdateText(string targetName)
    {
        infoText.text = targetName;
    }

    public void UpdateTextExtra(string targetName, string text)
    {
        infoText.text = targetName;
        infoText.text += text;
    }
}