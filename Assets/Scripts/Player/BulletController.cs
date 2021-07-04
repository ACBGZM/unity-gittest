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
            // 播放子弹爆炸动画、销毁子弹。子弹爆炸动画的销毁在自己的类里
            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);


            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.EnemyDie(); // 在Enemy类中执行enemy的死亡动画和Destroy操作
            
        }

        if(collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Environment") {

            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);

        }
    }


}
