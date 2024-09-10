using UnityEngine;
using System.Collections;
using Vuforia;
using UnityEngine.SceneManagement;
using System;

public class ButtonBehaviour : MonoBehaviour, Vuforia.IVirtualButtonEventHandler
{
    #region PRIVATE_VARIABLES
    /// <summary>
    /// The interface that will sofer some modification
    /// </summary>
    private IAddValueBehaviour addValueBehaviour = null;

    /// <summary>
    /// Boolean to tell if there is contact, and to do not get the script every frame that there is contact.
    /// </summary>
    private bool contact = false;
    
    // it is used as a lock
    /// <summary>
    /// Boolean used as a lock to see if there is a game object near enough to this game object.
    /// </summary>
    private bool found = false;

    /// <summary>
    /// Maximum distance to tell if there is contact or not
    /// </summary>
    [SerializeField] private int _detectionDistance;
    /// <summary>
    /// Maximum distance to tell if there is contact or not
    /// (get, set)
    /// </summary>
    public int DetectionDistance
    {
        get { return _detectionDistance; }
        set
        {
            if (value >= 0)
            {
                _detectionDistance = value;
            }
        }
    }

    /// <summary>
    /// Game object that represents a rect that will be drawn between this game object and other game object that is near enough.
    /// </summary>
    private GameObject rect;
    #endregion

    #region PUBLIC_VARIABLES
    /// <summary>
    /// Game object that will be checked. Game objects that are not in this array will not have contact detected
    /// </summary>
    public GameObject[] distanceSubjects;

    /// <summary>
    /// The game object that will act as a base to calulate the distance. Notice that depending the position of this game object the detection distance should be higher or smaller.
    /// </summary>
    public GameObject distanceObserver;

    
    #endregion

    #region UNITY_METHODS
    // Use this for initialization
    void Start()
    {
        // Initiate the virtual buttons
        VirtualButtonBehaviour[] vb = GetComponentsInChildren<VirtualButtonBehaviour>();
        foreach (VirtualButtonBehaviour vbb in vb)
        {
            vbb.RegisterEventHandler(this);
        }

    }

    // Update is called once per frame
    void Update()
    {
     
        // check if there was detection and contact   
        foreach (GameObject distanceGameObject in distanceSubjects)
        {
            found = false;
            if (Vector3.Distance(distanceObserver.transform.position, distanceGameObject.transform.position) < _detectionDistance)
            {
                found = true;

                // draws the rect to have a visual response of the detection
                drawRect(distanceGameObject.transform.position);

                if (found && !contact)
                {
                    // if it is the firts time contact get the IAddValueBehaviour script (or one of its implementation)
                    addValueBehaviour = distanceGameObject.GetComponent<IAddValueBehaviour>();
                    // show the data of the IAddValueBehaviour script, if there is any
                    addValueBehaviour.ShowData();
                    // creates the rect
                    createRect();
                    
                    contact = true;
                }
                // to get just the first contact
                break;
            }
        }

        // contact recives what is in found, be it true, or false
        // this is a way to ensure that in there was contact, true is kept, if not than contact changes
        contact = found;

        // if there was no contact
        if (!contact)
        {
            // hide the data being shown, if there is a IAddValueBehaviour script
            if (addValueBehaviour != null) { addValueBehaviour.HideData(); }
            // "destroy" the reference to IAddValueBehaviour script
            addValueBehaviour = null;
            // destroy the rect
            GameObject.Destroy(rect);
        }
    }


    #endregion

    #region VUFORIA_METHODS
    public void OnButtonPressed(VirtualButtonAbstractBehaviour vb)
    {
        // takes action depend of the button name
        switch (vb.VirtualButtonName)
        {
            case "plus":
                checkAndCallAddMethod(1);
                break;

            case "minus":
                checkAndCallAddMethod(-1);
                break;
        }
    }

    public void OnButtonReleased(VirtualButtonAbstractBehaviour vb)
    {
        
    }
    #endregion

    #region PRIVATE_METHODS
    /// <summary>
    /// Check if there is a IAddValueBehaviour script and call the method AddValue.
    /// </summary>
    /// <param name="number">The number that will be add or decreased.</param>
    private void checkAndCallAddMethod(int number)
    {
        // calls the AddValue method of the IAddValueBehaviour, if there is one
        if (addValueBehaviour != null)
        {
            addValueBehaviour.AddValue(number);
        }
    }

    /// <summary>
    /// Creates a rect.
    /// </summary>
    private void createRect()
    {
        // reacte a empty game object
        rect = new GameObject();
        // sets the position of the rect as the position of the distanceObserver.
        rect.transform.position = distanceObserver.transform.position;
        // adds a line renderer component on the game object
        rect.AddComponent<LineRenderer>();
    }

    /// <summary>
    /// Change the position of the rect.
    /// </summary>
    /// <param name="endPosition">The new end position if the rect.</param>
    private void drawRect(Vector3 endPosition)
    {
        // if there is a rect
        if (rect != null) {
            // get the line render
            LineRenderer lr = rect.GetComponent<LineRenderer>();
            // sets it material as green
            lr.material = Resources.Load<Material>("GreenMaterial");
            // set its stat position
            lr.SetPosition(0, distanceObserver.transform.position);
            // set its final position 
            lr.SetPosition(1, endPosition);
        }
    }
    #endregion
}
