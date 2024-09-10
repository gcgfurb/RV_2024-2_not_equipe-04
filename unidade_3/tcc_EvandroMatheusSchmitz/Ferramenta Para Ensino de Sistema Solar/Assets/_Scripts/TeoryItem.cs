using UnityEngine;
using System.Collections;
using System;

public class TeoryItem : MonoBehaviour, ICollision
{

    #region PUBLIC_VARIABLES
    /// <summary>
    /// Teory represented by this script.
    /// </summary>
    public DisplayMode teory;

    /// <summary>
    /// The information represented by this script.
    /// </summary>
    public Information information;

    /// <summary>
    /// Script that will be modified.
    /// </summary>
    public TeoryChange teoryScript;

    /// <summary>
    /// The tag of the collider object that should let the trigger event happen.
    /// </summary>
    public TargetsTags targetCollision = TargetsTags.MULTI_TARGET;
    #endregion


    #region UNITY_METHODS
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        // just "selected" tags do this
        if (other.tag == targetCollision.ToString())
        {
            // sets new teory
            teoryScript.ChangeTeory(teory, information);

            // says that there was a collision
            teoryScript.OnCollisionFound();
        }
    }

    void OnTriggerExit(Collider other)
    {

        // just "selected" tags do this
        if (other.tag == targetCollision.ToString())
        {
            // end of the collision
            OnCollisionLost();
        }
    }

    public void OnCollisionFound()
    {
        
    }

    public void OnCollisionLost()
    {
        // end of the collision
        teoryScript.OnCollisionLost();
    }
    #endregion
}
