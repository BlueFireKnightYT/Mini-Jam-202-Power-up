using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject enemy;

    public float baseNeededTime = 2;
    float neededTime = 2f;
    int side = 1;
    public GameObject cam;
    float camPosY;
    float camPosX;

    private void Update()
    {
        if (neededTime > 0)
        {
            neededTime -= Time.deltaTime;
        }
        else if (neededTime <= 0)
        {
            camPosX = cam.transform.position.x;
            camPosY = cam.transform.position.y;

            //spawns the enemy in a random direction outside the camera vieuw
            side = Random.Range(1, 5);

            //right
            if(side == 1)
            { 
                Instantiate(enemy, new Vector2(camPosX + Random.Range(10, 15), camPosY + Random.Range(-7, 7)), Quaternion.identity);
                neededTime = baseNeededTime;
            }
            //up
            else if (side == 2)
            {
                Instantiate(enemy, new Vector2(camPosX + Random.Range(-10, 10), camPosY + Random.Range(6, 10)), Quaternion.identity);
                neededTime = baseNeededTime;
            }
            //left
            else if (side == 3)
            {
                Instantiate(enemy, new Vector2(camPosX + Random.Range(-15, -10), camPosY + Random.Range(-7, 7)), Quaternion.identity);
                neededTime = baseNeededTime;
            }
            //down
            else if (side == 4)
            {
                Instantiate(enemy, new Vector2(camPosX + Random.Range(-10, 10), camPosY + Random.Range(-10, -6)), Quaternion.identity);
                neededTime = baseNeededTime;
            }
        }
    }
}
