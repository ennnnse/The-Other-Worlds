using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Attributes
{
    public class PlayerHpBar : MonoBehaviour
    {
        [SerializeField] private RectTransform _hpBar;
        private Health _player;

        private void Awake()
        {
            _player = GameObject.FindWithTag("Player").GetComponent<Health>();
        }

        private void Update()
        {
            _hpBar.localScale = new Vector3(_player.GetHealthInFriction(), 1f, 1f);
        }

    }

}