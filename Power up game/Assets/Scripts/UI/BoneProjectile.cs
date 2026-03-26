
using UnityEngine;

public class BoneProjectile : MonoBehaviour
{
    Rigidbody2D rb;
    float rotation;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rotation = Random.Range(85, 160);
    }

    void FixedUpdate()
    {
        rb.angularVelocity = rb.linearVelocity.magnitude * rotation;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            EnemyBehaviour enemyScript = collision.gameObject.GetComponent<EnemyBehaviour>();
            enemyScript.DamageTakenText(5);
            enemyScript.enemyHealth -= 5;
            Destroy(this.gameObject);
        }
    }
}
