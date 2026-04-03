using UnityEngine;

public class SplitterChips : MonoBehaviour
{
    CoinProjectile chipScript;
    ChipWeapon chipAttributes;
    public int splitAmount = 1;

    private void Awake()
    {
        chipScript = GetComponent<CoinProjectile>();
        chipAttributes = chipScript.chipAttributes;
        chipScript.onRicocchet += PassSplitter;
        chipScript.onHit += SplitRicochet;
    }

    void PassSplitter()
    {
        if (chipScript.nextCoin.GetComponent<SplitterChips>() == null)
        {
            SplitterChips next = chipScript.nextCoin.AddComponent<SplitterChips>();
            next.splitAmount = splitAmount;
        }
        else
        {
            chipScript.nextCoin.GetComponent<SplitterChips>().splitAmount++;
        }
    }

    void SplitRicochet()
    {
        if (chipScript.enemyDistances.Count > 0)
        {
            int targetsHit = Mathf.Min(splitAmount, chipScript.enemyDistances.Count - 1);
            int totalTargets = targetsHit + 1;

            chipScript.currentDamage /= totalTargets;
            chipScript.currentDamage = Mathf.RoundToInt(chipScript.currentDamage);
            for (int i = 1; i <= splitAmount; i++)
            {
                if (i >= chipScript.enemyDistances.Count) break;

                EnemyBehaviour behaviour = chipScript.enemyDistances[i].enemy.GetComponent<EnemyBehaviour>();
                behaviour.enemyHealth -= chipScript.currentDamage;
                behaviour.DamageTakenText(chipScript.currentDamage);
                chipScript.ps.DoLifesteal(chipScript.currentLifesteal);
                chipScript.DrawEnemyLine(chipScript.enemyDistances[i].enemy.transform.position);
            }
        }
    }
}
