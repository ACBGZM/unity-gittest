using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator playeranimator;

    public float speed = 350.0f;
    public float jumpforce;


    // ��ײ�塢����ͷ�����
    public Collider2D coll_small;
    public Collider2D coll_big;
    public LayerMask ground;
    public Transform groundCheck;

    public Transform ceilingPoint;


    // �ж���Ծ���¶�
    public bool isOnGround, isJump;
    public int extraJumpValue;
    private int extraJump;

    public bool isCrouching;

    public int num_getcherry = 0;
    public int num_getgem = 0;

    public Text cherry_get;
    public Text gem_get;

    public int health = 10;
    private bool ishurt = false;

 
    public GameObject restartDialog;

    public Transform firePoint;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playeranimator = GetComponent<Animator>();

        extraJump = extraJumpValue;
    }

    private void Update() {
        // GetButtonDown ��Ҫ��Update��ʹ��

        // ����¶׼�
        Crouch();

        if (!ishurt) {
           // GroundMovement();
            Jump();     
        }


    }

    void FixedUpdate() {
        // ������Ĳ�����FixedUpdateд
        // �ж��Ƿ��ڵ�����
        isOnGround = Physics2D.OverlapCircle(groundCheck.position, 0.05f, ground);

        if (!ishurt) {
            GroundMovement();
            //Jump();
        }
        SwitchAnimation();
    }


    void GroundMovement() {
        float horizontalmove = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalmove * speed * Time.fixedDeltaTime, rb.velocity.y);
        playeranimator.SetFloat(name = "running", Mathf.Abs(horizontalmove));

        if (horizontalmove != 0) {
            transform.localScale = new Vector3(horizontalmove, 1, 1);
        }
    }

    void Jump() {

        if (isOnGround) {
            extraJump = extraJumpValue;
        }

        if (Input.GetButtonDown("Jump") && extraJump > 0) {
            // isJumpPressed = false; �������ǣ�ÿFixedUpdate0.02s�������Ծ���Ƿ��¡�
            // ������ʱ���ڵ���
            if (isOnGround) {
                rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            }
            // �ڿ���
            else {
                rb.velocity = new Vector2(rb.velocity.x, jumpforce * 0.7f);
            }

            extraJump--;

            playeranimator.SetBool(name = "jumping", true);

            SoundManager.instance.PlayJumpAudio(); // ����SoundManager�ľ�̬������������
        }
    }


    void Crouch() {
        // ͷ��û��ground�ϰ���ʱ���ſ����л��¶���̬
        if (!Physics2D.OverlapCircle(ceilingPoint.position, 0.2f, ground)) {
            if (Input.GetButtonDown("Crouch")) {
                // �׵�ʱ�򰴣��˳���״̬
                if (isCrouching) {
                    isCrouching = false;
                    playeranimator.SetBool(name = "crouching", false);
                    coll_big.enabled = true;
                    firePoint.position = new Vector3(firePoint.position.x, firePoint.position.y + 0.15f, firePoint.position.z);
                }
                // �Ƕװ��������״̬
                else {
                    isCrouching = true;
                    playeranimator.SetBool(name = "crouching", true);
                    coll_big.enabled = false;
                    firePoint.position = new Vector3(firePoint.position.x, firePoint.position.y - 0.15f, firePoint.position.z);
                }
            }
        }
        
    }


    void SwitchAnimation() {

        // ����ٶ����£�falling����Ϊ�棻�������Ծ����½�����jumping����Ϊ��
        if (rb.velocity.y < 0) {
            playeranimator.SetBool(name = "falling", true);

            if (playeranimator.GetBool(name = "jumping")==true) {
                playeranimator.SetBool(name = "jumping", false);
            }
        }

        // �������棬falling����Ϊ�٣�idle����Ϊ��
        if (coll_small.IsTouchingLayers(ground)) {
            
            playeranimator.SetBool(name = "falling", false);

        }

        // ���˶��������˽���
        if(ishurt == true) {
            playeranimator.SetBool("hurt", true);
            playeranimator.SetFloat("running", 0.0f);
            if (Mathf.Abs(rb.velocity.x) < 0.5) {
                ishurt = false;
                playeranimator.SetBool("hurt", false);
            }
        }
    }



    // Trigger�ж�
    private void OnTriggerEnter2D(Collider2D collision) {
        // �ռ���Ʒ
        if(collision.tag == "Items_cherry") {

            SoundManager.instance.PlayGetCherryAudio();

            //Destroy(collision.gameObject);
            //num_getcherry++;
            // ���٣�+1���ڶ���event���ã��൱�������ӳٷ�ֹ�ظ��ж���ײ���������ַ���Update����ֹ�ӳ�
            collision.gameObject.GetComponent<Animator>().Play("cherry_got");
            //cherry_get.text = num_getcherry.ToString();
        }
        if (collision.tag == "Items_gem") {

            SoundManager.instance.PlayGetGemAudio();

            //Destroy(collision.gameObject);
            //num_getgem++;
            collision.gameObject.GetComponent<Animator>().Play("gem_got");
            //gem_get.text = num_getgem.ToString();
        }

        // ���䵽deadline�����õ�ǰ����
        if(collision.tag == "DeadLine") {
            GetComponent<AudioSource>().enabled = false; // ������������
            restartDialog.SetActive(true);
            Invoke(name = "RestartLevel", 1.5f);
        }

    }


    // Collidion�ж�
    // ����������Enemies����
    // ����enemeis��rigidbody�����ܰ�collider����Ϊtrigger������enemies�����ground��
    // ʹ�� OnCollisionEnter2D ����
    private void OnCollisionEnter2D(Collision2D collision) {
        
        
        if(collision.gameObject.tag == "Enemy") {

            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            // ����������������ˣ����ҵ���λ������ҵĵʹ���С�� + ����
            if (playeranimator.GetBool("falling") == true && collision.gameObject.transform.position.y < this.transform.position.y) {
                enemy.EnemyDie(); // ��Enemy����ִ������������Destroy����
                //Destroy(collision.gameObject);
                rb.velocity = new Vector2(rb.velocity.x, jumpforce * 0.6f);
                playeranimator.SetBool(name = "jumping", true);
            }
            else {
                // �����ұ��������ˣ����� + ��Ѫ
                if (this.transform.position.x < collision.gameObject.transform.position.x) {
                    this.rb.velocity = new Vector2(-6, rb.velocity.y);
                    ishurt = true;
                    health -= 1;
                }
                else if (this.transform.position.x > collision.gameObject.transform.position.x) {
                    this.rb.velocity = new Vector2(+6, rb.velocity.y);
                    ishurt = true;
                    health -= 1;
                }


                SoundManager.instance.PlayHurtAudio();

            }

        }
        
    }
    

    void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void CherryCountAdd() {
        num_getcherry += 1;
        cherry_get.text = num_getcherry.ToString();
    } 
    public void GemCountAdd() {
        num_getgem += 1;
        gem_get.text = num_getgem.ToString();
    }
}

