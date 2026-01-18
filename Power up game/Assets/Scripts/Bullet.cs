using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D bulletRb;
    [SerializeField] float bulletSpeed = 10f;
    PlayerShooting playerShooting;
    GameObject playerObj;
    int shotPierceAmount;

    void Start()
    {
        bulletRb = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, 2);
        playerObj = GameObject.FindGameObjectWithTag("player");
        playerShooting = playerObj.GetComponent<PlayerShooting>();
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
            enemyScript.enemyHealth -= playerShooting.damage;
            playerShooting.DoLifesteal();
            shotPierceAmount++;
            if (shotPierceAmount >= playerShooting.pierceAmount)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
