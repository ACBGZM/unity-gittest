using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cherryController : MonoBehaviour
{
    
    public void GotCherry() {
        FindObjectOfType<PlayerController>().CherryCountAdd();
        Destroy(gameObject);
    }


}
