using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UranoBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 axisRotate;
    public float rotationSpeed;
    void Start()
    {
        axisRotate = new Vector3(0, 1, 0);
        rotationSpeed = 2;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(axisRotate * rotationSpeed * Time.deltaTime);
    }
}