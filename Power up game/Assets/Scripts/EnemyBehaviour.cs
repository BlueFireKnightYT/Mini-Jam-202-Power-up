using UnityEngine;
public class EnemyBehaviour : MonoBehaviour
{
    GameObject playerObj;
    HpSystem hpSys;
    LevelSystem levelSys;
    Rigidbody2D rb;
    public float enemySpeed;
    public int enemyDamage;
    Vector2 playerVector;
    bool canHit;
    public float iFrames;
    public float enemyBaseHealth;
    public float enemyHealth;

    private void Start()
    {
        enemyHealth = enemyBaseHealth;
        canHit = true;
        playerObj = GameObject.FindGameObjectWithTag("player");
        rb = GetComponent<Rigidbody2D>();
        hpSys = playerObj.GetComponent<HpSystem>();
        levelSys = playerObj.GetComponent<LevelSystem>();
    }
    private void Update()
    {
        if (enemyHealth <= 0)
        {
            levelSys.xpCount += 10;
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        playerVector = transform.position - playerObj.transform.position;
        rb.linearVelocity = (-playerVector.normalized * enemySpeed);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            if (canHit)
            {
                if(hpSys.shield > 0)
                {
                    hpSys.shield -= enemyDamage;
                }
                else
                {
                    hpSys.hp -= enemyDamage;
                } 
                canHit = false;
                Invoke("HitCooldown", iFrames);
            }
            

        }
    }

    void HitCooldown()
    {
        canHit = true;
    }
}
