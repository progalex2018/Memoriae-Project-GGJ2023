using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Memoriae.Core
{
    public class PersistentObjectSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject persistentObjectPrefab;

        private static bool hasSpawned = false;

        private GameObject persistentObject;

        private void Awake()
        {
            if (hasSpawned) return;

            SpawnPersistentObject();
            hasSpawned = true;
        }

        private void SpawnPersistentObject()
        {
            persistentObject = Instantiate(persistentObjectPrefab);
            DontDestroyOnLoad(persistentObject);
        }

        public void DestroyPersistentObject()
        {
            Destroy(persistentObject);
            hasSpawned = false;
        }
    }
}
