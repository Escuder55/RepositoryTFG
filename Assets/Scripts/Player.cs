using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum actions { LIGHTATTACK, HEAVYATTACK, DASH, REPEL, INTERACT, JUMP, CHARGEATTACK, WALK, RUN, NONE }

public class Player : Agent
{
    #region ENUMS
    enum skills { }
    #endregion

    #region VARIABLES
    //movement and camera
    float angle;
    float targetAngle;
    public CharacterController controller;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public Transform cam;
    //
    public actions currentAction;
    //Movement
    private Rigidbody playerBody;
    InputManager myInput;
    bool isJumping;
    private int MAXLIFE = 6;
    public int comboCounter=0;
    private float distToGround;
    public bool heavyAttack = false;
    bool chargeAttack = false;
    bool repelMode = false;
    [SerializeField] Collider myCol;
    /// // cada 10 es un crystal
    [SerializeField] int MAXSWORDCRYSTALS = 20;
    [SerializeField] int MAXSHIELDCRYSTALS = 20;
    [SerializeField] int MAXBOOTSCRYSTALS = 20;

    [SerializeField] float swordCrystalEnergy;
    [SerializeField] float shieldCrystalEnergy;
    [SerializeField] float bootsCrystalEnergy;

    //public/////////////
    public int myCrystals;
    public int myCrystalsPowder;
    public Weapons CurrentWeapon;
    public int currentLife;
    #endregion

    #region METHODS
    private void Start()
    {
        currentAction = actions.NONE;
        //Getting components //Input
        myInput = GameObject.FindGameObjectWithTag("GameManager").GetComponent<InputManager>();
        //Rb
        playerBody = GetComponent<Rigidbody>();

        //Initialize
        speed = 3;
        currentLife = MAXLIFE;
        //EnergyCounters
        swordCrystalEnergy = MAXSWORDCRYSTALS;
        shieldCrystalEnergy = MAXSHIELDCRYSTALS;
        bootsCrystalEnergy = MAXBOOTSCRYSTALS;

        distToGround = myCol.bounds.extents.y;
    }

    void AddMovement()
    {
        if ((currentAction != actions.LIGHTATTACK) && (currentAction != actions.HEAVYATTACK) && (currentAction != actions.DASH))
        {
            ///////////////////////
            Vector3 direction = myInput.inputVector.normalized;

            if (direction.magnitude >= 0.1f)
            {
                targetAngle = Mathf.Atan2(direction.x, direction.z)* Mathf.Rad2Deg + cam.eulerAngles.y;
                angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDirection.normalized * speed * Time.deltaTime);
            }

            ///////////////////////
            //playerBody.velocity = new Vector3(myInput.inputVector.x * speed, playerBody.velocity.y, myInput.inputVector.z * speed);
            //rotate
            //transform.LookAt(transform.position + new Vector3(myInput.inputVector.x, 0f, myInput.inputVector.z));
            if (playerBody.velocity.magnitude > 2)
                currentAction = actions.WALK;
            if (playerBody.velocity.magnitude > 2.9)
                currentAction = actions.RUN;
            else
                currentAction = actions.NONE;
        }
    }

    void Skills(skills skill)
    {
    }

    void Jump()
    {
        playerBody.AddForce(new Vector3(0,100,0),ForceMode.Impulse);
        currentAction = actions.JUMP;
    }

    void Dash()
    {
        Debug.Log("Dash done");
        currentAction = actions.DASH;
    }

    public void CleanComboCounter()
    {
        comboCounter = 0;
    }

    void LightAttack()
    {
        currentAction = actions.LIGHTATTACK;
        comboCounter++;
        myInput.lightAttack = false;
    }

    void HeavyAttack()
    {
        currentAction = actions.HEAVYATTACK;
        heavyAttack = true;
        Debug.Log("Heavy Attack!");
        comboCounter = 0;
    }

    private void FixedUpdate()
    {
        //movement
        AddMovement();
    }

    private void Update()
    {
        //controlamos input recogido
        Controller();
        //
        UpdateCounters();
    }

    private void Controller()
    {
        if (myInput.jump && IsGrounded())
        {
            Jump();
            isJumping = true;
            myInput.jump = false;
        }
        else if(IsGrounded())
        {
            isJumping = false;
        }
        if (myInput.dash && (bootsCrystalEnergy>=10))
        {
            Dash();
            myInput.dash = false;
            bootsCrystalEnergy -= 10;
        }
        if (myInput.lightAttack)
        {
            LightAttack();
        }
        if (myInput.HeavyAttack)
        {
            HeavyAttack();
            myInput.HeavyAttack = false;
        }
        if ((myInput.ChargeAttack && !chargeAttack) && (swordCrystalEnergy>10))
        {
            ChargeAttackStart();
        }
        if (chargeAttack && !myInput.ChargeAttack)
        {
            ChargeAttackFinish();
        }
        if (myInput.Repel && (shieldCrystalEnergy > 10))
        {
            repelMode = true;
            Debug.Log("RepelModeOn");
        }
        if (repelMode && !myInput.Repel)
        {
            repelMode = false;
            Debug.Log("RepelModeOff");
        }
    }

    private void ChargeAttackStart()
    {
        currentAction = actions.CHARGEATTACK;
        chargeAttack = true;
        Debug.Log("Charge Attack Start");
    }

    private void ChargeAttackFinish()
    {
        Debug.Log("Charge Attack finish");
        chargeAttack = false;
    }

    public void StopMovement()
    {
    }

    public void SetCurrentAtionNone()
    {
        currentAction = actions.NONE;
    }

    private void UpdateCounters()
    {
        if (bootsCrystalEnergy < MAXBOOTSCRYSTALS)
            bootsCrystalEnergy += Time.deltaTime;
        if (swordCrystalEnergy < MAXSWORDCRYSTALS)
            swordCrystalEnergy += Time.deltaTime;
        if (shieldCrystalEnergy < MAXSHIELDCRYSTALS)
            shieldCrystalEnergy += Time.deltaTime;
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, (float)(distToGround));
    }

    #endregion
}
