using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] float minimumTimeForRepeatedFlightSpawning = 2;
    [SerializeField] float maximumTimeForRepeatedFlightSpawning = 5;
    [SerializeField] List<string> pooledObjects;

    private float range;
    private int num;
    public bool canSpawn;
    private void Start()
    {
        canSpawn = true;
        StartCoroutine(SpawnObjectsWithDelay());
    }
    private IEnumerator SpawnObjectsWithDelay()
    {
        while (true)
        {
            range = Random.Range(minimumTimeForRepeatedFlightSpawning, maximumTimeForRepeatedFlightSpawning);
            yield return new WaitForSeconds(range);

            if (canSpawn)
            {
                SpawnObjects();
            }
        }
    }
    private void SpawnObjects()
    {
        num = Random.Range(0, pooledObjects.Count);
        GameObject obj = ObjectPool.SharedInstance.GetPooledObject(pooledObjects[num]);
        obj.transform.position = transform.position;
        obj.transform.rotation = transform.rotation;
        obj.SetActive(true);
    }
}
