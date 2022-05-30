using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using TMPro;

namespace Controller
 {
    public class PlayerAim : MonoBehaviour
    {
        [Header("Main parameters")]
        [SerializeField] private CinemachineVirtualCamera _aimCamera;
        private StarterAssetsInputs _playerInput;
        private ThirdPersonController _playerController;
        [SerializeField] private LayerMask _toAimMask;
        private Animator _myAnim;

        [Header("Instantiating")]
        [SerializeField] private GameObject _sword;
        [SerializeField] private Transform _throwPosition;

        [Header("Throwable  Sword parameters")]
        [SerializeField] private float _throwedSwordCounter =  1;
        public float GetSwordCounter(float increment) => _throwedSwordCounter = increment;
        [SerializeField] private TextMeshProUGUI _swordCounterText;

        [SerializeField] private GameObject _test;

        private void Awake()
        {
            _playerInput = GetComponent<StarterAssetsInputs>();
            _playerController = GetComponent<ThirdPersonController>();
            _myAnim = GetComponent<Animator>();
        }

        private void Start()
        {
            _aimCamera.gameObject.SetActive(false);
        }


        private bool _noMoreSwordLeft = false;
        private void Update()
        {
            _swordCounterText.text = _throwedSwordCounter.ToString();


            //casting ray in the middle point of the screen
            Vector3 mouseWorldPos = Vector3.zero;
            Vector3 screenMiddlePoint = new Vector3(Screen.width / 2, Screen.height / 2);

            Ray ray = Camera.main.ScreenPointToRay(screenMiddlePoint);
            RaycastHit hit;
            //checking if the ray hits the middle point
            if(Physics.Raycast(ray,out hit,999,_toAimMask))
            {
                mouseWorldPos = hit.point;

                if (_test == null) return;
                _test.transform.position = hit.point;
            }

            //players aim input
            if(_playerInput.aim)
            {
                //camera parameters
                _aimCamera.gameObject.SetActive(true);
                _aimCamera.m_Lens.FieldOfView = 25f;

                //player controller parameters
                _playerController.GetSensivity(.5f);
//                _playerController.PlayerCannotMove();

                //the player cannot move on y aixs
                Vector3 mousePos = mouseWorldPos;
                mousePos.y = transform.position.y;

                //the direction btw player and mouse position
                Vector3 aimDirection = Vector3.Normalize(mousePos - transform.position);

                _playerController.PlayerCannotMove();

                //rotating the character based on  mouse position
                transform.forward = Vector3.Lerp(transform.forward, aimDirection, 20 * Time.deltaTime);
                _myAnim.SetLayerWeight(1,1f);

                //instantiane the sword 
                if(_playerInput.throwSword)
                {
                    //direction btw throwposition and mouse posiiton 
                    Vector3 throwAimDir = Vector3.Normalize(mouseWorldPos - _throwPosition.position);
                    if (_throwedSwordCounter == 0)
                    {
                        _noMoreSwordLeft = true;
                    }
                    else
                        _noMoreSwordLeft = false;

                    if(_noMoreSwordLeft == true)                 
                    return;
                   
                    
                    Instantiate(_sword, _throwPosition.position, Quaternion.LookRotation(throwAimDir));
                   _throwedSwordCounter--;
                    _swordCounterText.text = _throwedSwordCounter.ToString();
                    _playerInput.throwSword = false;
                }

            }
            else
            {
                //camera parameters
                _aimCamera.gameObject.SetActive(false);
                _aimCamera.m_Lens.FieldOfView = 45f;
                //     _playerController.PlayerCanMove();

                _playerController.PlayerCanMove();

                //player controller parameters
                _playerController.GetSensivity(1f);


                _myAnim.SetLayerWeight(1, 0f);

            }
        }

    }
}

