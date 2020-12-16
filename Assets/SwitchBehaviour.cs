using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBehaviour : MonoBehaviour
{

    #region VARIABLES
    public Animator anim;
    public ImanBehavior iman;
    public GameObject crystal;
    public GameObject cinematicCamera;
    public Room1Manager manager;
    bool done = false;
    #endregion

    #region VARIABLES
    void Update()
    {
        if ((iman.myPole == global::iman.NEGATIVE || iman.myPole == global::iman.POSITIVE) && !done)
        {
            manager.IncreaseButtonPressed();
            done = true;
            anim.SetBool("Active",true);
            cinematicCamera.SetActive(true);

            Invoke("DeactivateCrystal", 1);
            Invoke("RestartCamera", 3);
        }
    }
    #endregion  

    #region DEACTIVATE CRYSTAL
    void DeactivateCrystal()
    {
        crystal.SetActive(false);
    }
    #endregion

    #region RESET CAMERA
    void RestartCamera()
    {
        cinematicCamera.SetActive(false);
    }
    #endregion

}
