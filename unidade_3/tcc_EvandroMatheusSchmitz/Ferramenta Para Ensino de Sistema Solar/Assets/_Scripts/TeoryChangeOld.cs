using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class TeoryChangeOld : MonoBehaviour, ICollision 
{

    #region PRIVATE_VARIABLES
    /// <summary>
    /// To know what the teory was choosen.
    /// </summary>
    private DisplayMode teoryChosen;
    #endregion

    #region PUBLIC_VARIABLES
    /// <summary>
    /// Transform of the object that will have it's rotation detected.
    /// Needs to change depending of the world center mode, just as the game object that recives this script. 
    /// </summary>
    public Transform subjectTransform;

    /// <summary>
    /// Panel of the text that will be displayed.
    /// </summary>
    public Image panelText;

    /// <summary>
    /// Text that will be displayed.
    /// </summary>
    public Text teoryText;

    /// <summary>
    /// Script that will have the information to display changed.
    /// </summary>
    public ShowInformation showInformation;
    /// <summary>
    /// The tag of the collider object that should let the trigger event happen.
    /// </summary>
    public TargetsTags targetCollision = TargetsTags.MULTI_TARGET;
    
    /// <summary>
    /// The gameObject that has the arrow that will be used as a way to display the modification.
    /// It can be null.
    /// </summary>
    public GameObject arrowRotation;
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
            // enable the panel for background
            panelText.enabled = true;
            /*if (pointer != null)
            {
                pointer.SetActive(false);
            }*/
        }
        
    }

    void OnTriggerStay(Collider other)
    {
        // just "selected" tags do this
        if (other.tag == targetCollision.ToString())
        {
            // if the rotaion is between 0 and 120
            if (subjectTransform.rotation.eulerAngles.y >= 0 && subjectTransform.rotation.eulerAngles.y < 120)
            {
                // HELIOCENTRICO was choosen
                teoryChosen = DisplayMode.HELIOCENTRICO;
                // display it
                teoryText.text = teoryChosen.ToString();
                // and change it
                showInformation.informationToDisplay = Information.HELIO;

                if (arrowRotation != null)
                {
                    // sets the rotaion of the arrow
                    arrowRotation.transform.eulerAngles = new Vector3(0, 60, 0);
                }
            }
            else if (subjectTransform.rotation.eulerAngles.y >= 120 && subjectTransform.rotation.eulerAngles.y < 240)
            {
                // GEOCENTRICO was choosen
                teoryChosen = DisplayMode.GEOCENTRICO;
                // display it
                teoryText.text = teoryChosen.ToString();
                // and change it
                showInformation.informationToDisplay = Information.GEO;

                if (arrowRotation != null)
                {
                    // sets the rotaion of the arrow
                    arrowRotation.transform.eulerAngles = new Vector3(0, 180, 0);
                }
            }
            else
            {
                // GEOCENTRICO_TYCHO was choosen
                teoryChosen = DisplayMode.GEOCENTRICO_TYCHO;
                // display it
                teoryText.text = teoryChosen.ToString();
                // and change it
                showInformation.informationToDisplay = Information.GEO_TYCHO;

                if (arrowRotation != null)
                {
                    // sets the rotaion of the arrow
                    arrowRotation.transform.eulerAngles = new Vector3(0, 300, 0);
                }
            }

            // change the text to take out the "_", just for beaty
            teoryText.text = teoryText.text.Replace("_", " ");
            PlanetSubject.instance.changeDisplayMode(teoryChosen);
        }
    }

    void OnTriggerExit(Collider other)
    {
        // just "selected" tags do this
        if (other.tag == targetCollision.ToString())
        {
            HideInformation();
        }
    }
    #endregion

    #region PUBLIC_METHODS
    /// <summary>
    /// Method that hides the information
    /// </summary>
    public void HideInformation()
    {
        // if there is a panel
        if (panelText != null)
        {
            Debug.Log("Foi chamado");
            // disable the panel for background
            panelText.enabled = false;
            // erase the text
            teoryText.text = "";
            
        }
    }

    public void OnCollisionFound()
    {
        
    }

    public void OnCollisionLost()
    {
        HideInformation();
    }


    #endregion
}
