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


    // �����궯�����ã���
    public void DestroyEnemy() {

        Destroy(gameObject);
    }


    // �ж������߼�����ã���
    public void EnemyDie() {

        deathAudio.Play();

        // �Ƚ�����ײ�壬��ֹ���Ŷ���ʱ�����ж�
        GetComponent<Collider2D>().enabled = false;

        enemyAnimator.SetTrigger("death");
        alive = false;
    }

    
}
