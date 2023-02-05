using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Osmose.Game;

namespace Osmose.Gameplay
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
    float timeBfJumping;

    // Animations 
    public Animator animator;
    int isWalkingHash;
    int isRunningHash;
    int isStoppingHash;
    int isJumpingHash;

    //Audios
    [SerializeField] private AudioClip audioJump = null;
    [SerializeField] private AudioClip audioRun = null;
    [SerializeField] private AudioClip audioWalk = null;
    private AudioSource controller_AudioSource;


    // A commenter car utilisation de animator.Play("...");
    int isDeadHash;
    int isOpeningChestHash;
    int isChallengingHash;
    ///////////////////////////////////////////////////////

    bool isMovementPressed;
    bool isRunPressed;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        controller_AudioSource = GetComponent<AudioSource>();
    public class PlayerMotor : MonoBehaviour
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
        float timeBfJumping;

        // Animations 
        Animator animator;
        int isWalkingHash;
        int isRunningHash;
        int isStoppingHash;
        int isJumpingHash;

        bool isMovementPressed;
        bool isRunPressed;


        private void Start()
        {
            controller = GetComponent<CharacterController>();
            animator = GetComponentInChildren<Animator>();

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

        }
        // S'arrete de courir, commence a marcher : mouvement appuye et courir pas appuye, avant il courrait
        else if (!isRunPressed && isRunning)
        {
            animator.SetBool(isRunningHash, false);
            controller_AudioSource.Stop();
            controller_AudioSource.clip = audioWalk;
            controller_AudioSource.Play();
            isWalkingHash = Animator.StringToHash("isWalking");
            isRunningHash = Animator.StringToHash("isRunning");
            isStoppingHash = Animator.StringToHash("isStopping");
            isJumpingHash = Animator.StringToHash("isJumping");
        }


        void handleAnimation()
        {
            bool isWalking = animator.GetBool(isWalkingHash);
            bool isRunning = animator.GetBool(isRunningHash);
            bool isStopping = animator.GetBool(isStoppingHash);

            if (isRunPressed)
            {
                animator.SetBool(isRunningHash, true);
                
                if(controller_AudioSource.clip != audioRun)
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
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("OpenChest"))
        {
            if (isMovementPressed && !isJumping && timeBfJumping <= 0)
            {
                isJumping = true;
                animator.Play("Base Layer.Jump");
                animator.SetBool(isJumpingHash, true);
                controller_AudioSource.PlayOneShot(audioJump);
                StartCoroutine(JumpEvent());
                
            // Commence a marcher : mouvement appuye, avant il ne marchait pas
            if (isMovementPressed && !isWalking)
            {
                animator.SetBool(isWalkingHash, true);
            }
            // S'arrete de marcher : mouvement pas appuye, avant il marchait
            else if (!isMovementPressed && isWalking)
            {
                isJumping = true;
                animator.Play("Base Layer.Jump Still");
                controller_AudioSource.PlayOneShot(audioJump);
                //animator.SetBool(isJumpingHash, true);
                StartCoroutine(JumpEvent());
                animator.SetBool(isWalkingHash, false);
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
            }
            // S'arrete de courir, commence a marcher : mouvement appuye et courir pas appuye, avant il courrait
            else if (!isRunPressed && isRunning)
            {
                animator.SetBool(isRunningHash, false);
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
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("OpenChest")
                && !animator.GetCurrentAnimatorStateInfo(0).IsName("Challenge")
                && !animator.GetCurrentAnimatorStateInfo(0).IsName("Run To Stop"))
            {
                // Affectation des variables
                isMovementPressed = _direction.x != 0 || _direction.y != 0;
                isRunPressed = _isRunning == 1;

                if (isRunPressed) animator.SetBool(isRunningHash, true);

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
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("OpenChest")
                && !animator.GetCurrentAnimatorStateInfo(0).IsName("Praise")
                && !animator.GetCurrentAnimatorStateInfo(0).IsName("Challenge"))
            {
                if (isMovementPressed && !isJumping && timeBfJumping <= 0)
                {
                    jumpMultiplier = 8.0f;
                    isJumping = true;
                    animator.Play("Base Layer.Jump");
                    animator.SetBool(isJumpingHash, true);
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
                    }
                    else
                    {
                        jumpMultiplier = 5.0f; 
                        animator.Play("Base Layer.Jump Still");
                    }
                    StartCoroutine(JumpEvent());
                }
            }
        }

        // Appele par PlayerController quand on appuie sur "OpenChest" (f)
        public void OpenChest()
        {
            if (controller.isGrounded
                && !animator.GetCurrentAnimatorStateInfo(0).IsName("Praise")
                && !animator.GetCurrentAnimatorStateInfo(0).IsName("Challenge"))
            {
                GetComponent<AttackHitboxHandler>().OnOpenChest();
                animator.Play("Base Layer.OpenChest");
            }
        }

        // Appele par PlayerController quand on appuie sur "Attack" (clic gauche)
        public void Attack()
        {
            if (controller.isGrounded
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
            float jumpForce = jumpFallOff.Evaluate(timeInAir);
            controller.Move(Vector3.up * jumpForce * jumpMultiplier * Time.deltaTime);
            
            timeInAir += Time.deltaTime;
            yield return null;
        } while (!controller.isGrounded && controller.collisionFlags != CollisionFlags.Above);

        controller.slopeLimit = 45.0f;
        isJumping = false;
        animator.SetBool(isJumpingHash, false);
        
            if (controller.isGrounded
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
            if (controller.isGrounded
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
            if (controller.isGrounded
                && !animator.GetCurrentAnimatorStateInfo(0).IsName("Challenge")
                && !animator.GetCurrentAnimatorStateInfo(0).IsName("OpenChest"))
            {
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
            handleAnimation();
        }

        // Mouvements lies a la souris, rotation haut-bas droite-gauche
        private void PerformRotation()
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("OpenChest")
                && !animator.GetCurrentAnimatorStateInfo(0).IsName("Praise")
                && !animator.GetCurrentAnimatorStateInfo(0).IsName("Challenge"))
            {
                transform.Rotate(rotation);
                playerCamera.localEulerAngles = cameraRotation;
            }
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