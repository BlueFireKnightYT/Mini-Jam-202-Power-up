using UnityEngine;

[CreateAssetMenu(fileName = "CardWeaponPowerupCard", menuName = "Scriptable Objects/PowerupCard/SpecialAbilities/CircleShoot")]
public class SpecialAbilityPowerupCard : PowerupCard
{
    public int projectileCircleAmount;

    public override void onActivate(Vector2 center, GameObject projectilePrefab)
    {
        for (int i = 0; i < projectileCircleAmount; i++)
        {
            float angle = i * Mathf.PI * 2 / projectileCircleAmount;
            float x = Mathf.Cos(angle) * 1;
            float y = Mathf.Sin(angle) * 1;
            Vector2 spawnPos = center + new Vector2(x, y);
            Quaternion rotation = Quaternion.identity;
            rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);
            GameObject.FindGameObjectWithTag("player").GetComponent<PlayerShooting>().ShootCard(spawnPos, rotation);
        }
    }
}
