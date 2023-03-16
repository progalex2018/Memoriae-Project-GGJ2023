using UnityEngine;
using Memoriae.Control;

namespace Memoriae.Audio
{
    public class FogDensityControl : MonoBehaviour
    {
        [SerializeField] private VoiceDetection detector;
        [SerializeField] private float volumeSensibility = 100;
        [SerializeField] private float threshold = 0.1f;

        private EnemyController[] enemy;

        private void Start()
        {
            enemy = FindObjectsOfType<EnemyController>();
        }

        private void Update()
        {
            float volume = detector.GetVolumeFromMicrophone() * volumeSensibility;

            if (Time.timeScale != 0)
            {
                if (volume > threshold)
                {
                    RenderSettings.fogDensity -= 0.005f;
                    if (RenderSettings.fogDensity < 0) RenderSettings.fogDensity = 0;
                    else if (RenderSettings.fogDensity < 0.05f) RenderSettings.fogDensity = 0.05f;

                    foreach (var item in enemy)
                    {
                        item.EngageTarget();
                    }
                }
                else
                {
                    foreach (var item in enemy)
                    {
                        item.IgnoreTarget();
                    }

                    RenderSettings.fogDensity += 0.005f;
                    if (RenderSettings.fogDensity > 0.2) RenderSettings.fogDensity = 0.2f;
                }
            }
            else
            {
                RenderSettings.fogDensity = 0.2f;
            }
        }
    }
}
