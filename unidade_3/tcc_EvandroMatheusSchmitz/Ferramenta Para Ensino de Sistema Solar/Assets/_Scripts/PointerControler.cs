using UnityEngine;
using System.Collections;
using System;

public class PointerControler : MonoBehaviour {

    #region UNITY_METHODS
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "SpeedStake":
                // if it is a speed tag change the color of the pointer to blue
                GameControler.instance.ChangeColor(gameObject, Color.blue);
                break;
            case "Teory":
                // if it is a teory tag change the color of the pointer to purple
                GameControler.instance.ChangeColor(gameObject, new Color(0.74f,0,1,1));
                break;
            default:
                if (other.tag == TargetsTags.TARGET.ToString() || other.tag == TargetsTags.MULTI_TARGET.ToString())
                {
                    // if it is a anything else change the color of the pointer to green
                    GameControler.instance.ChangeColor(gameObject, Color.green);
                }
                break;
        }
    }

    void OnTriggerStay(Collider other)
    {
      
    }

    void OnTriggerExit(Collider other)
    {
        switch (other.tag)
        {
            case "SpeedStake":
            case "Teory":
                // as the contact with other collider ends the color returns to red
                GameControler.instance.ChangeColor(gameObject, Color.red);
                break;
            default:
                if (other.tag == TargetsTags.TARGET.ToString() || other.tag == TargetsTags.MULTI_TARGET.ToString())
                {
                    // as the contact with other collider ends the color returns to red
                    GameControler.instance.ChangeColor(gameObject, Color.red);
                }
                break;
        }

    }
    #endregion

    #region PRIVATE_METHODS
    #endregion
}
