using UnityEngine;

namespace MobaPrototype.World
{
    public class DayNightCycle : MonoBehaviour
    {
        [SerializeField] private Light directionalLight;
        [SerializeField] private float cycleLengthSeconds = 600f;
        [SerializeField] private Gradient lightColor;
        [SerializeField] private AnimationCurve lightIntensity = AnimationCurve.EaseInOut(0f, 0.25f, 0.5f, 1f);
        [SerializeField] private float movementSpeedNightMultiplier = 1.15f;

        public bool IsNight { get; private set; }
        public float NormalizedTime { get; private set; }
        public float MovementSpeedMultiplier => IsNight ? movementSpeedNightMultiplier : 1f;

        private void Update()
        {
            NormalizedTime = (NormalizedTime + Time.deltaTime / cycleLengthSeconds) % 1f;
            float daylight = (Mathf.Sin(NormalizedTime * Mathf.PI * 2f - Mathf.PI / 2f) + 1f) * 0.5f;
            IsNight = daylight < 0.35f;

            if (directionalLight == null) return;
            directionalLight.intensity = lightIntensity.Evaluate(daylight);
            directionalLight.color = lightColor != null ? lightColor.Evaluate(daylight) : Color.white;
        }
    }
}
