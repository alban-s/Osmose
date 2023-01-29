using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* TODO :   Edit > Project Settings > Input Manager
 *          Use Physical Keys -> check
 *          Dans Axes > Fire3 > Alt Positive > mouse 1
 */


[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    // Animations 
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

    // Reglages vitesse
    [SerializeField] float walkSpeed = 4.0f;
    [SerializeField] float runSpeed = 7.0f;

    // Reglages sensibilite souris 
    [SerializeField] private float mouseSensitivityX = 1.0f;
    [SerializeField] private float mouseSensitivityY = 1.0f;

    // Variables deplacement fluide
    [SerializeField][Range(0.0f, 0.5f)] float moveSmoothTime = 0.2f;
    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;

    // Dependance playerMotor
    private PlayerMotor motor;

    float cameraPitch = 0.0f;

    // Variables gestion gravite
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

        if (isMovementPressed && !isWalking)
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
            animator.SetBool(isStoppingHash, true);
            animator.SetBool(isStoppingHash, false);
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
        keyboardUpdate();
        mouseUpdate();
        handleAnimation();
    }

    void keyboardUpdate()
    {
        float run = Input.GetAxisRaw("Fire3");
        float jump = Input.GetAxisRaw("Jump");
        //float openChest = Input.GetKeyDown(KeyCode.F);
        //float fight = Input.GetKeyDown(KeyCode.F);

        //if (Input.GetAxis("mouse 0") == 1) Debug.Log("hi!!!");

        isJumpPressed = jump != 0;
        isRunPressed = run != 0;

        // Calculer le mouvement du joueur 
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Vertical"), Input.GetAxis("Horizontal"));
        targetDir.Normalize();

        isMovementPressed = targetDir.x != 0 || targetDir.y != 0;

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        if (motor.controller.isGrounded) velocityY = 0.0f;
        velocityY += gravity * Time.deltaTime;

        Vector3 velocity = (transform.forward * currentDir.x + transform.right * currentDir.y) * (walkSpeed + runSpeed * run) + Vector3.up * velocityY;


        motor.Move(velocity);

        // Calculer le saut du joueur 
        if (jump == 1)
        {
            motor.Jump();
        }

        // 
        /*if (Input.GetKeyDown(KeyCode.F))
        {
            animator.SetBool(isOpeningChest, true);
            animator.SetBool(isOpeningChest, false);
        }

        // 
        if (Input.GetKeyDown(KeyCode.Q))
        {

        }*/
    }

    void mouseUpdate()
    {
        // Calculer la rotation du joueur 
        Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        Vector3 rotation = Vector3.up * mouseDelta.x * mouseSensitivityX;
        motor.Rotate(rotation);

        // Calculer la rotation de la camera 
        cameraPitch -= mouseDelta.y * mouseSensitivityY;
        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 65.0f);
        motor.RotateCamera(Vector3.right * cameraPitch);
    }

}