using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{


    public Transform player;

    public float y_rotate;
    public float y_rotete_pre;


    void Start() {
        y_rotete_pre = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.localScale.x == 1) {
            y_rotate = 0f;
        }
        else if(player.localScale.x == -1) {
            y_rotate = 180f;
        }

        if (y_rotate != y_rotete_pre) {
            transform.Rotate(0f, 180f, 0f);
            y_rotete_pre = y_rotate;
        }
    }
}
