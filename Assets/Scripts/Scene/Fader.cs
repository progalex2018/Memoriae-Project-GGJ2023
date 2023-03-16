using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Memoriae.Scene
{
    public class Fader : MonoBehaviour
    {
        private CanvasGroup canvasGroup;
        private TextMeshProUGUI pathText;

        private Coroutine currentActiveFade = null;

        void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            pathText = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void FadeOutImmediate()
        {
            canvasGroup.alpha = 1;
        }

        public IEnumerator FadeOut(float time, string pathName)
        {
            pathText.text = pathName;
            return Fade(1, time);
        }

        public IEnumerator FadeIn(float time)
        {
            return Fade(0, time);
        }

        private IEnumerator Fade(float target, float time)
        {
            if (currentActiveFade != null)
            {
                StopCoroutine(currentActiveFade);
            }
            currentActiveFade = StartCoroutine(FadeRoutine(target, time));
            yield return currentActiveFade;
        }

        private IEnumerator FadeRoutine(float target, float time)
        {
            while (!Mathf.Approximately(canvasGroup.alpha, target))
            {
                canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, target, Time.deltaTime / time);
                yield return null;
            }
        }
    }
}
