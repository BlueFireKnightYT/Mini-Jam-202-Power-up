using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D bulletRb;
    
    PlayerShooting playerShooting;
    public int lifestealAmount;
    GameObject playerObj;
    int shotPierceAmount;
    ParticleSystem particles;
    SpriteRenderer sr;
    

    [Header("Weapon Properties")]
    public float damage;
    public float bulletSpeed;
    public PlayerWeapon equippedWeapon;

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

    void Start()
    {
        bulletRb = GetComponent<Rigidbody2D>();
        playerObj = GameObject.FindGameObjectWithTag("player");
        playerShooting = playerObj.GetComponent<PlayerShooting>();
        equippedWeapon = playerShooting.weapons[playerShooting.currentWeapon];
        particles = GetComponent<ParticleSystem>();
        sr = GetComponent<SpriteRenderer>();
        currentDirecton = transform.up;
        if (equippedWeapon.willDisappear)
        {
            Destroy(this.gameObject, equippedWeapon.disappearTime);
        }

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
        if (!equippedWeapon.gravityEnabled)
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
    }
}
