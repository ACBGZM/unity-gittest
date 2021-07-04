using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    protected Animator enemyAnimator;
    protected Rigidbody2D rb;

    protected bool alive;

    protected AudioSource deathAudio;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        deathAudio = GetComponent<AudioSource>();


        alive = true;
    }


    // 播放完动画调用，后
    public void DestroyEnemy() {

        Destroy(gameObject);
    }


    // 判断死亡逻辑后调用，先
    public void EnemyDie() {

        deathAudio.Play();

        // 先禁用碰撞体，防止播放动画时还在判定
        GetComponent<Collider2D>().enabled = false;

        enemyAnimator.SetTrigger("death");
        alive = false;
    }

    
}
