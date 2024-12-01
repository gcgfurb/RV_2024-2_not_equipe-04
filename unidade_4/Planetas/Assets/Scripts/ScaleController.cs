using UnityEngine;
using UnityEngine.UI;

public class SimpleScaleController : MonoBehaviour
{
    public Button increaseButton; // Bot�o para aumentar a escala
    public Button decreaseButton; // Bot�o para diminuir a escala
    public float scaleStep = 0.05f; // O valor para aumentar ou diminuir a escala
    public float scaleStepAsteroids = 0.01f;
    public float minScale = 0.05f; // Valor m�nimo de escala
    public float maxScale = 2.0f; // Valor m�ximo de escala

    private Transform urano; // Refer�ncia para o objeto Urano
    private Transform mercurio;
    private Transform jupiter;
    private Transform sun;
    private Transform asteroids;
    private Transform earth;
    private Transform moon;
    private Transform saturn;

    void Start()
    {
        // Procurar os objetos Urano e J�piter na cena
        urano = GameObject.Find("Urano")?.transform;
        mercurio = GameObject.Find("Mercurio")?.transform;
        jupiter = GameObject.Find("Jupiter")?.transform;
        sun = GameObject.Find("Sun")?.transform;
        asteroids = GameObject.Find("Asteroids Belt")?.transform;
        earth = GameObject.Find("EarthMoon")?.transform;
        moon = GameObject.Find("MoonIsolated")?.transform;
        saturn = GameObject.Find("Saturn")?.transform;

        // Configurar os eventos dos bot�es
        if (increaseButton != null)
        {
            increaseButton.onClick.AddListener(IncreaseScale);
        }

        if (decreaseButton != null)
        {
            decreaseButton.onClick.AddListener(DecreaseScale);
        }
    }

    // Aumentar a escala de Urano e J�piter
    void IncreaseScale()
    {
        if (urano != null)
        {
            ChangeScale(urano, scaleStep);
        }
        if (mercurio != null)
        {
            ChangeScale(mercurio, scaleStep);
        }
        if (jupiter != null)
        {
            ChangeScale(jupiter, scaleStep);
        }
        if (sun != null)
        {
            ChangeScale(sun, scaleStep);
            ChangeScale(asteroids, scaleStepAsteroids);
        }
        if (earth != null)
        {
            ChangeScale(earth, scaleStep);
        }
        if (moon != null)
        {
            ChangeScale(moon, scaleStep);
        }
        if (saturn != null)
        {
            ChangeScale(saturn, scaleStep);
        }
    }

    // Diminuir a escala de Urano e J�piter
    void DecreaseScale()
    {
        if (urano != null)
        {
            ChangeScale(urano, -scaleStep);
        }
        if (mercurio != null)
        {
            ChangeScale(mercurio, -scaleStep);
        }
        if (jupiter != null)
        {
            ChangeScale(jupiter, -scaleStep);
        }
        if (sun != null)
        {
            ChangeScale(sun, -scaleStep);
            ChangeScale(asteroids, -scaleStepAsteroids);
        }
        if (earth != null)
        {
            ChangeScale(earth, -scaleStep);
        }
        if (moon != null)
        {
            ChangeScale(moon, -scaleStep);
        }
        if (saturn != null)
        {
            ChangeScale(saturn, -scaleStep);
        }
    }

    // Alterar a escala de um objeto, garantindo que n�o ultrapasse os limites
    void ChangeScale(Transform planet, float scaleDelta)
    {
        // Obt�m o valor atual da escala (assumindo que x, y, z s�o iguais)
        float currentScale = planet.localScale.x;
        float newScaleValue = Mathf.Clamp(currentScale + scaleDelta, minScale, maxScale);

        // Aplica a nova escala
        Vector3 newScale = new Vector3(newScaleValue, newScaleValue, newScaleValue);
        planet.localScale = newScale;

        Debug.Log($"Escala de {planet.name} alterada para: {newScale}");
    }
}
