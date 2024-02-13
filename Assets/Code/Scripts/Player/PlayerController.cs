using Assets.Code.Scripts.Player.StateMachine;
using UnityEngine;

namespace Assets.Code.Scripts.Player
{
    public enum PlayerStateType { Live, Dead };
    public class PlayerController : MonoBehaviour
    {
        [InspectorName("Movement")]
        [SerializeField]
        public MovementSettings movementSettings = new MovementSettings()
        {
            walkSpeed = 0.8f,
            startFlySpeed = 30f,
            flyUpSpeed = 10f,
            flyRotationSpeed = 40f,
            flyBackRotationSpeed = 30f,
            maxFlyAngle = 40f,
            accelerationTime = 0.1f,
            angleRotationTime = 0.3f,
            
        };

        [SerializeField]
        internal AudioClip flapAudio;

        // todo: move to game manager
        [SerializeField]
        internal ParticleSystem crushParticle;
        [SerializeField]
        internal ParticleSystem winParticle;

        [SerializeField] GameObject heart;
        [SerializeField] GameObject body;

        PlayerStateMachine playerStateMachine;

        internal Rigidbody rb;
        internal Animator animator;

        bool isTransitioning = false;
        bool collisionEnabled = true;
        bool isUserControlled = false;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            animator = GetComponentInChildren<Animator>();

            InitStateMachine();
            GameManager.Instance.PlayerIsDead += PlayerIsDead;
        }

        private void PlayerIsDead()
        {
            PlayDeathFX();
        }

        private void InitStateMachine()
        {
            var playerStateFabric = new PlayerStateFabric(this);
            playerStateMachine = new PlayerStateMachine(playerStateFabric);
            playerStateMachine.InitState(PlayerAnimStateType.Idle);
        }


        void Update()
        {
            UpdateState();

            playerStateMachine.Update();
        }

        private void UpdateState()
        {
            if (isTransitioning) return;

            isUserControlled = false;
            if (Input.GetKey(KeyCode.Space))
            {
                playerStateMachine.TransitTo(PlayerAnimStateType.Fly);
                isUserControlled = true;
            }

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                isUserControlled = true;
                if (playerStateMachine.CurrentStateType != PlayerAnimStateType.Fly)
                {
                    playerStateMachine.TransitTo(PlayerAnimStateType.Walk);
                }
            }

            Idle();
        }

        private void Idle()
        {
            if (!IsInMove())
            {
                playerStateMachine.TransitTo(PlayerAnimStateType.Idle);
            }
        }

        private bool IsInMove()
        {
            bool isMoving = isUserControlled || rb.velocity.magnitude != 0;
            return isMoving;
        }

        internal void MoveUp(float speed, float accelerationTime)
        {
            var saveConstraints = rb.constraints;
            rb.freezeRotation = true;

            Vector3 upForce = Vector3.up * speed / accelerationTime * Time.deltaTime;
            rb.AddRelativeForce(upForce);

            //Debug.Log($"UP: {upForce}");
            rb.freezeRotation = false;
            rb.constraints = saveConstraints;
        }

        internal float ApplyRotation(float speed, float angleRotationTime)
        {
            float dAngle = speed / angleRotationTime * Time.deltaTime;
            Vector3 fwd = Vector3.back * dAngle;
            //Debug.Log($"D: {fwd}");
            transform.Rotate(fwd);
            return dAngle;
        }

        // collision
        private void OnCollisionEnter(Collision collision)
        {
            if (isTransitioning || !collisionEnabled) { return; }

            switch (collision.gameObject.tag)
            {
                case "Deadly":
                    Kill();
                    break;
                case "Finish":
                    Win();
                    break;
                case "Ground":
                    LandBird();
                    break;
            }
        }

        private void Kill()
        {
            isTransitioning = true;
            GameManager.Instance.GameOver();
        }

        internal void PlayDeathFX()
        {
            AudioPlayer.Instance.Play(GameManager.Instance.gameSettings.crushAudio);
            playerStateMachine.TransitTo(PlayerAnimStateType.Dead);
            TurnOffUserControl();
            HideBody();
            PlayCrushFX();
        }

        private void HideBody()
        {
            heart?.SetActive(false);
            body?.SetActive(false);
            rb.useGravity = false;
        }

        private void TurnOffUserControl()
        {
            GetComponent<PlayerController>().enabled = false;
        }

        internal void PlayCrushFX()
        {
            crushParticle?.Play();
        }

        private void Win()
        {
            //isTransitioning = true;
            LandBird();
            TurnOffUserControl();
            Invoke("PlayWinFX", 0.5f);
        }

        private void PlayWinFX()
        {
            winParticle?.Play();
            GameManager.Instance.Win();
        }

        private void LandBird()
        {
            if (!isUserControlled)
            {
                playerStateMachine.TransitTo(PlayerAnimStateType.Idle);
            }
            else
            {
                playerStateMachine.TransitTo(PlayerAnimStateType.Walk);
            }
        }

        internal void RestorePosture()
        {
            rb.freezeRotation = false;
            transform.rotation = Quaternion.identity;
            rb.freezeRotation = true;
        }

        private void OnDestroy()
        {
            GameManager.Instance.PlayerIsDead -= PlayerIsDead;
        }
    }
}
