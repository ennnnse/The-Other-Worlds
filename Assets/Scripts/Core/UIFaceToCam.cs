using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class UIFaceToCam : MonoBehaviour
    {

        private void Update()
        {
            transform.forward = Camera.main.transform.forward;
        }
    }

}