using UnityEngine;

[CreateAssetMenu(fileName = "CardWeaponPowerupCard", menuName = "Scriptable Objects/PowerupCard/SpecialAbilities/Explosive Projectiles")]
public class SpecialAbilityExplosiveProjectiles : PowerupCard
{
    public GameObject projectilePrefab;
    public GameObject explosionPrefab;

    public override void EnableExplosiveBullets(bool enable)
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("player");
        playerObj.GetComponent<PlayerShooting>().explosiveBullets = enable;
    }
}
