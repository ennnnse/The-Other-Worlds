using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Attributes
{
    public class BossHealthBar : MonoBehaviour
    {
        [SerializeField] private RectTransform _bossHealthBar;
        private Health _bossHealth;
         
        private void Start()
        {
            _bossHealth = GameObject.FindWithTag("EnemyBoss").GetComponent<Health>();
        }

        private void Update()
        {
            _bossHealthBar.localScale = new Vector3(_bossHealth.GetHealthInFriction(), 1f, 1f);
                    }
    }
}
