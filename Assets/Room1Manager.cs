using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room1Manager : MonoBehaviour
{
    public Animator anim;
    public int maxButtons;
    int buttonsPresseds = 0;

    

    #region INCREASE BUTTON PRESSED
    public void IncreaseButtonPressed()
    {
        buttonsPresseds++;

        if (buttonsPresseds == maxButtons)
        {
            anim.SetBool("Active", true);
        }
    }
    #endregion

    #region DECREASE BUTTON PRESSED
    public void DecreaseButtonPressed()
    {
        buttonsPresseds--;
    }
    #endregion
}
