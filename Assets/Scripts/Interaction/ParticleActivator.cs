using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace Memoriae.Interaction
{
    public class ParticleActivator : MonoBehaviour
    {
        [SerializeField] private ParticleSystem familyTrace;
        [SerializeField] private PlayableDirector pD;

        private Key key;

        private void Start()
        {
            key = FindObjectOfType<Key>();

            familyTrace.Stop();

            if (key.IsKey())
            {
                pD.Play();
                familyTrace.Play();
            }
        }
    }
}
