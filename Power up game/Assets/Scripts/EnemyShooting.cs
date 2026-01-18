using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public EnemyBehaviour eB;
    GameObject player;
    public GameObject eBullet;

    Vector2 playerVector;
    Vector2 distance;

    public float nDistance = 4;
    public float minNDistance = -4;
    public float shootSpeed = 1;
    float neededTime;

    private void Start()
    {
        player = GameObject.FindWithTag("player");
    }
    private void Update()
    {
        playerVector = transform.position - player.transform.position;
        distance = transform.position - player.transform.position;
        if (distance.x < nDistance && distance.x > minNDistance && distance.y < nDistance && distance.y > minNDistance)
        {
            if (eB.canMove)
            { 
                eB.canMove = false;
                
                Debug.Log("false");
            }

            if (neededTime > 0)
            {
                neededTime -= Time.deltaTime;
            }
            else if (neededTime <= 0)
            {
                Shoot();
                neededTime = shootSpeed;
            }    
        }
        else
        {
            if (eB.canMove == false)
            {
                eB.canMove = true;
                Debug.Log("true");
            }
        }
    }

    void Shoot()
    {
        float angle = Mathf.Atan2(playerVector.y, playerVector.x) * Mathf.Rad2Deg;

        Instantiate(eBullet, transform.position, Quaternion.Euler(0, 0, angle + 90));
    }
}
