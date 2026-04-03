using System.Dynamic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;

public class CoinProjectile : MonoBehaviour
{
    Rigidbody2D rb;
    public PlayerShooting ps;
    public GameObject linePrefab;
    public float lineDuration = 0.1f;
    public bool beenHit;
    public bool isLastProjectile;
    bool finalShot;
    float minDistance;
    Vector2 nextCoinPos;
    public GameObject nextCoin;
    GameObject lineObj;
    public ChipWeapon chipAttributes;
    public float currentDamage;
    public int currentLifesteal;

    public event Action onHit;
    public event Action onRicocchet;
    public List<(GameObject enemy, float distance)> enemyDistances = new List<(GameObject, float)>();

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ps = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerShooting>();
        rb.AddRelativeForce(Vector2.up * chipAttributes.speed, ForceMode2D.Impulse);
        currentDamage = chipAttributes.damage;
        currentLifesteal = chipAttributes.lifesteal;
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
                collision.GetComponent<CoinProjectile>().currentDamage += currentDamage;
                collision.GetComponent<CoinProjectile>().currentLifesteal += currentLifesteal;
                nextCoin = collision.gameObject;
                onRicocchet?.Invoke();
            }
        }
    }

    public void CheckNextCoin()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 20f);
        minDistance = Mathf.Infinity;
        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject.CompareTag("coin") == true)
            {
                CoinProjectile coin = hit.GetComponent<CoinProjectile>();
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
            onRicocchet?.Invoke();
        }
        if (nextCoin == null)
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 20f);
            minDistance = Mathf.Infinity;
            foreach (Collider2D hit in hits)
            {
                if (hit.gameObject.CompareTag("enemy"))
                {
                    float distance = (hit.transform.position - transform.position).sqrMagnitude;
                    enemyDistances.Add((hit.gameObject, distance));
                }
            }
            enemyDistances.Sort((a, b) => a.distance.CompareTo(b.distance));
            if (enemyDistances.Count != 0)
            {
                EnemyBehaviour behaviour = enemyDistances[0].enemy.GetComponent<EnemyBehaviour>();
                behaviour.enemyHealth -= currentDamage;
                behaviour.DamageTakenText(currentDamage);
                ps.DoLifesteal(currentLifesteal);
                DrawEnemyLine(enemyDistances[0].enemy.transform.position);
                onHit?.Invoke();
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
            nextCoin.GetComponent<CoinProjectile>().currentDamage += currentDamage;
            nextCoin.GetComponent<CoinProjectile>().currentLifesteal += currentLifesteal;
        }
        Destroy(lineObj);
        Destroy(this.gameObject);
    }

    public void DrawEnemyLine(Vector3 endPos)
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
