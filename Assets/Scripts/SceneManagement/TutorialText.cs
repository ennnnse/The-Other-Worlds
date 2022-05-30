using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialText : MonoBehaviour
{

    private Keyboard _kb;
    [SerializeField] private GameObject _tutorialGameObject;

    private void Awake()
    {
        _kb = InputSystem.GetDevice<Keyboard>();
    }

    private void Start()
    {
        _tutorialGameObject.SetActive(false);
    }

    private void Update()
    {
        if (_tutorialGameObject == null) return; 
        if(_kb.pKey.isPressed)
        {
            _tutorialGameObject.SetActive(true);
        }
        if(_kb.oKey.isPressed)
        {
            _tutorialGameObject.SetActive(false);
                    }
    }
}
