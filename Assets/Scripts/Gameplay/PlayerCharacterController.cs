using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
    private Rigidbody rb;
    private int speed;

    private float x, y, z;

    private float movementX;
    private float movementY;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = 10;
        x = 0.0f;
        y = 1.0f;
        z = 0.0f;


    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.W))
        {
            z += 5 * Time.deltaTime;
            this.transform.position = new Vector3(x, y, z);

        }

        else if (Input.GetKey(KeyCode.A))
        {
            x -= 5 * Time.deltaTime;
            this.transform.position = new Vector3(x, y, z);

        }
        else if (Input.GetKey(KeyCode.S))
        {
            z -= 5 * Time.deltaTime;
            this.transform.position = new Vector3(x, y, z);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            x += 5 * Time.deltaTime;
            this.transform.position = new Vector3(x, y, z);

        }

        //TODO : les forces
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            //y += 15 *Time.deltaTime;
            rb.AddForce(0, 4.0f, 0, ForceMode.Impulse);

        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            //y -= 1.5f;
            //rb.AddForce(0, 0, 1.0f, ForceMode.Impulse);

        }

        //Vector3 movement = new Vector3(movementX, 0.0f, movementY);

    }
}