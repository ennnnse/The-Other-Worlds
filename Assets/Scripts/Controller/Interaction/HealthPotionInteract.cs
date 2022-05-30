using Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    public class HealthPotionInteract : MonoBehaviour, IInteraction
    {
        private Health _playerHealth;
        [SerializeField] private float _boosAmount = 20;

        private void Awake()
        {
            _playerHealth = GameObject.FindWithTag("Player").GetComponent<Health>();
        }

        public void Interaction()
        {
            Debug.Log("Increase player health");
            _playerHealth.HealthBoos(_boosAmount);
            Destroy(this.gameObject);
        }
    }
}

