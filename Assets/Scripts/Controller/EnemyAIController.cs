using Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    public class EnemyAIController : MonoBehaviour
    {
        private EnemyFighter _myFighter;
        private Health _target;
        [SerializeField] private float _chaseRadius;

        [SerializeField] private bool _enemyArcher = false;
        [SerializeField] private float _sphereCastRadius = 10;
        [SerializeField] private LayerMask _allyMask;

   

        private void Awake()
        {
            _myFighter = GetComponent<EnemyFighter>();
            _target = GameObject.FindWithTag("Player").GetComponent<Health>();

        }

        private void OnEnable()
        {
            EnemyArcherEVent._StartAttackEvent += StartAttack_Event;
        }


        private void OnDisable()
        {
            EnemyArcherEVent._StartAttackEvent -= StartAttack_Event;
        }

        private void StartAttack_Event()
        {
            if (!_enemyArcher) return;
                                  
            _myFighter.GetTarget(_target);
          Debug.Log("Player has enter the trigger zone");
        }

        private void Update()
        {
            if(ChaseDistance() == true )
            {
                _myFighter.GetTarget(_target);
            }
        }
             

        private bool ChaseDistance()
        {
            float distance = Vector3.Distance(transform.position, _target.transform.position);
            return distance < _chaseRadius;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _chaseRadius);
        }
    }
}
