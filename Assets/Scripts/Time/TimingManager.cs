using UnityEngine;
using TMPro;
using System;
using Memoriae.Control;
using Memoriae.Interaction;

namespace Memoriae.Timing
{
    public class TimingManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private GameObject container;
        [SerializeField] private float timeLeft;
        [SerializeField] private AudioSource startSound;

        private RootAnimation[] root;
        private WaterCollection[] water;
        private PlayerController player;
        private bool isTimerOn = false;
        private bool alreadyActive = false;

        private void Start()
        {
            root = FindObjectsOfType<RootAnimation>();
            water = FindObjectsOfType<WaterCollection>();
            player = FindObjectOfType<PlayerController>();
            if (container != null)
                container.SetActive(false);
        }

        void FixedUpdate()
        {
            if (isTimerOn)
            {
                if (timeLeft >= 0)
                {
                    Timer(timeLeft);
                }
                else
                {
                    player.transform.position = new Vector3(10, 8.5f, 0);
                    timerText.text = TimeSpan.FromSeconds(0f).ToString("ss\\:ff") + " s";
                    timeLeft = 45f;
                    alreadyActive = false;
                    StopTimer();
                    ResetWaterAndRoot();
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!alreadyActive && other.GetComponent<PlayerController>())
            {
                startSound.Play();
                alreadyActive = true;
                container.SetActive(true);
                isTimerOn = true;
            }
        }

        public void StopTimer()
        {
            isTimerOn = false;
        }

        private void Timer(float currentTime)
        {
            timeLeft -= Time.deltaTime;
            timerText.text = TimeSpan.FromSeconds(currentTime).ToString("ss\\:ff") + " s";
        }

        private void ResetWaterAndRoot()
        {
            foreach (var item in water)
            {
                item.ResetWater();
            }
            foreach (var item in root)
            {
                item.ResetCounterAndAnim();
            }
        }
    }
}
