using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureButton : MonoBehaviour
{
    public Room1Manager manager;

    #region TRIGGER ENTER
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CanBeHitted"))
        {
            manager.IncreaseButtonPressed();
        }   
    }
    #endregion

    #region TRIGGER EXIT
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CanBeHitted"))
        {
            manager.DecreaseButtonPressed();
        }
    }
    #endregion
}
