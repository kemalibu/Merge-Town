using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseCollider : MonoBehaviour
{
    private HouseMovement houseMovement;
    private int index;

    public GameObject[] houses;

    void Start()
    {
        houseMovement = GetComponent<HouseMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("House1"))
        {
            index = 1;
        }

        else if (other.gameObject.CompareTag("House2"))
        {
            index = 2;
        }

        else if (other.gameObject.CompareTag("House3"))
        {
            index = 3;
        }

        else if (other.gameObject.CompareTag("House4"))
        {
            index = 4;
        }

        else if (other.gameObject.CompareTag("House5"))
        {
            index = 5;
        }


    }

    void OnTriggerStay(Collider other)
    {   
        if (other.gameObject.tag == gameObject.tag && houseMovement.IsTrigger)
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
            GameObject newHouse = Instantiate(houses[index], transform.position, transform.rotation);
            newHouse.transform.parent = transform.parent;
        }

        //if(other.gameObject.tag != gameObject.tag && gameObject.GetComponent<HouseCollider>())
        //{
        //    other.gameObject.transform.position = houseMovement.FirstPosition;
        //}
    }
}
