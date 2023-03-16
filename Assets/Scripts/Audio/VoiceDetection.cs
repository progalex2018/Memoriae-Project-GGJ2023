using UnityEngine;

namespace Memoriae.Audio
{
    public class VoiceDetection : MonoBehaviour
    {
        [SerializeField] private int sampleWindow = 64;

        private AudioClip microphoneClip;
        private int micItem = 0;
        void Start()
        {
            MicrophoneToClip();
        }

        private void MicrophoneToClip()
        {
            string microphoneName = Microphone.devices[micItem];
            microphoneClip = Microphone.Start(microphoneName, true, 20, AudioSettings.outputSampleRate);
        }

        public float GetVolumeFromMicrophone()
        {
            return GetVolumeFromClip(Microphone.GetPosition(Microphone.devices[micItem]), microphoneClip);
        }

        public float GetVolumeFromClip(int clipPosition, AudioClip clip)
        {
            int startPosition = clipPosition - sampleWindow;

            if (startPosition < 0) return 0;

            float[] amplitudeData = new float[sampleWindow];
            clip.GetData(amplitudeData, startPosition);

            float totalVolume = 0;
            for (int i = 0; i < sampleWindow; i++)
            {
                totalVolume += Mathf.Abs(amplitudeData[i]);
            }

            return totalVolume / sampleWindow;
        }
    }
}
