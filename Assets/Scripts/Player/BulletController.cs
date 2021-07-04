using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 7f;
    public Rigidbody2D rb;

    public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }


    private void Update() {
        if (transform.position.x < -40 || transform.position.x > 70) {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            // �����ӵ���ը�����������ӵ����ӵ���ը�������������Լ�������
            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);


            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.EnemyDie(); // ��Enemy����ִ��enemy������������Destroy����
            
        }

        if(collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Environment") {

            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);

        }
    }


}
