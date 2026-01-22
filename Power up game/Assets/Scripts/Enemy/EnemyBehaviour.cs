using TMPro;
using UnityEngine;
public class EnemyBehaviour : MonoBehaviour
{
    GameObject playerObj;
    HpSystem hpSys;
    LevelSystem levelSys;
    Rigidbody2D rb;
    public Canvas canvas;
    public float enemySpeed;
    public int enemyDamage;
    Vector2 playerVector;
    bool canHit;
    public float iFrames;
    public float enemyBaseHealth;
    public float enemyHealth;
    public GameObject jokerCardPrefab;
    public int treasureChance;
    public bool canMove;
    public bool dashing;
    public GameObject damageText;
    bool hasDied;
    private void Start()
    {
        enemyHealth = enemyBaseHealth;
        canHit = true;
        playerObj = GameObject.FindGameObjectWithTag("player");
        rb = GetComponent<Rigidbody2D>();
        hpSys = playerObj.GetComponent<HpSystem>();
        levelSys = playerObj.GetComponent<LevelSystem>();
        treasureChance = Random.Range(0, 51);
        canMove = true;
    }
    private void Update()
    {
        if (enemyHealth <= 0 && !hasDied)
        {
            if(treasureChance == 0)
            {
                Instantiate(jokerCardPrefab, transform.position, Quaternion.identity);
            }
            levelSys.xpCount += 10;
            Destroy(this.gameObject, 0.5f);
            canMove = false;
            hasDied = true;
        }
    }

    private void FixedUpdate()
    {
        playerVector = transform.position - playerObj.transform.position;
        if (canMove == true)
        {
            rb.linearVelocity = (-playerVector.normalized * enemySpeed);
        }
        else
        {
            if (!dashing)
            {
                rb.linearVelocity = new Vector2(0, 0);
            }
        }
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

    public void DamageTakenText(float damageTaken)
    {
        GameObject damageTextObj = Instantiate(damageText, canvas.transform);
        TextMeshProUGUI textMesh = damageTextObj.GetComponent<TextMeshProUGUI>();
        textMesh.text = damageTaken.ToString();
    }
}
