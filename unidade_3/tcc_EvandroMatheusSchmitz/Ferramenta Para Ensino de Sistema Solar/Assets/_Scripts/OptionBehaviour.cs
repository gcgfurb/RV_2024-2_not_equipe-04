using UnityEngine;
using System.Collections.Generic;
using System;

public class OptionBehaviour : MonoBehaviour, ICollision
{

    #region  PRIVATE_VARIABLES
    /// <summary>
    /// Boolean used as a flag to know if the size was changed or not before actually changing it.
    /// </summary>
    private bool sizeChanged = false;

    /// <summary>
    /// Number that will help calculate how much the gameobject should shrink or increase
    /// </summary>
    private float increaseGameObjectFactor = 3;

    /// <summary>
    /// Number that will help calculate how much the collider should shrink or increase
    /// </summary>
    private float increaseColliderFactor = 2;
    #endregion

    #region PUBLIC_VARIABLES
    /// <summary>
    /// Boolean to determine if the size should change when a collision happens.
    /// </summary>
    public bool changeSize = true;

    /// <summary>
    /// Boolean to determine if the orentation should change in the Start method.
    /// </summary>
    public bool changeOrientation = true;

    /// <summary>
    /// Boolean to determine if the size of the collider should change when a collision happens.
    /// </summary>
    public bool changeColliderSize = true;

    /// <summary>
    /// The tag of the collider object that should let the trigger event happen.
    /// </summary>
    public TargetsTags targetCollision = TargetsTags.MULTI_TARGET;
    #endregion

    #region UNITY_METHODS
    // Use this for initialization
    void Start () {
        // set the position acording to the hand
        if (changeOrientation)
        {
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x * (int)GameControler.instance.UserHand, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
        }

    }
	
	// Update is called once per frame
	void Update () {
       
    }

    void OnTriggerEnter(Collider other)
    {
        
            if (other.tag == targetCollision.ToString())
            {
                increaseObject();
            }
    }

    void OnTriggerExit(Collider other)
    {
       
        // if it is a multitarget
        if (other.tag == targetCollision.ToString())
        {
            shrinkObject();
        }
    }
    #endregion

    #region PRIVATE_METHODS
    private void shrinkObject()
    {
        if (changeSize && sizeChanged)
        {
            if (changeColliderSize)
            {
                // shrink the gameObject
                CapsuleCollider cCollider = gameObject.GetComponent<CapsuleCollider>();
                cCollider.radius = (cCollider.radius / increaseColliderFactor) * increaseGameObjectFactor;
            }

            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x / increaseGameObjectFactor, gameObject.transform.localScale.y / increaseGameObjectFactor, gameObject.transform.localScale.z / increaseGameObjectFactor);
            sizeChanged = false;
        }
    }

    private void increaseObject()
    {
        if (changeSize && !sizeChanged)
        {
            // make the game object bigger
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * increaseGameObjectFactor, gameObject.transform.localScale.y * increaseGameObjectFactor, gameObject.transform.localScale.z * increaseGameObjectFactor);

            if (changeColliderSize)
            {
                CapsuleCollider cCollider = gameObject.GetComponent<CapsuleCollider>();
                cCollider.radius = (cCollider.radius / increaseGameObjectFactor) * increaseColliderFactor;
            }

            sizeChanged = true;
        }
    }

    #region PUBLIC_METHODS
    public void OnCollisionFound()
    {

    }

    public void OnCollisionLost()
    {
        shrinkObject();
    }
    #endregion
    #endregion
}
