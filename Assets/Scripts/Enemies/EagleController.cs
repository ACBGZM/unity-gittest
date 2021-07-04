using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleController : Enemy
{
    public Transform up_edge, down_edge;
    private float up_edge_y, down_edge_y;
    private bool start_down;

    public float speed;

    //private Rigidbody2D rb;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();


        start_down = true;

        up_edge_y = up_edge.transform.position.y;
        Destroy(up_edge.gameObject);
        down_edge_y = down_edge.transform.position.y;
        Destroy(down_edge.gameObject);

        //rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
    void Movement() {
        if (start_down) {
            if(transform.position.y > down_edge_y) {
                rb.velocity = new Vector2(0, -speed);
            }
            else {
                start_down = false;
            }
        }
        else {
            if (transform.position.y < up_edge_y) {
                rb.velocity = new Vector2(0, +speed);
            }
            else {
                start_down = true;
            }

        }
        
    }
}
