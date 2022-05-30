using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;
using Attributes;

namespace Combat
{
    public class Fighter : MonoBehaviour
    {
        [Header("Main Parameters")]
        private Animator _myAnim;
        private ThirdPersonController _playerController;

        [Header("Attack Parameters")]
        [SerializeField] private WeaponSO _weapon;
        [SerializeField] private float _timeBtwAttacks = 1f;
        private float _nextAttack;
        [SerializeField] private Transform _rightHand;
        [SerializeField] private Transform _leftHand;

        [Header("Damage Parameters")]
        [SerializeField] private Transform _attackPos;
        [SerializeField] private float _attackRadius = 2;
        [SerializeField] private LayerMask _enemyLayerMask;

        [Header("Skeleton Boss")]
        [SerializeField] private float _deadSkulls = 0;



        private void Awake()
        {
            _myAnim = GetComponent<Animator>();
            _playerController = GameObject.FindObjectOfType<ThirdPersonController>();
        }

        private void Start()
        {
            _weapon.GetWeapon(_myAnim, _rightHand, _leftHand);
        }
        

        #region Attack
        public void OnMeleeAttack(InputValue value)
        {
            if (value.isPressed)
            {
                AttackBehaviour();

            }


        }



        private void AttackBehaviour()
        {
            if (_nextAttack >= 0)
            {
                _myAnim.SetTrigger("Attack1");
                _nextAttack = _timeBtwAttacks;
            }
            else
                _nextAttack -= Time.deltaTime;
        }

        public void DealDamage()
        {
            Collider[] hits = Physics.OverlapSphere(_attackPos.position, _attackRadius, _enemyLayerMask);
            foreach (Collider hit in hits)
            {
                if (hit == null) continue;
                Projectile projectile = hit.gameObject.GetComponent<Projectile>();
                if (projectile != null)
                    if (projectile.GetKilledBySword())
                    {
                        _deadSkulls++;
                        Debug.Log(_deadSkulls);
                    }

                IDamageable target = hit.gameObject.GetComponent<IDamageable>();
                if (target == null) continue;
                target.GetHealth(_weapon.GetDamage());
            }
        }

        #endregion



        private void OnDrawGizmosSelected()
        {
            //draw a gizmos on attack transform
            Gizmos.DrawWireSphere(_attackPos.position, _attackRadius);
        }
        

    }

}