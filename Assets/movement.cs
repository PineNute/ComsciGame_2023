using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class movement : MonoBehaviour
{
    Rigidbody rb;
     [Header("Movement")]
     public float groundDrag;


     [Header("Ground Check")]
     public float playerHeight;
     public LayerMask whatIsGround;
     bool grounded;
     bool jump;

      public float movespeed;
      public Transform orientation;
      float horizontalInput;
      float verticalInput;
      Vector3 moveDirection;
      public float jumpforce = 500;
      public float jumpCooldown;
      public float airMultiplier;
      bool readyToJump;

      [Header("Keybinds")]
      public KeyCode jumpkey = KeyCode.Space;

      
    void Start()
    {
     
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        
        
    }

void Update()
    {
      Debug.Log(grounded);
      MyInput();
      grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight, whatIsGround);
      if(grounded){
        rb.drag = groundDrag;
      }else{
        rb.drag = 0;
      }

      jump = Physics.Raycast(transform.position, Vector3.down, playerHeight, whatIsGround);
  
      if(jump == true && Input.GetKeyDown(KeyCode.Space)){
        rb.AddForce(Vector3.up * jumpforce);
      }
      
        
    }

    private void FixedUpdate(){
      MovePlayer();
    }
    private void MyInput(){
      horizontalInput = Input.GetAxisRaw("Horizontal");
      verticalInput = Input.GetAxisRaw("Vertical");
      if(Input.GetKey(jumpkey) && readyToJump && grounded){
        readyToJump = false;

        Jump();

        Invoke(nameof(ResetJump), jumpCooldown);
      }
    }
    
    private void MovePlayer(){
      moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

      if(grounded){
        rb.AddForce(moveDirection.normalized * movespeed * 10f, ForceMode.Force);
      }else if(!grounded){
         rb.AddForce(moveDirection.normalized * movespeed * 10f * airMultiplier, ForceMode.Force);
      }
      
    }

    private void Jump(){
      rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.y); 
      rb.AddForce(transform.up * jumpforce, ForceMode.Impulse);
    }
    
    private void ResetJump(){
      readyToJump = true;
    }
    
}
