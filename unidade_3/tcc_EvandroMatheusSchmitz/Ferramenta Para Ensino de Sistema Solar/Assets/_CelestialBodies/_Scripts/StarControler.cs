using UnityEngine;
using System.Collections;

/// <summary>
/// The color of the star. It was created as the Unity Color enum did not have all needed colors.
/// </summary>
public enum StarColor : int { BLUE = 0, LIGHT_BLUE, WHITE, LIGHT_YELLOW, YELLOW, ORANGE, RED }

public class StarControler : CelestialBodyControler
{

    #region PUBLIC_VARIABLES
    #endregion

    #region PRIVATE_VARIABLES
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
    private long _lifeTime;
    /// <summary>
    /// Represents the life time of the star.
    /// (get, set)
    /// </summary>
    public long LifeTime
    {
        get { return _lifeTime; }
        set
        {
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
        set
        {
            if (value > 0)
            {
                _age = value;
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
        set
        {
            if (value > 0)
            {
                _ray = value;
            }
        }
    }
    #endregion

    #region UNITY_METHODS
    // Use this for initialization
    void Start()
    {
        calculateData();
    }

    // Update is called once per frame
    void Update()
    {
        // rotate the star
        SpaceMovement();
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
        calculateLifeTime();
    }

    /// <summary>
    /// Sets the color of the star, the material of the star and the color of the particle system that represents the flare of the star.
    /// </summary>
    public void setColors(StarColor color, Material material, Color particleColor)
    {
        _starColor = color;
        gameObject.GetComponent<Renderer>().sharedMaterial = material;

        ParticleSystem particleSystem = gameObject.GetComponentInChildren<ParticleSystem>();

        if (particleSystem != null)
        {
            particleSystem.startColor = particleColor;

            Gradient gradient = new Gradient();
            gradient.SetKeys(new GradientColorKey[] { new GradientColorKey(particleColor, 0f), new GradientColorKey(Color.white, 1f) }, new GradientAlphaKey[] { new GradientAlphaKey(0, 1), new GradientAlphaKey(1, 0) });

            var colorOverLifeTime = particleSystem.colorOverLifetime;
            colorOverLifeTime.enabled = true;
            colorOverLifeTime.color = gradient;
        }
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
            setColors(StarColor.WHITE, Resources.Load<Material>("_StarMaterial/WhiteStar"), new Color(1, 1, 1));
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
    /// The luminosity dempends of the mass.
    /// It has varius ways of doing it but on avarege it is L = M^3.
    /// But as the luminosity of the star for the ray need to be in watts, it is times 3.9*10^-5 watt(luminosity of the Sun).
    /// TODO: links of the site were this information was taken
    /// </summary>
    private void calculateStarLuminosity()
    {
        _starLuminosity = Mathf.Pow(_mass, 3) * 3.9f * Mathf.Pow(10, 26);
    }

    /// <summary>
    /// Calculates the ray using the temperature and the luminosity. The diameter is 2*ray.
    /// In the and the ray is divide by the rayDivider, that is based on the SunScale, to have a star with proporcional dimentions with our Sun.
    /// TODO: links of the site were this information was taken
    /// </summary>
    private void calculateStarRay()
    {
        _ray = (_starLuminosity) / ((5.67 * Mathf.Pow(10, -8)) * (Mathf.Pow(Temperature, 4)) * 4 * Mathf.PI);

        _ray = Mathf.Sqrt((float)_ray);
    }

    /// <summary>
    /// Calculates the time of life in the main sequence of the star, but this depends of the mass of the star.
    /// Here it is also calculated the time the star have when it is being generated and the time it has as a red giant or super giant, but this values are arbitrary and not based on scientific data.
    /// </summary>
    private void calculateLifeTime()
    {
        _lifeTime = (long)((1 / (Mathf.Pow(_mass, 2))) * Mathf.Pow(10, 10));
    }
    #endregion
}
