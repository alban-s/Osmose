using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Osmose.Game;

/* TODO :   Edit > Project Settings > Input Manager
 *          Use Physical Keys -> check
 *          Dans Axes > Fire3 > Alt Positive > mouse 1
 */

namespace Osmose.Gameplay
{

    [RequireComponent(typeof(PlayerMotor))]
    public class PlayerController : MonoBehaviour
    {
        // Reglages sensibilite souris 
        [SerializeField] private float mouseSensitivityX = 1.0f;
        [SerializeField] private float mouseSensitivityY = 1.0f;
        float cameraPitch = 0.0f;

        Vector2 currentDir = Vector2.zero;

        // Dependance playerMotor
        private PlayerMotor motor;


        private void Start()
        {
            motor = GetComponent<PlayerMotor>();
        }

        private void Update()
        {
            keyboardUpdate();
            mouseUpdate();
        }

        void keyboardUpdate()
        {
            float run = Input.GetAxisRaw("Fire3");
            float jump = Input.GetAxisRaw("Jump");
            float openChest = Input.GetAxisRaw("OpenChest");
            float attack = Input.GetAxisRaw("Attack");
            float attackBase = Input.GetAxisRaw("AttackBase");
            float associate = Input.GetAxisRaw("Associate");
            float praise = Input.GetAxisRaw("Praise");

            if (openChest == 1) motor.OpenChest();
            else if (attack == 1) motor.Attack();
            else if (attackBase == 1) motor.AttackBase();
            else if (associate == 1) motor.Associate();
            else if (praise == 1) motor.Praise();

            // Calculer le mouvement du joueur 
            currentDir = new Vector2(Input.GetAxisRaw("Vertical"), Input.GetAxis("Horizontal"));
            currentDir.Normalize();

            motor.Move(currentDir, run);

            // Calculer le saut du joueur 
            if (jump == 1) motor.Jump();

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
}