using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 200.0f; // Speed of turning

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float verticalInput = Input.GetAxis("Vertical");

        if (verticalInput == 0)
            return;
        // Apply velocity for movement
        Vector3 velocity = transform.forward * (verticalInput * moveSpeed);
        rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
    }

    void Update()
    {
        
        var horizontalInput = Input.GetAxis("Horizontal");
        
        if (horizontalInput == 0)
            return;
        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
    
    }
    
}
