using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    int range;
    private int num;
    [SerializeField] List<string> pooledObjects;
    private void Start()
    {
        range = Random.Range(2, 5);
        InvokeRepeating("SpawnObjects", 5, range);
    }
    public void SpawnObjects()
    {
        num = Random.Range(0, pooledObjects.Count);
        GameObject obj =  ObjectPool.SharedInstance.GetPooledObject(pooledObjects[0]);
        obj.transform.position = transform.position;
        obj.transform.rotation = transform.rotation;
        obj.SetActive(true);
    }
}
