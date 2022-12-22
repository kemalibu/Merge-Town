using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockIsAvailableForSpawn : MonoBehaviour
{
    public bool isAvailable = true;
    public bool IsAvailable { get { return isAvailable; } }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<HouseMovement>())
        {
            isAvailable = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<HouseMovement>())
        {
            isAvailable = true;
        }
    }
}
