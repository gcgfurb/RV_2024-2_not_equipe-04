using UnityEngine;
using System.Collections;

public class BeginStarSimulation : MonoBehaviour {

    #region PUBLIC_VARIABLES
    /// <summary>
    /// StarBehaviour script, that will do the simulation of the life cicle of a star  
    /// </summary>
    public StarSimulation starConfiguration;

    // an array to the simulation
    #endregion

    #region PRIVATE_VARIABLES
    #endregion

    #region UNITY_METHODS
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnTriggerEnter(Collider other)
    {
        // if it was hit by a MULTI_TARGET starts the simulation
        if (other.tag == TargetsTags.MULTI_TARGET.ToString())
        {
            // set the simulation as true
            starConfiguration.StartSimulation = true;
        }

    }

    void OnTriggerStay(Collider other)
    {

    }

    void OnTriggerExit(Collider other)
    {

    }
    #endregion
}
