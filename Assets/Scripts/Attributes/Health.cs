using UnityEngine;
using RPG.Core;
using RPG.Saving;
using RPG.Stats;
using System;
using GameDevTV.Utils;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        // [SerializeField] float regenerationPercentage = X; percentage to heal after level up
        LazyValue<float> health;

        bool isDead = false;

        private void Awake()
        {
            health = new LazyValue<float>(GetInitialHealth);
        }

        private float GetInitialHealth()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        private void Start()
        {
            // GetComponent<BaseStats>().levelupEvent += RegenerateHealth;
            // if (health < 0)
            // {
            //     health = GetComponent<BaseStats>().GetStat(Stat.Health);
            // }
            health.ForceInit();
        }



        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            print(gameObject.name + " took dmage: " + damage);
            health.value = Mathf.Max(health.value - damage, 0);
            print(health.value);

            if (health.value == 0)
            {
                Die();
                AwardExperience(instigator);
            }
        }

        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null) return;

            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
        }

        public void RegenerateHealth()
        {
            // float regenHealthPoints = GetComponent<BaseStats>().GetStat(Stat.Health) * (regenerationPercentage / 100);
            // health = Mathf.Max(health, regenHealthPoints);
            health.value = GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        public float GetHealth()
        {
            return health.value;
        }

        public float GetMaxHealth()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        private void Die()
        {
            if (isDead) return;

            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        public object CaptureState()
        {
            return health.value;
        }

        public void RestoreState(object state)
        {
            health.value = (float)state;

            if (health.value == 0)
            {
                Die();
            }
        }
    }
}
