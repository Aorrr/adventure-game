using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullKing : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] FireSkull skull;
    [SerializeField] GameObject skullKingPath;
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float SpawnSkullCD = 5f;
    [SerializeField] Fire iceFire;
    [SerializeField] kingScream scream;

    Sanity sanity;

    Enemy enemy;
    bool SummonSkull = true;
    List<Transform> wayPoints;
    bool canMove = true;
    Rigidbody2D body;
    Animator animator;
    EnemyBody enemyBody;


    // index for controlling king movement
    int nextPosIndex = 5;
    int currentPosIndex = 5;


    // For the sprint of fire skull king
    [SerializeField] float sprintCD = 3f;
    float sinceLastSprint = 0f;
    Vector2 targetPosition;
    float movementThisFrame;
    bool couldDamage = false;
    bool attacking = false;
    bool ifPrep = false;

    // timer for fire attack;
    float fireCD = 6f;
    float sinceLastFireAttack = 0f;

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
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);

            if (enemy.GetHealthPercentage() >= 2)
            {

                if (canMove)
                {
                    MoveToNextLocation();
                }

                if (SummonSkull)
                {
                    StartCoroutine(SummonSkulls());
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

        FireAttack();
        CorrectRotation();
    }

    IEnumerator SummonSkulls()
    {
        SummonSkull = false;
        Instantiate(skull, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(5);
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

    public void Sprint()
    {
        sinceLastSprint += Time.deltaTime;
        if (sinceLastSprint < sprintCD) { animator.SetBool("Fire", false);  return; }

        if(!attacking && !ifPrep)
        {
            scream.StartScream();
            animator.SetBool("Fire", true);
            StartCoroutine(PrepareFireAttack());
        } else if(attacking)
        {
            transform.position = Vector2.MoveTowards
            (transform.position, targetPosition, movementThisFrame);
            if (enemyBody.GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("Player")))
            {
                sanity.LoseSanity(enemy.GetDamage());
                couldDamage = false;
                attacking = false;
                animator.SetBool("Fire", false);
                sinceLastSprint = 0;
            }
            else if (transform.position.x == targetPosition.x && transform.position.y == targetPosition.y)
            {
                attacking = false;
                couldDamage = false;
                animator.SetBool("Fire", false);
                sinceLastSprint = 0;
            }
        } 

    }


    IEnumerator PrepareFireAttack()
    {
        ifPrep = true;
        yield return new WaitForSeconds(2);
        targetPosition = player.transform.position;
        movementThisFrame = moveSpeed * 4 * Time.deltaTime;
        couldDamage = true;
        attacking = true;
        ifPrep = false;
    }

    public void ToggleDamageStatus(bool status)
    {
        couldDamage = status;
    }

    private void FireAttack()
    {
        sinceLastFireAttack += Time.deltaTime;

        if(sinceLastFireAttack > fireCD - 1.5)
        {
            GetComponent<SpriteRenderer>().color = new Color(62 / 255f, 133 / 255f, 255 / 255f);
        }

        if(sinceLastFireAttack > fireCD)
        {
            Debug.Log("Fire");
            iceFire.StartFire();
            sinceLastFireAttack = 0;
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    private void CorrectRotation()
    {
        if(player.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x)
                , transform.localScale.y);
        } else
        {
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x)
    , transform.localScale.y);
        }
    }
}
