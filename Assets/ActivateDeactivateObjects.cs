using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDeactivateObjects : MonoBehaviour
{
    #region VARIABLES
    public enum ActivationType
    {
        NONE,
        ACTIVATE,
        DEACTIVATE
    };

    public ActivationType type;
    public GameObject[] objects;

    #endregion

    #region TRIGGER ENTER
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (type)
            {
                case ActivationType.NONE:
                    {                        
                        break;
                    }
                case ActivationType.ACTIVATE:
                    {
                        foreach (GameObject item in objects)
                        {
                            item.SetActive(true);
                        }
                        break;
                    }
                case ActivationType.DEACTIVATE:
                    {
                        foreach (GameObject item in objects)
                        {
                            item.SetActive(false);
                        }
                        break;
                    }
                default:
                    break;
            }
        }
    }
    #endregion

    #region TRIGGER EXIT
    #endregion
}
