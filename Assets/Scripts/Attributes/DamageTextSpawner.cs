using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Attribute
{
    public class DamageTextSpawner : MonoBehaviour
    {
        [SerializeField] private DamagePopUp _damagePopUp;

        public void SpawnDamageText(float damage)
        {
            DamagePopUp damagePopUp = Instantiate<DamagePopUp>(_damagePopUp, transform.parent);
            damagePopUp.UpdateDamageText(damage);
        }
    }
}
