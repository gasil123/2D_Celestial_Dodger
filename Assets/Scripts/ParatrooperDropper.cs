using System.Collections;
using UnityEngine;

public class ParatrooperDropper : MonoBehaviour
{
    [SerializeField] string[] soldierNames;
    [SerializeField] float minDropInterval = 0.5f;
    [SerializeField] float maxDropInterval = 2f;
    [SerializeField] float doubleDropChance = 0.1f;
    [SerializeField] float dropInterval;
    [SerializeField] float minimumDropchance = 0.3f;
    [SerializeField] float maximumDropchance = 0.55f;

    float paratrooperDropChance;
    int randomParatrooper;
    public bool canDrop = true;
    private void OnEnable()
    {
        EventManager.enemyMoreThanTarget += StopDropping;
        paratrooperDropChance = Random.Range(minimumDropchance, maximumDropchance);
        randomParatrooper = Random.Range(0, soldierNames.Length);
        dropInterval = Random.Range(minDropInterval, maxDropInterval);
        StartCoroutine(DropParatroopers());

    }
    private void OnDisable()
    {
        EventManager.enemyMoreThanTarget -= StopDropping;
    }
    private void StopDropping()
    {
        canDrop = false;
    }
    private IEnumerator DropParatroopers()
    {
        if (!canDrop)
        {
            yield break; // Exit the coroutine if canDrop is false
        }

        yield return new WaitForSeconds(dropInterval);

        if (Random.Range(0f, 1f) <= doubleDropChance)
        {
            for (int i = 0; i < 2; i++)
            {
                SpawnSoldier();
                yield return new WaitForSeconds(0.5f);
            }
        }
        else if (Random.Range(0f, 1f) <= paratrooperDropChance)
        {
            SpawnSoldier();
        }
    }

    private void SpawnSoldier()
    {
        GameObject soldier = GetSoldier();
        soldier.transform.position = transform.position;
        soldier.transform.rotation = Quaternion.identity;
        soldier.SetActive(true);
    }
    private GameObject GetSoldier()
    {
        GameObject soldier = ObjectPool.SharedInstance.GetPooledObject(soldierNames[randomParatrooper]);
        return soldier;
    }
}