using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScaleBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider scaleSlider;
    public GameObject objectToScale;
    public float minScale = 0.5f;
    public float maxScale = 3.0f;
    void Start()
    {
        scaleSlider.onValueChanged.AddListener(UpdateScale);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void UpdateScale(float value)
    {
        float scaledValue = Mathf.Lerp(minScale, maxScale, value);
        objectToScale.transform.localScale = Vector3.one * scaledValue;
    }
}
