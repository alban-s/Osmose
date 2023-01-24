using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isRunningHash;
    bool isMovementPressed;
    bool isRunPressed;

    // Pour voir sur unity dans l'inspector
    [SerializeField]
    private float speed = 3f;
    private float speedRun = 5f;

    [SerializeField]
    private float mouseSensitivityX = 10f;
    [SerializeField]
    private float mouseSensitivityY = 10f;

    private PlayerMotor motor;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
        animator = GetComponentInChildren<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
    }

    void handleAnimation()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);
        bool isStopping = animator.GetBool("isStopping");

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
    }

    private void Update()
    {
        // Calculer la velocite du mouvement 
        // Axe hor -> gauche droite 
        float xMov = Input.GetAxisRaw("Horizontal");
        // Recupere -1 pour q et 1 pour d ou 0 pour pas de déplacement
        float zMov = Input.GetAxisRaw("Vertical");

        //Left shift ou clic droit
        float run = Input.GetAxisRaw("Fire3");

        isMovementPressed = xMov != 0 || zMov != 0;
        isRunPressed = run != 0;

        handleAnimation();

        Vector3 moveHorizontal = transform.right * xMov;
        Vector3 moveVertical = transform.forward * zMov;

        Vector3 velocity = (moveHorizontal + moveVertical).normalized * (speed + speedRun * run);
        motor.Move(velocity);

        // Calculer la rotation du joueur 
        float yRot = Input.GetAxisRaw("Mouse X");

        Vector3 rotation = new Vector3(0, yRot, 0) * mouseSensitivityX;

        motor.Rotate(rotation);

        // Calculer la rotation de la camera 
        float xRot = Input.GetAxisRaw("Mouse Y");

        Vector3 cameraRotation = new Vector3(xRot, 0, 0) * mouseSensitivityY;

        motor.RotateCamera(cameraRotation);
    }

}