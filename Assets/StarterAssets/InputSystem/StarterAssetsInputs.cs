using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
    public class StarterAssetsInputs : MonoBehaviour
    {
        [Header("Character Input Values")]
        public Vector2 move;
        public Vector2 look;
        public bool jump;
        public bool sprint;
        public bool meleeAttack;
        public bool aim;
        public bool throwSword;
        public bool interaction;

        [Header("Movement Settings")]
        public bool analogMovement;

        [Header("Mouse Cursor Settings")]
        public bool cursorLocked = true;
        public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
        public void OnMove(InputValue value)
        {
            MoveInput(value.Get<Vector2>());
        }

        public void OnLook(InputValue value)
        {
            if (cursorInputForLook)
            {
                LookInput(value.Get<Vector2>());
            }
        }

        public void OnJump(InputValue value)
        {
            JumpInput(value.isPressed);
        }

        public void OnSprint(InputValue value)
        {
            SprintInput(value.isPressed);
        }

        public void OnMeleeAttack(InputValue value)
        {
            MeleeAttack(value.isPressed);
        }

        public void OnAim(InputValue value)
        {
            Aim(value.isPressed);
        }

        public void OnThrowSword(InputValue value)
        {
            ThrowSword(value.isPressed);
        }

        public void OnInteraction(InputValue value)
        {
            InteractionWithObjects(value.isPressed);
        }
#endif


        public void MoveInput(Vector2 newMoveDirection)
        {
            move = newMoveDirection;
        }

        public void LookInput(Vector2 newLookDirection)
        {
            look = newLookDirection;
        }

        public void JumpInput(bool newJumpState)
        {
            jump = newJumpState;
        }

        public void SprintInput(bool newSprintState)
        {
            sprint = newSprintState;
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            SetCursorState(cursorLocked);
        }

        private void SetCursorState(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }

        private void MeleeAttack(bool newAttackState)
        {
            meleeAttack = newAttackState;
        }

        private void Aim(bool newAimState)
        {
            aim = newAimState;
        }

        private void ThrowSword(bool newThrowState)
        {
            throwSword = newThrowState;
        }

        private void InteractionWithObjects(bool newInteractionState)
        {
            interaction = newInteractionState;
        }
    }

}