using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Attributes;
using Mover;
using Combat;

namespace Controller
{
    public class EnemyFighter : MonoBehaviour
    {
        [Header("Main parameters")]
        private Animator _anim;
        private Health _myHealth;
        private Health _target;
        private EnemyMover _myMover;

        // [Header("Attack Parameters")]
        [SerializeField] private WeaponSO _weapon;
        [SerializeField] private Transform _projectileTransform = null;
        [SerializeField] private Transform _rightHand;
        [SerializeField] private Transform _leftHand;
        [SerializeField] private float _timeSinceLastAttack = Mathf.Infinity;
        [SerializeField] private Transform _attackPos;
        [SerializeField] private LayerMask _playerMask;

       

        private void Awake()
        {
            _anim = GetComponent<Animator>();
            _myHealth = GetComponent<Health>();
            _myMover = GetComponent<EnemyMover>();

          

        }

        private void Start()
        {
            _weapon.GetWeapon(_anim, _rightHand, _leftHand);
        }

        private void Update()
        {
            _timeSinceLastAttack += Time.deltaTime;


            if (_myHealth.GetIsDead()) return;

            if (_target == null) return;
            if (_target.GetIsDead()) return;

            if (IsInAttackRange() == false)
            {
              _myMover._skeletonCanMove = true;
                                                
                if (_myMover._skeletonCanMove == true)
                {
                 _myMover.MoveTo(_target.transform);
                }
            }
            else
            {
                AttackBehaviour();
                _myMover.StopMoving();
            }
        }

        private void AttackBehaviour()
        {
            var direction = Vector3.Normalize(_target.transform.position - transform.position);
            transform.rotation = Quaternion.LookRotation(direction);
            _myMover._skeletonCanMove = false;
            if (_timeSinceLastAttack > _weapon.GetTimeForAttack())
            {
                _anim.SetTrigger("Attack");
                _timeSinceLastAttack = 0;
            }
        }

        // animation event
        public void ThrowProjectile()
        {
            if (_weapon.GetIsLeftHand())
            {
                _weapon.SpawnProjectile(_projectileTransform, _target);
            }
            else
            {
                Collider[] hits = Physics.OverlapSphere(_attackPos.position, _weapon.GetAttackRange(), _playerMask);
                foreach (Collider hit in hits)
                {
                    if (hit == null) continue;
                    IDamageable target = hit.gameObject.GetComponent<IDamageable>();
                    if (target == null) continue;
                    target.GetHealth(_weapon.GetDamage() / 2);
                }
            }
        }

        private bool IsInAttackRange()
        {
            float range = Vector3.Distance(transform.position, _target.transform.position);
            return range < _weapon.GetAttackRange();
        }

        public void GetTarget(Health target)
        {
            this._target = target;
        }
    }

}