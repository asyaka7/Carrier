using Assets.Code.Scripts.General.StateMachine;
using System;
using UnityEngine;

namespace Assets.Code.Scripts.Player.StateMachine.Flying
{
    internal class FlyAccelerationState : IState
    {
        PlayerController playerController;
        float currentTime = 0;

        public FlyAccelerationState(PlayerController playerController)
        {
            this.playerController = playerController;
        }

        public event Action OnFinished;

        public void Enter()
        {
        }

        public void Exit()
        {
        }

        public void Update()
        {
            currentTime += Time.deltaTime;

            if (currentTime < playerController.movementSettings.accelerationTime)
            {
                MoveUp();
                //Rotate();
            }
            else
            {
                OnFinished?.Invoke();
            }
        }

        private void MoveUp()
        {
            playerController.MoveUp(
                playerController.movementSettings.startFlySpeed, 
                playerController.movementSettings.accelerationTime);
        }

        //private void Rotate()
        //{
        //    float dAngle = playerController.ApplyRotation(
        //        playerController.startFlyAngle,
        //        playerController.accelerationTime);
        //}

    }
}
