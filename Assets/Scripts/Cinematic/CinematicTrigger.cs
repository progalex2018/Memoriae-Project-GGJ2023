using Memoriae.Control;
using UnityEngine;
using UnityEngine.Playables;

namespace Memoriae.Cinematic
{
    public class CinematicTrigger : MonoBehaviour
    {
        [SerializeField] private PlayableDirector pD;

        private bool alreadyActive = false;

        private void OnTriggerEnter(Collider other)
        {
            if (!alreadyActive && other.GetComponent<PlayerController>())
            {
                pD.Play();
                alreadyActive = true;
            }
        }
    }
}
