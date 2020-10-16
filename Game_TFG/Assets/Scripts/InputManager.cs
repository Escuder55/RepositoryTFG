using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    
    //
    public bool jump = false;
    public bool move = false;
    public bool dash = false;
    public bool lightAttack = false;
    public bool HeavyAttack = false;
    public Vector3 inputVector;


// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        inputVector = new Vector3(Input.GetAxisRaw("Horizontal"),0, Input.GetAxisRaw("Vertical"));
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            dash = true;
        }
        if (Input.GetMouseButtonDown(0))
        {
            lightAttack = true;
        }
        if (Input.GetMouseButtonDown(1))
        {
            HeavyAttack = true;
        }
    }
}
