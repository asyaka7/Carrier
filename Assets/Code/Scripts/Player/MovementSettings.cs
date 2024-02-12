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

        public AudioClip crushAudio;
        public AudioClip winAudio;

        //public ParticleSystem winParticle;
        //public ParticleSystem crushParticle;

        public GameSettings(float reloadDelay)
        {
            this.reloadDelay = reloadDelay;

            this.winAudio = null;
            this.crushAudio = null;
            //this.winParticle = null;
            //this.crushParticle = null;
        }
    }
}
