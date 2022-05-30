using System.Collections;
using System.Collections.Generic;
using Attributes;
using Management;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controller
{

    public class DialogueTrigger : MonoBehaviour
    {
        [Header("Main parameters")]
        private Health _player;
        private Keyboard _kb;

        [Header("Dialogue parameters")]
        private bool _canStartDialogue;
        [SerializeField] private GameObject _exclamationMark = null;
        [SerializeField] private TextAsset _Json = null;


        private void Awake()
        {
            _player = GameObject.FindWithTag("Player").GetComponent<Health>();
            _kb = InputSystem.GetDevice<Keyboard>();
        }

        private void Start()
        {
            _canStartDialogue = false;
            _exclamationMark.SetActive(false);
        }

        private void Update()
        {
            if (_canStartDialogue)
            {
                if (_kb.fKey.isPressed)
                {
                    DialogueManager.GetDialogueManager().StartDialogue(_Json);
                }
            }
            else
            {
                _exclamationMark.SetActive(false);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            Health player = other.gameObject.GetComponent<Health>();
            if (player != _player) return;
            _canStartDialogue = true;
            _exclamationMark.SetActive(true);
        }

        private void OnTriggerExit(Collider other)
        {
            Health player = other.gameObject.GetComponent<Health>();
            if (player != _player) return;
            _canStartDialogue = false;
            _exclamationMark.SetActive(false);
        }

    }

}