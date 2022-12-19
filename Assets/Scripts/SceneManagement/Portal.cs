using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    enum Destination
    {
        A, B, C, D
    }

    public class Portal : MonoBehaviour
    {
        [SerializeField] int SceneToLoad = -1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] Destination destination;
        [SerializeField] float timeToFadeOut = 2f;
        [SerializeField] float timeToFadeIn = 1f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                StartCoroutine(Transition());
                DontDestroyOnLoad(this.gameObject);
            }
        }

        private IEnumerator Transition()
        {
            Fader fader = FindObjectOfType<Fader>();

            DontDestroyOnLoad(gameObject);
            yield return fader.FadeOut(timeToFadeOut);

            SavingWrapper wrapper = FindObjectOfType<SavingWrapper>();

            wrapper.Save();

            yield return SceneManager.LoadSceneAsync(SceneToLoad);

            wrapper.Load();

            Portal otherPortal = GetOtherPortal();

            UpdatePlayer(otherPortal);

            wrapper.Save();

            yield return fader.FadeIn(timeToFadeIn);


            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = otherPortal.spawnPoint.position;
            player.transform.rotation = otherPortal.spawnPoint.rotation;
            player.GetComponent<NavMeshAgent>().enabled = true;

        }

        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;

                if (portal.destination != destination) continue;

                return portal;
            }
            return null;
        }
    }
}

