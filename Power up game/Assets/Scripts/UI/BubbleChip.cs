using UnityEngine;

public class BubbleChip : MonoBehaviour
{
    CoinProjectile chipScript;
    public GameObject bubblePrefab;

    private void Awake()
    {
        chipScript = GetComponent<CoinProjectile>();
        chipScript.onRicocchet += SpawnBubble;
    }

    void SpawnBubble()
    {
        Instantiate(bubblePrefab, transform.position, Quaternion.identity);
    }
}
