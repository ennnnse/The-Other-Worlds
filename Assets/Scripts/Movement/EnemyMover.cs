using System;
using System.Collections;
using System.Collections.Generic;
using Attributes;
using UnityEngine;
using UnityEngine.AI;

namespace Mover
{
    public class EnemyMover : MonoBehaviour
    {
        private Animator _myAnim;
        private Health _myHealth;
        private NavMeshAgent _myAgent;
        private CapsuleCollider _myCollider;
        public bool _skeletonCanMove = true;


        private void Awake()
        {
            _myAnim = GetComponent<Animator>();
            _myHealth = GetComponent<Health>();
            _myAgent = GetComponent<NavMeshAgent>();
            _myCollider = GetComponent<CapsuleCollider>();
        }

        private void Update()
        {
            _myCollider.enabled = !_myHealth.GetIsDead();

            UpdateAnimations();
        }

        public void MoveTo(Transform direction)
        {
            StartMoving();
            _myAgent.destination = direction.position;
        }

        public void StopMoving()
        {
            _skeletonCanMove = false;
            _myAgent.isStopped = true;
        }

        public void StartMoving()
        {
            _skeletonCanMove = true;
            _myAgent.isStopped = false ;

        }

        private void UpdateAnimations()
        {
            Vector3 getVelocity = _myAgent.velocity;
            Vector3 setVelocity = transform.InverseTransformDirection(getVelocity);
            float speed = setVelocity.z;
            _myAnim.SetFloat("Movement", speed);
        }
    }

}