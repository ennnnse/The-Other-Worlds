using System.Collections;
using System.Collections.Generic;
using Attributes;
using UnityEngine;
using UnityEngine.Events;

namespace Combat
{
    public class Projectile : MonoBehaviour, IDamageable
    {
        private Health _target;
        private float _damage;
        private Rigidbody _myRig;

        [SerializeField] private float _speed = 5f;
        [SerializeField] private GameObject _hitVFX;
        [SerializeField] private GameObject _whiteHitVFX;
        private Health _parentHealth;

        private bool _hasBeenHit = false;

        private Vector3 _direction;

        [SerializeField] private bool _isTheBoos = false;

        private void Awake()
        {
            _myRig = GetComponent<Rigidbody>();

            if (_isTheBoos == false) return;
            _parentHealth = GameObject.FindWithTag("EnemyBoss").GetComponent<Health>();
        }

        private void Start()
        {
            
        }

        private void Update()
        {
            //this is for normal skeleton projectile
         transform.LookAt(MiddlePointOfTarget(_target));
            transform.Translate(transform.forward * _speed * Time.deltaTime);

            if (_isTheBoos == false) return;

            //this one is for the boss projectile
            _direction = Vector3.Normalize(_parentHealth.transform.position - transform.position);
            if (_hasBeenHit == false)
            {
                transform.LookAt(MiddlePointOfTarget(_target));
                transform.Translate(transform.forward * _speed * Time.deltaTime);
            }
            else
            {
                transform.LookAt(MiddlePointOfTarget(_parentHealth));
                transform.Translate(transform.forward * _speed * Time.deltaTime);
            }

            Debug.DrawLine(transform.position, _direction, Color.blue);

        }

        public void ProjectileSetStats(Health target, float damage)
        {
            this._target = target;
            this._damage = damage;
        }


        private Vector3 MiddlePointOfTarget(Health target)
        {
            CapsuleCollider targetCollider = target.gameObject.GetComponent<CapsuleCollider>();
            if (targetCollider == null)
            {
                return target.transform.position;
            }

            return target.transform.position + Vector3.up * targetCollider.height / 4f;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (_hasBeenHit) return;
                _target.GetHealth(_damage);
                Debug.Log(other.gameObject.name);
                Instantiate(_hitVFX, MiddlePointOfTarget(_target), Quaternion.identity);
                Destroy(gameObject);

            }

            if (other.gameObject.CompareTag("EnemyBoss"))
            {
                Destroy(gameObject);
            }
        }


        bool _killedBySword = true;
        public bool GetKilledBySword() => _killedBySword;

        public void GetHealth(float damage)
        {
            Destroy(gameObject);

            if (_isTheBoos)
            { 
            Debug.Log("projectile has been destroyed by sword");
            _killedBySword = true;
            _hasBeenHit = true;
            }

            Instantiate(_whiteHitVFX, transform.position, Quaternion.identity);
        }
    }

}