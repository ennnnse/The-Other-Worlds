using System.Collections;
using System.Collections.Generic;
using Attributes;
using UnityEngine;

namespace Combat
{
    [CreateAssetMenu(fileName = ("Weapon"), menuName = ("Weapon/New Weapon"))]
    public class WeaponSO : ScriptableObject
    {
        [SerializeField] private string _weaponName;
        [SerializeField] private GameObject _weapon = null;
        [SerializeField] private AnimatorOverrideController _overrideController = null;

        [Header("Other parameters")]
        [SerializeField] private float _minDamage = 0;
        [SerializeField] private float _maxDamage;
        [SerializeField] private bool _isLeftHanded = false;
        [SerializeField] private float _attackRange = 1f;
        [SerializeField] private float _timeForNextAttackk;
        [SerializeField] private Projectile _projectile;


        public void GetWeapon(Animator anim, Transform rightHand, Transform leftHand)
        {
            if (anim == null) return;
            anim.runtimeAnimatorController = _overrideController;

            if (_weapon == null) return;
            Instantiate(_weapon, GetHandTransform(rightHand, leftHand));
        }

        public void SpawnProjectile(Transform spawnPoint, Health target)
        {
          //  if (_projectile == null) return;

            Projectile instance = Instantiate<Projectile>(_projectile, spawnPoint.position,Quaternion.identity);
            instance.ProjectileSetStats(target, _minDamage);
        }

        private Transform GetHandTransform(Transform rightHand, Transform leftHand)
        {
            Transform handTransform;

            if (!_isLeftHanded)
            {
                handTransform = rightHand;
            }
            else
                handTransform = leftHand;

            return handTransform;
        }


        public float GetDamage()
        {
            float damage = Random.Range(_minDamage, _maxDamage);
            return damage;
        }
          

        public bool GetIsLeftHand() => _isLeftHanded;

        public float GetAttackRange() => _attackRange;

        public float GetTimeForAttack() => _timeForNextAttackk;
    }

}