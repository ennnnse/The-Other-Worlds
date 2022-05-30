using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Attributes
{
    public class EnemyHpBar : MonoBehaviour
    {
        private Health _parentHealth;
        [SerializeField] private RectTransform _hpBar;
        [SerializeField] private RectTransform _damageBar;
        [SerializeField] float _durationForDamageBar = 1f;
        private float _timeBtwHits = Mathf.Infinity;

        private void Awake()
        {
            _parentHealth = GetComponentInParent<Health>();
        }

        private void Start()
        {
            _damageBar.localScale = new Vector3(_hpBar.localScale.x, 1f, 1f);
       }


        private void Update()
        {
            _timeBtwHits += Time.deltaTime;


            if (_parentHealth.GetIsDead())
                Destroy(gameObject);

          HpAndDamageBar();
        }

        private void HpAndDamageBar()
        {
            _hpBar.localScale = new Vector3(_parentHealth.GetHealthInFriction(), 1f, 1f);

            if (_timeBtwHits > _durationForDamageBar)
            {
                _damageBar.localScale = new Vector3(_hpBar.localScale.x, 1f, 1f);
                _timeBtwHits = 0;
            }

        }
    }

}