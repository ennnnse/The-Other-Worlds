using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controller
{
    public class Interaction : MonoBehaviour
    {
        private StarterAssetsInputs _playerInput;
        private bool _hasInteract = false;
        private IInteraction _interaction;

        private void Awake()
        {
            _playerInput = GetComponent<StarterAssetsInputs>();
        }

        private void Update()
        {
            if(_hasInteract)
            {
                if(_playerInput.interaction)
                {
                    if (_interaction == null) return;
                    _interaction.Interaction();
                    _playerInput.interaction = false;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            _interaction = other.gameObject.GetComponent<IInteraction>();
            if (_interaction == null) return;
            _hasInteract = true;

        }
    }
}