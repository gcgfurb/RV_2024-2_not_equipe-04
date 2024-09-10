using UnityEngine;
using System.Collections.Generic;
using System;

public class PlanetSubject : MonoBehaviour, ISubject {

    #region PRIVATE_VARIABLE
    /// <summary>
    /// An UpdateData that will be used in the notify method.
    /// </summary>
    private UpdateData update;

    /// <summary>
    /// Current speed of the solar system simulation.
    /// </summary>
    private float _speed;
    /// <summary>
    /// Current speed of the solar system simulation.
    /// (get, set)
    /// </summary>
    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    /// <summary>
    /// List of observers.
    /// </summary>
    private List<IObserver> observers = new List<IObserver>();

    /// <summary>
    /// List of size of celestial bodies being shown.
    /// </summary>
    private List<float> shown = new List<float>();

    /// <summary>
    /// Speed variation of the simulation. It is how many unity it will increase or decrease the speed, when it is changed.
    /// </summary>
    private float _varietion = 1;
    /// <summary>
    /// Speed variation of the simulation. It is how many unity it will increase or decrease the speed, when it is changed.
    /// (get, set)
    /// </summary>
    public float Varietion
    {
        get { return _varietion; }
        set
        {
            if (_varietion > 0)
            {
                _varietion = value;
            }
        }
    }
    #endregion

    #region PUBLIC_VARIABLE
    /// <summary>
    /// Singleton variable.
    /// </summary>
    public static PlanetSubject instance = null;
    #endregion

    #region UNITY_METHODS
    void Awake()
    {
        // Singleton intantiation
        if (instance == null) { instance = this; }
        else if (instance != this) { Destroy(gameObject); }

        //DontDestroyOnLoad(gameObject);
    }
    
    // Use this for initialization
    void Start () {
        // setting the normal speed to 10
        Speed = 10;
        // initiation of the UpdateData
        update = new UpdateData();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    #endregion

    #region PUBLIC_METHODS
    /// <summary>
    /// Adds the variation to the general speed of the simulation.
    /// </summary>
    public void addSpeed()
    {
        Speed += _varietion;
        callUpdate(Action.SPEED);
    }

    /// <summary>
    /// Reduces the variation from the general speed of the simulation.
    /// </summary>
    public void minusSpeed()
    {
        Speed -= _varietion;
        callUpdate(Action.SPEED);
    }

    /// <summary>
    /// Changes the display mode of the observers. 
    /// It depends of if the observer. 
    /// </summary>
    /// <param name="display">The new display mode</param>
    public void changeDisplayMode(DisplayMode display)
    {
        update.Data = display;
        callUpdate(Action.DISSECTION);
    }

    /// <summary>
    /// Add a new planet to the array of planets beeing shown. This also changes the sizes of the planets
    /// </summary>
    /// <param name="planetName">The planet whose target was found</param>
    public void addPlanet(Planet planetName)
    {
        shown.Add(GameControler.instance.planetSize[(int)planetName]);

        callUpdate(Action.SIZE);
    }

    /// <summary>
    /// Removes a planet from the array of planets beeing shown. This also changes the sizes of the planets
    /// </summary>
    /// <param name="planetName">The planet whose target was lost</param>
    public void removePlanet(Planet planetName)
    {
        shown.Remove(GameControler.instance.planetSize[(int)planetName]);

        if (shown.Count > 0)
        {
            callUpdate(Action.SIZE);
        }
    }

    #endregion

    #region PRIVATE_METHODS
    /// <summary>
    /// Prepares the UpdateData and calls the notify method
    /// </summary>
    /// <param name="actionUpdate">The action of the update</param>
    private void callUpdate(Action actionUpdate)
    {
        switch (actionUpdate)
        {
            case Action.SPEED:
                // set the data as the new speed
                update.Data = _speed;
                break;

            case Action.SIZE:
                // orders the array
                shown.Sort();
                // gets the biggest planet diameter, as this will be the base for the calculations of the new diameters of the other planets
                update.Data = shown[shown.Count - 1];
                break;
        }

        // sets the update type
        update.UpdateType = actionUpdate;
        notify();
    }
    #endregion

    #region OBSERVER_METHODS
    public void addObserver(IObserver observer)
    {
        observers.Add(observer);
    }

    public void removeObserver(IObserver observer)
    {
        observers.Remove(observer);
    }

    public void notify()
    {
        foreach (IObserver observer in observers)
        {
            observer.update(update);
        }
    }
    #endregion
}
