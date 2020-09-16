using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactuable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //ON COLLISION WITH INTERACTUABLE
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            //IF (Input)
            //SWITCH CASE (Que interactuable es)

            Debug.Log("Es el jugador bro");
        }
    }
}
