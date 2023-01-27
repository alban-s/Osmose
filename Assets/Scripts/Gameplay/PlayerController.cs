using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isRunningHash;
    int isStoppingHash;
    int isJumpingHash;
    int isDeadHash;
    int isOpeningChestHash;
    int isChallengingHash;

    bool isMovementPressed;
    bool isRunPressed;
    bool isJumpPressed;

    // Pour voir sur unity dans l'inspector
    [SerializeField] float walkSpeed = 2.0f;
    [SerializeField] float runSpeed = 6.0f;

    [SerializeField] private float mouseSensitivity = 3.0f;
    //[SerializeField] private float mouseSensitivityY = 3.0f;
    [SerializeField][Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;

    //[SerializeField] private float mouseSensitivityY = 10f;

    private PlayerMotor motor;

    float cameraPitch = 0.0f;

    float gravity = -13.0f;
    float velocityY = 0.0f;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
        animator = GetComponentInChildren<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isStoppingHash = Animator.StringToHash("isStopping");
        isJumpingHash = Animator.StringToHash("isJumping");
        isDeadHash = Animator.StringToHash("isDead");
        isOpeningChestHash = Animator.StringToHash("isOpeningChest");
        isChallengingHash = Animator.StringToHash("isChallenging");
    }

    void handleAnimation()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);
        bool isStopping = animator.GetBool(isStoppingHash);
        bool isJumping = animator.GetBool(isJumpingHash);
        bool isOpeningChest = animator.GetBool(isOpeningChestHash);
        bool isChallenging = animator.GetBool(isChallengingHash);
        bool isDead = animator.GetBool(isDeadHash);

        if(isMovementPressed && !isWalking)
        {
            animator.SetBool(isWalkingHash, true);
        }
        else if (!isMovementPressed && isWalking)
        {
            animator.SetBool(isWalkingHash, false);
        }
        else if ((isMovementPressed && isRunPressed) && !isRunning)
        {
            animator.SetBool(isRunningHash, true);
        }
        else if ((!isMovementPressed || !isRunPressed) && isRunning)
        {
            animator.SetBool(isRunningHash, false);
        }
        else if (isJumpPressed && !isJumping)
        {
            animator.SetBool(isJumpingHash, true);
        }
        else if (!isJumpPressed && isJumping)
        {
            animator.SetBool(isJumpingHash, false);
        }
    }

    private void Update()
    {
        //Left shift ou clic droit
        float run = Input.GetAxisRaw("Fire3");
        //Touche espace
        float jump = Input.GetAxisRaw("Jump");

        // Calculer le mouvement du joueur 
        /*************************************/
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Vertical"), Input.GetAxis("Horizontal"));
        targetDir.Normalize();
        isMovementPressed = targetDir.x != 0 || targetDir.y != 0;

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        if (motor.controller.isGrounded) velocityY = 0.0f;
        velocityY += gravity * Time.deltaTime;

        Vector3 velocity = (transform.forward * currentDir.x + transform.right * currentDir.y) * (walkSpeed + runSpeed * run) + Vector3.up * velocityY;
        motor.Move(velocity);
        /*************************************/

        // Calculer la rotation du joueur 
        /*************************************/
        Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        
        Vector3 rotation = Vector3.up * mouseDelta.x * mouseSensitivity;
        motor.Rotate(rotation);
        /*************************************/

        // Calculer la rotation de la camera 
        /*************************************/
        cameraPitch -= mouseDelta.y * mouseSensitivity;
        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 65.0f);
        motor.RotateCamera(Vector3.right * cameraPitch);
        /*************************************/

        // Saut
        if (jump == 1)
        {
            motor.Jump();
        }
        
        isJumpPressed = jump != 0;
        isRunPressed = run != 0;
        handleAnimation();
    }

}