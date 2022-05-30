using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Attributes
{
    public class SkeletonBossHpController : MonoBehaviour
    {
        private Health myHelath;
        [SerializeField] private GameObject[] archer;

        private void Awake()
        {
            myHelath = GetComponent<Health>();
        }

       
        private void Update()
        {
            if(myHelath.GetIsDead())
            {
                for(int i = 0;i <archer.Length;i++)
                {
                    archer[i].SetActive(true);
                }
            }
            
        }
    }
}
