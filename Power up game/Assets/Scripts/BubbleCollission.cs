using UnityEngine;

public class BubbleCollission : MonoBehaviour
{
    CircleCollider2D circleColl;
    GameObject hitEnemy;

    private void Start()
    {
        circleColl = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            Vector2 hitPoint = Physics2D.ClosestPoint(collision.transform.position, circleColl);
            Vector2 hitDirection = hitPoint - (Vector2)transform.position;
            EnemyBehaviour enemyScript = collision.gameObject.GetComponent<EnemyBehaviour>();
            hitEnemy = collision.gameObject;
            enemyScript.DamageTakenText(5);
            enemyScript.enemyHealth -= 5;
            enemyScript.enabled = false;
            collision.GetComponent<Rigidbody2D>().linearVelocity = hitDirection * 5; 
            Invoke("enableMovement", 0.5f);
            Destroy(this.gameObject, 0.5f);
            this.enabled = false;
        }
        if (collision.CompareTag("enemyProjectile"))
        {
            Destroy(this.gameObject, 0.5f);
            this.enabled = false;
            Destroy(collision.gameObject);
        }
    }

    void enableMovement()
    {
        hitEnemy.GetComponent<EnemyBehaviour>().enabled = true;  
    }
}
