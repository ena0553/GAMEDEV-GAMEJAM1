using UnityEngine;

public class Atmosphere : MonoBehaviour
{
    [Header("Ambient and fog")]
    public Color ambient = new Color(0.015f, 0.015f, 0.03f);
    public Color fogColor = new Color(0.01f, 0.005f, 0.005f);
    [Range(0f, 0.3f)] public float fogDensity = 0.07f;
    public bool disableDirectionalLights = true;

    [Header("Flashlight")]
    public bool addFlashlight = true;
    public Color flashlightColor = new Color(1f, 0.93f, 0.8f);
    public float flashlightIntensity = 2.5f;
    public float flashlightRange = 18f;
    [Range(10f, 120f)] public float flashlightAngle = 55f;
    [Range(0f, 1f)] public float flickerAmount = 0.25f;
    [Tooltip("Chance per second of a short blackout flicker")]
    public float blackoutChance = 0.08f;

    Light _flashlight;
    float _blackoutUntil;

    void Awake()
    {
        RenderSettings.skybox = null;
        RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
        RenderSettings.ambientLight = ambient;
        RenderSettings.fog = true;
        RenderSettings.fogMode = FogMode.ExponentialSquared;
        RenderSettings.fogColor = fogColor;
        RenderSettings.fogDensity = fogDensity;

        var cam = Camera.main;
        if (cam)
        {
            cam.clearFlags = CameraClearFlags.SolidColor;
            cam.backgroundColor = Color.black;
        }

        if (disableDirectionalLights)
        {
            foreach (var l in FindObjectsByType<Light>(FindObjectsSortMode.None))
                if (l.type == LightType.Directional) l.enabled = false;
        }

        if (addFlashlight && cam)
        {
            var go = new GameObject("Flashlight");
            go.transform.SetParent(cam.transform, false);
            _flashlight = go.AddComponent<Light>();
            _flashlight.type = LightType.Spot;
            _flashlight.color = flashlightColor;
            _flashlight.intensity = flashlightIntensity;
            _flashlight.range = flashlightRange;
            _flashlight.spotAngle = flashlightAngle;
            _flashlight.shadows = LightShadows.Soft;
        }
    }

    void Update()
    {
        if (!_flashlight) return;

        if (Time.time > _blackoutUntil && Random.value < blackoutChance * Time.deltaTime)
            _blackoutUntil = Time.time + Random.Range(0.05f, 0.35f);

        if (Time.time < _blackoutUntil)
        {
            _flashlight.intensity = flashlightIntensity * 0.05f;
            return;
        }

        float n = Mathf.PerlinNoise(Time.time * 9f, 0.5f);
        _flashlight.intensity = flashlightIntensity * (1f - flickerAmount + n * flickerAmount);
    }
}
