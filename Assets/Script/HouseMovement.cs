using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshCollider))]
public class HouseMovement : MonoBehaviour
{

    private Vector3 screenPoint;
    private Vector3 offset;

    private float xMin = -3f;
    private float xMax = 3f;
    private float zMin = -7f;
    private float zMax = 3f;
    private float yPos = -9.01f;
    
    private FindClosestBlock targetBlock;

    private Vector3 firstPosition;
    public Vector3 FirstPosition { get { return firstPosition; } }

    private bool isTrigger = false;
    public bool IsTrigger { get { return isTrigger; } }


    void Start()
    {
        targetBlock = GetComponent<FindClosestBlock>();

        if(targetBlock == null)
        {
            Debug.Log("Find Closest Block is NULL.");
        }
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            firstPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
    }

    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - 
                 Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,
                 screenPoint.z));
        isTrigger = false;
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = new Vector3(Mathf.Clamp(curPosition.x, xMin, xMax), yPos,
                                         Mathf.Clamp(curPosition.z, zMin, zMax));
        isTrigger = false;
    }

    void OnMouseUp()
    {
        transform.position = new Vector3(targetBlock.FindClosestTarget("Block").transform.position.x,
                             yPos, targetBlock.FindClosestTarget("Block").transform.position.z);
        isTrigger = true;
        StartCoroutine(MouseClickExit());
    }

    IEnumerator MouseClickExit()
    {
        yield return new WaitForSeconds(0.2f);
        isTrigger = false;
    }
}