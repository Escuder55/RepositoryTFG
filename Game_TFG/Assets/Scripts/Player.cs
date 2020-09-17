using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Agent
{
    #region ENUMS
    enum actions { ATTACK, DEFEND, INTERACT, JUMP, MOVE }
    enum skills { }
    #endregion

    #region VARIABLES

    //private////////////
    actions currentAction;
    //Movement
    private Rigidbody playerBody;
    Transform cam;
    InputManager myInput;
    bool isJumping;
    /// //
    
    //public/////////////
    public int myCrystals;
    public int myCrystalsPowder;
    public Weapons CurrentWeapon;
    #endregion

    #region METHODS
    private void Start()
    {

        //Getting components //Input
        myInput = GameObject.FindGameObjectWithTag("GameManager").GetComponent<InputManager>();
        //Rb
        playerBody = GetComponent<Rigidbody>();

        //Initialize
        speed = 3;
    }

    void GetInpuCOntroller()
    {
    }

    void HandleGamePadInput()
    {
    }

    void HandleKeyboardInput()
    {
    }

    void AddMovement()
    {
        playerBody.velocity = new Vector3 (myInput.inputVector.x * speed,playerBody.velocity.y, myInput.inputVector.z * speed);
        //rotate
        transform.LookAt(transform.position + new Vector3 (myInput.inputVector.x,0f, myInput.inputVector.z));
    }

    void Skills(skills skill)
    {
    }

    void Jump()
    {
        playerBody.AddForce(new Vector3(0,100,0),ForceMode.Impulse);
    }

    void Dash()
    {
    }

    void Attack()
    {
    }

    void Protect()
    {
    }

    private void FixedUpdate()
    {
        //movement
        AddMovement();
    }

    private void Update()
    {
        if (myInput.jump && !isJumping)
        {
            isJumping = true;
            myInput.jump = false;
            Jump();
        }
        else //if(isgrounded)
        {
            isJumping = false;
        }
    }

    public void StopMovement()
    {
    }

    
    #endregion
}
