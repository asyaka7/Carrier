using Assets.Code.Scripts.General.StateMachine;
using UnityEngine;

namespace Assets.Code.Scripts.Player.StateMachine
{
    internal class WalkingState : IState
    {
        PlayerController playerController;
        float backMovementFactor = 0.5f;

        public WalkingState(PlayerController playerController) 
        { 
            this.playerController = playerController;
        }
        public void Enter()
        {
            playerController.animator.SetTrigger("isWalking");
            playerController.RestorePosture();
        }

        public void Exit()
        {
        }

        public void Update()
        {
            Walk();
        }

        private void Walk()
        {
            float step = playerController.movementSettings.walkSpeed * Time.deltaTime;
            Vector3 stepDirection = new Vector3(step, 0, 0);

            if (Input.GetKey(KeyCode.A))
            {
                playerController.transform.position -= stepDirection*backMovementFactor;
            }

            if (Input.GetKey(KeyCode.D))
            {
                playerController.transform.position += stepDirection;
            }
        }
    }
}
