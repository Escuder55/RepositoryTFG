using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Item : MonoBehaviour
{
    #region VARIABLES
    public enum ItemType
    {
        NONE,
        CRYSTAL_DUST,
        CRYSTAL,
        LIFE_POTION,
        ATTACK_POTION,
        DEFENSE_POTION,
        SPEED_POTION

    };

    public enum ItemBehaviour
    {
        NONE,
        PICK_UP,
        ATTRACK
    };

    [Header("ITEM TYPE")]
    public ItemType item;
    public ItemBehaviour behaviour;

    [Header("ATTRACK")]
    public float timeToAttrack;
    public float speedAttractionItem;
    Animator animator;
    float timer;

    Transform playerTransform;

    #endregion

    #region START
    void Start()
    {
        animator = GetComponent<Animator>();

        if (behaviour == ItemBehaviour.ATTRACK)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
    }
    #endregion

    #region UPDATE
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeToAttrack)
        {
            animator.SetBool("Idle", true);

            timer = 0;
            Debug.Log("IR PARA EL JUGADOR");
            this.transform.DOMove(new Vector3(  playerTransform.position.x, 
                                                playerTransform.position.y, 
                                                playerTransform.position.z), 
                                                speedAttractionItem);
        }
    }
    #endregion
}
