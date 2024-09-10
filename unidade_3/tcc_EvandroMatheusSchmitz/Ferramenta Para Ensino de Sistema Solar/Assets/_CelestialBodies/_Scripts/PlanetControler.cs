using UnityEngine;
using System.Collections;

public class PlanetControler : CelestialBodyControler {

    #region PRIVATE_VARIABLES
    /// <summary>
    /// The day in the planet in the simulation. One day it the time to rotate around itself.
    /// Not protected against reverse rotation (going to the "past").
    /// It is not used
    /// </summary>
    private int _day;
    /// <summary>
    /// The day in the planet in the simulation. One day it the time to rotate around itself.
    /// Not protected against reverse rotation (going to the "past").
    /// (get)
    /// It is not used
    /// </summary>
    public int Day {
        get { return _day; }
    }

    /// <summary>
    /// The year in the planet in the simulation. One year it the time to rotate around the sun of the planet.
    /// Not protected against reverse rotation (going to the "past").
    /// It is not used
    /// </summary>
    private int _year;
    /// <summary>
    /// The year in the planet in the simulation. One year it the time to rotate around the sun of the planet.
    /// Not protected against reverse rotation (going to the "past").
    /// (get)
    /// It is not used
    /// </summary>
    public int Year {
        get { return _year; }
    }

    /// <summary>
    /// Boolean used to count the days.
    /// As it is used a system to compare the rotation between to values, this is needed to count only once a interval.
    /// It is not used
    /// </summary>
    private bool lockDay;
    /// <summary>
    /// Boolean used to count the years.
    /// As it is used a system to compare the position between to values, this is needed to count only once a interval.
    /// It is not used
    /// </summary>
    private bool lockYear;
    #endregion

    #region PUBLIC_VARIABLES
    /// <summary>
    /// The time the planet takes to complet a year in Earth days
    /// </summary>
    public float daysPerYear = 1;
    /// <summary>
    /// time that the planet takes to complet a rotation in Earth days
    /// </summary>
    public float rotationPeriod = 1;
    #endregion

    #region UNITY_METHODS
    // Use this for initialization
    void Start()
    {
        _day = 0;
        _year = 0;

        lockDay = false;
        lockYear = false;
    }

    // Update is called once per frame
    void Update()
    {
        // do the rotation
        SpaceMovement();

        // verefy if a day has passed
        /*float transformAux;
        if (axis == RotationAxis.Y) { transformAux = transform.rotation.y; }
        else { transformAux = transform.rotation.x; }

        if (transformAux < 0.5 && transformAux > -0.5)
        {
            if (lockDay)
            {
                _day++;
                lockDay = false;
            }
        } else { lockDay = true; }

        // verefy if a year has passed
        transformAux = transform.position.z;
        if (transformAux < 0.5 && transformAux > -0.5)
        {
            if (lockYear)
            {
                _year++;
                _day = 0;
                lockYear = false;
            }
        }
        else { lockYear = true; }*/
    }
    #endregion
}
