using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerController : MonoBehaviour
{
    public GameObject dialog;

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player") {
            dialog.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Player") {
            dialog.SetActive(false);
        }

    }
}
