using System.Collections;
using UnityEngine;

public class ParatrooperDropper : MonoBehaviour
{
    public GameObject paratrooperPrefab;
    public float minDropInterval = 0.5f;
    public float maxDropInterval = 2.5f;
    public float doubleDropChance = 0.1f;
    float paratrooperDropChance; // 40% chance for paratrooper to drop
    private float dropInterval;
    private void OnEnable()
    {
        paratrooperDropChance = Random.Range(0.5f, 0.55f);
        StartCoroutine(DropParatroopers());
        dropInterval = Random.Range(minDropInterval, maxDropInterval);
    }
    private IEnumerator DropParatroopers()
    {
        // Calculate random X position and drop interval for each drop
        yield return new WaitForSeconds(dropInterval); // Delay before each drop
        // Determine if you will drop two paratroopers
        bool dropTwo = Random.Range(0f, 1f) <= doubleDropChance;
        yield return new WaitForSeconds(dropInterval);
        // Check if the plane's position matches the random X position
        print("dropstart");
        if (dropTwo)
        {
            for (int i = 0; i < 2; i++)
            {
                print("dropping 2 troopers");
                Instantiate(paratrooperPrefab, transform.position, Quaternion.identity);
            }
        }
        else
        {
            print("dropping 1 troopers");

            // Determine if the single paratrooper drops (40% chance)
            bool dropParatrooper = Random.Range(0f, 1f) <= paratrooperDropChance;

            if (dropParatrooper)
            {
                Instantiate(paratrooperPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}