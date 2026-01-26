using UnityEngine;

[CreateAssetMenu(fileName = "PlayerWeapon", menuName = "Scriptable Objects/PlayerWeapon")]
public class PlayerWeapon : ScriptableObject
{
    public string weaponName;
    public Sprite weaponSprite;
    public GameObject projectilePrefab;
    public float projectileForce;
    public float cooldownTime;
    public float damage;
}
