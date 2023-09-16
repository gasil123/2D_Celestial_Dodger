using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem
{
    public int amoutToPool;
    public GameObject objectToPool;
    public bool shouldExpand;
}

public class ObjectPool : MonoBehaviour
{

    public List<ObjectPoolItem> itemsToPool;

    public static ObjectPool SharedInstance;
    public List<GameObject> pooledObjects;
    public Transform _poolPosition;
    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        pooledObjects = new List<GameObject>();
        foreach(ObjectPoolItem item in itemsToPool)
        {
            for (int i = 0; i < item.amoutToPool; i++)
            {
                GameObject obj = Instantiate(item.objectToPool, _poolPosition);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }
        
    }
    public GameObject GetPooledObject(string tag)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
            {
                return pooledObjects[i];
            }
        }
        foreach(ObjectPoolItem item in itemsToPool)
        {
            if(item.objectToPool.tag == tag)
            {
                if (item.shouldExpand)
                {
                    GameObject obj = Instantiate(item.objectToPool, _poolPosition);
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                    return obj;
                }
            }
        }
        return null;
    }
}
