using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public HpSystem hpSys;

    Rigidbody2D rb;

    Vector2 input;
    public float baseSpeed;
    public float speed = 5f;
    public Transform cardRotate;
    public GameObject jokerMenu;

    InputAction lookAt;
    PlayerInput pi;

    private void Start()
    {
        baseSpeed = speed;
        rb = GetComponent<Rigidbody2D>();
        pi = GetComponent<PlayerInput>();
        lookAt = pi.actions.FindAction("LookAt");
        jokerMenu.SetActive(false);
        
    }

    private void FixedUpdate()
    {  
        if (hpSys.inMenu == false)
        {
            rb.AddForce(input * speed * 5);
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(lookAt.ReadValue<Vector2>());
            cardRotate.up = mousePos - new Vector2(transform.position.x, transform.position.y);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("joker") == true)
        {
            jokerMenu.SetActive(true);
            Destroy(collision.gameObject);
            Time.timeScale = 0f;
        }
    }
}
