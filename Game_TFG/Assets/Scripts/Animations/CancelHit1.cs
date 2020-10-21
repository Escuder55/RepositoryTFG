using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelHit1 : StateMachineBehaviour
{
    Player myPlayer;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        myPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        myPlayer.CleanComboCounter();
        //myPlayer.currentAction = actions.NONE;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (myPlayer.heavyAttack)
        {            
            animator.SetBool("Heavy", true);
            myPlayer.currentAction = actions.HEAVYATTACK;
        }
        else if (myPlayer.comboCounter > 0)
        {
            animator.SetBool("Attack2", true);
            myPlayer.currentAction = actions.LIGHTATTACK;
        }
        else
        {
            animator.SetBool("Attack1", false);
            myPlayer.currentAction = actions.NONE;
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        myPlayer.CleanComboCounter();
        animator.SetBool("Attack1", false);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
