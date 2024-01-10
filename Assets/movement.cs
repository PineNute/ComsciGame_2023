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
//Creates the user as a rigid body
    Rigidbody rb;
     [Header("Movement")]
     public float groundDrag;

//lets the user check for the ground
     [Header("Ground Check")]
     public float playerHeight;
     public LayerMask whatIsGround;
     bool grounded;
     bool jump;

//Declares variables for mvoement
      public float movespeed;
      public Transform orientation;
      float horizontalInput;
      float verticalInput;
      Vector3 moveDirection;
      public float jumpforce = 500;
      public float jumpCooldown;
      public float airMultiplier;
      bool readyToJump;
//Declares keybinds
      [Header("Keybinds")]
      public KeyCode jumpkey = KeyCode.Space;

      
    void Start()
    {
     
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        
        
    }

void Update()
    {
    //Basic debug program
      Debug.Log(grounded);
      MyInput();
      grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight, whatIsGround);
      if(grounded){
        rb.drag = groundDrag;
      }else{
        rb.drag = 0;
      }
//Enables the user to jump
      jump = Physics.Raycast(transform.position, Vector3.down, playerHeight, whatIsGround);
  
      if(jump == true && Input.GetKeyDown(KeyCode.Space)){
        rb.AddForce(Vector3.up * jumpforce);
      }
      
        
    }

    private void FixedUpdate(){
      MovePlayer();
    }
    //Code to decide if user is able to jump
    private void MyInput(){
      horizontalInput = Input.GetAxisRaw("Horizontal");
      verticalInput = Input.GetAxisRaw("Vertical");
      if(Input.GetKey(jumpkey) && readyToJump && grounded){
        readyToJump = false;

        Jump();

        Invoke(nameof(ResetJump), jumpCooldown);
      }
    }
    // Code that enables the player to move diagonally in the air
    private void MovePlayer(){
      moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

      if(grounded){
        // Code that enables the player to move diagonally in the air
      
        rb.AddForce(moveDirection.normalized * movespeed * 10f, ForceMode.Force);
      }else if(!grounded){
         rb.AddForce(moveDirection.normalized * movespeed * 10f * airMultiplier, ForceMode.Force);
      }
      
    }
// Code that decides what happens to the user
    private void Jump(){
      rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.y); 
      rb.AddForce(transform.up * jumpforce, ForceMode.Impulse);
    }
    // Code that resets the jump after the user has jumped 
    private void ResetJump(){
      readyToJump = true;
    }
    
}
