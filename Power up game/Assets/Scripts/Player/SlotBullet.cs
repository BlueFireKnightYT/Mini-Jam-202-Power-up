using UnityEngine;

public class SlotBullet : MonoBehaviour
{
    Rigidbody2D bulletRb;
    [SerializeField] float bulletSpeed = 10f;
    PlayerShooting playerShooting;
    public int lifestealAmount;
    GameObject playerObj;

    float Damage = 10;

    void Start()
    {
        bulletSpeed = Random.Range(8, 12);
        bulletRb = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, .4f);
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
            enemyScript.DamageTakenText(Damage);
            enemyScript.enemyHealth -= Damage;
            playerShooting.DoLifesteal(lifestealAmount);
            Destroy(this.gameObject);
        }
    }
}
