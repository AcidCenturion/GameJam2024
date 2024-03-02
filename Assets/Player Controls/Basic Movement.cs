using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    Rigidbody2D rb;

    // SerializedField means these values can be changed from Unity interface under this script
    [SerializeField] float moveForce;
    [SerializeField] private float jumpForce;

    private bool isJumping = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        /*rb.AddForce(Input.GetAxis("Horizontal") * moveForce * transform.right, ForceMode2D.Force);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }*/

        // a & d or left arrow key & right arrow key for left and right
        if (Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxisRaw("Horizontal") < 0)
        {
            rb.AddForce(new Vector2(Input.GetAxisRaw("Horizontal") * moveForce, 0f), ForceMode2D.Impulse);
        }

        // W & Space bar are jump buttons
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && !isJumping)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);

        }


    }

    // detects when collider makes contact with object with tag "Platform"
    // set isJumping to true since this means they are on the ground and can jump
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isJumping = false;
        }
    }

    // detects when colliderno longer makes contact with object with tag "Platform"
    // set isJumping to false since it means they jumped and can't double jump
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isJumping = true;
        }
    }
}
