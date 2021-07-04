using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTest : MonoBehaviour
{
    public Transform groundCheck;
    public bool isGrounded;
    public LayerMask ground;

    private Rigidbody2D rb;

    public int jumpTimesValue;
    private int jumpTimes;

    private float moveInput;

    public float speed;

    void Start()
    {
        jumpTimes = jumpTimesValue;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.01f, ground);
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }
   
    void Update()
    {
        if (isGrounded) {
            jumpTimes = jumpTimesValue;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && jumpTimes > 0) {
            if (isGrounded) {
                rb.velocity = new Vector2(rb.velocity.x, speed);
            }
            else{
                rb.velocity = new Vector2(rb.velocity.x, speed*0.5f);
            }
            jumpTimes--;
        }
    }
}
