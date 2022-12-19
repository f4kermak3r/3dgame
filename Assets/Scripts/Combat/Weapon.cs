using System;
using UnityEngine;
using RPG.Attributes;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController animatorOverride = null;

        [SerializeField] GameObject equippedPrefab = null;

        [SerializeField] float weaponRange = 2f;

        [SerializeField] float weaponDamage = 5f;
        [SerializeField] float percentageBonus = 0;


        [SerializeField] bool isRightHanded = true;

        [SerializeField] Projectile projectile;

        const string weaponName = "Weapon";

        public void Spawn(Transform leftHand, Transform rightHand, Animator animator)
        {
            DestroyOldWeapon(leftHand, rightHand);

            if (equippedPrefab != null)
            {
                Transform handTransform = GetTransform(leftHand, rightHand);
                GameObject weapon = Instantiate(equippedPrefab, handTransform);
                weapon.name = "Weapon";
            }
            var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
            else if (overrideController != null)
            {
                {
                    animator.runtimeAnimatorController = overrideController;
                }
            }
        }

        private void DestroyOldWeapon(Transform leftHand, Transform rightHand)
        {
            Transform oldWeapon = rightHand.Find(weaponName);
            if (oldWeapon == null)
            {
                oldWeapon = leftHand.Find(weaponName);
            }
            if (oldWeapon == null) return;

            oldWeapon.name = "DESTROYING";
            Destroy(oldWeapon.gameObject);
        }

        private Transform GetTransform(Transform leftHand, Transform rightHand)
        {
            Transform handTransform;

            if (isRightHanded) handTransform = rightHand;
            else handTransform = leftHand;
            return handTransform;
        }

        public bool hasProjectile()
        {
            return projectile != null;
        }

        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target, GameObject instigator, float calculatedDamage)
        {
            Projectile projectileInstance = Instantiate(projectile, GetTransform(leftHand, rightHand).position, Quaternion.identity);
            projectileInstance.SetTarget(target, instigator, calculatedDamage);
        }

        public float GetDamage()
        {
            return weaponDamage;
        }

        public float GetPercentageBonus()
        {
            return percentageBonus;
        }

        public float GetRange()
        {
            return weaponRange;
        }

    }
}