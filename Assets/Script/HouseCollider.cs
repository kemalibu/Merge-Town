using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseCollider : MonoBehaviour
{
    [SerializeField] private float movePositionTime = 0.2f;

    private int index;

    public GameObject[] houses;


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("House1"))
        {
            index = 1;
        }

        else if (other.CompareTag("House2"))
        {
            index = 2;
        }

        else if (other.CompareTag("House3"))
        {
            index = 3;
        }

        else if (other.CompareTag("House4"))
        {
            index = 4;
        }
    }

    void OnTriggerStay(Collider other)
    {
        HouseMovement houseMovement = GetComponent<HouseMovement>();
        if (houseMovement == null) { return; }

        if(other.gameObject.tag == gameObject.tag && houseMovement.IsTrigger)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, Mathf.Epsilon);
            GameObject newHouse = Instantiate(houses[index], hitColliders[1].transform.position,
                                  Quaternion.identity);
            newHouse.transform.parent = transform.parent;
            Destroy(gameObject);
            Destroy(other.gameObject);
        }

        else if (other.gameObject.tag != gameObject.tag &&
                 other.gameObject.transform.parent == gameObject.transform.parent &&
                 houseMovement.IsTrigger)
        {
            other.gameObject.transform.DOMove(houseMovement.FirstPosition, movePositionTime);
        }
    }
}
