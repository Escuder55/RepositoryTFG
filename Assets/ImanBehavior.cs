using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum mobilityType { MOBILE, STATIC, NONE };
public enum iman { POSITIVE, NEGATIVE, NONE };
public enum forceType { ATRACT, REPULSE, NONE };

public class ImanBehavior : MonoBehaviour
{
    [SerializeField]private List<GameObject> nearImantableObjects;

    [Header("CHECKING CHARGES")]
    SphereCollider mysphereCollider;
    float oneCharge, twoCharge, ThreeCharge;
    [Header("ELEMENT TYPE")]
    public mobilityType mobility = mobilityType.NONE;
    public iman myPole = iman.NONE;
    public LayerMask whatCanBeImanted;
    private Rigidbody myRB;

    [Header("FORCES")]
    [SerializeField] float radius = 25;
    [SerializeField] float force = 1;
    bool applyForce = false;
    Vector3 directionForce;
    Collider[] others;
    GameObject otherGO;
    forceType myForceType = forceType.NONE;

    // Start is called before the first frame update
    void Start()
    {
        myRB = this.GetComponent<Rigidbody>();
        mysphereCollider = this.GetComponent<SphereCollider>();
        mysphereCollider.radius = 0.5f;
        nearImantableObjects = new List<GameObject>();
    }

    private void Update()
    {
        /*if (applyForce)
        {
            if (Vector3.Distance(otherGO.transform.position, this.transform.position) > 20)
            {
                ResetObject();
                Debug.Log("Mu lejos");
            }
            else if (Vector3.Distance(otherGO.transform.position, this.transform.position) < 3f)
            {
                ResetObject();
                Debug.Log("Mu cerca");
            }
        }*/
        if (myPole!=iman.NONE)
        {
            CalculateDirectionForce();
        }
    }

    private void FixedUpdate()
    {
        if (applyForce)
        {
            myRB.AddForce(directionForce * force, ForceMode.Acceleration);
            directionForce = new Vector3(0, 0, 0);
        }
    }

    ///New technique////////
    #region UPDATING ELEMENTS NEAR
    private void OnTriggerEnter(Collider other)
    {
        if (myPole!=iman.NONE)
        {
            if ((other.gameObject.layer == 10) && other.gameObject != this.gameObject)
            {
                if (!nearImantableObjects.Contains(other.gameObject))
                    nearImantableObjects.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            nearImantableObjects.Remove(other.gameObject);
        }
    }
    #endregion

    void CalculateDirectionForce()
    {
        foreach (GameObject obj in nearImantableObjects)
        {
            //comprobamos q se tenga q calcular
            if (obj.GetComponent<ImanBehavior>().myPole != iman.NONE)
            {
                if (obj.GetComponent<ImanBehavior>().myPole == myPole)
                {
                    //Repulsion
                    directionForce += CalculateOneForce(this.gameObject, obj, forceType.REPULSE);
                }
                else if (obj.GetComponent<ImanBehavior>().myPole != myPole)
                {
                    //atraccion
                    directionForce += CalculateOneForce(this.gameObject, obj, forceType.ATRACT);
                }
            }
        }
        applyForce = true;
    }

    public void AddCharge(iman typeIman, int numCharge)
    {
        
        this.gameObject.tag = "Untagged";
        //Primero asignamos polo para que no haya problemas en otra parte del codigo
        if (typeIman == iman.POSITIVE)
            myPole = iman.POSITIVE;
        else
            myPole = iman.NEGATIVE;

        switch (numCharge)
        {
            case 1:
                mysphereCollider.enabled = true;
                mysphereCollider.radius = 10;
                break;
            case 2:
                break;
            case 3:
                break;
            default:
                break;
        }

        

        if (Physics.CheckSphere(transform.position, radius, whatCanBeImanted))
        {
            //CheckOthers();
        }
    }

    private Vector3 CalculateOneForce(GameObject myGO, GameObject otherGO, forceType typeOfForce)
    {
        Vector3 finalForce= new Vector3(0,0,0);
        //Suma de cargas
        float numChargesSum = 10;//tendriamos q sumar las cargas de ambos objetos
        switch (typeOfForce)
        {
            case forceType.ATRACT:
                finalForce = CalculateVectorAB(myGO.transform.position, otherGO.transform.position);                
                break;
            case forceType.REPULSE:
                finalForce = CalculateVectorAB(otherGO.transform.position, myGO.transform.position);
                break;
            case forceType.NONE:
                break;
            default:
                break;
        }
        float invertedDistance = (1f / finalForce.magnitude*numChargesSum) ;    
        
        finalForce = finalForce.normalized * invertedDistance;
        //Debug.Log(finalForce);
        
        return finalForce;
    }

    ///Oldtechinque
    public void AddNegative(int numCharge)
    {
        //cambiamos polo
        myPole = iman.NEGATIVE;
        //Hay alguno cerca que checkear?
        if (Physics.CheckSphere(transform.position, radius, whatCanBeImanted))
        {
            CheckOthers();
        }

    }

    private void CheckOthers()
    {
        others = Physics.OverlapSphere(transform.position, radius, whatCanBeImanted);

        CleanOthers();
        if (otherGO != null)
        {
            if (otherGO.GetComponent<ImanBehavior>().myPole != iman.NONE)
            {
                if (otherGO.GetComponent<ImanBehavior>().myPole == myPole)
                    myForceType = forceType.REPULSE;
                else
                    myForceType = forceType.ATRACT;

                otherGO.GetComponent<ImanBehavior>().AnotherFound(this.gameObject, myPole);
                applyForce = true;
                //this.gameObject.layer = LayerMask.NameToLayer("Default");
            }
        }
    }

    void CleanOthers()
    {
        for (int i = 0; i < others.Length; i++)
        {
            if (others[i].gameObject != this.gameObject)
            {
                otherGO = others[i].gameObject;
            }
        }
    }

    public void AnotherFound(GameObject other, iman pole)
    {
        if (pole == myPole)
            myForceType = forceType.REPULSE;
        else
            myForceType = forceType.ATRACT;
        //this.gameObject.layer = LayerMask.NameToLayer("Default");
        applyForce = true;
    }

    private Vector3 CalculateVectorAB(Vector3 A, Vector3 B)
    {
        Vector3 result = new Vector3(B.x - A.x, B.y - A.y, B.z - A.z);
        return result;
    }

    private void ResetObject()
    {
        myPole = iman.NONE;
        applyForce = false;
        otherGO = null;
        others = null;
    }

}
