using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    private int num;
    [SerializeField] List<string> pooledObjects;
    private void Start()
    {
        InvokeRepeating("SpawnObjects", 5, 2);
    }
    public void SpawnObjects()
    {
        Debug.Log("spawning");
        num = Random.Range(0, pooledObjects.Count);
        GameObject obj =  ObjectPool.SharedInstance.GetPooledObject(pooledObjects[0]);
        obj.transform.position = transform.position;
        obj.transform.rotation = transform.rotation;
        obj.SetActive(true);
    }
}
