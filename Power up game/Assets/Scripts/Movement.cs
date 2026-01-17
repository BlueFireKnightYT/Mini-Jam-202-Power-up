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

    InputAction lookAt;
    PlayerInput pi;

    private void Start()
    {
        baseSpeed = speed;
        rb = GetComponent<Rigidbody2D>();
        pi = GetComponent<PlayerInput>();
        lookAt = pi.actions.FindAction("LookAt");
        
    }

    private void Update()
    {  
        if (hpSys.inMenu == false)
        {
            rb.AddForce(input * speed);
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

        }
    }
}
