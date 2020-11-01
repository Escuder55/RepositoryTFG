using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHandlerCamera : MonoBehaviour
{
    //REFERENCIA AL MODELO
    public Transform model;

    //target object
    public Transform targetLock;

    //VELOCIDAD DE ROTACION
    [Range(20, 80f)]
    public float rotationSpeed = 20f;

    //CAMERA PRINCIPAL
    private Camera mainCamera;

    //REFERENCIA AL ANIMADOR
    private Animator anim;

    //INPUT DIRECTION
    private Vector3 StickDirection;

    private bool isWeaponEquipped = false;
    private bool isTargetLocked = false;

    //CINEMACHINE
    [Header("Cinemachine parameters")]
    [SerializeField] private CinemachineFreeLook targetLockCinemachineCamera;

    //Coge el script que haya en este objeto (en este caso se encuentra en TargetLockedObject)
    [SerializeField] private CinemachineTargetGroup scriptTargetGroup;
    //public GameObject prueba;

    //GameObjects front of player
    [Header("Enemies in front of camera")]
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private ColliderEnemies scriptColliderEnemies;

    private void Awake()
    {
        //En caso de que no este linkeada
        if (scriptTargetGroup == null)
            throw new NullReferenceException("No esta linkeado el script de targetGroup");
    }

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        anim = model.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //WASD INPUT
        StickDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        HandleInputData();

        //HANDLE ROTATION PARA MIRAR A DONDE SEÑALAMOS
        if (isTargetLocked)
        {
            //FUNCION DE CAMARA FIJADA
            HandleTargetLockedLocomotionRotation();

            //FUNCION DE CAMBIAR DE OBJETIVO
            ChangeObjective();
        }
        else
        {
            HandleStandardLocomotionRotation();
        }
    }

    private void HandleStandardLocomotionRotation()
    {
        Vector3 rotationOffset = mainCamera.transform.TransformDirection(StickDirection);
        rotationOffset.y = 0; //solo queremos los movimientos laterales.
        //smooth rotation
        //model.forward += Vector3.Lerp(model.forward, rotationOffset, Time.deltaTime * rotationSpeed);
    }

    private void HandleTargetLockedLocomotionRotation()
    {
        //aqui referenciamos la rotacion del enemigo
        //targetposition - currentposition = vector direction from currentposition to target position
        Vector3 rotationOffset = targetLock.transform.position - model.position;
        rotationOffset.y = 0;
        //smooth rotation
        model.forward += Vector3.Lerp(model.forward, rotationOffset, Time.deltaTime * rotationSpeed);
    }

    private void HandleInputData()
    {
        //USING KEYBOARD AND PRESSING A DIAGONAL DIRECTION YOU COULD REACH A MAGNITUDE OF 1.4 so we clamp it.

        //VARIABLES VELOCIDAD
        anim.SetFloat("Speed", Vector3.ClampMagnitude(StickDirection, 1).magnitude);
        anim.SetFloat("Horizontal", StickDirection.x);
        anim.SetFloat("Vertical", StickDirection.z);
        isWeaponEquipped = anim.GetBool("IsWeaponEquipped");
        isTargetLocked = anim.GetBool("IsTargetLocked");

        //SOLO PODEMOS TARGETEAR EL ENEMIGO CUANDO ESTA EL ARMA EQUIPADA
        if (Input.GetKeyDown(KeyCode.Z))
        {
            anim.SetBool("IsWeaponEquipped", !isWeaponEquipped);
            anim.SetBool("IsTargetLocked", !isTargetLocked);
            isWeaponEquipped = !isWeaponEquipped;
            isTargetLocked = !isTargetLocked;

            //GETENEMIES FROM COLLIDER
            enemies = scriptColliderEnemies.getEnemies();

            //PRUEBA DE CAMBIO DE ENEMIGO
            //targetLockCinemachineCamera.LookAt = prueba.transform;
            //scriptTargetGroup.m_Targets[1].target = prueba.transform;
        }

    }

    public void ChangeObjective()
    {
        //CHANGE ENEMY (RIGHT)
        if (Input.GetKeyDown(KeyCode.C))
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (scriptTargetGroup.m_Targets[1].target.name == enemies[i].name)
                {
                    if(i < (enemies.Count - 1))
                    {
                        scriptTargetGroup.m_Targets[1].target = enemies[i+1].transform;
                        targetLock = enemies[i + 1].transform;
                        Debug.Log("Siguiente");
                    }
                }
            }
        }

        //CHANGE ENEMY (LEFT)
        if (Input.GetKeyDown(KeyCode.X))
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (scriptTargetGroup.m_Targets[1].target.name == enemies[i].name)
                {
                    if (i > 0)
                    {
                        scriptTargetGroup.m_Targets[1].target = enemies[i - 1].transform;
                        targetLock = enemies[i - 1].transform;
                        Debug.Log("Siguiente");
                    }
                }
            }
        }
    }
}
