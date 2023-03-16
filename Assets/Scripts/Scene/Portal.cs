using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Memoriae.Control;
using Memoriae.Interaction;

namespace Memoriae.Scene
{
    public class Portal : MonoBehaviour
    {
        enum DestinationId
        {
            A, B, C, D
        }

        [SerializeField] private int sceneToLoad = -1;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private DestinationId destination;
        [SerializeField] private float fadeOutTime = 3f;
        [SerializeField] private float fadeInTime = 3f;
        [SerializeField] private float waitTime = 2f;
        [SerializeField] private string pathName = null;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerController>())
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            if (sceneToLoad < 0)
            {
                Debug.LogError("Scene to load non-existent.");
                yield break;
            }

            if (sceneToLoad == 5)
            {
                SceneManager.LoadScene(sceneToLoad);
                yield break;
            }

            DontDestroyOnLoad(gameObject);

            Fader fader = FindObjectOfType<Fader>();

            yield return fader.FadeOut(fadeOutTime, pathName);

            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            UpdateKey();

            yield return new WaitForSeconds(waitTime);

            yield return fader.FadeIn(fadeInTime);

            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<PlayerController>().enabled = false;
            player.transform.position = otherPortal.spawnPoint.position;
            player.transform.rotation = otherPortal.spawnPoint.rotation;
            player.GetComponent<PlayerController>().enabled = true;
        }

        private void UpdateKey()
        {
            Key key = FindObjectOfType<Key>();

            GameObject keySpawn = GameObject.FindWithTag("KeySpawn");
            if (keySpawn == null)
                return;
            else
                key.SpawnKey(keySpawn);
        }

        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
                if (portal.destination != destination) continue;

                return portal;
            }
            return null;
        }
    }
}
