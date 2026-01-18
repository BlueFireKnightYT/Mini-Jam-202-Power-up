using Unity.VisualScripting;
using UnityEngine;

public class EnemyDasher : MonoBehaviour
{
    GameObject player;
    public Rigidbody2D rb;

    EnemyBehaviour eB;

    Vector2 playerVector;
    Vector2 distance;
    bool hasDashed;

    public float nDistance = 4;
    public float minNDistance = -4;
    public float dashForce;
    public float dashCooldown;
    private void Start()
    {
        player = GameObject.FindWithTag("player");
        rb = GetComponent<Rigidbody2D>();
        eB = GetComponent<EnemyBehaviour>();
    }
    private void Update()
    {
        playerVector = transform.position - player.transform.position;
        distance = transform.position - player.transform.position;
        if (distance.x < nDistance && distance.x > minNDistance && distance.y < nDistance && distance.y > minNDistance)
        {
            if (!hasDashed)
            {
                rb.AddForce(-playerVector.normalized * dashForce, ForceMode2D.Impulse);
                hasDashed = true;
                eB.enabled = false;
                Invoke("DoDashCooldown", dashCooldown);
            }
        }
    }

    void DoDashCooldown()
    {
        hasDashed = false;
        eB.enabled = true;
    }
}
