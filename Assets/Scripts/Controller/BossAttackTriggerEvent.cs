using Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    public class BossAttackTriggerEvent : MonoBehaviour
    {
        [SerializeField] private Health _archer1;
        [SerializeField] private Health _archer2;
        [SerializeField] private Health _archer3;

        private BoxCollider _myCollider;

        [SerializeField] private GameObject _bossSkeletonHealthBar;

        private void Awake()
        {
            _myCollider = GetComponent<BoxCollider>();
        }

        private void Start()
        {
            _bossSkeletonHealthBar.SetActive(false);
        }

        private void Update()
        {
            Debug.Log("My collider is triggered : " + _myCollider.isTrigger);
            if(_archer1.GetIsDead() && _archer2.GetIsDead() && _archer3.GetIsDead())
            {
                _myCollider.isTrigger = true;
                _bossSkeletonHealthBar.SetActive(true);
                              
            }
        }
    }
}
