using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpossumController : Enemy
{
    public Transform left_edge, right_edge;
    private float left_edge_x, right_edge_x;

    public bool toLeft;

    public float speed;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        left_edge_x = left_edge.position.x;
        Destroy(left_edge.gameObject);
        right_edge_x = right_edge.position.x;
        Destroy(right_edge.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (alive) {
            Movement();
        }
    }


    void Movement() {
        float left_dis = transform.position.x - left_edge_x;
        float right_dis = right_edge_x - transform.position.x;

        if (toLeft) {
            if(0< left_dis) {
                transform.localScale = new Vector3(1, 1, 1);
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
            else {
                toLeft = false;
            }
        }
        else {
            if (0 < right_dis) {
                transform.localScale = new Vector3(-1, 1, 1);
                rb.velocity = new Vector2(+speed, rb.velocity.y);
            }
            else {
                toLeft = true;
            }
        }
    }

}
