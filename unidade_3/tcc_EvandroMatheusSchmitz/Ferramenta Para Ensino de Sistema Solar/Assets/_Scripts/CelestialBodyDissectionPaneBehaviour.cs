using System;
using UnityEngine;
using Vuforia;

public class CelestialBodyDissectionPaneBehaviour : CelestialBodyPaneBehaviour
{

    #region PRIVATE_VARIABLES
    /// <summary>
    /// Bool to say if the planet should change size or not.
    /// If true, when this target is found or lost it informs the others that there should be change in someone size.
    /// If false, when this target or other targets are found or lost, there no change of size.
    /// </summary>
    [SerializeField] private bool _changeSize = true;
    public bool ChangeSize
    {
        get { return _changeSize; }
        set
        {
            _changeSize = value;

            if (render && _changeSize)
            {
                PlanetSubject.instance.addPlanet(planetName);
            } else if(render && !_changeSize)
            {
                PlanetSubject.instance.removePlanet(planetName);
            }
        }
    }

    [SerializeField]
    private bool _changeInformarion = true;
    public bool ChangeInformarion
    {
        get { return _changeInformarion; }
        set
        {
            _changeInformarion = value;
        }
    }

    #endregion

    #region PUBLIC_VARIABLES
    /// <summary>
    /// The name of the planet.
    /// </summary>
    public Planet planetName;

    /// <summary>
    /// The information that should be display when the is the planet display
    /// </summary>
    public Information planetInformation;
    
    /// <summary>
    /// The information that should be display when the is the dissection display
    /// </summary>
    public Information dissectionInformation;
    #endregion

    protected override void Start()
    {
        base.Start();
        AdaptInformation(CurrentDisplay);
    }

    #region PUBLIC_METHODS

    /// <summary>
    /// Implementation of the ITrackableEventHandler function called when the
    /// tracking state changes.
    /// </summary>
    public override void OnTrackableStateChanged(
                                    TrackableBehaviour.Status previousStatus,
                                    TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            OnTrackingFound();
            // if the children should have its size changed, call the PlanetSubject to say that a planet was added
            if (_changeSize)
            {
                PlanetSubject.instance.addPlanet(planetName);
            }
        }
        else
        {
            OnTrackingLost();
            // if the children should have its size changed, call the PlanetSubject to say that a planet was lost
            if (_changeSize)
            {
                PlanetSubject.instance.removePlanet(planetName);
            }
        }
    }
    #endregion // PUBLIC_METHODS

    #region PRIVATE_METHODS
    /// <summary>
    /// Change the information tha show be displayed.
    /// </summary>
    /// <param name="currentSystemDisplay">The current DisplayMode of the aplication. This parameter is necessary as the CurrentDisplay of this script may not change</param>
    private void AdaptInformation(DisplayMode currentSystemDisplay)
    {
        // gets the ShowInformation script  of this game object, if there is any
        ShowInformation script = gameObject.GetComponent<ShowInformation>();
        if (script != null)
        {
            // verifies what is the current display and changes the information the ShowInformation script shhould show
            switch (currentSystemDisplay)
            {
                case DisplayMode.PLANET:
                    script.informationToDisplay = planetInformation;
                    break;
                case DisplayMode.DISSECTION:
                    script.informationToDisplay = dissectionInformation;
                    break;
            }
        }
    }
    #endregion

    #region OBSERVER_METHODS
    public override void update(UpdateData update)
    {
        // involke the base method
        base.update(update);

        // switch between the diferent displays types
        switch (update.UpdateType)
        {
            case Action.DISSECTION:
                // change the information that should the displayed
                if (_changeInformarion) {
                    AdaptInformation((DisplayMode)update.Data);
                }
                break;
            case Action.SIZE:
                // change size if it should
                if (_changeSize)
                {
                    float size;
                    
                    foreach (Transform child in transform)
                    {
                        // change the size of anything that is not micellaneus and it is not this game object
                        // use the transform to find the children
                        if (child.tag != "Miscellaneous" && child != this.transform)
                        {
                            // calculate the proportion of the size of the planets
                            size = (GameControler.instance.planetSize[(int)planetName] / (float)update.Data);
                            // atributes the new size
                            child.transform.localScale = new Vector3(size, size, size);
                        }
                    }
                }
                break;
        }
    }
    #endregion
}
