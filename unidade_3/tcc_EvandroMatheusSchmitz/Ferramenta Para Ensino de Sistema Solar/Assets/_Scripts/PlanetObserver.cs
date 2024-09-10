using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class PlanetObserver : MonoBehaviour, IObserver{


    #region PRIVATE_VARIABLES
    /// <summary>
    /// Text representing the name of the planet. 
    /// </summary>
    private Text nameText;
    /// <summary>
    /// Text for some data of the planet.
    /// </summary>
    private Text dataText;

    /// <summary>
    /// Panel where the texts are shown
    /// </summary>
    private Image panelText;
    #endregion

    #region PUBLIC_VARIABLES
    /// <summary>
    /// The direction of the revolution.
    /// </summary>
    public RotationDirection direction = RotationDirection.WEST_EAST;
    #endregion

    #region UNITY_METHODS
    // Use this for initialization
    void Start () {
        // add the observer
        PlanetSubject.instance.addObserver(this);
        // get the texts components
        nameText = GameObject.FindGameObjectWithTag("SimulationText").GetComponent<Text>();
        dataText = GameObject.FindGameObjectWithTag("SimulationData").GetComponent<Text>();
        // get the panel component
        panelText = GameObject.FindGameObjectWithTag("SimulationPanel").GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {

	}

    /*
        The triggers and showDaysAndYears was an idea to show how many days and years have passed in the planet since the begin of the simulation.
        As the project was evolving the idea was descarted but the code is comented to save, as this can be a cool feature for the furute.
         */

    void OnTriggerEnter(Collider other)
    {
        // if is a pointer
        /*if (other.tag == targetsTags.POINTER.ToString())
        {
            // show the days and years 
            showDaysAndYears();
        }*/
    }

    void OnTriggerStay(Collider other)
    {
        /*if (other.tag == targetsTags.POINTER.ToString())
        {
            // show the days and years 
            showDaysAndYears();
        }*/
    }

    void OnTriggerExit(Collider other)
    {
        // disable the panel
        /*panelText.enabled = false;
        // erase the text
        nameText.text = "";
        dataText.text = "";*/
    }
    #endregion

    #region PRIVATE_METHODS
    /// <summary>
    /// Show how many days and years have passed in the planet since the begin of the simulation
    /// </summary>
    private void showDaysAndYears()
    {
        // get the PlanetControler script attached to this component
        /*PlanetControler controler = gameObject.GetComponent<PlanetControler>();

        // enable the panel for a background
        panelText.enabled = true;
        /*this is to showw the days and years
         * nameText.text = controler.labelText;
        dataText.text = "Anos deste o começo da simulação: " + controler.Year +
                        "\nDias desde o ínico do último ano: " + controler.Day;*/
    }
    #endregion

    #region OBSERVER_METHODS
    public void update(UpdateData update)
    {
        // the update of the planets
        switch (update.UpdateType)
        {
            case Action.SPEED:
                // get the planet controler script atteched to this componet
                PlanetControler planet = gameObject.GetComponent<PlanetControler>();
                // sees if the is a PlanetControler
                if (planet != null) {
                    // change the revolution speed if the planet rotates has years
                    if (planet.daysPerYear != 0)
                    {
                        planet.RevolutionSpeed = ((float)(365.26 * (float)update.Data / planet.daysPerYear) * (int)direction);
                    }

                    // change the rotation speed using the direction as a way to correct the direction as some planets spin in diferent direction, if the planet has days
                    if (planet.rotationPeriod != 0) {
                        planet.RotationSpeed = ((float)(365.26 * (float)update.Data / planet.rotationPeriod)) * (int)planet.direction;
                    }
                } else
                {
                    // if there us no PlanetControler than it is the Sun, so
                    CelestialBodyControler celestialBody = gameObject.GetComponent<CelestialBodyControler>();
                    // if there is a CelestialBodyControler than change the speed.
                    if (celestialBody != null)
                    {
                        celestialBody.RevolutionSpeed = ((float)40 * ((float)update.Data / 10f) * (int)direction);
                    }
                }
                break;
        }
    }
    #endregion
}
