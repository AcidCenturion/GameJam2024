using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovingPlatform : MonoBehaviour
{
    public Transform positionA, positionB;
    public int speed;
    Vector2 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = positionB.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, positionA.position) < .1f) targetPosition = positionB.position;

        if (Vector2.Distance(transform.position, positionB.position) < .1f) targetPosition = positionA.position;

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    /*private void OnTriggerEnterTrigger2D(Collider2D collision)
    {
        if (collision.CompareTag("Plyaer"))
        {
            collision.transform.SetParent(this.transform);
        }
    }*/
}
