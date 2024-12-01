using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotationBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider rotationSlider; // Referência ao Slider
    public GameObject[] planets; // Lista de planetas para aplicar a rotação
    public float rotationSpeed = 10f; // Velocidade inicial de rotação

    private float currentRotationSpeed; // Velocidade de rotação calculada pelo slider
    void Start()
    {
        if (rotationSlider != null)
        {
            // Configura o listener para o slider
            rotationSlider.onValueChanged.AddListener(UpdateRotationSpeed);
        }

        // Define a velocidade inicial
        currentRotationSpeed = rotationSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        RotatePlanets();
    }

    // Atualiza a velocidade de rotação com base no slider
    void UpdateRotationSpeed(float value)
    {
        currentRotationSpeed = value * rotationSpeed; // Ajusta a rotação proporcional ao slider
        Debug.Log($"Velocidade de rotação ajustada para: {currentRotationSpeed}");
    }

    // Aplica a rotação aos planetas
    void RotatePlanets()
    {
        if (planets == null || planets.Length == 0)
        {
            return;
        }

        foreach (GameObject planet in planets)
        {
            if (planet != null)
            {
                // Aplica a rotação no eixo Y (como exemplo)
                planet.transform.Rotate(Vector3.up, currentRotationSpeed * Time.deltaTime);
            }
        }
    }
}
