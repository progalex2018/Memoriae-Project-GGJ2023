using System.Collections;
using Memoriae.Control;
using UnityEngine;

namespace Memoriae.UI
{
    public class InstructionsVolume : MonoBehaviour
    {
        [SerializeField] private GameObject instructionCanvas;

        private bool alreadyActive = false;

        private void OnTriggerEnter(Collider other)
        {
            if (!alreadyActive && other.GetComponent<PlayerController>())
            {
                StartCoroutine(ProcessInstructions());
            }
        }

        private IEnumerator ProcessInstructions()
        {
            instructionCanvas.SetActive(true);
            yield return new WaitForSeconds(20);
            instructionCanvas.SetActive(false);
        }
    }
}
