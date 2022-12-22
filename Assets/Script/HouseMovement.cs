using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshCollider))]
public class HouseMovement : MonoBehaviour
{

    private Vector3 screenPoint;
    private Vector3 offset;
    private FindClosestBlock targetBlock;

    private Vector3 firstPosition;
    public Vector3 FirstPosition { get { return firstPosition; } }

    private bool isTrigger = false;
    public bool IsTrigger { get { return isTrigger; } }


    void Start()
    {
        targetBlock = GetComponent<FindClosestBlock>();
    }

    void OnMouseDown()
    {
        firstPosition = new Vector3(gameObject.transform.position.x, 
                                            gameObject.transform.position.y,
                                             gameObject.transform.position.z);

        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        offset = gameObject.transform.position - 
                 Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,
                 screenPoint.z));
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = new Vector3(Mathf.Clamp(curPosition.x, -2f, 2f), 1.33f,
                                         Mathf.Clamp(curPosition.z, -2f, 5f));
    }

    void OnMouseUp()
    {
        transform.position = new Vector3(targetBlock.FindClosestTarget("Block").transform.position.x,
                             1.33f, targetBlock.FindClosestTarget("Block").transform.position.z);
        isTrigger = true;
    }
}