using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Attribute
{
    public class DamagePopUp : MonoBehaviour
    {
        [SerializeField]  private TextMeshProUGUI _damageText;

       

        public void UpdateDamageText(float damage)
        {
            _damageText.text = string.Format("{0:0}", damage);
            Debug.Log(damage);
        }
    }
}
