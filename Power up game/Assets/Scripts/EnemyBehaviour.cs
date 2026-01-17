using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class EnemyBehaviour : MonoBehaviour
{
    GameObject playerObj;
    HpSystem hpSys;
    LevelSystem levelSys;
    Rigidbody2D rb;
    public float enemySpeed;
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
            Destroy(this.gameObject);
            levelSys.xpCount += 10;
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
                    hpSys.shield -= 10;
                }
                else
                {
                    hpSys.hp -= 10;
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
