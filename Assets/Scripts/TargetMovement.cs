using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TargetMovement : MonoBehaviour
{

    public float speed;
    private Rigidbody rb;
    private float zDirection;    
    private float xDirection;
    private string x;
    private string z;

    void Start()
    {
        if(gameObject.CompareTag("P1Target")) {
            z = "Vertical1";
            x = "Horizontal1";
        }

        else if(gameObject.CompareTag("P2Target")) {
            z = "Vertical2";
            x = "Horizontal2";
        }  

        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        zDirection = Input.GetAxis(z);
        xDirection = Input.GetAxis(x);

        Vector3 movement = new Vector3(xDirection, 0f, zDirection);

        transform.position += movement * speed / 20;
    }
}
