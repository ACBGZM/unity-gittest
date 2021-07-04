using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gemController : MonoBehaviour
{
    public void GotGem() {
        FindObjectOfType<PlayerController>().GemCountAdd();
        Destroy(gameObject);
    }
}
