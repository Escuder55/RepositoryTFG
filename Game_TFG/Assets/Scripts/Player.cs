using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Agent
{
    #region ENUMS
    enum actions { HEAVYATTACK, LIGHTATTACK, DEFEND, INTERACT, JUMP, MOVE }
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
    //Attack
    int currentCombo;
    float timeAttackCounter;

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
        currentCombo = 0;
        timeAttackCounter = 0f;
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

    void Attack(actions attackType)
    {        
        if (attackType == actions.LIGHTATTACK)
        {
            switch (currentCombo)
            {
                case 0:
                    currentCombo++;
                    //activate 1st hit animation

                    Debug.Log("Curent combo : " + currentCombo);
                    break;
                case 1:
                    currentCombo++;
                    //activate 2nd hit animation

                    Debug.Log("Curent combo : " + currentCombo);
                    break;
                default:
                    break;
            }
        }
        else if (attackType == actions.HEAVYATTACK)
        {
            //activate heavy attack
            Debug.Log("Heavy attack done");

            resetCombo();//anim debera notificar reset
        }
    }

    void resetCombo()
    {
        //maybe handle anim

        //logic
        currentCombo = 0;
        Debug.Log("Combo reset. Curent combo : " + currentCombo);
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
        //jump
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

        //Light attack
        if (Input.GetMouseButtonDown(0))
        {
            Attack(actions.LIGHTATTACK);
            timeAttackCounter = 2;
        }
        //Heavy attack
        if (Input.GetMouseButtonDown(1))
        {
            Attack(actions.HEAVYATTACK);
            
        }

        if (timeAttackCounter > 0)
        {
            timeAttackCounter = timeAttackCounter - Time.deltaTime;
            if (timeAttackCounter <= 0)
            {
                resetCombo();
            }
        }
        
    }

    public void StopMovement()
    {
    }
        
    #endregion
}
