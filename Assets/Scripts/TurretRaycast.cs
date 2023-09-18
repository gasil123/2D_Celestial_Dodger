using System.Collections;
using UnityEngine;

public class TurretRaycast : MonoBehaviour
{
    [SerializeField] GameObject[] enemyClimbHelpersLeft;
    [SerializeField] GameObject[] enemyClimbHelpersRight;
    [SerializeField] int _maximumEnemyForGameOver;

    public int numberOfEnemyHitOnRight;
    public int numberOfEnemyHitOnLeft;
    private bool CanSearchForEnemycount = true;
    private bool enemiesMoving = false; 
    private bool canMovie = true;

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
                Debug.Log("Invoking enemymorethantarget event");
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
        enemiesMoving = true;

        if (numberOfEnemyHitOnLeft >= _maximumEnemyForGameOver)
        {
           StartCoroutine( MoveEnemiesToHelpers(enemyClimbHelpersLeft, -transform.right));
        }
        else if (numberOfEnemyHitOnRight >= _maximumEnemyForGameOver)
        {
           StartCoroutine( MoveEnemiesToHelpers(enemyClimbHelpersRight, transform.right));
        }

        yield return new WaitForSeconds(10f);
        enemiesMoving = false;
        canMovie = false;
    }

    IEnumerator MoveEnemiesToHelpers(GameObject[] helperPositions, Vector3 direction)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction);

        for (int i = 0; i < helperPositions.Length; i++)
        {
            Collider2D colliderToMove = null;

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.CompareTag("Soldier1"))
                {
                    colliderToMove = hit.collider;
                    break;
                }
            }
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
        colliderToMove.transform.position = targetPosition;
        colliderToMove.transform.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        colliderToMove.transform.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        colliderToMove.transform.gameObject.tag = "asd";
    }
}
