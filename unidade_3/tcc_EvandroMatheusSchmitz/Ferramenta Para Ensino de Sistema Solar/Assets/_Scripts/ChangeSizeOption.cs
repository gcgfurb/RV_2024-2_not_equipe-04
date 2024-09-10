using UnityEngine;
using System.Collections;

public class ChangeSizeOption : MonoBehaviour {

    #region PUBLIC_VARIABLES

    /// <summary>
    /// The tag of the collider object that should let the trigger event happen.
    /// </summary>
    public TargetsTags targetCollision = TargetsTags.MULTI_TARGET;
    #endregion

    #region UNITY_METHODS
    // Use this for initialization
    void Start ()
    {

    }

    // Update is called once per frame
    void Update ()
    {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == targetCollision.ToString())
        {
            CelestialBodyDissectionPaneBehaviour parentScript = gameObject.GetComponentInParent<CelestialBodyDissectionPaneBehaviour>();
            if (parentScript != null)
            {
                parentScript.ChangeSize = !parentScript.ChangeSize;
            }
        }
    }
    #endregion
}
