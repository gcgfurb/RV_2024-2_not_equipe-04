using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotationBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider rotationSlider; // Refer�ncia ao Slider
    public GameObject[] planets; // Lista de planetas para aplicar a rota��o
    public float rotationSpeed = 10f; // Velocidade inicial de rota��o

    private float currentRotationSpeed; // Velocidade de rota��o calculada pelo slider
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

    // Atualiza a velocidade de rota��o com base no slider
    void UpdateRotationSpeed(float value)
    {
        currentRotationSpeed = value * rotationSpeed; // Ajusta a rota��o proporcional ao slider
        Debug.Log($"Velocidade de rota��o ajustada para: {currentRotationSpeed}");
    }

    // Aplica a rota��o aos planetas
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
                // Aplica a rota��o no eixo Y (como exemplo)
                planet.transform.Rotate(Vector3.up, currentRotationSpeed * Time.deltaTime);
            }
        }
    }
}
