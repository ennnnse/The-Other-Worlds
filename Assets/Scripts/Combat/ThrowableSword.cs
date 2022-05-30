using Attributes;
using Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class ThrowableSword : MonoBehaviour
    {
        private Rigidbody _myRig;

        private Health _nextTarget;
        [SerializeField] private float speed;
        [SerializeField] private LayerMask _archerMask;
        [SerializeField] private float _sphereCastRadius = 10f;
        bool hasHit = false;

        private PlayerAim _playerAim;
        private void Awake()
        {
            _myRig = GetComponent<Rigidbody>();
            _playerAim = GameObject.FindWithTag("Player").GetComponent<PlayerAim>();
        }

        private void Start()
        {
            _myRig.velocity = transform.forward * speed * Time.deltaTime;
            _myRig.AddTorque(transform.TransformDirection(Vector3.up) * 100f);

        }

        private void Update()
        {
         

            if (!hasHit) return;
            //cast a sphere cast when the sword hits enemy
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, _sphereCastRadius, Vector3.up, 0, _archerMask);
            foreach (RaycastHit hit in hits)
            {
                if (hits == null) continue;

                //the first hit will be the object that we hit 
                //to get the next target we want the second hit
                _nextTarget = hits[1].transform.gameObject.GetComponent<Health>();
                if (_nextTarget == null) continue;

                Transform nextTarget = hits[1].transform;
                Debug.Log(nextTarget.gameObject.name);
                //calculate the distance btw the second hit and the current swords position
                Vector3 direction = nextTarget.position - transform.position;

                RaycastHit[] secondHits = Physics.RaycastAll(transform.position, direction, _archerMask);
                foreach(RaycastHit sHit in secondHits)
                {
                    Debug.DrawRay(transform.position, direction);
                    _myRig.velocity = transform.forward * speed * Time.deltaTime;
                    _myRig.AddTorque(transform.TransformDirection(Vector3.up) * 100f);
                    transform.rotation = Quaternion.LookRotation(direction);
                }

            }

        }

        private void OnTriggerEnter(Collider other)
        {

            if(other.gameObject.CompareTag("Shield"))
            {
                _playerAim.GetSwordCounter(1);
                Destroy(this.gameObject);
            }
            if (!other.gameObject.CompareTag("EnemyArcher")) return;

            Health target = other.gameObject.GetComponent<Health>();
            target.GetHealth(300);
            hasHit = true;


        }

        

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _sphereCastRadius);
        }
    }
}
