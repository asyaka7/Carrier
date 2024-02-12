using System;
using UnityEngine;

namespace Assets.Code.Scripts
{
    [Serializable]
    public struct MovementSettings
    {
        public float walkSpeed;

        public float startFlySpeed;
        public float flyUpSpeed;
        public float flyRotationSpeed;
        public float flyBackRotationSpeed;
        public float maxFlyAngle;

        public float accelerationTime;
        public float angleRotationTime;

    }

    [Serializable]
    internal struct GameSettings
    {
        public float reloadDelay;
        public float deathDuration;

        public AudioClip crushAudio;
        public AudioClip winAudio;
    }
}
