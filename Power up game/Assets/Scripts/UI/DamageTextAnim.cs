using UnityEngine;

public class DamageTextAnim : MonoBehaviour
{
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(Random.Range(-1, 1), Random.Range(1.5f, 2)), ForceMode2D.Impulse);
        Destroy(this.gameObject, 0.5f);
    }
}
