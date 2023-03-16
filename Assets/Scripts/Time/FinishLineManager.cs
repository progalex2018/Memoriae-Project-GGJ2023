using System.Collections;
using System.Collections.Generic;
using Memoriae.Control;
using UnityEngine;

namespace Memoriae.Timing
{
    public class FinishLineManager : MonoBehaviour
    {
        [SerializeField] private AudioSource finishSound;
        private TimingManager timer;
        private bool alreadyActive = false;

        // Start is called before the first frame update
        void Start()
        {
            timer = FindObjectOfType<TimingManager>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!alreadyActive && other.GetComponent<PlayerController>())
            {
                finishSound.Play();
                timer.StopTimer();
                alreadyActive = true;
            }
        }
    }
}
