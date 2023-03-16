using System.Collections;
using UnityEngine;

namespace Memoriae.Interaction
{
    public class TreeVolume : MonoBehaviour
    {
        [SerializeField] private GameObject dialogueText;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                StartCoroutine(ProcessDialogue());
            }
        }

        private IEnumerator ProcessDialogue()
        {
            dialogueText.SetActive(true);
            yield return new WaitForSeconds(20);
            dialogueText.SetActive(false);
        }
    }
}
