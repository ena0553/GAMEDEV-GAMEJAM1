using UnityEngine;

[DisallowMultipleComponent]
public class PacmanChomp : MonoBehaviour
{
    [Header("Jaws")]
    public Transform upperJaw;
    public Transform lowerJaw;
    [Range(0f, 70f)] public float maxOpenAngle = 38f;
    public float chompsPerSecond = 2.5f;

    [Header("Roblox glow")]
    public Light auraLight;
    public float auraBaseIntensity = 3f;
    public float auraFlicker = 0.8f;
    public Renderer[] eyeRenderers;
    public Color eyeColor = new Color(1f, 0.05f, 0.02f);
    public float eyeIntensity = 5f;

    MaterialPropertyBlock _mpb;
    static readonly int EmissionId = Shader.PropertyToID("_EmissionColor");

    void Awake()
    {
        _mpb = new MaterialPropertyBlock();
    }

    void Update()
    {
        // 0 -> 1 -> 0 smooth chomp cycle
        float open = Mathf.SmoothStep(0f, 1f, Mathf.PingPong(Time.time * chompsPerSecond * 2f, 1f));
        float a = open * maxOpenAngle;

        if (upperJaw) upperJaw.localRotation = Quaternion.Euler(-a, 0f, 0f);
        if (lowerJaw) lowerJaw.localRotation = Quaternion.Euler(a, 0f, 0f);

        float flicker = Mathf.PerlinNoise(Time.time * 7f, 0.37f);

        if (auraLight)
            auraLight.intensity = auraBaseIntensity + (flicker - 0.5f) * 2f * auraFlicker;

        if (eyeRenderers != null)
        {
            float pulse = eyeIntensity * (0.85f + 0.3f * flicker);
            foreach (var r in eyeRenderers)
            {
                if (!r) continue;
                r.GetPropertyBlock(_mpb);
                _mpb.SetColor(EmissionId, eyeColor * pulse);
                r.SetPropertyBlock(_mpb);
            }
        }
    }
}
