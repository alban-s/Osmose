using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

using Osmose.Game;

/* TODO :   Edit > Project Settings > Input Manager
 *          Use Physical Keys -> check
 *          Dans Axes > Fire3 > Alt Positive > mouse 1
 */

namespace Osmose.Gameplay
{
    [RequireComponent(typeof(PlayerMotor))]
    public class PlayerController : NetworkBehaviour
    {
        float run;
        float jump;
        float openChest;
        float attack;
        float attackBase;
        float associate;
        float praise;
        // Reglages sensibilite souris 
        [SerializeField] private float mouseSensitivityX = 1.0f;
        [SerializeField] private float mouseSensitivityY = 1.0f;
        float cameraPitch = 0.0f;

        private PlayerMotor motor;

        private void Start()
        {
            motor = GetComponent<PlayerMotor>();
        }
        
        private void Update()
        {
            keyboardUpdate();
            if (isLocalPlayer) mouseUpdate();  
        }

        [Command]
        private void update_inputs(float run, float jump, float openChest, 
        float attack, float attackBase, float associate, float praise){
            update_inputs_clients(run,jump,openChest,attack,attackBase,associate,praise);
        }

        [ClientRpc]
        private void update_inputs_clients(float run, float jump, float openChest, 
        float attack, float attackBase, float associate, float praise){
            this.run = run;
            this.jump = jump;
            this.openChest = openChest;
            this.attack = attack;
            this.attackBase = attackBase;
            this.associate = associate;
            this.praise = praise;
        }

        void keyboardUpdate()
        {
            if (isLocalPlayer){
                run = Input.GetAxisRaw("Fire3");
                jump = Input.GetAxisRaw("Jump");
                openChest = Input.GetAxisRaw("OpenChest");
                attack = Input.GetAxisRaw("Attack");
                attackBase = Input.GetAxisRaw("AttackBase");
                associate = Input.GetAxisRaw("Associate");
                praise = Input.GetAxisRaw("Praise");

                update_inputs(run,jump,openChest,attack,attackBase,associate,praise);
            }
            
            Vector2 currentDir = Vector2.zero;

            // Dependance playerMotor

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
