using UnityEngine;
using System.Collections;
using System;

public class StarControlerOld : CelestialBodyControler {

    #region PUBLIC_VARIABLES
    /// <summary>
    /// The state where the star is locaded.
    /// Is can be a nebula (it will increse without end),
    /// main sequence (it will behaviour as our Sun),
    /// red (it will turn red and increse without end) or
    /// death ( it will shrink and be destroyed)
    /// </summary>
    public StarTime state;
    #endregion

    #region PRIVATE_VARIABLES
    /// <summary>
    /// Any diference in the scale between this star and any representation of the Sun.
    /// If there is none use 1.
    /// The formula is: (2 * ray * scalediference) / sunScale .
    /// </summary>
    [SerializeField] private float _scaleDiference = 1;
    /// <summary>
    /// Any diference in the scale between this star and any representation of the Sun.
    /// If there is none use 1.
    /// The formula is: (2 * ray * scalediference) / sunScale .
    /// (get, set)
    /// </summary>
    public float ScaleDiference
    {
        get { return _scaleDiference; }
        set
        {
            if (value > 0)
            {
                _scaleDiference = value;
            }
        }
    }

    /// <summary>
    /// The scale of the our Sun used in the editor.
    /// This influences on the ray of the star.
    /// </summary>
    [SerializeField] private float _sunScale;
    /// The scale of the our Sun used in the editor.
    /// This influences on the ray of the star.
    /// (get, set)
    /// </summary>
    public float SunScale
    {
        get { return _sunScale; }
        set {
            if (value > 0) {
                _sunScale = value;
                ajustDivider();
            }
        }
    }

    /// <summary>
    /// The divider of the ray, uses the SunScale as base.
    /// </summary>
    private double rayDivider;

    /// <summary>
    /// Represents the color of the star.
    /// Its an enum.
    /// </summary>
    private StarColor _starColor;
    /// <summary>
    /// Represents the color of the star.
    /// Its an enum.
    /// (get, set)
    /// </summary>
    public StarColor StarColor
    {
        get { return _starColor; }
        set { _starColor = value; }
    }
    
    /// <summary>
    /// Represents the luminosity of the star. The sun luminosity is used as 1.
    /// </summary>
    private float _starLuminosity;
    /// <summary>
    /// Represents the luminosity of the star. The sun luminosity is used as 1. 
    /// (get, set)
    /// </summary>
    public float StarLuminosity
    {
        get { return _starLuminosity; }
        set { _starLuminosity = value; }
    }

    /// <summary>
    /// Represents the life time of the star.
    /// </summary>
    private double _lifeTime;
    /// <summary>
    /// Represents the life time of the star.
    /// (get, set)
    /// </summary>
    public double LifeTime
    {
        get { return _lifeTime; }
        set {
            if (value > 0)
            {
                _lifeTime = value;
            }
        }
    }

    /// <summary>
    /// Represents the age of the star.
    /// </summary>
    private long _age;
    /// <summary>
    /// Represents the age of the star.
    /// (get, set)
    /// </summary>
    public long Age
    {
        get { return _age; }
        set {
            if (value > 0)
            {
                _age = value;
            }
        }
    }

    
    /// <summary>
    /// Represents the bright time of the star.
    /// </summary>
    private long _brightTime;
    /// <summary>
    /// Represents the bright time of the star.
    /// (get, set)
    /// </summary>
    public long BrightTime
    {
        get { return _brightTime; }
        set {
            if (value > 0) {
                _brightTime = value;
            }
        }
    }

    /// <summary>
    /// Represents the ray of the star in meters.
    /// </summary>
    private double _ray;
    /// <summary>
    /// Represents the ray of the star in meters.
    /// (get, set)
    /// </summary>
    public double Ray
    {
        get { return _ray; }
        set {
            if (value > 0)
            {
                _ray = value;
            }
        }
    }

    /// <summary>
    /// Flag used once in the update method.
    /// Its use is for set the final ray just once.
    /// </summary>
    private bool change = true;

    /// <summary>
    /// If true says that this star should simulate its life cicle, using it's state as base. If false it stops what it was beeing doing.
    /// </summary>
    private bool _startSimulation = false;
    /// <summary>
    /// If true says that this star should simulate its life cicle, using it's state as base. If false it stops what it was beeing doing.
    /// (get, set)
    /// </summary>
    public bool StartSimulation
    {
        get { return _startSimulation; }
        set
        {
            _startSimulation = value;
        }
    }
    #endregion

    #region UNITY_METHODS
    // Use this for initialization
    void Start()
    {
        ajustDivider();

        calculateData();
    }

    // Update is called once per frame
    void Update()
    {
        // rotate the star
        SpaceMovement();

        float increase = 0;
        if (_startSimulation)
        {
            switch (state)
            {
                case StarTime.NEBULA:

                    /* Increases the diameter of the star from 0 to its diameter in the main sequence, in a smooth way
                     * needs revision
                     */
                    if (transform.localScale.x <= ((2 * _ray * _scaleDiference) / _sunScale)) {
                        increase = transform.localScale.x + ((float)((_ray / _sunScale) / (_lifeTime * StarSimulation.percentNebula)) * Time.deltaTime);
                        transform.localScale = new Vector3(increase, increase, increase);
                    }
                    break;

                case StarTime.MAIN_SEQUENCE:
                    if (change)
                    {
                        /* Calculates the final size of the sun
                        */
                        increase = (float) ((2 * _ray * _scaleDiference) / _sunScale);

                        transform.localScale = new Vector3(increase, increase, increase);

                        change = false;
                    }
                    break;

                case StarTime.RED:
                    /* Sets the color of the star as red and expands it until its life ends
                     */
                    setColors(StarColor.RED, Resources.Load<Material>("_StarMaterial/RedStar"), Color.red);
                    if (_lifeTime != 0) {
                        increase = transform.localScale.x + (float)(Time.deltaTime / (_lifeTime * 0.1));
                    }
                    transform.localScale = new Vector3(increase, increase, increase);
                    break;

                case StarTime.DEATH:
                    /* Destroy the flare of the star and start to shrink the star.
                     * When its scale is 0 or below, the star is destroyed of the scene
                     */
                    GameObject.Destroy(gameObject.GetComponentInChildren<ParticleSystem>());
                    increase = transform.localScale.x - (float)(Time.deltaTime * 2 * _mass);
                    transform.localScale = new Vector3(increase, increase, increase);
                    if (increase <= 0)
                    {
                        GameObject.Destroy(gameObject);
                    }
                    break;
            }
        }
        
    }
    #endregion

    #region PRUBLIC_METHODS
    /// <summary>
    /// Calculates the ray, the luminosity, the temperature and the color of the star.
    /// </summary>
    public void calculateData()
    {
        calculateStarTemperatureAndColor();
        calculateStarLuminosity();
        calculateStarRay();
    }
    #endregion

    #region PRIVATE_METHODS
    /// <summary>
    /// Deduces the temperature and the color of the star.
    /// The temperature depends of the mass.
    /// The color depends of the temperature.
    /// TODO: links of the site were this information was taken
    /// </summary>
    private void calculateStarTemperatureAndColor()
    {
        if (_mass <= 0.3)
        {
            Temperature = 3500;
            setColors(StarColor.RED, Resources.Load<Material>("_StarMaterial/RedStar"), Color.red);
        }
        else if (_mass <= 0.8)
        {
            Temperature = UnityEngine.Random.Range(3500, 5000);
            setColors(StarColor.ORANGE, Resources.Load<Material>("_StarMaterial/OrangeStar"), new Color(1, 0.5f, 0));
        }
        else if (_mass <= 1.1)
        {
            Temperature = UnityEngine.Random.Range(5000, 6000);
            setColors(StarColor.YELLOW, Resources.Load<Material>("_StarMaterial/Sun"), Color.yellow);
        }
        else if (_mass <= 1.7)
        {
            Temperature = UnityEngine.Random.Range(6000, 7500);
            setColors(StarColor.LIGHT_YELLOW, Resources.Load<Material>("_StarMaterial/LightYellowStar"), new Color(1, 1, 0.5f));
        }
        else if (_mass <= 3.2)
        {
            Temperature = UnityEngine.Random.Range(7500, 10000);
            setColors(StarColor.WHITE, Resources.Load<Material>("_StarMaterial/WhiteStar"), new Color(0, 0, 0));
        }
        else if (_mass <= 18)
        {
            Temperature = UnityEngine.Random.Range(10500, 25000);
            setColors(StarColor.LIGHT_BLUE, Resources.Load<Material>("_StarMaterial/LightBlueStar"), new Color(0, 0.5f, 1));
        }
        else
        {
            Temperature = 25000;
            setColors(StarColor.BLUE, Resources.Load<Material>("_StarMaterial/BlueStar"), Color.blue);
        }
    }

    /// <summary>
    /// Sets the color of the star, the material of the star and the color of the particle system that represents the flare of the star.
    /// </summary>
    private void setColors(StarColor color, Material material, Color particleColor)
    {
        _starColor = color;
        gameObject.GetComponent<Renderer>().sharedMaterial = material;

        ParticleSystem particleSystem = gameObject.GetComponentInChildren<ParticleSystem>();

        if (particleSystem != null) {
            particleSystem.startColor = particleColor;

            Gradient gradient = new Gradient();
            gradient.SetKeys(new GradientColorKey[] { new GradientColorKey(particleColor, 0f), new GradientColorKey(Color.white, 1f) }, new GradientAlphaKey[] { new GradientAlphaKey(0, 1), new GradientAlphaKey(1, 0) });

            var colorOverLifeTime = particleSystem.colorOverLifetime;
            colorOverLifeTime.enabled = true;
            colorOverLifeTime.color = gradient;
        }


    }

    /// <summary>
    /// The luminosity dempends of the mass.
    /// It has varius ways of doing it but on avarege it is L = M^3.
    /// But as the luminosity of the star for the ray need to be in watts, it is times 3.9*10^-5 watt(luminosity of the Sun).
    /// TODO: links of the site were this information was taken
    /// </summary>
    private void calculateStarLuminosity()
    {
        _starLuminosity = Mathf.Pow(_mass, 3) * 3.9f * Mathf.Pow(10,26);
    }

    /// <summary>
    /// Calculates the ray using the temperature and the luminosity. The diameter is 2*ray.
    /// In the and the ray is divide by the rayDivider, that is based on the SunScale, to have a star with proporcional dimentions with our Sun.
    /// TODO: links of the site were this information was taken
    /// </summary>
    private void calculateStarRay()
    {
        _ray = (_starLuminosity) / ((5.67 * Mathf.Pow(10, -8)) * (Mathf.Pow(Temperature, 4)) * 4 * Mathf.PI);

        _ray = Mathf.Sqrt((float)_ray)/rayDivider;
    }

    /// <summary>
    /// Calculate the divider of the ray with the SunScale as base.
    /// The formula is 1400000000(diameter of the sun in meters)/SunScale.
    /// </summary>
    private void ajustDivider()
    {
        rayDivider = (1400000000 / _sunScale);
    }
    #endregion
}
