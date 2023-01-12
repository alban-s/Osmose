using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{

    // Pour voir sur unity dans l'inspector
    [SerializeField]
    private float speed = 3f;

    [SerializeField]
    private float mouseSensitivityX = 10f;
    [SerializeField]
    private float mouseSensitivityY = 10f;

    private PlayerMotor motor;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    private void Update()
    {
        // Calculer la velocite du mouvement 
        // Axe hor -> gauche droite 
        float xMov = Input.GetAxisRaw("Horizontal");
        // Recupere -1 pour q et 1 pour d ou 0 pour pas de déplacement
        float zMov = Input.GetAxisRaw("Vertical");

        //float yMov = Input.GetAxisRaw("Space");

        Vector3 moveHorizontal = transform.right * xMov;
        Vector3 moveVertical = transform.forward * zMov;
        //Vector3 moveUp = transform.up * xMov;

        Vector3 velocity = (moveHorizontal + moveVertical).normalized * speed;
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