using UnityEngine;

[CreateAssetMenu(fileName = "HomingProjectile PowerupCard", menuName = "Scriptable Objects/PowerupCard/SpecialAbilities/Homing Projectiles")]
public class SpecialAbilityCardHomingProjectile : PowerupCard
{
    public float homingAccuracy;

    public override void EnableHomingProjectiles(bool enable)
    {
        GameObject player = GameObject.FindGameObjectWithTag("player");
        PlayerShooting shootScript = player.GetComponent<PlayerShooting>();
        shootScript.isHoming = enable;
        shootScript.homingSpeed = homingAccuracy;
    }
}
