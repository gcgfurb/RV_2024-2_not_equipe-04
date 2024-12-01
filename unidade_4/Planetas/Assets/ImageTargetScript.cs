using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ImageTargetScript.cs
public class ImageTargetScript : MonoBehaviour
{
    public GameObject mainScriptGameObject; // Arraste o GameObject principal aqui

    void Start()
    {
        // Este código será executado apenas uma vez, no início
        Debug.Log("Script inicializado!");
        Debug.Log("ImageTarget encontrada: " + gameObject.name); // Adicione esta linha
        
    }

    public void OnTrackingFound()
    {
        Debug.Log("ImageTarget encontrada: " + gameObject.name); // Adicione esta linha
        // Certifique-se que mainScriptGameObject não é nulo antes de acessar
        if (mainScriptGameObject != null)
        {
            MainScript mainScript = mainScriptGameObject.GetComponent<MainScript>();
            mainScript.UpdateText(gameObject.name);
        }
        else
        {
            Debug.LogError("GameObject principal não está definido!");
        }
    }

    //public void OnTrackingFound()
    //{
    //    // Código a ser executado quando a Image Target for encontrada
    //    Debug.Log("Image Target encontrada AAAAAAAAAAAA!");
    //}

    public void OnTrackingLost()
    {
        // Código a ser executado quando a Image Target for perdida
        Debug.Log("Image Target perdida UUUUUUUUUUUUU!");
    }

}
