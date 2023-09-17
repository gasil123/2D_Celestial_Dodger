using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] float minimumTimeForRepeatedFlightSpawning = 2;
    [SerializeField] float maximumTimeForRepeatedFlightSpawning = 5;

    float range;
    private int num;
    [SerializeField] List<string> pooledObjects;
    private void Start()
    {
        range = Random.Range(minimumTimeForRepeatedFlightSpawning, maximumTimeForRepeatedFlightSpawning);
        InvokeRepeating("SpawnObjects", 5, range);
    }
    public void SpawnObjects()
    {
        num = Random.Range(0, pooledObjects.Count);
        GameObject obj =  ObjectPool.SharedInstance.GetPooledObject(pooledObjects[num]);
        obj.transform.position = transform.position;
        obj.transform.rotation = transform.rotation;
        obj.SetActive(true);
    }
}
