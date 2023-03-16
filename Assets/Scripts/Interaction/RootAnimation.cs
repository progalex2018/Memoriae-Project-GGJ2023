using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Memoriae.Interaction
{
    public class RootAnimation : MonoBehaviour
    {

        [SerializeField] private float waterCounter = 0f;
        [SerializeField] private float amountNeeded = 5f;

        private Animation rootAnim;
        private AudioSource rootSound;

        void Start()
        {
            rootAnim = GetComponent<Animation>();
            rootSound = GetComponent<AudioSource>();
        }

        public void IncreaseWaterCounter()
        {
            waterCounter++;
            if (waterCounter == amountNeeded)
            {
                rootAnim["rootAnim"].speed = 1;
                rootAnim["rootAnim"].time = 0;
                rootAnim.Play("rootAnim");
                rootSound.Play();
            }
        }

        public void ResetCounterAndAnim()
        {
            waterCounter = 0;
            rootAnim["rootAnim"].speed = -1;
            rootAnim["rootAnim"].time = rootAnim["rootAnim"].length;
            rootAnim.Play("rootAnim");
        }
    }
}
