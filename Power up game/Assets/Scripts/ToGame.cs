using UnityEngine;
using UnityEngine.SceneManagement;
public class ToGame : MonoBehaviour
{
    CircleCollider2D cColl;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cColl = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("player") == true)
        {
            SceneManager.LoadScene("StartScene");
        }
    }
}
