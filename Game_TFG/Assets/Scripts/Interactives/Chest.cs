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
