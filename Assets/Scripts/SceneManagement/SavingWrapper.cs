using UnityEngine;
using RPG.Saving;
using System.Collections;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        const string saveFile = "save";
        [SerializeField] float fadeInTime = 1.5f;

        private void Awake()
        {
            StartCoroutine(LoadLastScene());
        }

        private IEnumerator LoadLastScene()
        {

            yield return GetComponent<SavingSystem>().LoadLastScene(saveFile);
            Fader fader = FindObjectOfType<Fader>();

            fader.FadeOutInstantly();

            yield return fader.FadeIn(fadeInTime);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }

            if (Input.GetKeyDown(KeyCode.Delete))
            {
                Delete();
                Debug.Log("save file " + saveFile + " was deleted");
            }
        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(saveFile);
        }

        public void Load()
        {
            GetComponent<SavingSystem>().Load(saveFile);
        }

        public void Delete()
        {
            GetComponent<SavingSystem>().Delete(saveFile);
        }
    }
}

