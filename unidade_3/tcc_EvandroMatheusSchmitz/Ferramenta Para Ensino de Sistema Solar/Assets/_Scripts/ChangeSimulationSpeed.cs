using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ChangeSimulationSpeed : MonoBehaviour, IAddValueBehaviour, ICollision
{

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
    public Text speedText;

    /// <summary>
    /// Default text that will be added in every display.
    /// </summary>
    public String speedTextString = "Velocidade:";

    /// <summary>
    /// Float number. This is a value used as a way to have a more smooth speed change.
    /// The formula is absolute value of (currentRotaion - lastRotaion) > regulation.
    /// This way not every little movent causes a change of speed.
    /// </summary>
    public float regulationValue = 1;

    /// <summary>
    /// The tag of the collider object that should let the trigger event happen.
    /// </summary>
    public TargetsTags targetCollision = TargetsTags.MULTI_TARGET;
    #endregion

    #region PRIVATE_VARIABLES
    /// <summary>
    /// The last rotation. Used to calcule diference and know if it is to increase or decrease values.
    /// </summary>
    private float lastRotation = 0;
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
        if (other.tag == targetCollision.ToString())
        {
            // showing data, or default data
            ShowData();
        }

    }

    void OnTriggerStay(Collider other)
    {
        // if it is a multitarget
        if (other.tag == targetCollision.ToString())
        {
            // gets the rotation
            float currentRotation = subjectTransform.rotation.eulerAngles.y;

            // calculates the absolute value of current - last to see if it is above regulation value
            // this is to give a more regular rotation
            if (Mathf.Abs((currentRotation - lastRotation)) >= regulationValue)
            {
                // add the value
                AddValue(currentRotation - lastRotation);

                // atributes the currentRotation to the lastRotation
                lastRotation = currentRotation;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // if it is a multitarget
        if (other.tag == targetCollision.ToString())
        {
            // hide the data
            HideData();
        }
    }
    #endregion

    #region PUBLIC_METHODS
    public void AddValue(float add)
    {
        if (add > 0)
        {
            // increasing the value of the simulation speed
            PlanetSubject.instance.addSpeed();
        }
        else
        {
            // decreasing the value of the simulation speed
            PlanetSubject.instance.minusSpeed();
        }
        // showing the speed to the user
        speedText.text = speedTextString + " " + PlanetSubject.instance.Speed + "";
    }

    public void ShowData()
    {
        // enable the panel to have a color backgroud. Better vision
        panelText.enabled = true;
        // showing the speed to the user
        speedText.text = speedTextString + " " + PlanetSubject.instance.Speed + "";
    }

    public void HideData()
    {
        // disable the panel.
        panelText.enabled = false;
        // erase the text
        speedText.text = "";
    }

    public void OnCollisionFound()
    {
        
    }

    public void OnCollisionLost()
    {
        HideData();
    }
    #endregion

    #region PRIVATE_METHODS
    #endregion
}
