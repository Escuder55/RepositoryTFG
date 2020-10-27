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
    public GameObject prueba;

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

        //PRUEBA DE CAMBIO DE ENEMIGO
        //targetLockCinemachineCamera.LookAt = prueba.transform;
        //scriptTargetGroup.m_Targets[1].target = prueba.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //WASD INPUT
        StickDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        HandleInputData();

        //HANDLE ROTATION PARA MIRAR A DONDE SEÑALAMOS
        if(isTargetLocked)
        {
            HandleTargetLockedLocomotionRotation();
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
        model.forward += Vector3.Lerp(model.forward, rotationOffset, Time.deltaTime * rotationSpeed);
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
        anim.SetFloat("Speed", Vector3.ClampMagnitude(StickDirection, 1).magnitude);
        anim.SetFloat("Horizontal", StickDirection.x);
        anim.SetFloat("Vertical", StickDirection.z);
        isWeaponEquipped = anim.GetBool("IsWeaponEquipped");
        isTargetLocked = anim.GetBool("IsTargetLocked");

        //SOLO PODEMOS TARGETEAR EL ENEMIGO CUANDO ESTA EL ARMA EQUIPADA
        if(Input.GetKeyDown(KeyCode.Z))
        {
            anim.SetBool("IsWeaponEquipped", !isWeaponEquipped);
            anim.SetBool("IsTargetLocked", !isTargetLocked);
            isWeaponEquipped = !isWeaponEquipped;
            isTargetLocked = !isTargetLocked;
        }
    }
}
