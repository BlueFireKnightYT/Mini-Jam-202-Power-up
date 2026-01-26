using System.Collections;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject[] enemyTypes;

    public float baseNeededTime = 5;
    int spawnedEnemies = 0;
    public GameObject cam;
    Vector2 camPos;

    private void Awake()
    {
        StartCoroutine(SpawnClock());
    }
    IEnumerator SpawnClock()
    {
        yield return new WaitForSeconds(1);

        while (true)
        { 
            int side = Random.Range(1, 5);
            int chance = Random.Range(1, 101);
            int enemyIndex = Random.Range(0, enemyTypes.Length);
            camPos = cam.transform.position;

            switch (side)
            {
                //als side 1 is spawnt die een enemy boven de camera
                case 1:
                    Instantiate(enemyTypes[enemyIndex], new Vector2(camPos.x + Random.Range(-10, 10), camPos.y + Random.Range(6, 10)), Quaternion.identity);
                    break;
                // Als side 2 is spawnt die de enemy rechts van de camera
                case 2:
                    Instantiate(enemyTypes[enemyIndex], new Vector2(camPos.x + Random.Range(10, 15), camPos.y + Random.Range(-7, 7)), Quaternion.identity);
                    break;
                // Als de side 3 is spawnt die de enenmy onder de camera
                case 3:
                    Instantiate(enemyTypes[enemyIndex], new Vector2(camPos.x + Random.Range(-10, 10), camPos.y + Random.Range(-10, -6)), Quaternion.identity);
                    break;
                // Al side 4 is spawnt de enemy links van de camera
                case 4:
                    Instantiate(enemyTypes[enemyIndex], new Vector2(camPos.x + Random.Range(-15, -10), camPos.y + Random.Range(-7, 7)), Quaternion.identity);
                    break;
            }
            spawnedEnemies++;
            enemySpawnRateIncrease(spawnedEnemies);

            float neededTime = baseNeededTime;
            yield return new WaitForSeconds(neededTime);
        }
    }

    void enemySpawnRateIncrease(int spawnedEnemies)
    {
        if (spawnedEnemies % 5 == 0)
        {

            if (baseNeededTime > 0.5f)
            {
                baseNeededTime = baseNeededTime / 100 * 95;
            }
            else if (baseNeededTime <= 0.5f)
            {
                baseNeededTime = 0.5f;
            }
        }
    }
}