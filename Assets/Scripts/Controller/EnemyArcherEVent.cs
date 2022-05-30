using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Controller
{
    public class EnemyArcherEVent : MonoBehaviour
    {
        public static event Action _StartAttackEvent;

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                _StartAttackEvent?.Invoke();
            }    
        }
    }
}
