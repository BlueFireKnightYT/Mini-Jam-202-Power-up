using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject enemy;
    public GameObject shootingEnemy;

    public float baseNeededTime = 2;
    float neededTime = 2f;
    int spawnedEnemies = 0;
    int side = 1;
    public GameObject cam;
    float camPosY;
    float camPosX;

    int chance;

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

            chance = Random.Range(1, 101);

            //right
            if(side == 1)
            { 
                if (chance <= 80)
                { 
                    Instantiate(enemy, new Vector2(camPosX + Random.Range(10, 15), camPosY + Random.Range(-7, 7)), Quaternion.identity);
                }
                else if (chance > 80)
                {
                    Instantiate(shootingEnemy, new Vector2(camPosX + Random.Range(10, 15), camPosY + Random.Range(-7, 7)), Quaternion.identity);
                }
                spawnedEnemies++;
                if (spawnedEnemies % 5 == 0)
                {

                    if (baseNeededTime > 0.8)
                    {
                        neededTime = baseNeededTime / 100 * 95;
                    }
                    else if (baseNeededTime <= .8)
                    {
                        baseNeededTime = 0.8f;
                    }
                }
                neededTime = baseNeededTime;
            }
            //up
            else if (side == 2)
            {
                if (chance <= 80)
                {
                    Instantiate(enemy, new Vector2(camPosX + Random.Range(-10, 10), camPosY + Random.Range(6, 10)), Quaternion.identity);
                }
                else if (chance > 80)
                {
                    Instantiate(shootingEnemy, new Vector2(camPosX + Random.Range(-10, 10), camPosY + Random.Range(6, 10)), Quaternion.identity);
                }
                spawnedEnemies++;
                if (spawnedEnemies % 5 == 0)
                {

                    if (baseNeededTime > 0.8)
                    {
                        neededTime = baseNeededTime / 100 * 95;
                    }
                    else if (baseNeededTime <= .8)
                    {
                        baseNeededTime = 0.8f;
                    }
                }
                neededTime = baseNeededTime;
            }
            //left
            else if (side == 3)
            {
                if (chance <= 80)
                {
                    Instantiate(enemy, new Vector2(camPosX + Random.Range(-15, -10), camPosY + Random.Range(-7, 7)), Quaternion.identity);
                }
                else if (chance > 80)
                {
                    Instantiate(shootingEnemy, new Vector2(camPosX + Random.Range(-15, -10), camPosY + Random.Range(-7, 7)), Quaternion.identity);
                }
                spawnedEnemies++;
                if (spawnedEnemies % 5 == 0)
                {

                    if (baseNeededTime > 0.8)
                    {
                        neededTime = baseNeededTime / 100 * 95;
                    }
                    else if (baseNeededTime <= .8)
                    {
                        baseNeededTime = 0.8f;
                    }
                }
                neededTime = baseNeededTime;
            }
            //down
            else if (side == 4)
            {
                if (chance <= 80)
                {
                    Instantiate(enemy, new Vector2(camPosX + Random.Range(-10, 10), camPosY + Random.Range(-10, -6)), Quaternion.identity);
                }
                else if (chance > 80)
                {
                    Instantiate(shootingEnemy, new Vector2(camPosX + Random.Range(-10, 10), camPosY + Random.Range(-10, -6)), Quaternion.identity);
                }
                spawnedEnemies++;
                if (spawnedEnemies % 5 == 0)
                {

                    if (baseNeededTime > 0.8)
                    {
                        baseNeededTime = baseNeededTime / 100 * 95;
                    }
                    else if (baseNeededTime <= .8)
                    {
                        baseNeededTime = 0.8f;
                    }
                }
                neededTime = baseNeededTime;

            }
            


        }
    }
}
