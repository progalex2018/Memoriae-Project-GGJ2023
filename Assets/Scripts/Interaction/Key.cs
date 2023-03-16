using System.Collections;
using System.Collections.Generic;
using Memoriae.Control;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Memoriae.Interaction
{
    public class Key : MonoBehaviour
    {
        private bool isCorrectKey = false;
        private int visitationCheck = 0;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerController>())
            {
                isCorrectKey = true;
                MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
                foreach (MeshRenderer r in renderers)
                {
                    r.enabled = false;
                }

                GetComponentInChildren<Light>().enabled = false;
                gameObject.GetComponent<BoxCollider>().enabled = false;
            }
        }

        public void SpawnKey(GameObject keySpawn)
        {
            if (visitationCheck != SceneManager.GetActiveScene().buildIndex)
                visitationCheck += SceneManager.GetActiveScene().buildIndex;
            else
                visitationCheck += 0;

            if (visitationCheck < 7)
            {
                MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
                foreach (MeshRenderer r in renderers)
                {
                    r.enabled = false;
                }

                GetComponentInChildren<Light>().enabled = false;
                gameObject.GetComponent<BoxCollider>().enabled = false;
            }
            else
            {
                MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
                foreach (MeshRenderer r in renderers)
                {
                    r.enabled = true;
                }

                GetComponentInChildren<Light>().enabled = true;
                gameObject.GetComponent<BoxCollider>().enabled = true;
                transform.position = keySpawn.transform.position;
            }
        }

        public bool IsKey()
        {
            return isCorrectKey;
        }
    }
}
