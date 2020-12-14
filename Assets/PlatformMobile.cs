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
    public Transform platform;
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
                    if (platformIman.myPole != iman.NONE && platformIman.myPole == leftBounderIman.myPole)
                    {
                        currentPosition = PlatformPosition.GOING_TO_RIGHT;

                        GoToRight();
                    }
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
                    if (platformIman.myPole != iman.NONE && platformIman.myPole == leftBounderIman.myPole)
                    {
                        currentPosition = PlatformPosition.GOING_TO_RIGHT;

                        GoToRight();
                    }
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
        platform.DOMove(leftSide.position, speed);

        Invoke("ArriveToLeft", speed);
    }
    #endregion

    #region ARRIVE LEFT
    void ArriveToLeft()
    {
        currentPosition = PlatformPosition.LEFT;
    }
    #endregion

    #region GO TO RIGHT
    void GoToRight()
    {
        platform.DOMove(rightSide.position, speed);

        Invoke("ArriveToRight", speed);
    }
    #endregion

    #region ARRIVE RIGHT
    void ArriveToRight()
    {
        currentPosition = PlatformPosition.RIGHT;
    }
    #endregion

    #region TRIGGER ENTER
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entra el jugador");
        if (other.CompareTag("Player"))
        {
            
            //SetParent(other);

            other.transform.SetParent(this.transform);
            //other.transform.parent = this.transform;
        }
    }
    #endregion

    #region TRIGGER EXIT
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Sale el jugador");
        if (other.CompareTag("Player"))
        {
            
            //UnsetParent(other);

            other.transform.SetParent(null);
            //other.transform.parent = null;
        }
    }
    #endregion

    #region SET PARENT
    void SetParent(Collider other)
    {
        other.transform.SetParent(this.transform);
        //other.transform.parent = this.transform;
    }
    #endregion

    #region UNSET PARENT
    void UnsetParent(Collider other)
    {
        other.transform.SetParent(null);
        //other.transform.parent = null;
    }
    #endregion

}
