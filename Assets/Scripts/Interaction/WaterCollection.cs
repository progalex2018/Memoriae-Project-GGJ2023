using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Memoriae.Interaction
{
    public class WaterCollection : MonoBehaviour
    {
        private RootAnimation[] root;

        private AudioSource waterSound;

        private void Start()
        {
            root = FindObjectsOfType<RootAnimation>();
            waterSound = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                waterSound.Play();
                MeshRenderer renderer = GetComponentInChildren<MeshRenderer>();
                renderer.enabled = false;

                foreach (var item in root)
                {
                    item.IncreaseWaterCounter();
                }
                gameObject.GetComponent<BoxCollider>().enabled = false;
            }
        }

        public void ResetWater()
        {
            MeshRenderer renderer = GetComponentInChildren<MeshRenderer>();
            renderer.enabled = true;

            gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }
}
