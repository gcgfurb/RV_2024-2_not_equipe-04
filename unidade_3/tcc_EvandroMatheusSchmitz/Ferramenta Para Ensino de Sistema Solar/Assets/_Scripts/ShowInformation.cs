using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Linq;

public class ShowInformation : MonoBehaviour, IChildTrackBehaviour, ICollision {

    #region PUBLIC_VARIABLES
    /// <summary>
    /// Text component for the name of something.
    /// </summary>
    public Text nameText;

    /// <summary>
    /// Text component for some data of something.
    /// </summary>
    public Text dataText;

    /// <summary>
    /// Panel where the texts are shown.
    /// </summary>
    public Image panelText;

    /// <summary>
    /// Array of targets that trigger the show information.
    /// </summary>
    public TargetsTags[] tagsMatter = new TargetsTags[1];

    /// <summary>
    /// An enum value that represents about who is the information.
    /// </summary>
    public Information informationToDisplay;
    #endregion

    #region PRIVATE_VARIEBLES
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
        // if there are tags and the other's tag is the tagsMatter array
        if (tagsMatter != null && Enum.IsDefined(typeof(TargetsTags), other.tag) && tagsMatter.Contains((TargetsTags)Enum.Parse(typeof(TargetsTags), other.tag))) {
            // dislpay the information
            DisplayInformation();
            // add other to the collision array
            
            Debug.Log("Deu add");
        }
    }

    void OnTriggerExit(Collider other)
    {
        // hides the information
        if (tagsMatter != null && Enum.IsDefined(typeof(TargetsTags), other.tag) && tagsMatter.Contains((TargetsTags)Enum.Parse(typeof(TargetsTags), other.tag)))
        {
            HideInformation();
        }
    }
    #endregion

    #region PUBLIC_METHODS
    /// <summary>
    /// Method that displays the information, if there is any
    /// </summary>
    public void DisplayInformation()
    {
        // if there is an key about the information
        if (GameControler.instance.Information.ContainsKey(informationToDisplay)) {
            // enable the panel, to show a background
            panelText.enabled = true;
            // get the name of the information
            nameText.text = GameControler.instance.Information[informationToDisplay].title;
            // get the data about the information
            dataText.text = GameControler.instance.Information[informationToDisplay].data;
        }
    }

    /// <summary>
    /// Method that hides the information
    /// </summary>
    public void HideInformation()
    {
        // if there is a panel
        if (panelText != null)
        {
            // sees if the information being displayed is this script's information.
            // This prevents that other script hides this script's information
            if (nameText.text == GameControler.instance.Information[informationToDisplay].title && dataText.text == GameControler.instance.Information[informationToDisplay].data)
            {
                // disable the panel
                panelText.enabled = false;
                // erase the texts
                nameText.text = "";
                dataText.text = "";
            }
        }
    }

    public void OnFind()
    {
        
    }

    public void OnLost()
    {
        HideInformation();
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
