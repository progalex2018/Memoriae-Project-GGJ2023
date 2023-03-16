using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Memoriae.Interaction
{
    public class KeyValidation : MonoBehaviour
    {
        [SerializeField] private GameObject dialogueText;

        private Key key;

        void Start()
        {
            key = FindObjectOfType<Key>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                if (key.IsKey())
                {
                    Debug.Log("Access Granted...");
                    gameObject.transform.Find("Wall").gameObject.SetActive(false);
                }
                else
                {
                    StartCoroutine(ProcessDialogue());
                }
            }
        }

        private IEnumerator ProcessDialogue()
        {
            dialogueText.SetActive(true);
            yield return new WaitForSeconds(15);
            dialogueText.SetActive(false);
        }
    }
}
