using UnityEngine;
using System.Collections.Generic;
using System;


/// <summary>
/// Display modes. It is used with a system of game objects with the same name as tags to show or hide something.
/// </summary>
public enum DisplayMode { HELIOCENTRICO, GEOCENTRICO, GEOCENTRICO_TYCHO, DISSECTION, PLANET, VIDA_ESTRELA, SISTEMA_BINARIO, SISTEMA_BINARIO_MASSA_DIFERENTE };

/// <summary>
/// Targets tags (have the same name as the tags of the targers or some of its children). It is used as a way to say if an information should be displayed or not as a game object/target collides.
/// </summary>
public enum TargetsTags { TARGET, POINTER, MULTI_TARGET };

/// <summary>
/// Our solar system sun and planets. It is used mainly as a easy way to access an array with size information about the planets.
/// </summary>
public enum Planet : int { SUN = 0, MERCURY, VENUS, EARTH, MARS, JUPITER, SATURN, URANUS, NEPTUNE };

/// <summary>
/// Used in the book page, to see what page is missing (not used).
/// </summary>
public enum Page { RIGHT, LEFT };

/// <summary>
/// Used in the information dictionary.
/// Each value has a string containg information about the it.
/// It is also used to store information about someone, but not in a pratical way.
/// </summary>
public enum Information:int { HELIO = 0, GEO, GEO_TYCHO, SUN, MERCURY, VENUS, EARTH, MARS, JUPITER, SATURN, URANUS, NEPTUNE, SUN_DISSECTION, MERCURY_DISSECTION, VENUS_DISSECTION, EARTH_DISSECTION, MARS_DISSECTION, JUPITER_DISSECTION, SATURN_DISSECTION, URANUS_DISSECTION, NEPTUNE_DISSECTION, STAR }

/// <summary>
/// The hand of the user
/// </summary>
public enum Hand : int { LEFT_HAND = -1, RIGTH_HAND = 1 }

public class GameControler : MonoBehaviour {

    #region PRIVATE_VARIABLE
    /// <summary>
    /// Information about something. Mainly about the solar system, its planets and teories.
    /// Uses a enum as key to be easer to use.
    /// </summary>
    private Dictionary<Information, InformationData> _information;
    /// <summary>
    /// Information about something. Mainly about the solar system, its planets and teories.
    /// Uses a enum as key to be easer to use.
    /// (get)
    /// </summary>
    public Dictionary<Information, InformationData>  Information
    {
        get { return _information; }
    }

    /// <summary>
    /// The users hand.
    /// </summary>
    [SerializeField]private Hand _userHand = Hand.RIGTH_HAND;
    public Hand UserHand
    {
        get { return _userHand; }
        set
        {
            _userHand = value;
        }
    }
    #endregion

    #region PUBLIC_VARIABLE
    /// <summary>
    /// Instance of this game object.
    /// </summary>
    public static GameControler instance = null;

    /// <summary>
    /// size of the planets in scale 1 = 10 thousant.
    /// </summary>
    public float[] planetSize = { 140f, 0.49f, 1.21f, 1.27f, 0.67f, 14.2f, 12f, 5.1f, 4.9f };
    #endregion


    #region UNITY_METHODS
    void Awake()
    {
        // Singleton intantiation
        if (instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(gameObject);
        }

        // to do not destroy on load new scenes
        DontDestroyOnLoad(gameObject);

        // Loading the information
        LoadInformation();
    }

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        
	}
    #endregion

    #region PUBLIC_METHODS
    /// <summary>
    /// Changes the color of a game object
    /// </summary>
    /// <param name="_gameObject">The game object that will have its color changed</param>
    /// <param name="color">The desired color</param>
    public void ChangeColor(GameObject _gameObject, Color color)
    {
        Material material = _gameObject.GetComponent<Renderer>().material;
        material.color = color;
        material.SetColor("_EmissionColor", color);
    }
    #endregion

    #region PRIVATE_METHODS
    /// <summary>
    /// Loads the stored information and puts it in the dictionary.
    /// </summary>
    private void LoadInformation()
    {
        _information = new Dictionary<Information, InformationData>();
        ProjectInformation projectInformation = ProjectInformation.CreateFromJson();

        foreach (InformationData informationData in projectInformation.informations)
        {
            _information.Add(informationData.key, informationData);
        }
    }
    #endregion
}
