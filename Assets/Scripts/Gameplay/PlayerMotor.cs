using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Osmose.Gameplay
{
    public class PlayerMotor : NetworkBehaviour
    {
        [SerializeField] private Transform playerCamera = null;
        CharacterController controller = null;

        private Vector3 velocity;
        private Vector3 rotation;
        private Vector3 cameraRotation;

        // Reglages vitesse
        [SerializeField] float walkSpeed = 4.0f;
        [SerializeField] float runSpeed = 7.0f;

        // Variables gestion gravite
        float gravity = -13.0f;
        float velocityY = 0.0f;

        // Variables saut
        [SerializeField] private AnimationCurve jumpFallOff;
        [SerializeField] private float jumpMultiplier = 8.0f;
        bool isJumping;
        float timeBfJumping = 0.0f;
        float timeBfMoving = 0.0f;

        // Animations 
        Animator animator;
        int isWalkingHash;
        int isRunningHash;
        int isStoppingHash;
        int isJumpingHash;

        //Audios
        [SerializeField] private AudioClip audioJump = null;
        [SerializeField] private AudioClip audioRun = null;
        [SerializeField] private AudioClip audioWalk = null;
        [SerializeField] private AudioClip audioChest = null;
        [SerializeField] private AudioClip audioPoints = null;
        [SerializeField] private AudioClip audioPraise = null;
        [SerializeField] private AudioClip audioBase = null;
        [SerializeField] private AudioClip audioAttack = null;
        [SerializeField] private AudioClip audioAttackNull = null;
        [SerializeField] private AudioClip shilili = null;
        private AudioSource controller_AudioSource;

        bool isMovementPressed;
        bool isRunPressed;
        
        private void Start()
        {
            controller = GetComponent<CharacterController>();
            animator = GetComponentInChildren<Animator>();
            controller_AudioSource = GetComponent<AudioSource>();
           
            isWalkingHash = Animator.StringToHash("isWalking");
            isRunningHash = Animator.StringToHash("isRunning");
            isStoppingHash = Animator.StringToHash("isStopping");
            isJumpingHash = Animator.StringToHash("isJumping");
        }

        [Command]
        private void update_inputs(bool move_pressed, bool run_pressed){
            update_inputs_clients(move_pressed,run_pressed);
        }

        [ClientRpc]
        private void update_inputs_clients(bool move_pressed, bool run_pressed){
            this.isMovementPressed = move_pressed;
            this.isRunPressed = run_pressed;
        }

        void handleAnimation()
        {           
            bool isWalking = animator.GetBool(isWalkingHash);
            bool isRunning = animator.GetBool(isRunningHash);
            bool isStopping = animator.GetBool(isStoppingHash);

            // Commence a marcher : mouvement appuye, avant il ne marchait pas
            if (isMovementPressed && !isWalking)
            {
                animator.SetBool(isWalkingHash, true);
                controller_AudioSource.Stop();
                controller_AudioSource.clip = audioWalk;
                controller_AudioSource.Play();
            }
            // S'arrete de marcher : mouvement pas appuye, avant il marchait
            else if (!isMovementPressed && isWalking)
            {
                animator.SetBool(isWalkingHash, false);
                controller_AudioSource.Stop();
            }
            // Commence a courir : mouvement et courir appuye, avant il ne courrait pas
            else if ((isMovementPressed && isRunPressed) && !isRunning)
            {
                animator.SetBool(isRunningHash, true);
            }
            // S'arrete de courir : mouvement et courir pas appuye, avant il courrait
            else if (!isMovementPressed && !isRunPressed && isRunning)
            {
                animator.SetBool(isRunningHash, false);
                animator.Play("Base Layer.Run To Stop");
                controller_AudioSource.Stop();
            }
            // S'arrete de courir, commence a marcher : mouvement appuye et courir pas appuye, avant il courrait
            else if (!isRunPressed && isRunning)
            {
                animator.SetBool(isRunningHash, false);
                controller_AudioSource.Stop();
                controller_AudioSource.clip = audioWalk;
                controller_AudioSource.Play();
            }

        }


        // Appele par PlayerController quand on appuie sur "Horizontal" ou "Vertical"
        public void Move(Vector3 _velocity)
        {
            isMovementPressed = _velocity.x != 0 || _velocity.z != 0;
            velocity = _velocity;
        }

        // Appele par PlayerController quand on appuie sur "Horizontal" ou "Vertical"
        public void Move(Vector2 _direction, float _isRunning)
        {
            // Si il n'est pas bloque par une animation ouverture coffre, combats... 
            // Il peut bouger donc on calcule sa velocite
            if (timeBfMoving <= 0
                &&!animator.GetCurrentAnimatorStateInfo(0).IsName("OpenChest")
                && !animator.GetCurrentAnimatorStateInfo(0).IsName("Challenge")
                && !animator.GetCurrentAnimatorStateInfo(0).IsName("Run To Stop"))
            {
                // Affectation des variables
                bool moving = _direction.x != 0 || _direction.y != 0;
                bool running = _isRunning == 1;

                if (isLocalPlayer) update_inputs(moving, running);

                if (isRunPressed)
                {
                    animator.SetBool(isRunningHash, true);
                    if (controller_AudioSource.clip != audioRun)
                    {
                        controller_AudioSource.Stop();
                        controller_AudioSource.clip = audioRun;
                        controller_AudioSource.Play();
                    }
                }

                // Application de la gravite
                if (controller.isGrounded) velocityY = 0.0f;
                velocityY += gravity * Time.deltaTime;

                // Calcul de la velocite 
                velocity = (transform.forward * _direction.x + transform.right * _direction.y) * (walkSpeed + runSpeed * _isRunning) + Vector3.up * velocityY;
            }
            else
            {
                velocity = Vector3.zero;
            }
        }

        // Appele par PlayerController quand on bouge la souris droite-gauche 
        public void Rotate(Vector3 _rotation)
        {
            rotation = _rotation;
        }

        // Appele par PlayerController quand on bouge la souris haut-bas  
        public void RotateCamera(Vector3 _cameraRotation)
        {
            cameraRotation = _cameraRotation;
        }

        // Appele par PlayerController quand on appuie sur "Jump"
        public void Jump()
        {
            if (timeBfMoving <= 0
                && !animator.GetCurrentAnimatorStateInfo(0).IsName("OpenChest")
                && !animator.GetCurrentAnimatorStateInfo(0).IsName("Praise")
                && !animator.GetCurrentAnimatorStateInfo(0).IsName("Challenge"))
            {
                if (isMovementPressed && !isJumping && timeBfJumping <= 0)
                {
                    jumpMultiplier = 8.0f;
                    isJumping = true;
                    animator.Play("Base Layer.Jump");
                    animator.SetBool(isJumpingHash, true);
                    controller_AudioSource.PlayOneShot(audioJump);
                    StartCoroutine(JumpEvent());
                }
                else if (!isMovementPressed && !isJumping && timeBfJumping <= 0)
                {
                    isJumping = true;
                    int jumpRand = Random.Range(0, 10);
                    if (jumpRand == 3)
                    {
                        jumpMultiplier = 2.0f;
                        animator.Play("Base Layer.Still Jump");
                        controller_AudioSource.PlayOneShot(shilili);
                        controller_AudioSource.PlayOneShot(audioJump);
                    }
                    else
                    {
                        jumpMultiplier = 5.0f; 
                        animator.Play("Base Layer.Jump Still");
                        controller_AudioSource.PlayOneShot(audioJump);
                    }
                    StartCoroutine(JumpEvent());
                }
            }
        }

        // Appele par PlayerController quand on appuie sur "OpenChest" (f)
        public void OpenChest()
        {
            if (controller.isGrounded && timeBfMoving <= 0
                && !animator.GetCurrentAnimatorStateInfo(0).IsName("Praise")
                && !animator.GetCurrentAnimatorStateInfo(0).IsName("Challenge"))
            {
                animator.Play("Base Layer.OpenChest");
                GetComponent<AttackHitboxHandler>().OnOpenChest();
            }
        }

        public void playChest()
        {
            controller_AudioSource.PlayOneShot(audioChest);
            controller_AudioSource.clip = audioPoints;
            controller_AudioSource.PlayDelayed(audioChest.length + 0.5f);
        }

        public void playPoints()
        {
            controller_AudioSource.PlayOneShot(audioPoints);
        }

        public void playBase()
        {
            controller_AudioSource.PlayOneShot(audioBase);
        }

        public void playEnnemyBase()
        {
            controller_AudioSource.PlayOneShot(audioBase);
            controller_AudioSource.clip = audioPoints;
            controller_AudioSource.PlayDelayed(audioBase.length + 0.3f);
        }

        public void playAttack()
        {
            controller_AudioSource.PlayOneShot(audioAttack);
        }

        public void playAttackNull()
        {
            controller_AudioSource.PlayOneShot(audioAttackNull);
        }

        public void cantMoveAttackNull()
        {
            timeBfMoving = 5.0f;
        }


        // Appele par PlayerController quand on appuie sur "Attack" (clic gauche)
        public void Attack()
        {
            if (controller.isGrounded && timeBfMoving <= 0
                && !animator.GetCurrentAnimatorStateInfo(0).IsName("OpenChest")
                && !animator.GetCurrentAnimatorStateInfo(0).IsName("Praise"))
            {
                GetComponent<AttackHitboxHandler>().OnAttack();
                animator.Play("Base Layer.Challenge");
            }
        }

        // Appele par PlayerController quand on appuie sur "AttackBase" (e)
        public void AttackBase()
        {
            if (controller.isGrounded && timeBfMoving <= 0
                && !animator.GetCurrentAnimatorStateInfo(0).IsName("OpenChest")
                && !animator.GetCurrentAnimatorStateInfo(0).IsName("Praise"))
            {
                GetComponent<AttackHitboxHandler>().OnAttackBase();
                animator.Play("Base Layer.Challenge");
            }
        }

        // Appele par PlayerController quand on appuie sur "Associate" (a)
        public void Associate()
        {
            if (controller.isGrounded && timeBfMoving <= 0
                && !animator.GetCurrentAnimatorStateInfo(0).IsName("OpenChest")
                && !animator.GetCurrentAnimatorStateInfo(0).IsName("Challenge")
                && !animator.GetCurrentAnimatorStateInfo(0).IsName("Praise"))
            {
                GetComponent<AttackHitboxHandler>().OnAssociate();
            }
        }

        // Appele par PlayerController quand on appuie sur "Praise"
        public void Praise()
        {
            if (controller.isGrounded && timeBfMoving <= 0
                && !animator.GetCurrentAnimatorStateInfo(0).IsName("Challenge")
                && !animator.GetCurrentAnimatorStateInfo(0).IsName("OpenChest"))
            {
                if (controller_AudioSource != audioPraise)
                {
                    controller_AudioSource.Stop();
                    controller_AudioSource.clip = audioPraise;
                    controller_AudioSource.Play();
                }
                animator.Play("Base Layer.Praise");
            }
        }

        // Update physique ?
        private void FixedUpdate()
        {
            PerformMovement();
            PerformRotation();
        }

        // Update continu des animations 
        private void Update()
        {
            timeBfJumping -= Time.deltaTime;
            timeBfMoving -= Time.deltaTime;

            handleAnimation();
        }

        // Mouvements lies a la souris, rotation haut-bas droite-gauche
        private void PerformRotation()
        {
            if (timeBfMoving <= 0
                && !animator.GetCurrentAnimatorStateInfo(0).IsName("OpenChest")
                && !animator.GetCurrentAnimatorStateInfo(0).IsName("Praise")
                && !animator.GetCurrentAnimatorStateInfo(0).IsName("Challenge"))
            {
                transform.Rotate(rotation);
            }
            playerCamera.localEulerAngles = cameraRotation;

        }

        // Mouvements lies au clavier, deplacement devant-derriere droite-gauche
        private void PerformMovement()
        {
            if (velocity != Vector3.zero)
            {
                controller.Move(velocity * Time.deltaTime);
            }
        }

        // Controle du saut
        IEnumerator JumpEvent()
        {
            controller.slopeLimit = 90.0f;
            float timeInAir = 0.0f;

            do
            {
                float jumpForce = jumpFallOff.Evaluate(timeInAir);
                controller.Move(Vector3.up * jumpForce * jumpMultiplier * Time.deltaTime);
                timeInAir += Time.deltaTime;
                yield return null;
            } while (!controller.isGrounded && controller.collisionFlags != CollisionFlags.Above);

            controller.slopeLimit = 45.0f;
            isJumping = false;
            animator.SetBool(isJumpingHash, false);

            timeBfJumping = 0.3f;
        }

    }
}