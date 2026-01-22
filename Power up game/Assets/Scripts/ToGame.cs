using UnityEngine;
using UnityEngine.SceneManagement;
public class ToGame : MonoBehaviour
{
    CircleCollider2D cColl;
    void Start()
    {
        cColl = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("player") == true)
        {
            SceneManager.LoadScene("StartScene");
        }
    }
}
