using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlatformMobile : MonoBehaviour
{
    #region VARIABLES
    public enum PlatformPosition { NONE, LEFT, GOING_TO_LEFT, GOING_TO_RIGHT, RIGHT};

    public PlatformPosition startingPosition;
    PlatformPosition currentPosition;
    [Header("IMAN")]
    public ImanBehavior platformIman;
    public ImanBehavior leftBounderIman;
    public ImanBehavior rightBounderIman;
    [Header("PLATFORM MOVEMENT")]
    public float speed = 1.5f;
    public Transform leftSide;
    public Transform rightSide;
    #endregion

    #region START
    private void Start()
    {
        currentPosition = startingPosition;
    }
    #endregion

    #region UPDATE
    void Update()
    {
        switch (currentPosition)
        {
            case PlatformPosition.NONE:
                {
                    break;
                }
            case PlatformPosition.LEFT:
                {
                    break;
                }
            case PlatformPosition.GOING_TO_LEFT:
                {
                    break;
                }
            case PlatformPosition.GOING_TO_RIGHT:
                {
                    break;
                }
            case PlatformPosition.RIGHT:
                {
                    break;
                }
            default:
                break;
        }
    }
    #endregion

    #region GO TO LEFT
    void GoToLeft()
    {

    }
    #endregion

    #region GO TO RIGHT
    void GoToRight()
    {

    }
    #endregion
    
    #region TRIGGER ENTER
    private void OnTriggerEnter(Collider other)
    {
        
    }
    #endregion

    #region TRIGGER EXIT
    private void OnTriggerExit(Collider other)
    {
        
    }
    #endregion

    #region SET PARENT
    void SetParent()
    {

    }
    #endregion

    #region UNSET PARENT
    void UnsetParent()
    {

    }
    #endregion

}
