using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMotor : MonoBehaviour
{
    [SerializeField] private Transform playerCamera = null;

    private Vector3 velocity;
    private Vector3 rotation;
    private Vector3 cameraRotation;

    public CharacterController controller = null;

    [SerializeField] private AnimationCurve jumpFallOff;
    [SerializeField] private float jumpMultiplier = 8.0f;
    private bool isJumping;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Appele par PlayerController quand on appuie sur "Horizontal" ou "Vertical"
    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
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
        if (!isJumping)
        {
            isJumping = true;
            StartCoroutine(JumpEvent());
        }
    }

    private void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }

    // Mouvements lies a la souris, rotation haut-bas droite-gauche
    private void PerformRotation()
    {
        transform.Rotate(rotation);
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
    }

}
