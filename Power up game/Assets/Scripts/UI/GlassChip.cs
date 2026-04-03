using UnityEngine;

public class GlassChip : MonoBehaviour
{
    CoinProjectile coinScript;

    private void Start()
    {
        coinScript = GetComponent<CoinProjectile>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            collision.GetComponent<EnemyBehaviour>().DamageTakenText(coinScript.currentDamage / 2);
            collision.GetComponent<EnemyBehaviour>().enemyHealth -= (coinScript.currentDamage / 2);
            Destroy(this.gameObject);
        }
    }
}
