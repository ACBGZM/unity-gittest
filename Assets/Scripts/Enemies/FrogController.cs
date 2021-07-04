using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : Enemy
{

    //private Rigidbody2D rb;

    public Transform left_edge, right_edge;
    private float left_edge_x, right_edge_x;

    public Collider2D coll;
    public LayerMask ground;

    public float speed;
    public float jumpforce;

    public bool faceleft;
    private bool jumping;


    //public Animator froganimator;


    // Start is called before the first frame update
    protected override void Start() {

        base.Start();

        //rb = GetComponent<Rigidbody2D>();

        left_edge_x = left_edge.position.x;
        Destroy(left_edge.gameObject);
        right_edge_x = right_edge.position.x;
        Destroy(right_edge.gameObject);
    }

    // Update is called once per frame
    void Update() {
        // Movement();
        if (alive) {

            SwitchAnimation();
        }
    }

    void Movement() {

        float left_distance = transform.position.x - left_edge_x;
        float right_distance = right_edge_x - transform.position.x;

        // 左边有空间，并且在地上，就向左跳，没空间就转向
        if (faceleft) {
            if (left_distance - speed >= 0) {
                if (coll.IsTouchingLayers(ground)) {
                    transform.localScale = new Vector3(1, 1, 1);
                    jumping = true;
                    rb.velocity = new Vector2(-speed, jumpforce);
                }
            }
            else {
                faceleft = false;
            }
        }
        // 右边有空间，并且在地上，就向右跳，没空间就转向
        else {
            if (right_distance - speed >= 0) {
                if (coll.IsTouchingLayers(ground)) {
                    transform.localScale = new Vector3(-1, 1, 1);
                    jumping = true;
                    rb.velocity = new Vector2(+speed, jumpforce);
                }
            }
            else {
                faceleft = true;
            }
        }
    }

    void SwitchAnimation() {
        if (jumping == true) {
            enemyAnimator.SetBool(name = "jumping", true);
            jumping = false;
        }
        if (rb.velocity.y < 0 && !coll.IsTouchingLayers(ground)) {
            enemyAnimator.SetBool(name = "falling", true);
            enemyAnimator.SetBool(name = "jumping", false);
        }
        if (coll.IsTouchingLayers(ground)) {
            enemyAnimator.SetBool(name = "falling", false);
        }

    }

}
