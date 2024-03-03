using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer spr;

    // SerializedField means these values can be changed from Unity interface under this script
    [SerializeField] float moveForce;
    [SerializeField] private float jumpForce;
    [SerializeField] private float totalDashes = 1f;

    // time is in seconds
    [SerializeField] private float dashCooldownTime = 1f;
    [SerializeField] private float dashLength = .2f;

    [SerializeField] private float dashForce;

    private bool isJumping = false;
    private Vector3 spawnPoint;
    private float horizontalMovement;
    private float verticalMovement;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        spr = gameObject.GetComponent<SpriteRenderer>();
        spawnPoint = transform.position;
    
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        // a & d or left arrow key & right arrow key for left and right
        if (horizontalMovement > 0 || horizontalMovement < 0)
        {
            rb.AddForce(new Vector2(horizontalMovement * moveForce, 0f), ForceMode2D.Impulse);
        }

        // W & Space bar are jump buttons
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && !isJumping)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);

        }

        // LShift for dash. Requires movement
        if ((horizontalMovement > 0 || horizontalMovement < 0 || verticalMovement > 0) && Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(dash());
        }

        //Flip sprite base on direction of movement
        if(horizontalMovement < 0)
        {
            spr.flipX = true;
        }
        if(horizontalMovement > 0)
        {
            spr.flipX = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // detects when collider makes contact with object with tag "Platform"
        // set isJumping to false since this means they are on the ground and can jump
        if (collision.gameObject.tag == "Platform")
        {
            isJumping = false;
        }

        // detects when players enters a death barrier
        // set player position to most recent respawn point
        if(collision.gameObject.tag == "DeathBarrier")
        {
            transform.position = spawnPoint;
        }

        // detects if player makes it through checkpoint
        // set the players new respawn point to the checkpoint
        if(collision.gameObject.tag == "Respawn")
        {
            spawnPoint = transform.position;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // detects when collider no longer makes contact with object with tag "Platform"
        // set isJumping to true since it means they jumped and can't double jump
        if (collision.gameObject.tag == "Platform")
        {
            isJumping = true;
        }
    }

    // Dash function that takes into account cooldown, total dashes, direction, and gravity
    IEnumerator dash()
    {
        if (totalDashes > 0)
        {
            rb.AddForce(new Vector2(horizontalMovement * dashForce, verticalMovement * dashForce), ForceMode2D.Impulse);
            totalDashes--;
            float originalGravity = rb.gravityScale;
            rb.gravityScale = 0f; // prevent height loss when dashing
            yield return new WaitForSeconds(dashLength);
            rb.gravityScale = originalGravity; // return gravity
            yield return new WaitForSeconds(dashCooldownTime);
            totalDashes++;
        }
    }
}
