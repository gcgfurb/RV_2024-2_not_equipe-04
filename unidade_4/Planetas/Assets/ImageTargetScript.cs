using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ImageTargetScript.cs
public class ImageTargetScript : MonoBehaviour
{
    public GameObject mainScriptGameObject; // Arraste o GameObject principal aqui

    void Start()
    {
        // Este c�digo ser� executado apenas uma vez, no in�cio
        Debug.Log("Script inicializado!");
        Debug.Log("ImageTarget encontrada: " + gameObject.name); // Adicione esta linha
        
    }

    public void OnTrackingFound()
    {
        Debug.Log("ImageTarget encontrada: " + gameObject.name); // Adicione esta linha
        // Certifique-se que mainScriptGameObject n�o � nulo antes de acessar
        if (mainScriptGameObject != null)
        {
            MainScript mainScript = mainScriptGameObject.GetComponent<MainScript>();
            mainScript.UpdateText(gameObject.name);
        }
        else
        {
            Debug.LogError("GameObject principal n�o est� definido!");
        }
    }

    //public void OnTrackingFound()
    //{
    //    // C�digo a ser executado quando a Image Target for encontrada
    //    Debug.Log("Image Target encontrada AAAAAAAAAAAA!");
    //}

    public void OnTrackingLost()
    {
        // C�digo a ser executado quando a Image Target for perdida
        Debug.Log("Image Target perdida UUUUUUUUUUUUU!");
    }

}
