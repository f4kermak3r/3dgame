using UnityEngine;
using System.Collections;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {

        CanvasGroup canvasGroup;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void FadeOutInstantly()
        {
            canvasGroup.alpha = 1;
        }

        public IEnumerator FadeOut(float time)
        {
            while (canvasGroup.alpha < 1) // while alpha not 1
            {
                canvasGroup.alpha += Time.deltaTime / time;// alpha move towards 1
                yield return null;
            }
        }

        public IEnumerator FadeIn(float time)
        {
            while (canvasGroup.alpha > 0) // while alpha not 1
            {
                canvasGroup.alpha -= Time.deltaTime / time;// alpha move towards 1
                yield return null;
            }
        }
    }
}
