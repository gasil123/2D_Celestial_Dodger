using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurretRaycast : MonoBehaviour
{
    [SerializeField] GameObject[] enemyClimbHelpersLeft;
    [SerializeField] GameObject[] enemyClimbHelpersRight;
    [SerializeField] int _maximumEnemyForGameOver;

    public int numberOfEnemyHitOnRight;
    public int numberOfEnemyHitOnLeft;
    private bool CanSearchForEnemycount = true;
    private bool enemiesMoving = false; // Flag to check if enemies are currently moving
    private bool canMovie = true; // Flag to check if enemies are currently moving

    private void Update()
    {
        if (CanSearchForEnemycount && !enemiesMoving)
        {
            numberOfEnemyHitOnRight = RaycastAndFindenemyCount(transform.right);
            numberOfEnemyHitOnLeft = RaycastAndFindenemyCount(-transform.right);

            if (numberOfEnemyHitOnRight >= _maximumEnemyForGameOver ||
                numberOfEnemyHitOnLeft >= _maximumEnemyForGameOver)
            {
                EventManager.enemyMoreThanTarget?.Invoke();
                Debug.Log("Invoking enemy more than target event");
                if (!enemiesMoving && canMovie)
                {
                    StartCoroutine(MoveEnemiesToHelpers());
                }
            }
        }
    }

    private int RaycastAndFindenemyCount(Vector3 direction)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction);
        return hits.Length;
    }

    private IEnumerator MoveEnemiesToHelpers()
    {
        yield return new WaitForSeconds(4f);
        enemiesMoving = true; // Set the flag to indicate that enemies are moving

        if (numberOfEnemyHitOnLeft >= _maximumEnemyForGameOver)
        {
           StartCoroutine( MoveEnemiesToHelpers(enemyClimbHelpersLeft, -transform.right));
        }
        else if (numberOfEnemyHitOnRight >= _maximumEnemyForGameOver)
        {
           StartCoroutine( MoveEnemiesToHelpers(enemyClimbHelpersRight, transform.right));
        }

        // Wait for all enemies to reach their destinations
        yield return new WaitForSeconds(10f);

        // At this point, all enemies have reached their destinations
        // You can perform any required logic here

        enemiesMoving = false; // Reset the flag to indicate that enemies have stopped moving
        canMovie = false;
    }

    IEnumerator MoveEnemiesToHelpers(GameObject[] helperPositions, Vector3 direction)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction);

        for (int i = 0; i < helperPositions.Length; i++)
        {
            Collider2D colliderToMove = null;

            // Iterate through the hits to find the first game object with the "Soldier1" tag
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.CompareTag("Soldier1"))
                {
                    colliderToMove = hit.collider;
                    break;
                }
            }

            // Check if we found a game object with the "Soldier1" tag
            if (colliderToMove != null)
            {
                helperPositions[i].SetActive(true);
                Vector3 targetPosition = helperPositions[i].transform.position;
                StartCoroutine(SmoothMoveCollider(colliderToMove, targetPosition, 2f));

               
            }
            yield return new WaitForSeconds(5f);
        }
    }

    private IEnumerator SmoothMoveCollider(Collider2D colliderToMove, Vector3 targetPosition, float moveDuration)
    {
        float elapsedTime = 0f;
        Vector3 initialPosition = colliderToMove.transform.position;

        while (elapsedTime < moveDuration)
        {
            colliderToMove.transform.position = Vector3.MoveTowards(initialPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the collider ends up exactly at the target position
        colliderToMove.transform.position = targetPosition;
        colliderToMove.transform.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        colliderToMove.transform.gameObject.tag = "asd";
    }
}
