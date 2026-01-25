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

    public bool canExplode;
    public GameObject explosionPrefab;

    void Start()
    {
        bulletRb = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, 2);
        playerObj = GameObject.FindGameObjectWithTag("player");
        playerShooting = playerObj.GetComponent<PlayerShooting>();
        particles = GetComponent<ParticleSystem>();
        sr = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        bulletRb.linearVelocity = transform.up * bulletSpeed;
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
                Instantiate(explosionPrefab, transform, true);
                Debug.Log("boom");
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
