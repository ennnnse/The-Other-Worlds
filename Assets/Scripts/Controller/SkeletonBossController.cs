using Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Controller
{
    public class SkeletonBossController : MonoBehaviour
    {
        [SerializeField] private float _sphereCast;
         private Health[] _archers;
        private bool _canChase = false;

        public bool GetBossCanChase() => _canChase;
        private void Awake()
        {
            _archers = GameObject.FindWithTag("EnemyArcher").GetComponents<Health>();
       }

 

        private void Update()
        {
           for(int i = 0; i < _archers.Length; i++)
           {
                if(_archers[i].GetIsDead())
                {
                    Debug.Log("oll bre mo");
                    _canChase = true;
                }
           }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, _sphereCast);
        }
    }

}
