using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    Rigidbody2D eBulletRb;
    GameObject player;
    public float eBulletSpeed = 10f;
    public int dmg = 30;

    HpSystem hpSys;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(this.gameObject, 2.5f);
        player = GameObject.FindGameObjectWithTag("player");
        hpSys = player.GetComponent<HpSystem>();
        eBulletRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        eBulletRb.linearVelocity = transform.up * eBulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            hpSys.hp -= dmg;
            Destroy(this.gameObject);
        }
    }
}
