using System.Dynamic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class CoinProjectile : MonoBehaviour
{
    Rigidbody2D rb;
    PlayerShooting ps;
    CapsuleCollider2D coll;
    public GameObject linePrefab;
    public float lineDuration = 0.1f;
    Vector3 startPosition;
    Vector3 endPosition;
    public bool beenHit;
    public bool isLastProjectile;
    bool finalShot;
    public float baseDamage;
    float damage;
    float minDistance;
    Vector2 nextCoinPos;
    GameObject nextCoin;
    GameObject closestEnemy;
    float coinOffset = 2f;
    GameObject lineObj;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<CapsuleCollider2D>();
        ps = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerShooting>();
        rb.AddForce(transform.up * 5, ForceMode2D.Impulse);
        damage = baseDamage;
    }

    void FixedUpdate()
    {
        rb.angularVelocity = rb.linearVelocity.magnitude * 100;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ps.lastProjectile == this.gameObject)
        {
            if (collision.CompareTag("coin") == true)
            {
                Destroy(this.gameObject);
                collision.GetComponent<CoinProjectile>().Invoke("CheckNextCoin", lineDuration);
                if (collision.GetComponent<CoinProjectile>().beenHit == true)
                {
                    print("hasbeenhit");
                }  
            }
        }
        if (collision.CompareTag("enemy") == true)
        {
            EnemyBehaviour enemyScript = collision.gameObject.GetComponent<EnemyBehaviour>();
            enemyScript.enemyHealth -= damage;
            Destroy(this.gameObject);
        }
    }

    public void CheckNextCoin()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 20f);
        minDistance = Mathf.Infinity;
        foreach (Collider2D hit in hits)
        {
            CoinProjectile coin = hit.GetComponent<CoinProjectile>();
            if (hit.gameObject.CompareTag("coin") == true)
            {
                if (!coin.beenHit)
                {
                     if(hit.gameObject != this.gameObject)
                     {
                         float distance = (hit.transform.position - transform.position).sqrMagnitude;
                         if (distance < minDistance)
                         {
                             minDistance = distance;
                             nextCoinPos = hit.transform.position;
                             nextCoin = hit.gameObject;
                         }
                     }    
                }
            }
        }
        if (nextCoin != null && nextCoin.CompareTag("coin"))
        {
            DrawLine(nextCoinPos);
        }
        if (nextCoin == null)
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 20f);
            minDistance = Mathf.Infinity;
            foreach (Collider2D hit in hits)
            {
                if (hit.gameObject.CompareTag("enemy") == true)
                {
                    float distance = (hit.transform.position - transform.position).sqrMagnitude;
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        closestEnemy = hit.gameObject;
                    }
                }
            }
            if(closestEnemy != null)
            {
                EnemyBehaviour behaviour = closestEnemy.GetComponent<EnemyBehaviour>();
                behaviour.enemyHealth -= damage;
                DrawEnemyLine(closestEnemy.transform.position);
            }
            Destroy(this.gameObject, lineDuration);
        }
        
    }

    public void DrawLine(Vector3 endPos)
    {
        lineObj = Instantiate(linePrefab);
        LineRenderer lr = lineObj.GetComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, endPos);
        Invoke("NextCoin", lineDuration);
        Destroy(this.gameObject, lineDuration * 2);
    }

    public void NextCoin()
    {
        nextCoin.GetComponent<CoinProjectile>().beenHit = true;
        if (nextCoin != null)
        {
                nextCoin.GetComponent<CoinProjectile>().Invoke("CheckNextCoin", lineDuration);
            nextCoin.GetComponent<CoinProjectile>().damage += damage;
        }
        Destroy(lineObj);
        Destroy(this.gameObject);
    }

    void DrawEnemyLine(Vector3 endPos)
    {
        lineObj = Instantiate(linePrefab);
        LineRenderer lr = lineObj.GetComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, endPos);
        Destroy(lineObj, lineDuration);
    }
    private void Update()
    {
        if (ps.lastProjectile == this.gameObject && !finalShot)
        {
            isLastProjectile = true;
        }
        else isLastProjectile = false;
    }
}   
