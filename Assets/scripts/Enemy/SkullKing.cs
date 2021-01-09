using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullKing : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] FireSkull skull;
    [SerializeField] GameObject skullKingPath;
    [SerializeField] float moveSpeed = 2f;

    Enemy enemy;
    bool SummonSkull = true;
    List<Transform> wayPoints;
    bool canMove = true;
    Rigidbody2D body;


    // index for controlling king movement
    int nextPosIndex = 0;
    int currentPosIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
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
            if(SummonSkull)
            {
                StartCoroutine(SummonSkulls());
            }

            if(canMove)
            {
                MoveToNextLocation();
            }
        }
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
        var targetPosition = wayPoints[nextPosIndex].transform.position;
        var movementThisFrame = moveSpeed * Time.deltaTime;

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
            Debug.Log("move");
            transform.position = Vector2.MoveTowards
                (transform.position, targetPosition, movementThisFrame);
        }
    }
}
