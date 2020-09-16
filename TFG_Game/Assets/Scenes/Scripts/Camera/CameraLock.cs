using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraLock : MonoBehaviour
{
    public float lockDelta = 0.5F;
    private float nextLock = 0.5F;
    private float myTime = 0.0F;
    private bool haveToRotate = false;

    public GameObject closestEnemy;
    private Vector3 distanceToEnemy;
    public float angleRotation;
    public float smooth = 1.0f;

    void Update()
    {
        myTime = myTime + Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && myTime > nextLock)
        {
            nextLock = myTime + lockDelta;

            // create code here that animates the newProjectile
            Debug.Log("Entro en el LOCK");
            closestEnemy = FindClosestEnemy();

            //LOOK TO ENEMY
            //LERP (NO SNAP)
            //haveToRotate = true;           

            //LOOK AT (SNAP)
            //transform.LookAt(closestEnemy.transform.position);
            transform.DOLookAt(closestEnemy.transform.position, 0.5f);

            nextLock = nextLock - myTime;
            myTime = 0.0F;
        }

        RotateTo(closestEnemy);
    }

    public GameObject FindClosestEnemy()
    {
        //PARA OPTIMIZAR HABRíA QUE TENER DESDE UN INICIO TODOS LOS ENEMIGOS CARGADOS EN UN ARRAY (PARA NO SOBRECARGAR EL UPDATE)
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float minDistance = Mathf.Abs((enemies[0].transform.position - transform.position).magnitude);
        GameObject closest = enemies[0];

        foreach(GameObject enemy in enemies)
        {
            float calculateMinDistance = Mathf.Abs((enemy.transform.position - transform.position).magnitude);
            if (calculateMinDistance < minDistance)
            {
                minDistance = calculateMinDistance;
                closest = enemy;
            }
        }

        return closest;
    }

    public void RotateTo(GameObject closestEnemy)
    {
        if(haveToRotate)
        {
            distanceToEnemy = (closestEnemy.transform.position - transform.position).normalized;
            angleRotation = Vector3.Angle(transform.forward, distanceToEnemy);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angleRotation, transform.up), 10 * smooth * Time.deltaTime);
        }
    }
}
