using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class TeoryChange : MonoBehaviour, ICollision 
{

    #region PRIVATE_VARIABLES
    /// <summary>
    /// To know what the teory was choosen.
    /// </summary>
    private DisplayMode teoryChosen;
    #endregion

    #region PUBLIC_VARIABLES
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
    #endregion

    #region UNITY_METHODS
    // Use this for initialization
    void Start () {
                
    }

    // Update is called once per frame
    void Update () {

    }
    #endregion

    #region PUBLIC_METHODS
    /// <summary>
    /// Method to change the teory of the solar system that is being shown.
    /// </summary>
    /// <param name="newTeory">The new teory that will be displayed</param>
    public void ChangeTeory(DisplayMode newTeory, Information infoChange)
    {
        // change teory
        teoryChosen = newTeory;
        // display it
        teoryText.text = teoryChosen.ToString();
        // change the text to take out the "_", just for beaty
        teoryText.text = teoryText.text.Replace("_", " ");

        // change the information that will be displayed
        showInformation.informationToDisplay = infoChange;

        // warning everybory about the change
        PlanetSubject.instance.changeDisplayMode(teoryChosen);
    }


    /// <summary>
    /// Method that hides the information
    /// </summary>
    public void HideInformation()
    {
        // if there is a panel
        if (panelText != null)
        {
            // disable the panel for background
            panelText.enabled = false;
            // erase the text
            teoryText.text = "";
            
        }
    }

    public void OnCollisionFound()
    {
        if (panelText != null)
        {
            // enable the panel for background
            panelText.enabled = true;

            // display it
            teoryText.text = teoryChosen.ToString();

            // change the text to take out the "_", just for beauty
            teoryText.text = teoryText.text.Replace("_", " ");
        }
    }

    public void OnCollisionLost()
    {
        HideInformation();
    }


    #endregion
}
