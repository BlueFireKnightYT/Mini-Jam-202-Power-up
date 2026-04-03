using UnityEngine;

public class TntChip : MonoBehaviour
{
    CoinProjectile chipScript;
    ChipWeapon chipAttributes;
    public float explosionRadius = 5f;

    private void Awake()
    {
        chipScript = GetComponent<CoinProjectile>();
        chipAttributes = chipScript.chipAttributes;
        chipScript.onRicocchet += PassTNT;
        chipScript.onHit += ExplodeOnEnemy;
    }

    void PassTNT()
    {
        if (chipScript.nextCoin.GetComponent<TntChip>() == null)
        {
            print(chipScript.nextCoin);
            chipScript.nextCoin.AddComponent<TntChip>();
        }
    }

    void ExplodeOnEnemy(GameObject enemy)
    {
        Vector2 enemyLocation = enemy.transform.position;

        RaycastHit2D[] rayCollider = Physics2D.CircleCastAll(enemyLocation, explosionRadius, Vector2.down, 0f);
        foreach (RaycastHit2D t in rayCollider)
        {
            if (t.collider.gameObject.CompareTag("enemy") && t.collider.gameObject != enemy)
            {
                t.collider.gameObject.GetComponent<EnemyBehaviour>().enemyHealth -= (chipScript.currentDamage) / 2;
                t.collider.gameObject.GetComponent<EnemyBehaviour>().DamageTakenText((chipScript.currentDamage) / 2);
            }
            enemy.GetComponent<ParticleSystem>().Play();
        }
    }
}
