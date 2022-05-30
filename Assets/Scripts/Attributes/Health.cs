using System;
using System.Collections;
using System.Collections.Generic;
using Combat;
using Controller;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Attributes
{
    public class Health : MonoBehaviour, IDamageable
    {
        [SerializeField] private float _maxHp = 100;
        private float _currentHp;
        private bool _isDead;
        public bool GetIsDead() => _isDead;

        [SerializeField] private bool _isSkeleton = false;
        [SerializeField] private GameObject Skeleton = null;
        [SerializeField] private GameObject Skeleton2 = null;

        [Header("Events")]
        [SerializeField] private DamagePopUpEvent _DamagePopUpEvent;
        [SerializeField] private UnityEvent _damageVFX;

        [Header("EnemyArcher")]
        [SerializeField] private bool _isEnemyArcher = false;
        [SerializeField] private GameObject _archersShield;
        [SerializeField] private float _healthBoostWhenDead = 30f;
       [SerializeField] private bool _isNotPlayer = true;
        private Health _player;

        private Health _bossHealth;

        private Health _myHealth;

        [System.Serializable]
        public class DamagePopUpEvent : UnityEvent<float>
        {
        }

      

        private void Awake()
        {
            _bossHealth = GameObject.FindWithTag("EnemyBoss").GetComponent<Health>();

            _myHealth = GetComponent<Health>();

            if(_isNotPlayer)
            {
                _player = GameObject.FindWithTag("Player").GetComponent<Health>();
            }
        }

        private void Start()
        {
            _isDead = false;
            _currentHp = _maxHp;
        }

        private void Update()
        {
            if(_bossHealth.GetIsDead())
            {
                if(this.GetIsDead())
                {
                    SceneManager.LoadScene(2);
                }
            }

            if(_myHealth._isDead)
            {
                SceneManager.LoadScene(2);
            }


        }

        public float GetHealthInFriction()
        {
            return _currentHp / _maxHp;
        }

        public void GetHealth(float damage)
        {
            if(_isSkeleton && _archersShield != null)
            {
                return;
            }

            if (_isDead) return;

            _currentHp = Mathf.Max(_currentHp - damage, 0);

            _DamagePopUpEvent?.Invoke(damage);
            _damageVFX?.Invoke();
            if (_currentHp == 0)
            {
                Dead();
            }
        }

        public void HealthBoos(float boosAmount)
        {
            _currentHp = Mathf.Min(_currentHp + boosAmount, _maxHp);
        }

        [SerializeField] private GameObject[] parts;
        [SerializeField] private WeaponSO _unarmed;
        private void Dead()
        {
            _isDead = true;
            if(_isNotPlayer)
            {
                _player.HealthBoos(_healthBoostWhenDead);
            }
            if (_isSkeleton)
            {
                Skeleton.SetActive(false);
                Skeleton2.SetActive(false);

                for (int i = 0; i < parts.Length; i++)
                {
                    parts[i].SetActive(true);
                }
            }
        }
    }

}