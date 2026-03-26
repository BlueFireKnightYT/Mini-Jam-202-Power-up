using UnityEngine;

[CreateAssetMenu(fileName = "ChipWeapon", menuName = "Scriptable Objects/ChipWeapon")]
public class ChipWeapon : ScriptableObject
{
    public string chipName;
    public Sprite chipSprite;
    public float damage;
    public int lifesteal;
    public float speed;
    public GameObject chipPrefab;
}
