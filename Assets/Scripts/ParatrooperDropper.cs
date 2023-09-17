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
    private void OnEnable()
    {
        paratrooperDropChance = Random.Range(minimumDropchance, maximumDropchance);
        randomParatrooper = Random.Range(0, soldierNames.Length);
        dropInterval = Random.Range(minDropInterval, maxDropInterval);
        StartCoroutine(DropParatroopers());
    }
    private IEnumerator DropParatroopers()
    {      
        bool dropTwo = Random.Range(0f, 1f) <= doubleDropChance;
        yield return new WaitForSeconds(dropInterval);
       
        print("dropstart");
        if (dropTwo)
        {
            for (int i = 0; i < 2; i++)
            {
                print("dropping 2 troopers");
                SpawnSoldier();
                yield return new WaitForSeconds(0.5f);
            }
        }
        else
        {
            print("dropping 1 trooper");
            bool dropParatrooper = Random.Range(0f, 1f) <= paratrooperDropChance;

            if (dropParatrooper)
            {
                SpawnSoldier();
            }
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