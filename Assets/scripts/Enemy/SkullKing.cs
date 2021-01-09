using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullKing : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] FireSkull skull;
    [SerializeField] GameObject skullKingPath;
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float SpawnSkullCD = 4f;

    Sanity sanity;

    Enemy enemy;
    bool SummonSkull = true;
    List<Transform> wayPoints;
    bool canMove = true;
    Rigidbody2D body;
    Animator animator;
    EnemyBody enemyBody;


    // index for controlling king movement
    int nextPosIndex = 0;
    int currentPosIndex = 0;


    // For the sprint of fire skull king
    float sprintCD = 2.5f;
    float sinceLastSprint = 0f;
    Vector2 targetPosition;
    float movementThisFrame;
    bool couldDamage = false;

    // Start is called before the first frame update
    void Start()
    {
        sanity = FindObjectOfType<Sanity>();
        enemyBody = GetComponentInChildren<EnemyBody>();
        animator = GetComponent<Animator>();
        enemy = GetComponent < Enemy > ();
        wayPoints = new List<Transform>();
        body = GetComponent<Rigidbody2D>();
        foreach(Transform child in skullKingPath.transform)
        {
            wayPoints.Add(child);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy.IfRage())
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);

            if (enemy.GetHealthPercentage() >= 0.6)
            {
                if (SummonSkull)
                {
                    StartCoroutine(SummonSkulls());
                }

                if (canMove)
                {
                    MoveToNextLocation();
                }
            } else if(enemy.GetHealthPercentage() > 0)
            {
                nextPosIndex = 5;
                if(currentPosIndex != 5)
                {
                    MoveToNextLocation();
                } else
                {
                    Sprint();
                }
            }
        }

        animator.SetBool("Fire", couldDamage);
    }

    IEnumerator SummonSkulls()
    {
        SummonSkull = false;
        Instantiate(skull, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(10);
        SummonSkull = true;
    }

    public void MoveToNextLocation()
    {
        targetPosition = wayPoints[nextPosIndex].transform.position;
        movementThisFrame = moveSpeed * Time.deltaTime;

        if (transform.position.x == targetPosition.x && transform.position.y == targetPosition.y)
        {
            currentPosIndex = nextPosIndex;
            nextPosIndex = Random.Range(0, 5);
            if (nextPosIndex == currentPosIndex)
            {
                nextPosIndex = (currentPosIndex + 1) % 5;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards
                (transform.position, targetPosition, movementThisFrame);
        }
    }

    // The ice skull king sprints towards the player
    public void Sprint()
    {

        sinceLastSprint += Time.deltaTime;
        if(sinceLastSprint > sprintCD)
        {
            animator.SetBool("Fire", true);
            transform.position = Vector2.MoveTowards
            (transform.position, targetPosition, movementThisFrame);

            if(enemyBody.GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("Player")))
            {
                if(couldDamage)
                {
                    sanity.LoseSanity(enemy.GetDamage());
                    couldDamage = false;
                }
            }

        } else
        {
            animator.SetBool("Fire", false);
            targetPosition = player.transform.position;
            movementThisFrame = moveSpeed * 2 * Time.deltaTime;
        }

        if(transform.position.x == targetPosition.x && transform.position.y == targetPosition.y)
        {
            sinceLastSprint = 0;
        }
    }

    public void ToggleDamageStatus(bool status)
    {
        couldDamage = status;
    }
}
