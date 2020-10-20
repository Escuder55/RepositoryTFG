using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Chest : MonoBehaviour
{
    #region VARIABLES
    public enum ChestType
    {
        NONE,
        NORMAL,
        KEY_CHEST,
        SPECIAL
    };

    bool playerInside;  

    [Header("CHEST")]
    public Animator animator;
    public ChestType chestType;

    [Header("CHEST")]
    public List<GameObject> items;
    public List<Transform> itemSpawner;
    public Transform initialSpawner;
    public float timeSpawner = 0.35f;
    public float spawnForce = 2;

    float timer;
    bool isSpawning = false;
    int itemCounter = 0;
    bool isOpened = false;

    //Camera
    [Header("Camera")]
    public Transform newPositionCamera;
    public Transform cameraPivot;
    public float transitionCameraTime = 1f;
    public float animationCameraTime = 4f;
    public Animator camAnimator;

    Camera mainCamera;
    Vector3 lastCameraPosition;
    Vector3 lastCamerRotation;
    float lastFOV;
    float timeAnimation;
    #endregion

    #region START
    private void Start()
    {
        if (chestType == ChestType.SPECIAL)
        {
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }
    }
    #endregion

    #region UPDATE
    void Update()
    {
        //Si está abierto controlar el Spawn
        if (isSpawning && itemCounter < items.Count)
        {
            timer += Time.deltaTime;
            if (timer >= timeSpawner)
            {
                //intanciamos el item
                GameObject obj =  Instantiate(items[itemCounter], initialSpawner.position, Quaternion.identity) as GameObject;

                //Animamos el item
                obj.transform.DOJump(itemSpawner[itemCounter].position, spawnForce, 1,timeSpawner);

                //Aumentamos el counter
                itemCounter++;

                //Reestablecemos el counter
                timer = 0;
            }
        }
        //Comprobamos el input
        if (!isOpened && playerInside && Input.GetKeyDown(KeyCode.E))
        {
            switch (chestType)
            {
                case ChestType.NONE:
                    {
                        break;
                    }
                case ChestType.NORMAL:
                    {
                        animator.SetBool("Open", true);
                        isSpawning = true;
                        isOpened = true;
                        break;
                    }
                case ChestType.KEY_CHEST:
                    {
                        break;
                    }
                case ChestType.SPECIAL:
                    {
                        //Guardamos la posición de la cámara
                        lastCameraPosition = mainCamera.transform.position;
                        lastCamerRotation = mainCamera.transform.rotation.eulerAngles;

                        //Movemos la cámara
                        mainCamera.transform.DOMove(newPositionCamera.position, transitionCameraTime);
                        mainCamera.transform.DORotate(newPositionCamera.rotation.eulerAngles, transitionCameraTime);

                        Invoke("CameraAnimation", transitionCameraTime);

                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
    }
    #endregion

    #region CAMERA ANIMATION
    void CameraAnimation()
    {
        //hacemos que la cámara que sea hijo
        mainCamera.transform.parent = newPositionCamera;

        //Animación de abrir el cofre
        animator.SetBool("Open", true);
        isOpened = true;

        //Animamos la rotacion de la camara
        camAnimator.SetBool("Active", true);

        //Hacemos girar la cámara
        cameraPivot.DORotate(new Vector3(0,450,0), animationCameraTime);

        //Después restablecemos la cámara
        Invoke("RestartCamera", animationCameraTime + transitionCameraTime);
        
    }
    #endregion

    #region RESTART CAMERA 
    void RestartCamera()
    {
        //Quitamos que sea hijo 
        mainCamera.transform.parent = null;

        //Movemos la cámara a la posición inicial
        mainCamera.transform.DOMove(lastCameraPosition, transitionCameraTime);
        mainCamera.transform.DORotate(lastCamerRotation, transitionCameraTime);
    }
    #endregion

    #region TRIGGER ENTER
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
        }
    }
    #endregion

    #region TRIGGER EXIT
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }
    #endregion
    
}
