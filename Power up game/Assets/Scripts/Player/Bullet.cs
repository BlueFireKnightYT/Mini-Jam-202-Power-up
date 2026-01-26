using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D bulletRb;
    [SerializeField] float bulletSpeed = 10f;
    PlayerShooting playerShooting;
    public int lifestealAmount;
    GameObject playerObj;
    int shotPierceAmount;
    ParticleSystem particles;
    SpriteRenderer sr;
    public float damage;

    [Header("Explosion Attributes")]
    public bool canExplode;
    public GameObject explosionPrefab;
    public float explosionRadius;

    [Header("Homing Attributes")]
    public bool isHoming;
    public float homingSpeed;
    public Vector2 homingDirection;
    public Vector2 currentDirecton;
    public GameObject closeEnemy;
    float angle;

    [Header("Coin Attributes")]
    public bool isCoin;
    public GameObject linePrefab;
    public float lineDuration = 0.1f;
    public bool beenHit;
    public bool isLastProjectile;
    bool finalShot;
    float minDistance;
    Vector2 nextCoinPos;
    GameObject nextCoin;
    GameObject closestEnemy;
    GameObject lineObj;

    void Start()
    {
        bulletRb = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, 2);
        playerObj = GameObject.FindGameObjectWithTag("player");
        playerShooting = playerObj.GetComponent<PlayerShooting>();
        particles = GetComponent<ParticleSystem>();
        sr = GetComponent<SpriteRenderer>();
        currentDirecton = transform.up;

        if (isHoming)
        {
            InvokeRepeating("UpdateHoming", 0, 0.1f);
        }
    }

    void UpdateHoming()
    {
        RaycastHit2D[] rayCollider = Physics2D.CircleCastAll(transform.position, 10, Vector2.down, 0f);
        float minDistance = Mathf.Infinity;

        foreach (RaycastHit2D hit in rayCollider)
        {
            if (hit.collider.gameObject.CompareTag("enemy"))
            {
                float distance = (hit.collider.gameObject.transform.position - transform.position).sqrMagnitude;
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closeEnemy = hit.collider.gameObject;

                }
                homingDirection = (closeEnemy.transform.position - transform.position).normalized;
                Vector2 direction = closeEnemy.transform.position - transform.position;
                angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            }
            else
            {
                if (closeEnemy == null)
                {
                    homingDirection = transform.up;
                }
                else
                {
                    homingDirection = (closeEnemy.transform.position - transform.position).normalized;
                }
            }
        }

    }

    void FixedUpdate()
    {
        if (!isHoming)
        {
            bulletRb.linearVelocity = transform.up * bulletSpeed;
        }
        else
        {
            currentDirecton.x = Mathf.MoveTowards(currentDirecton.x, homingDirection.x, homingSpeed);
            currentDirecton.y = Mathf.MoveTowards(currentDirecton.y, homingDirection.y, homingSpeed);
            bulletRb.linearVelocity = currentDirecton * bulletSpeed;
            if (closeEnemy != null)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
            }
            else
            {
                bulletRb.linearVelocity = transform.up * bulletSpeed;
            }
        }
    }

    private void Update()
    {
        if (playerShooting.lastProjectile == this.gameObject && !finalShot)
        {
            isLastProjectile = true;
        }
        else isLastProjectile = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            EnemyBehaviour enemyScript = collision.gameObject.GetComponent<EnemyBehaviour>();
            enemyScript.DamageTakenText(playerShooting.damage);
            enemyScript.enemyHealth -= playerShooting.damage;
            playerShooting.DoLifesteal(lifestealAmount);
            if (canExplode)
            {
                RaycastHit2D[] rayCollider = Physics2D.CircleCastAll(transform.position, explosionRadius, Vector2.down, 0f);
                foreach (RaycastHit2D t in rayCollider)
                {
                    if (t.collider.gameObject.CompareTag("enemy") && t.collider.gameObject != collision.gameObject)
                    {
                        t.collider.gameObject.GetComponent<EnemyBehaviour>().enemyHealth -= playerShooting.damage / 2;
                        t.collider.gameObject.GetComponent<EnemyBehaviour>().DamageTakenText(playerShooting.damage / 2);
                    }
                }
                particles.Play();
            }
            shotPierceAmount++;
            if (shotPierceAmount >= playerShooting.pierceAmount)
            {
                bulletSpeed = 0f;
                bulletRb.linearVelocity = Vector2.zero;
                sr.sprite = null;
                Destroy(this.gameObject, 0.5f);
            }

        }
        if (isCoin)
        {

        }
    }

    public void CoinHandler(Collider2D collision)
    {
        if (playerShooting.lastProjectile == this.gameObject)
        {
            if (collision.CompareTag("coin") == true)
            {
                Destroy(this.gameObject);
                collision.GetComponent<Bullet>().Invoke("CheckNextCoin", lineDuration);
                collision.GetComponent<Bullet>().damage += damage;
                collision.GetComponent<Bullet>().lifestealAmount += lifestealAmount;
            }
        }
        if (collision.CompareTag("enemy") == true)
        {
            EnemyBehaviour enemyScript = collision.gameObject.GetComponent<EnemyBehaviour>();
            enemyScript.enemyHealth -= damage;
            enemyScript.DamageTakenText(damage);
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
                    if (hit.gameObject != this.gameObject)
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
            if (closestEnemy != null)
            {
                EnemyBehaviour behaviour = closestEnemy.GetComponent<EnemyBehaviour>();
                behaviour.enemyHealth -= damage;
                behaviour.DamageTakenText(damage);
                playerShooting.DoLifesteal(lifestealAmount);
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
            nextCoin.GetComponent<Bullet>().Invoke("CheckNextCoin", lineDuration);
            nextCoin.GetComponent<Bullet>().damage += damage;
            nextCoin.GetComponent<Bullet>().lifestealAmount += lifestealAmount;
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
}
