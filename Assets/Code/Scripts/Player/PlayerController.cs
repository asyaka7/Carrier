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

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            animator = GetComponentInChildren<Animator>();

            InitStateMachine();
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

            bool isMoving = false;

            if (Input.GetKey(KeyCode.Space))
            {
                playerStateMachine.TransitTo(PlayerAnimStateType.Fly);
                isMoving = true;
            }

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                isMoving = true;
                if (playerStateMachine.CurrentStateType != PlayerAnimStateType.Fly)
                {
                    playerStateMachine.TransitTo(PlayerAnimStateType.Walk);
                }
            }

            if (!isMoving)
            {
                Idle();
            }
        }

        private void Idle()
        {
            if (rb.velocity.magnitude == 0)
            {
                playerStateMachine.TransitTo(PlayerAnimStateType.Idle);
            }
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

            AudioPlayer.Instance.Play(GameManager.Instance.gameSettings.crushAudio);
            playerStateMachine.TransitTo(PlayerAnimStateType.Dead);
            GetComponent<PlayerController>().enabled = false;
            heart?.SetActive(false);
            body?.SetActive(false);
            rb.useGravity = false;
            PlayCrushFX();
            GameManager.Instance.GameOver();
        }

        internal void PlayCrushFX()
        {
            crushParticle?.Play();
        }

        private void Win()
        {
            //isTransitioning = true;
            //playerStateMachine.TransitTo(PlayerAnimStateType.Dead);
            LandBird();
            Invoke("PlayWinFX", 0.5f);
        }

        private void PlayWinFX()
        {
            winParticle?.Play();
            GameManager.Instance.Win();
        }

        private void LandBird()
        {
            playerStateMachine.InitState(PlayerAnimStateType.Idle);
        }

        internal void RestorePosture()
        {
            rb.freezeRotation = false;
            transform.rotation = Quaternion.identity;
            rb.freezeRotation = true;
        }
    }
}
