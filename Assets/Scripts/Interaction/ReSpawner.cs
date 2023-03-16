using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Memoriae.Control
{
    public class ReSpawner : MonoBehaviour
    {
        [SerializeField] private Transform spawn;

        private PlayerController player;

        private void Start()
        {
            player = FindObjectOfType<PlayerController>();
        }

        private void FixedUpdate()
        {
            if (player.transform.position.y < -40f)
            {
                player.transform.position = spawn.position;
            }
        }
    }
}
