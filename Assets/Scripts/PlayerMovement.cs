using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    
    public float minX = -5f;
    public float maxX = 5f;
    public float minZ = -5f;
    public float maxZ = 5f;
    
    void Update()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");
        
        var movement = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        
        MovePlayer(movement);
    }
    
    private void MovePlayer(Vector3 movement)
    {
        var movementAmount = movement * (moveSpeed * Time.deltaTime);
        var newX = Mathf.Clamp(transform.position.x + movementAmount.x, minX, maxX);
        var newZ = Mathf.Clamp(transform.position.z + movementAmount.z, minZ, maxZ);

        transform.position = new Vector3(newX, transform.position.y, newZ);

        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(movement);
            
            //todo set animation to walking
        }
        else
        {
            //todo set animation to stopping
        }
    }
}
