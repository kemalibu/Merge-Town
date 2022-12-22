using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomPositonHouse1 : MonoBehaviour
{
    public GameObject house1;
    public GameObject houses;

    private List<GameObject> blocks = new List<GameObject> ();

    private Component[] isAvailableForSpawns;
    

    private void Start()
    {
        isAvailableForSpawns = GetComponentsInChildren<BlockIsAvailableForSpawn>();
        StartCoroutine(SpawnHouse1());
    }

    private void Update()
    {
        foreach (BlockIsAvailableForSpawn isAvailableForSpawn in isAvailableForSpawns)
        {
            if (isAvailableForSpawn.IsAvailable && !blocks.Contains(isAvailableForSpawn.gameObject))
            {
                blocks.Add(isAvailableForSpawn.gameObject);
            }

            if (!isAvailableForSpawn.IsAvailable)
            {
                blocks.Remove(isAvailableForSpawn.gameObject);
            }
        }      
    }

    IEnumerator SpawnHouse1()
    {   while(true)
        {
            yield return new WaitForSecondsRealtime(5f);
            if (blocks.Count > 0)
            {
                GameObject block = blocks[Random.Range(0, blocks.Count)];
                Vector3 House1Position = new Vector3(block.transform.position.x,
                                                         1.33f, block.transform.position.z);
                GameObject spawnHouse = Instantiate(house1, House1Position, Quaternion.Euler(0, 180, 0));
                spawnHouse.transform.parent = houses.transform;
            } 
        }   
    }
}
