using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Ink.Runtime;

namespace Management
{
    public class DialogueManager : MonoBehaviour
    {
        //singelton
        public static DialogueManager instance;

        private Keyboard _kb;

        [Header("UI")]
        [SerializeField] private GameObject _panel;
        [SerializeField] private TextMeshProUGUI _textField;

        private bool _dialogueHasStart;
        private Story _currentStory;

        private void Awake()
        {
            instance = this;
            _kb = InputSystem.GetDevice<Keyboard>();
        }

        public static DialogueManager GetDialogueManager()
        {
            return instance;
        }

        private void Start()
        {
            _panel.SetActive(false);
            _dialogueHasStart = false;
        }

        private float _timeBtwLines = 0.002f;
        private float _timeForNextLine;
        private void Update()
        {
            if (!_dialogueHasStart) return;

            if (_kb.bKey.wasReleasedThisFrame)
            {
                if (_timeForNextLine >= 0)
                {
                    //continue to the next line
                    ContinueDialogue();
                    _timeForNextLine = _timeBtwLines;
                }
                else
                {
                    _timeForNextLine -= Time.deltaTime;
                }
            }
        }

        public void StartDialogue(TextAsset json)
        {
            _currentStory = new Story(json.text);
            _dialogueHasStart = true;

            _panel.SetActive(true);

            ContinueDialogue();
        }

        private void EndDialogue()
        {
            _dialogueHasStart = false;
            _panel.SetActive(false);
            _textField.text = "";
        }

        private void ContinueDialogue()
        {
            if (_currentStory.canContinue)
            {
                _textField.text = _currentStory.Continue();
            }
            else
            {
                EndDialogue();
            }
        }
    }

}