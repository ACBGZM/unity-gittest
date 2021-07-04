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


    // 碰撞体、地面头顶检测
    public Collider2D coll_small;
    public Collider2D coll_big;
    public LayerMask ground;
    public Transform groundCheck;

    public Transform ceilingPoint;


    // 判断跳跃、下蹲
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
        // GetButtonDown 需要在Update里使用

        // 检查下蹲键
        Crouch();

        if (!ishurt) {
           // GroundMovement();
            Jump();     
        }


    }

    void FixedUpdate() {
        // 物理检测的部分在FixedUpdate写
        // 判断是否在地面上
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
            // isJumpPressed = false; 的作用是：每FixedUpdate0.02s，检测跳跃键是否按下。
            // 按跳的时候在地上
            if (isOnGround) {
                rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            }
            // 在空中
            else {
                rb.velocity = new Vector2(rb.velocity.x, jumpforce * 0.7f);
            }

            extraJump--;

            playeranimator.SetBool(name = "jumping", true);

            SoundManager.instance.PlayJumpAudio(); // 调用SoundManager的静态单例播放声音
        }
    }


    void Crouch() {
        // 头顶没有ground障碍物时，才可以切换下蹲形态
        if (!Physics2D.OverlapCircle(ceilingPoint.position, 0.2f, ground)) {
            if (Input.GetButtonDown("Crouch")) {
                // 蹲的时候按，退出蹲状态
                if (isCrouching) {
                    isCrouching = false;
                    playeranimator.SetBool(name = "crouching", false);
                    coll_big.enabled = true;
                    firePoint.position = new Vector3(firePoint.position.x, firePoint.position.y + 0.15f, firePoint.position.z);
                }
                // 非蹲按，进入蹲状态
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

        // 如果速度向下，falling设置为真；如果是跳跃后的下降，把jumping设置为假
        if (rb.velocity.y < 0) {
            playeranimator.SetBool(name = "falling", true);

            if (playeranimator.GetBool(name = "jumping")==true) {
                playeranimator.SetBool(name = "jumping", false);
            }
        }

        // 碰到地面，falling设置为假；idle设置为真
        if (coll_small.IsTouchingLayers(ground)) {
            
            playeranimator.SetBool(name = "falling", false);

        }

        // 受伤动画和受伤结束
        if(ishurt == true) {
            playeranimator.SetBool("hurt", true);
            playeranimator.SetFloat("running", 0.0f);
            if (Mathf.Abs(rb.velocity.x) < 0.5) {
                ishurt = false;
                playeranimator.SetBool("hurt", false);
            }
        }
    }



    // Trigger判断
    private void OnTriggerEnter2D(Collider2D collision) {
        // 收集物品
        if(collision.tag == "Items_cherry") {

            SoundManager.instance.PlayGetCherryAudio();

            //Destroy(collision.gameObject);
            //num_getcherry++;
            // 销毁，+1都在动画event调用，相当于设置延迟防止重复判断碰撞。更新数字放在Update，防止延迟
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

        // 下落到deadline，重置当前场景
        if(collision.tag == "DeadLine") {
            GetComponent<AudioSource>().enabled = false; // 禁用所有音乐
            restartDialog.SetActive(true);
            Invoke(name = "RestartLevel", 1.5f);
        }

    }


    // Collidion判断
    // 下落中碰到Enemies消灭
    // 由于enemeis有rigidbody，不能把collider设置为trigger，否则enemies会掉下ground。
    // 使用 OnCollisionEnter2D 函数
    private void OnCollisionEnter2D(Collision2D collision) {
        
        
        if(collision.gameObject.tag == "Enemy") {

            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            // 下落过程中碰到敌人，并且敌人位置在玩家的低处，小跳 + 消灭
            if (playeranimator.GetBool("falling") == true && collision.gameObject.transform.position.y < this.transform.position.y) {
                enemy.EnemyDie(); // 在Enemy类中执行死亡动画和Destroy操作
                //Destroy(collision.gameObject);
                rb.velocity = new Vector2(rb.velocity.x, jumpforce * 0.6f);
                playeranimator.SetBool(name = "jumping", true);
            }
            else {
                // 从左、右边碰到敌人，后退 + 掉血
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

