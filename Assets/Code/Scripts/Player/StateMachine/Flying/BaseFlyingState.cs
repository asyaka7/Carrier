using Assets.Code.Scripts.General.StateMachine;
using UnityEngine;

namespace Assets.Code.Scripts.Player.StateMachine.Flying
{
    internal class BaseFlyingState : IState
    {
        PlayerController playerController;
        float currentRotation = 0;

        public BaseFlyingState(PlayerController playerController) 
        {
            this.playerController = playerController;
        }

        public void Enter()
        {
            currentRotation = 0;
        }

        public void Exit()
        {
            
        }

        public void Update()
        {
            MoveUp();
            ControlDirection();
        }

        private void MoveUp()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                playerController.MoveUp(
                    playerController.movementSettings.flyUpSpeed,
                    playerController.movementSettings.accelerationTime);
            }
        }

        private void ControlDirection()
        {
            if (Input.GetKey(KeyCode.A))
            {
                RotateSlowDown();
            }

            if (Input.GetKey(KeyCode.D))
            {
                RotateToFly();
            }
        }

        private void RotateToFly()
        {
            if (currentRotation < playerController.movementSettings.maxFlyAngle)
            {
                float dAngle = playerController.ApplyRotation(
                    playerController.movementSettings.flyRotationSpeed,
                    playerController.movementSettings.angleRotationTime);
                currentRotation += dAngle;
            }
        }

        private void RotateSlowDown()
        {
            if (currentRotation > 0)
            {
                float dAngle = playerController.ApplyRotation(
                    -playerController.movementSettings.flyBackRotationSpeed,
                    playerController.movementSettings.angleRotationTime);
                currentRotation += dAngle;
            }
        }

    }
}
