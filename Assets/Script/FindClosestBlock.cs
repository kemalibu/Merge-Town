using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FindClosestBlock : MonoBehaviour
{
    public GameObject FindClosestTarget(string target)
    {
        Vector3 position = transform.position;
        return GameObject.FindGameObjectsWithTag(target)
            .OrderBy(t => (t.transform.position - position).sqrMagnitude)
            .FirstOrDefault();
    }
}
