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
    private bool canSpawn = true;
    private void OnEnable()
    {
        EventManager.enemyMoreThanTarget += DisableGameobject;
        StartCoroutine(SpawnObjectsWithDelay());
    }
    private void OnDisable()
    {
        EventManager.enemyMoreThanTarget -= DisableGameobject;
    }
    private void DisableGameobject()
    {
        canSpawn = false;
        gameObject.SetActive(false);
    }
    private IEnumerator SpawnObjectsWithDelay()
    {
        while (canSpawn)
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
