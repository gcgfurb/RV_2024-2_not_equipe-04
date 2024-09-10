using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {

    /// <summary>
    /// A game object prefab that conteins the PlanetSubject and GameControler script 
    /// </summary>
    public GameObject gameManager;
    public GameObject planetManager;

    void Awake()
    {
        // see if there is no instance of the script
        if (GameControler.instance == null)
        {
            // instantiate the script
            Instantiate(gameManager);
        }

        // see if there is no instance of the script
        if (PlanetSubject.instance == null)
        {
            // instantiate the script
            Instantiate(planetManager);
        }
    }
}
