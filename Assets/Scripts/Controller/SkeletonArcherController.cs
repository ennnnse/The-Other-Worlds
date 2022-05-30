
using Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Controller
{
    public class SkeletonArcherController : MonoBehaviour,IDamageable
    {

        [SerializeField] private Transform _target;
        [SerializeField] private Transform _myLine;
        [SerializeField] private LayerMask _SkeletonArcherMask;

        [SerializeField] private GameObject[] parts;
        [SerializeField] private GameObject Skeleton = null;
        [SerializeField] private GameObject Skeleton2 = null;
        [SerializeField] private float _health = 100;
        private bool _isDead = false;
        [SerializeField] private GameObject _archerShield;

        private void Start()
        {
            _archerShield.SetActive(true);
        }

        private void Update()
        {
            if (_isDead) return;
            BondToArcher();

            if(_isDead)
            {
                Destroy(_target.gameObject);
            }
        }

        private void BondToArcher()
        {
            Vector3 direction = (_target.position - transform.position);

            float directionZLength = direction.z;

            // looking towards target
            _myLine.rotation = Quaternion.LookRotation(direction);

            bool hasHit = Physics.Raycast(transform.position, direction, out RaycastHit hit, _SkeletonArcherMask);
            Color collor = Color.red;
            Debug.DrawRay(transform.position, direction, collor);
            if (hasHit)
            {
                Debug.Log(hit.transform.name);
                //setting the lines z scale equl to the distance btw line and target
                var scale = _myLine.localScale;
                scale.z = directionZLength;
                _myLine.localScale = scale;
            }
        }

      
        public bool GetPrayingSkeletonIsDead() => _isDead;

        public void GetHealth(float damage)
        {
            if (_isDead) return;

            _health = Mathf.Max(_health - damage, 0);

            _OnHitEvent?.Invoke();
            
            if(_health == 0 )
            {
                Dead();
            }
        }

        [Tooltip("This is to disable the line when this object is dead")]
        [SerializeField] private GameObject _myLineGameObject;

        [SerializeField] private UnityEvent _OnHitEvent;

        private void Dead()
        {
            _isDead = true;

            _archerShield.SetActive(false);
            _myLineGameObject.SetActive(false);

            Destroy(_target.gameObject);

            Skeleton.SetActive(false);
            Skeleton2.SetActive(false);

            for (int i = 0; i < parts.Length; i++)
            {
                parts[i].SetActive(true);
            }
        }
    }
}
