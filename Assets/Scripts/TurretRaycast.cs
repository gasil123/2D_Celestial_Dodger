using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class TurretRaycast : MonoBehaviour
{
    [SerializeField] GameObject[] enemyClimbHelpersLeft;
    [SerializeField] GameObject[] enemyClimbHelpersRight;

    [SerializeField] Transform rightCast;
    [SerializeField] Transform leftCast;
 

    [SerializeField] int _maximumEnemyForGameOver;
    [SerializeField] float destinationThreshold;
    public LayerMask enemyLayer;


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
                    canMovie = false;
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
           StartCoroutine( MoveEnemiesToHelpers(enemyClimbHelpersLeft, leftCast, -transform.right));
        }
        else if (numberOfEnemyHitOnRight >= _maximumEnemyForGameOver)
        {
           StartCoroutine( MoveEnemiesToHelpers(enemyClimbHelpersRight, rightCast,transform.right));
        }

        yield return new WaitForSeconds(10f);
        enemiesMoving = false;
        canMovie = false;
    }
    IEnumerator MoveEnemiesToHelpers(GameObject[] helperPositions, Transform rayPosition, Vector3 direction)
    {
        for (int i = 0; i < helperPositions.Length; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(rayPosition.position, direction * 200, enemyLayer);

            NavMeshAgent agent = hit.collider.gameObject.GetComponent<NavMeshAgent>();
            agent.enabled = true;
            agent.updateUpAxis = false;
            agent.updateRotation = false;
            agent.SetDestination(helperPositions[i].transform.position);

            Rigidbody2D body = agent.gameObject.GetComponent<Rigidbody2D>();
            body.isKinematic = true;
            body.constraints = RigidbodyConstraints2D.FreezeAll;
            agent.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
           
            yield return new WaitForSeconds(7);
        }
    }
}
