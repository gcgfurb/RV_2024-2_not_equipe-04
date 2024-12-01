using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Linq;

public class CollisionDetection : MonoBehaviour {

    #region PRIVATE_VARIABLES
    /// <summary>
    /// List of colliders, used as a way to detect the end of the collision and to do a on trigger exit.
    /// This is necessary as Vuforia sometimes lose the target, but this does not call a trigger exit method as the collider was just disable and not really ended the colision.
    /// So this is just to have a way to end the collision, to show some consistency
    /// </summary>
    private List<Collider> collisions;

    private int collisionNumber;
    #endregion

    #region PUBLIC_VARIABLES
    /// <summary>
    /// Array of targets that have the detection detected.
    /// </summary>
    public TargetsTags[] tagsMatter = new TargetsTags[1];
    #endregion

    // Use this for initialization
    void Start () {
        // list initialization
        collisions = new List<Collider>();

        collisionNumber = 0;
    }
	
	// Update is called once per frame
	void Update () {
        // interates in the collision array
        for (int i = 0; i < collisions.Count;)
        {
            // if there is collision is enable increase i
            if (collisions[i].enabled)
            {
                i++;
            }
            else
            {
                // if there collision is disable (Target was lost), remove it
                collisions.RemoveAt(i);
                // if the array is empty
                if (collisions.Count == 0 && collisionNumber > 0)
                {
                    collisionNumber = 0;
                    ICollision[] behaviours = gameObject.GetComponents<ICollision>();
                    
                    foreach (ICollision behaviour in behaviours)
                    {
                        behaviour.OnCollisionLost();
                    }
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // if there are tags and the other's tag is the tagsMatter array
        if (tagsMatter != null && Enum.IsDefined(typeof(TargetsTags), other.tag) && tagsMatter.Contains((TargetsTags)Enum.Parse(typeof(TargetsTags), other.tag)))
        {
            // add other to the collision array
            collisions.Add(other);
            collisionNumber++;
        }

    }

    void OnTriggerExit(Collider other)
    {

        // if there are tags and the other's tag is the tagsMatter array
        if (tagsMatter != null && Enum.IsDefined(typeof(TargetsTags), other.tag) && tagsMatter.Contains((TargetsTags)Enum.Parse(typeof(TargetsTags), other.tag)))
        {
            // remove other from the collision array
            collisions.Remove(other);
            collisionNumber--;
        }
    }
}
