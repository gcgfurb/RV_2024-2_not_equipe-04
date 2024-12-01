using UnityEngine;
using System.Collections;

public class StarNebulaControler : MonoBehaviour {

    #region PRIVATE_VARIABLES
    /// <summary>
    /// Flag to know if the particle system emiter was changed.
    /// </summary>
    private bool change = true;

    /// <summary>
    /// The life time of the nebula. after.
    /// After the time reaches 0 the nebula is destroyed.
    /// </summary>
    private float _lifeTime;
    /// <summary>
    /// The life time of the nebula. after.
    /// After the time reaches 0 the nebula is destroyed.
    /// (get, set)
    /// </summary>
    public float LifeTime
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
    /// Flag to know if it to simulate the life and destroction of the nebula.
    /// If true simulates, if false it stops or do nothing.
    /// </summary>
    private bool _simulate = false;
    /// <summary>
    /// Flag to know if it to simulate the life and destroction of the nebula.
    /// If true simulates, if false it stops or do nothing.
    /// (get, set)
    /// </summary>
    public bool Simulate
    {
        get { return _simulate; }
        set { _simulate = value; }
    }

    /// <summary>
    /// The ShapeModule of the particle system attached to this game object
    /// </summary>
    private ParticleSystem.ShapeModule shape;
    #endregion

    #region UNITY_METHODS
    // Use this for initialization
    void Start () {
        // gets the ShapeModule of the particle system attached to this game object
        shape = GetComponent<ParticleSystem>().shape;
    }
	
	// Update is called once per frame
	void Update () {

        // if it is to simulate
        if (_simulate)
        {
            // if the emiter was not changed
            if (change)
            {
                // change the shape of the emiter
                changeParticleShape();

                // atributes false to change, say that there is no more need to change
                change = false;
            }


            // This gives the emiter a ball shape, giving the ilusion of a cloud gathering at its center, 
            // as a gas cloud does to form a star

            // if the length of the shape is above 0 reduces it
            if (shape.length > 0)
            {
                shape.length -= (float)(_lifeTime * Time.deltaTime);
            }
            // if the lenght is below 0 and the is still the raduis above 0.1, decreases the radius
            else if (shape.radius > 0.1)
            {
                shape.radius -= (float)(_lifeTime * Time.deltaTime);
            }

            // decreases time
            _lifeTime -= Time.deltaTime;

            if (_lifeTime <= 0)
            {
                // if the time has reached 0 destroy this game object
                GameObject.Destroy(gameObject);
            }
        }
	}
    #endregion

    #region PRIVATE_METHODS
    private void changeParticleShape()
    {
        // say that there is no more reason to change
        change = false;
        // change the shape of the emitter
        shape.shapeType = ParticleSystemShapeType.ConeVolume;

        // sets the lenght of the shape
        shape.length = 5;
        // sets the radius of the shape
        shape.radius = 5;
    }
    #endregion
}
