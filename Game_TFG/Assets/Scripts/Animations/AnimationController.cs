using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator characterAnimator;


    // Update is called once per frame
    void Update()
    {
        //IDLE
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            IdleAnimation();
        }
        //WALK
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            WalkAnimation();
        }
        //RUN
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            RunAnimation();
        }
        //LIGHT ATTACK 1
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            LightAttack1Animation();
        }
        //LIGHT ATTACK 2
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            LightAttack2Animation();
        }
        //LIGHT ATTACK 3
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            LightAttack3Animation();
        }
        //HEAVY ATTACK
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            HeavyAttackAnimation();
        }
    }

    #region IDLE
    public void IdleAnimation()
    {
        ResetAnimation();
    }
    #endregion

    #region WALK
    public void WalkAnimation()
    {
        characterAnimator.SetBool("Walk", true);
        characterAnimator.SetBool("Run", false);
    }
    #endregion

    #region RUN
    public void RunAnimation()
    {
        characterAnimator.SetBool("Run", true);
        characterAnimator.SetBool("Walk", false);
    }
    #endregion    

    #region LIGHT ATTACK 1
    public void LightAttack1Animation()
    {
        characterAnimator.SetBool("Attack1", true);
    }
    #endregion

    #region LIGHT ATTACK 2
    public void LightAttack2Animation()
    {
        characterAnimator.SetBool("Attack2", true);
    }
    #endregion   

    #region LIGHT ATTACK 3
    public void LightAttack3Animation()
    {
        characterAnimator.SetBool("Attack3", true);
    }
    #endregion   

    #region HEAVY ATTACK 
    public void HeavyAttackAnimation()
    {
        characterAnimator.SetBool("Heavy", true);
    }
    #endregion   

    #region RESET ANIMATION
    public void ResetAnimation()
    {
        characterAnimator.SetBool("Walk", false);
        characterAnimator.SetBool("Run", false);
        characterAnimator.SetBool("Attack1", false);
        characterAnimator.SetBool("Attack2", false);
        characterAnimator.SetBool("Attack3", false);
        characterAnimator.SetBool("Heavy", false);
    }
    #endregion
}
