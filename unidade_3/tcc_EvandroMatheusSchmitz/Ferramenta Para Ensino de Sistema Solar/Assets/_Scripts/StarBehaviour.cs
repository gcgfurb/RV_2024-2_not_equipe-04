using UnityEngine;
using System.Collections;
using System;

public class StarBehaviour : MonoBehaviour {

    #region PUBLIC_VARIABLES

    /// <summary>
    /// Percentage of the life time of the star in the out of the main sequence and before its dead.
    /// </summary>
    public static float percentRed = 0.1f;

    /// <summary>
    /// The scripts of the nebulas used in the simulation.
    /// </summary>
    public StarNebulaControler[] starNebulas;

    /// <summary>
    /// The prefab of the star.
    /// </summary>
    public GameObject starBodyPrefab;

    /// <summary>
    /// The prefabs that represent the dead of the star (white dwarf, neutron star, black hole and should stay in this order).
    /// </summary>
    public GameObject[] endsPrefabs = new GameObject[3];

    /// <summary>
    /// Any game object that the star should the child.
    /// Used for mainly because Vuforia, but it can also be used as a way to put the star in other parts of the scene.
    /// </summary>
    public GameObject starFather;

    /// <summary>
    /// The nebulas of explosions representing the dead of a star.
    /// (planetary nebula and supernova and should be inserted in this order)
    /// </summary>
    public GameObject[] nebulasExplosionPrefab = new GameObject[3];
    #endregion

    #region PRIVATE_VARIABLES
    /// <summary>
    /// The mass of the star, in solar mass. 1 solar mass equals the mass of our Sun.
    /// </summary>
    [SerializeField]private float _starMass = 1;
    /// <summary>
    /// The mass of the star, in solar mass. 1 solar mass equals the mass of our Sun.
    /// (get, set)
    /// </summary>
    public float StarMass
    {
        get { return _starMass; }
        set
        {
            if (value > 0)
            {
                _starMass = value;
            }
        }
    }

    /// <summary>
    /// The time used in the simulation. 1 equals 1 minute and the it is the time that will take to pass 10 billion years.
    /// As the time speed increases the time that takes to 10 billion years to pass also increases.
    /// </summary>
    [SerializeField]private int _timeSpeed = 1;
    /// <summary>
    /// The time used in the simulation. 1 equals 1 minute and the it is the time that will take to pass 10 billion years.
    /// As the time speed increases the time that takes to 10 billion years to pass also increases.
    /// (get, set)
    /// </summary>
    public int TimeSpeed
    {
        get { return _timeSpeed; }
        set
        {
            if (value > 0)
            {
                _timeSpeed = value;
            }
        }
    }

    /// <summary>
    /// The life of the star. It is divied in three parts: 
    ///  - nebula: the star is being generated;
    ///  - main sequence: where the stay will star most time of its life
    ///  - red giant or supergiant (named only as red): where the star will expand.
    /// </summary>
    private double[] lifeTime = new double[3];

    /// <summary>
    /// If true starts the simulation of the life time of the star if false, it does nothing or stops the simulation.
    /// </summary>
    private bool _startSimulation = false;
    /// <summary>
    /// If true starts the simulation of the life time of the star if false, it does nothing or stops the simulation.
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

    /// <summary>
    /// The state of the star:
    ///  - nebula: it is being born;
    ///  - main sequence: where the stay will star most time of its life;
    ///  - red giant or supergiant (named only as red): where the star will expand;
    ///  - death: shrink, be destroyed and be repleced be a white dwarf, neutron star or black hole.
    /// </summary>
    private StarTime state;
    /// <summary>
    /// Seconds between the instatiation of the nebula of the exploxion and the  instation of dead prefab of the star
    /// </summary>
    private float contSeconds;
    /// <summary>
    /// A flag varieable to say if the nebula has "changed" (it started to shrink).
    /// Basically it is use to "change" the nebula, instantiete the star prefab, parent it to its father, if there is one, just once per simulation.
    /// </summary>
    private bool changeNebula;
    /// <summary>
    /// Used to replace the star with its dead prefab just once
    /// </summary>
    private bool changeBody;

    /// <summary>
    /// The instace of the star created.
    /// </summary>
    private GameObject starInstance;
    /// <summary>
    /// The star controler of the starInstance.
    /// </summary>
    private StarControlerOld starControler;
    /// <summary>
    /// The prefab that the represents what the star will be when it dies
    /// </summary>
    private GameObject prefabDead;
    /// <summary>
    /// The explosion nebula that will the displayed before the prefabDead
    /// </summary>
    private GameObject nebulaDead;

    /// <summary>
    /// It say what will be the end of the star
    /// </summary>
    private StarEnd dead;
    #endregion

    #region UNITY_METHODS
    // Use this for initialization
    void Start () {
        Init();
	}
	
	// Update is called once per frame
	void Update () {

        // to see if it is to simulate or not
        if (_startSimulation)
        {
            // what state the star is now and what to do in it
            switch (state)
            {
                case StarTime.NEBULA:
                    
                    // just to take this actions once
                    if (changeNebula)
                    {
                        changeNebula = false;

                        // destroy prefabs of previous simulations
                        GameObject.Destroy(prefabDead);
                        GameObject.Destroy(nebulaDead);

                        // calls the method to calculate the times of the star
                        calculateTime();

                        // set the life time of the nebulas, and informs then that the simulation started
                        foreach (StarNebulaControler nebula in starNebulas)
                        {
                            if (nebula != null)
                            {
                                nebula.LifeTime = (float)lifeTime[(int)state];
                                nebula.Simulate = true;
                            }
                        }

                        // instatiate the star using a prefab
                        starInstance = Instantiate(starBodyPrefab);

                        // parent the star with it father in the (0,0,0), if there is a father
                        if (starFather != null)
                        {
                            starInstance.transform.parent = starFather.transform;
                            starInstance.transform.localPosition = new Vector3(0, 0, 0);
                        }
                        // set the star scale to 0, as it has to grow over time and not start as a full size small sun
                        starInstance.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
                        // gets the StarControler script of the star
                        starControler = starInstance.GetComponent<StarControlerOld>();
                        // sets the mass of the star
                        starControler.Mass = _starMass;
                        // set the diference of the scale
                        starControler.ScaleDiference = 0.5f;
                        // sets the state of the star as the current state
                        starControler.state = state;
                        // calculate the data of the star
                        starControler.calculateData();
                        // set the life of the star in the main sequence
                        starControler.LifeTime = (long)lifeTime[(int)StarTime.MAIN_SEQUENCE];
                        // says that the starControler should start the simulation
                        starControler.StartSimulation = true;

                        // uses the mass of the star to determinated what will be its end
                        if (_starMass < 10)
                        {
                            dead = StarEnd.WHITE_DWARF;
                        } else if (_starMass <  25)
                        {
                            dead = StarEnd.NEUTRON_STAR;
                        } else
                        {
                            dead = StarEnd.BLACK_HOLE;
                        }
                    }

                    break;

                case StarTime.MAIN_SEQUENCE:
                    // sets the state of the star as the current state
                    starControler.state = state;
                    break;

                case StarTime.RED:
                    // sets the state of the star as the current state
                    starControler.state = state;
                    break;

                case StarTime.DEATH:
                    starControler.state = state;
                    // see if the starControler is null, because this should start only after the star is destroyed
                    if (starControler == null)
                    {
                        
                        if (contSeconds >= 5)
                        {
                            nebulaDead = null;

                            // instantiate a nebula to be used in the dead of the star, depending of what the star will be when it dies
                            switch (dead)
                            {
                                case StarEnd.WHITE_DWARF:
                                    nebulaDead = Instantiate(nebulasExplosionPrefab[0]);
                                    break;
                                case StarEnd.NEUTRON_STAR:
                                case StarEnd.BLACK_HOLE:
                                    nebulaDead = Instantiate(nebulasExplosionPrefab[1]);
                                    break;
                            }

                            // if the nebulaDead is not null and there is a father for the star, parent the nebula to the father
                            if (starFather != null && nebulaDead != null)
                            {
                                nebulaDead.transform.parent = starFather.transform;
                                nebulaDead.transform.localPosition = new Vector3(0, 0, 0);
                            }
                            
                            nebulaDead.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

                        }

                        // when the contSeconds ends and if changeBody is true
                        if (changeBody && contSeconds <= 0)
                        {
                            // instanciate the prefabs the represent what will be leaf of the star after dead
                            prefabDead = Instantiate(endsPrefabs[(int)dead]);

                            // parent it to the father
                            if (starFather != null)
                            {
                                prefabDead.transform.parent = starFather.transform;
                                prefabDead.transform.localPosition = new Vector3(0, 0, 0);
                            }

                            // set a diferent scale depending of the what is the dead type
                            switch (dead)
                            {
                                case StarEnd.WHITE_DWARF:
                                    prefabDead.transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);
                                    break;
                                case StarEnd.NEUTRON_STAR:
                                case StarEnd.BLACK_HOLE:
                                    prefabDead.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                                    break;
                            }

                            // changes changeBody to false, because this actions are to happen only one time
                            changeBody = false;

                            // reinitate the main variables of the simulation
                            Init();
                        }

                        // decrease the time from the seconds of diference of the explosion tho the remains of the star
                        contSeconds -= Time.deltaTime;
                    } else
                    {
                        // just to ensure that contSeconds is 5 the first time, because as it is a float it can change.
                        contSeconds = 5;
                    }
                    break;
            }
            
            // sees if it can decrease the time of the life time of the star
            if (state < StarTime.DEATH && !changeNebula)
            {
                lifeTime[(int)state] -= Time.deltaTime;

                // if a lifeTime of a state is 0 or below, increse the state
                if (lifeTime[(int)state] <= 0)
                {
                    state++;
                }
            }

        }
	}
    #endregion

    #region PRUBLIC_METHODS
    #endregion

    #region PRIVATE_METHODS
    /// <summary>
    /// Calculates the time of life in the main sequence of the star, but this depends of the mass of the star.
    /// Here it is also calculated the time the star have when it is being generated and the time it has as a red giant or super giant, but this values are arbitrary and not based on scientific data.
    /// </summary>
    private void calculateTime()
    {
        long MSTime = (long)((1 / (Mathf.Pow(_starMass, 2))) * Mathf.Pow(10, 10));

        /* here it is add 20 % more time 10 % berefe it enters in the main sequence and 
         * 10 % after the main sequence. It it not a rule it is an arbitrary value that I chose
        */
        lifeTime[(int)StarTime.MAIN_SEQUENCE] = (_timeSpeed * 60) * (MSTime/10000000000f);
        
        lifeTime[(int)StarTime.NEBULA] = lifeTime[(int)StarTime.MAIN_SEQUENCE] * StarSimulation.percentNebula;
        lifeTime[(int)StarTime.RED] = lifeTime[(int)StarTime.MAIN_SEQUENCE] * percentRed;
    }

    /// <summary>
    /// Initiates the variables used in the simulation process.
    /// It is also used in the end of the simulation to prepare for another simulation.
    /// </summary>
    private void Init()
    {
        state = StarTime.NEBULA;
        changeNebula = true;
        changeBody = true;
        _startSimulation = false;
    }
    #endregion

}
