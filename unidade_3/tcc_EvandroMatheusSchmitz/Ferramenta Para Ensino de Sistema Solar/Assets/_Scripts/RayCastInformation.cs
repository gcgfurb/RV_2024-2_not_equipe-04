using UnityEngine;
using System.Collections;

public class RayCastInformation : MonoBehaviour {
    #region PRIVATE_VARIABLES

    /// <summary>
    /// The ShowInformation script attached to this game object
    /// </summary>
    private ShowInformation informationScript;

    /// <summary>
    /// Boolean that if true show the information when there is a touch in the screen.
    /// If false does nothing.
    /// </summary>
    [SerializeField] private bool _touchShowInformation = false;
    /// <summary>
    /// Boolean that if true show the information when there is a touch in the screen.
    /// If false does nothing.
    /// (get, set)
    /// </summary>
    public bool TouchShowInformation
    {
        get { return _touchShowInformation; }
        set { _touchShowInformation = value; }
    }
    #endregion

    #region UNITY_METHODS
    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        // sees if it is to show the information and if there was a touch (when the finger is lifted from the screen) or mouse click
        if (_touchShowInformation && 
            (Input.GetButtonDown("Fire1") || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)))
        {
            // This to lines serve for the same purpose, get the ray from the position of the click
            // it just change that one if for touch screen and the other for mouse
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

            // who was hit
            RaycastHit hit;

            // sees if there was a hit
            if (Physics.Raycast(ray, out hit))
            {
                // draws a line just for debugging reasons
                Debug.DrawLine(ray.origin, hit.point);
                // gets the ShowInformation script attached on this game object
                informationScript = hit.transform.GetComponent<ShowInformation>();
                // if there is a script
                if (informationScript != null)
                {
                    // then show the information
                    informationScript.DisplayInformation();
                }
            } else
            {
                // if there was no hit and there is a script
                // basically the condition is, hit something show, hit nothing then hide
                if (informationScript != null)
                {
                    // hide the information
                    informationScript.HideInformation();
                    // erase the reference
                    informationScript = null;
                }
            }
        }

	}
    #endregion
}
