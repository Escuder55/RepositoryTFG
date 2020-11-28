using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum iman { POSITIVE,NEGATIVE, NONE};
public enum forceType { ATRACT, REPULSE, NONE };

public class ImanBehavior : MonoBehaviour
{
    public iman myPole= iman.NONE;
    public LayerMask whatCanBeImanted;
    private Rigidbody myRB;
    [SerializeField] float radius=25;
    bool applyForce=false;
    Vector3 directionForce;
    [SerializeField]float force=1;
    Collider[] others;
    GameObject other;
    forceType myForceType= forceType.NONE;
    // Start is called before the first frame update
    void Start()
    {
        myRB = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (applyForce)
        {
            if (Vector3.Distance(other.transform.position, this.transform.position) > 20)
            {
                ResetObject();
                Debug.Log("Mu lejos");
            }
            else if (Vector3.Distance(other.transform.position, this.transform.position) < 3f)
            {
                ResetObject();
                Debug.Log("Mu cerca");
            }
        }
    }

    private void FixedUpdate()
    {
        if (applyForce)
        {
            directionForce = CalculateVectorAB(this.transform.position, other.transform.position);
            if (myForceType == forceType.REPULSE)
            {
                directionForce *= -1;
            }
            myRB.AddForce(directionForce* force,ForceMode.Acceleration);
        }
    }

    public void AddPositive()
    {
        myPole = iman.POSITIVE;

        if (Physics.CheckSphere(transform.position, radius, whatCanBeImanted))
        {
            CheckOthers();
        }
    }    

    public void AddNegative()
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
        if (other != null)
        {
            if (other.GetComponent<ImanBehavior>().myPole != iman.NONE)
            {
                if (other.GetComponent<ImanBehavior>().myPole == myPole)                
                    myForceType = forceType.REPULSE;
                else
                    myForceType = forceType.ATRACT;

                other.GetComponent<ImanBehavior>().AnotherFound(this.gameObject, myPole);
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
                other = others[i].gameObject;
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
        Vector3 result= new Vector3(B.x-A.x, B.y - A.y, B.z - A.z);
        return result.normalized;
    }

    private void ResetObject()
    {
        myPole = iman.NONE;
        applyForce = false;
        other = null;
        others = null;
    }
    
}
