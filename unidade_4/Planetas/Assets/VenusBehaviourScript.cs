using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenusBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotationSpeed = 10f;
    public GameObject sunObject;
    void Start()
    {
        // If the Sun object wasn't assigned in the Inspector, try to find it by name
        if (sunObject == null)
        {
            sunObject = GameObject.Find("Sun");
        }

        // Check if the Sun object was found
        if (sunObject == null)
        {
            Debug.LogError("Sun object not found. Please assign it in the Inspector or ensure it exists in the scene.");
            return;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (sunObject != null)
        {
            transform.RotateAround(sunObject.transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
        }

    }
}
