using UnityEngine;
using System.Collections;

/// <summary>
/// The rotation axis of the planet. Y is for planets as earth and X is for planets as Uranus
/// </summary>
public enum RotationAxis {X, Y};

/// <summary>
/// Planet rotation direction.
/// EAST WEST equals Venus (rotate the oposite), WEST EAST planets as Earth.
/// </summary>
public enum RotationDirection : int { EAST_WEST = 1, WEST_EAST = -1 };

public class CelestialBodyControler : MonoBehaviour {

    #region PRIVATE_VARIABLES
    /// <summary>
    /// Used if the planet has a 90º degree inclination.
    /// The script creates a new game object to be the parent of this game object, this way is possible to have a rotation around itself and the sun
    /// </summary>
    private Transform father;

    /// <summary>
    /// Speed(angle) to translate the barycenter.
    /// The higher, the faster.
    /// </summary>
    [SerializeField] private float _revolutionSpeed;
    /// <summary>
    /// Speed(angle) to translate the barycenter.
    /// The higher, the faster.
    /// If this game object has a father than the father speed is also changed.
    /// (get, set)
    /// </summary>
    public float RevolutionSpeed
    {
        get { return _revolutionSpeed; }
        set
        {
            if (father != null)
            {
                CelestialBodyControler celestialBody = father.GetComponent<CelestialBodyControler>();
                if (celestialBody != null)
                {
                    celestialBody.RevolutionSpeed = value;
                }
            }
            _revolutionSpeed = value;
        }
    }

    /// <summary>
    /// Speed(angle) to translate the around itself.
    /// The higher, the faster.
    /// </summary>
    [SerializeField] private float _rotationSpeed;

    /// <summary>
    /// Speed(angle) to translate the around itself.
    /// The higher, the faster.
    /// (get, set)
    /// </summary>
    public float RotationSpeed
    {
        get { return _rotationSpeed; }
        set { _rotationSpeed = value; }
    }

    /// <summary>
    /// The mass of the celestial body.
    /// If it is a star than it is in solar mass.
    /// </summary>
    [SerializeField] protected float _mass;

    /// <summary>
    /// The mass of the celestial body.
    /// If it is a star than it is in solar mass.
    /// (get, set)
    /// </summary>
    public float Mass
    {
        get { return _mass; }
        set
        {
            if (value > 0)
            {
                _mass = value;
            }
        }
    }

    /// <summary>
    /// Represents the temperature of the celestial body in K.
    /// </summary>
    [SerializeField]
    private float _temperature;
    /// <summary>
    /// Represents the temperature of the celestial body in K.
    /// (get, set)
    /// </summary>
    public float Temperature
    {
        get { return _temperature; }
        set
        {
            if (value > 0)
            {
                _temperature = value;
            }
        }
    }
    #endregion

    #region PUBLIC_VARIABLES
    /// <summary>
    /// The center of translation. Around what this game object should rotate.
    /// </summary>
    public GameObject barycenter;

    /// <summary>
    /// Axis of the rotation of the planet.
    /// If Y, it is as the Earth, if X then it is as Uranus
    /// </summary>
    public RotationAxis axis = RotationAxis.Y;

    /// <summary>
    /// Axis inclination in relation with the vertical.
    /// It sets the rotation of the planet acording with the give value.
    /// </summary>
    public float axisInclination;

    // the direction of the rotation
    /// <summary>
    /// The direction of the rotation.
    /// Venus is EAST WEST, Earth is WEST EAST
    /// </summary>
    public RotationDirection direction;
    #endregion

    #region UNITY_METHODS
    void Awake()
    {
        // sets the rotation of the planet
        transform.Rotate(new Vector3(0,0, axisInclination));

        // see if the rotation axis is diferent of Y
        if (axis != RotationAxis.Y)
        {
            // creates a empty game object to be the father of this game object
            GameObject father = new GameObject(gameObject.name + "Father");
            // sets the position as the same as this game object
            father.transform.position = transform.position;
            // adds a CelestialBodyControler to the father
            father.AddComponent<CelestialBodyControler>();
            // gets the CelestialBodyControler added to the father
            CelestialBodyControler contreler = father.GetComponent<CelestialBodyControler>();

            // sets the barycenter as the same
            contreler.barycenter = this.barycenter;
            // sets the revolution speed. The father is responsible for the revolution, the son for the rotaion
            contreler._revolutionSpeed = this._revolutionSpeed;

            // if this game object has a previus father, parents the father to this game object previus father 
            if (gameObject.transform.parent != null) { father.transform.parent = gameObject.transform.parent; }

            // parents this game object to the father
            gameObject.transform.parent = father.transform;

            // sets the father, to keeps a reference if it is need. Ex: to chanfge the speed
            this.father = father.transform;
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // does the movement
        SpaceMovement();
    }

    #endregion

    #region PRIVATE_METHODS
    protected void SpaceMovement()
    {
        // if this game object has a body center
        if (barycenter != null)
        {
            // if tha axis is Y rotates around the barycenter. If not the father of this game object will take care of this
            if (axis == RotationAxis.Y) { transform.RotateAround(barycenter.transform.position, transform.up, _revolutionSpeed * Time.deltaTime); }
        }

        // rotates around itself
        transform.Rotate(0, _rotationSpeed * Time.deltaTime, 0);
        
    }
    #endregion

    #region PUBLIC_METHODS
    #endregion

}
