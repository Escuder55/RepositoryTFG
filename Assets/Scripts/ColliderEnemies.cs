using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderEnemies : MonoBehaviour
{
    //VARIABLES
    [SerializeField] List<GameObject> enemies;

    //OnColliderEnter
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemies.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemies.Remove(other.gameObject);
        }
    }

    #region GETENEMIES
    public List<GameObject> getEnemies()
    {
        return enemies;
    }
    #endregion
}