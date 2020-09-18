using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLock : MonoBehaviour
{
    //Target object to lock to
    public Transform Model;
    public Transform TargetLock;
    private bool isTargetLocked = false;
    private Camera mainCamera;

    //RANGO CONFIGURABLE DE VELOCIDAD DE ROTACION
    [Range(20f, 80f)]
    public float RotationSpeed = 20f;

    //INPUT DIRECTION
    private Vector3 StickDirection;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        StickDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        //EN EL TUTO SITUAN AQUI LA INFORMACION DEL ANIMATOR
        HandleStandardLocomotionRotation();

        //Fijamos enemigo cuando pulsemos espacio
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (isTargetLocked)
            {
                Debug.Log("LockRotation");
                HandleLockedLocomotionRotation();
                isTargetLocked = false;
            }
            else
            {
                Debug.Log("StandardRotation");
                HandleStandardLocomotionRotation();
                isTargetLocked = true;
            }
        }
    }

    private void HandleStandardLocomotionRotation()
    {
        Vector3 rotationOffset = mainCamera.transform.TransformDirection(StickDirection);
        rotationOffset.y = 0;

        //Podría usarse el DoTween tambien
        Model.forward += Vector3.Lerp(Model.forward, rotationOffset, Time.deltaTime * RotationSpeed);
    }

    private void HandleLockedLocomotionRotation()
    {
        //en este caso la referencia de rotacion es el target, no la camara
        //targetposition - currentposition = vector direccion desde la posiciona actual hasta la posicion del objetivo
        Vector3 rotationOffset = TargetLock.transform.position - Model.transform.position;
        rotationOffset.y = 0;

        //Podría usarse el DoTween tambien
        Model.forward += Vector3.Lerp(Model.forward, rotationOffset, Time.deltaTime * RotationSpeed);
    }
}
