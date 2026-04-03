using UnityEngine;

public class SkeletonChip : MonoBehaviour
{
    CoinProjectile chipScript;
    ChipWeapon chipAttributes;
    public GameObject bonePrefab;
    private void Awake()
    {
        chipScript = GetComponent<CoinProjectile>();
        chipAttributes = chipScript.chipAttributes;
        chipScript.onRicocchet += SpawnBones;
        chipScript.onHit += SpawnBones;
    }

    private void SpawnBones()
    {
        GameObject bone1 = Instantiate(bonePrefab, transform.position , Quaternion.identity);
        GameObject bone2 = Instantiate(bonePrefab, transform.position, Quaternion.identity);
        GameObject bone3 = Instantiate(bonePrefab, transform.position, Quaternion.identity);

        bone1.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 2, ForceMode2D.Impulse);
        bone2.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 2, ForceMode2D.Impulse);
        bone3.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 2, ForceMode2D.Impulse);
    }
}
