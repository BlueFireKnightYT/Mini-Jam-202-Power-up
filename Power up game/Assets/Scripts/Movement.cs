using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    Rigidbody2D rb;

    Vector2 input;
    public float speed = 5f;

    InputAction lookAt;
    PlayerInput pi;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pi = GetComponent<PlayerInput>();
        lookAt = pi.actions.FindAction("LookAt");
        
    }

    private void Update()
    {
        rb.AddForce(input * speed);

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(lookAt.ReadValue<Vector2>());
        transform.up = mousePos - new Vector2(transform.position.x, transform.position.y);

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }
}
