using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePedal : MonoBehaviour
{
    public float x1;
    public float x2;
    public float y1;
    public float y2;
    public float speed;
    public int dir = 0; // 0:left, 1:right

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    private void move()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        Vector2 target;
        if (dir == 0)
        {
            target = new Vector2(x1, y1);
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        } 
        else if (dir == 1)
        {
            target = new Vector2(x2, y2);
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
        
        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(x2, y2)) < 0.01)
        {
            dir = 0;
        }
        else if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(x1, y1)) < 0.01)
        {
            dir = 1;      
        }
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        GameObject obj = collision.gameObject;
        Vector2 target;
        if (dir == 1)
        {
            target = new Vector2(x2, obj.transform.position.y);
        }
        else
        {
            target = new Vector2(x1, obj.transform.position.y);
        }
        obj.transform.position = Vector2.MoveTowards(obj.transform.position, target, speed * Time.deltaTime);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        return; 
    }

}
