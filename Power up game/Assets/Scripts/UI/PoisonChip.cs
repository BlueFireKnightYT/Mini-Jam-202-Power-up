using UnityEngine;

public class PoisonChip : MonoBehaviour
{ 
    CoinProjectile chipScript;
    ChipWeapon chipAttributes;

    private void Awake()
    {
        chipScript = GetComponent<CoinProjectile>();
        chipAttributes = chipScript.chipAttributes;
        chipScript.onRicocchet += PassPoison;
        chipScript.onHit += PoisonEnemy;
    }

    void PassPoison()
    {
      if (chipScript.nextCoin.GetComponent<PoisonChip>() == null)
         {
            chipScript.nextCoin.AddComponent<PoisonChip>();
         }
    }
    void PoisonEnemy(GameObject enemy)
    {
        enemy.GetComponent<EnemyBehaviour>().isPoisoned = true;
        enemy.GetComponent<EnemyBehaviour>().StartCoroutine(chipScript.enemyDistances[0].enemy.GetComponent<EnemyBehaviour>().PoisonHandler(5));
    }
}
