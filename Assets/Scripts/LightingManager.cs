using UnityEngine;

public class LightingManager : MonoBehaviour
{
    [SerializeField] private Light directionalLight;

    [Header("Debug")]
    [SerializeField] private bool triggerDay = false;
    [SerializeField] private bool triggerNight = false;

    [Header("Day Preset")]
    [SerializeField] private Color dayAmbientColor = new Color(0.5f, 0.5f, 0.5f);
    [SerializeField] private Color dayLightColor = Color.white;
    [SerializeField] private float dayLightIntensity = 1f;
    [SerializeField] private Vector3 dayLightRotation = new Vector3(50f, -30f, 0f);

    [Header("Night Preset")]
    [SerializeField] private Color nightAmbientColor = new Color(0.02f, 0.02f, 0.08f);
    [SerializeField] private Color nightLightColor = new Color(0.1f, 0.1f, 0.3f);
    [SerializeField] private float nightLightIntensity = 0.05f;
    [SerializeField] private Vector3 nightLightRotation = new Vector3(50f, -30f, 0f);

    [Header("Transition")]
    [SerializeField] private float transitionDuration = 2f;

    private bool isDay = true;
    private bool isTransitioning = false;
    private float transitionProgress = 0f;

    private Color fromAmbient, toAmbient;
    private Color fromLightColor, toLightColor;
    private float fromIntensity, toIntensity;

    void Start()
    {
        ApplyDay();
    }

    void Update()
    {
        if (isTransitioning)
{
            transitionProgress += Time.deltaTime / transitionDuration;
            transitionProgress = Mathf.Clamp01(transitionProgress);

            RenderSettings.ambientLight = Color.Lerp(fromAmbient, toAmbient, transitionProgress);
            directionalLight.color = Color.Lerp(fromLightColor, toLightColor, transitionProgress);
            directionalLight.intensity = Mathf.Lerp(fromIntensity, toIntensity, transitionProgress);

    if (transitionProgress >= 1f)
        isTransitioning = false;
}

        if (triggerDay)
    {
        triggerDay = false;
        SetDay();
    }
    if (triggerNight)
    {
        triggerNight = false;
        SetNight();
    }

    }

    public void SetDay()
    {
        isDay = true;
        StartTransition(dayAmbientColor, dayLightColor, dayLightIntensity, dayLightRotation);
    }

    public void SetNight()
    {
        isDay = false;
        StartTransition(nightAmbientColor, nightLightColor, nightLightIntensity, nightLightRotation);
    }

    public void Toggle()
    {
        if (isDay) SetNight(); else SetDay();
    }

    private void StartTransition(Color ambient, Color lightColor, float intensity, Vector3 rotation)
    {
        fromAmbient = RenderSettings.ambientLight;
        fromLightColor = directionalLight.color;
        fromIntensity = directionalLight.intensity;

        toAmbient = ambient;
        toLightColor = lightColor;
        toIntensity = intensity;

        directionalLight.transform.eulerAngles = rotation;

        transitionProgress = 0f;
        isTransitioning = true;
    }

    private void ApplyDay()
    {
        RenderSettings.ambientLight = dayAmbientColor;
        directionalLight.color = dayLightColor;
        directionalLight.intensity = dayLightIntensity;
        directionalLight.transform.eulerAngles = dayLightRotation;
    }
}