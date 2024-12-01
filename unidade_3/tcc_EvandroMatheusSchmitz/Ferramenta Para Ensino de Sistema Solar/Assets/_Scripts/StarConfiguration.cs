using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

/// <summary>
/// Configuration used to see what the rotation of the cube should modifie.
/// </summary>
public enum Configuration { TEORY, SPEED, MASS }

public class StarConfiguration : MonoBehaviour, IAddValueBehaviour, ICollision {

    #region PUBLIC_VARIABLES

    /// <summary>
    /// Transform of the object that will have it's rotation detected.
    /// Needs to change depending of the world center mode, just as the game object that recives this script .
    /// </summary>
    public Transform subjectTransform;

    /// <summary>
    /// Panel of the text that will be displayed.
    /// </summary>
    public Image panelText;

    /// <summary>
    /// Main text that will be displayed.
    /// </summary>
    public Text dataText;

    /// <summary>
    /// Text that will be displayed as help of the main text.
    /// </summary>
    public Text helpText;

    /// <summary>
    /// The type of the configuration.
    /// </summary>
    public Configuration configType;

    /// <summary>
    /// The script that will suffer configuration modifications
    /// </summary>
    public StarSimulation starConfiguration;

    /// <summary>
    /// Float number. This is a value used as a way to have a more smooth speed change.
    /// The formula is absolute value of (currentRotaion - lastRotaion) > regulation.
    /// This way not every little movent causes a change of speed.
    /// </summary>
    public float regulationValue = 10;
    #endregion

    #region PRIVATE_VARIABLES
    /// <summary>
    /// The last rotation. Used to calcule diference and know if it is to increase or decrease values.
    /// </summary>
    private float lastRotation = 0;
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
        // showing data or defaul data
        if (other.tag == TargetsTags.MULTI_TARGET.ToString())
        {
            // showing data, or default data
            ShowData();
        }


    }

    void OnTriggerStay(Collider other)
    {
        // variable to get a number, just positive or negative
        int number = 0;
        // if it is a multitarget
        if (other.tag == TargetsTags.MULTI_TARGET.ToString())
        {
            // enable the panel to have a background
            panelText.enabled = true;

            // gets the rotation
            float currentRotation = subjectTransform.rotation.eulerAngles.y;

            // calculates the absolute value of current - last to see if it is above regulation value
            // this is to give a more regular rotation
            if (Mathf.Abs((currentRotation - lastRotation)) >= regulationValue)
            {
                // sees who is bigger
                if (currentRotation > lastRotation)
                {
                    // if current this means that there was an increase in the value, so posivite
                    number = 1;
                } else
                {
                    // if it was last this means that there was a decrease in the value, so negative
                    number = -1;
                }

                // atributes the currentRotation to the lastRotation
                lastRotation = currentRotation;
            }

            // calls the method with a value, and show a data
            callStarBehaviourAndShowInfo(number);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == TargetsTags.MULTI_TARGET.ToString())
        {
            // hide the data
            HideData();
        }
    }
    #endregion

    #region PUBLIC_METHODS
    // this method AddValue is useful if used with virtual buttons
    public void AddValue(float add)
    {
        // calls the method with a value, and show a data
        callStarBehaviourAndShowInfo((int)add);
    }

    public void ShowData()
    {
        // enable the panel for a background
        panelText.enabled = true;
        // calls the method with zero, so it will just show the data as default
        callStarBehaviourAndShowInfo(0);
    }

    public void HideData()
    {
        // disable the panel
        panelText.enabled = false;
        // erase the text
        dataText.text = "";
        helpText.text = "";
    }

    public void OnCollisionFound()
    {
        
    }

    public void OnCollisionLost()
    {
        // hide the data if the target or its parent was lost
        HideData();
    }
    #endregion

    #region PRIVATE_METHODS
    private void callStarBehaviourAndShowInfo(int number)
    {
        // depending of the configuration call a diferent method
        switch (configType)
        {
            case Configuration.SPEED:
                // if it is the speed change the speed of the star simulation and show its new value
                starConfiguration.TimeSpeed += number;
                dataText.text = "Velocidade da Simulação: " + starConfiguration.TimeSpeed;
                helpText.text = "1: 1 minuto = 10 bilhões de anos";
                break;
            case Configuration.MASS:
                // if it is the mass change the speed of the star simulation and show its new value
                starConfiguration.StarMass += number;
                dataText.text = "Massa da Estrela: " + starConfiguration.StarMass;
                helpText.text = "Massas solares";
                break;
        }
    }
    #endregion
}
