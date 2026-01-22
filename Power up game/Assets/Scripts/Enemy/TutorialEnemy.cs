using UnityEngine;

public class TutorialEnemy : MonoBehaviour
{
    EnemyBehaviour eB;
    public GameObject arrow;

    private void Start()
    {
        eB = GetComponent<EnemyBehaviour>();
    }
    void Update()
    {
        if(eB.enemyHealth <= 0)
        {
            arrow.SetActive(true);
        }
    }
}
