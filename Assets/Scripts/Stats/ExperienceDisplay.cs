using UnityEngine;
using UnityEngine.UI;
using System;

namespace RPG.Stats
{
    public class ExperienceDisplay : MonoBehaviour
    {
        Experience experience;

        private void Awake()
        {
            experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
        }

        private void Update()
        {
            GetComponent<Text>().text = String.Format("{0:0}/{1:0}", experience.GetExperience(), experience.GetMaxExperience());
        }
    }
}

