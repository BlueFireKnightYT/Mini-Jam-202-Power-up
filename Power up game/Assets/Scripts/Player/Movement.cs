using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public HpSystem hpSys;
    LevelSystem lvlSys;

    Rigidbody2D rb;

    Vector2 input;
    public float baseSpeed;
    public float speed = 5f;
    public Transform weaponRotate;
    public GameObject jokerMenu;

    InputAction lookAt;
    PlayerInput PlayerInput;

    private void Start()
    {
        baseSpeed = speed;
        rb = GetComponent<Rigidbody2D>();
        PlayerInput = GetComponent<PlayerInput>();
        lvlSys = GetComponent<LevelSystem>();
        lookAt = PlayerInput.actions.FindAction("LookAt");
        jokerMenu.SetActive(false);
        
    }

    private void FixedUpdate()
    {  
        if (lvlSys.inMenu == false || hpSys.inPauseMenu == false)
        {
            rb.AddForce(input * speed * 5);
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(lookAt.ReadValue<Vector2>());
            weaponRotate.up = mousePos - Vector2.zero;
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
