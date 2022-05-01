using System;
using RPG.Attributes;
using UnityEngine;
using UnityEngine.Serialization;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [FormerlySerializedAs("animatorOverride")] //see [Tip] To avoid losing references when renaming your SerializedFields question
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [FormerlySerializedAs("equippedPrefab")]
        [SerializeField] GameObject equippedPrefab = null;
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float weaponDamage = 5f;
        [SerializeField] bool isRightHanded = true;
        [SerializeField] Projectile projectile = null;
        GameObject weaponInstance;

        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            if (equippedPrefab != null)
            {
                Transform handTransform = GetTransform(rightHand, leftHand);
                weaponInstance = Instantiate(equippedPrefab, handTransform);

            }
            
            var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController; //as is the same Cast in unreal
            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
            else if (overrideController != null)
            {
                animator.runtimeAnimatorController = overrideController.runtimeAnimatorController;
            }
        }

        public void Despawn()
        {
            Destroy(weaponInstance);
        }

        Transform GetTransform(Transform rightHand, Transform leftHand)
        {
            /*  is the same meaning as:
                    if (isRightHanded) handTransform = rightHand;
                    else handTransform = leftHand;
                    (see ?: operator (C# reference) in docs microsoft for details)*/
            return isRightHanded ? rightHand : leftHand;
        }

        public bool HasProjectile()
        {
            return projectile != null;
        }

        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target, GameObject instigator, float calculateDamage)
        {
            Projectile projectileInstance = Instantiate(projectile, GetTransform(rightHand, leftHand).position, Quaternion.identity);
            projectileInstance.SetTarget(target, instigator, calculateDamage);
        }

        public float GetDamage()
        {
            return weaponDamage;
        }

        public float GetRange()
        {
            return weaponRange;
        }
    }
}