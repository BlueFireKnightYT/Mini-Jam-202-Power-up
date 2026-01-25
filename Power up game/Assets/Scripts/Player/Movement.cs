using NUnit.Framework;
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
    public PlayerWeapon[] everyWeapon;

    InputAction lookAt;
    PlayerInput PlayerInput;
    PlayerShooting playerShooting;

    private void Start()
    {
        baseSpeed = speed;
        rb = GetComponent<Rigidbody2D>();
        PlayerInput = GetComponent<PlayerInput>();
        playerShooting = GetComponent<PlayerShooting>();
        lvlSys = GetComponent<LevelSystem>();
        lookAt = PlayerInput.actions.FindAction("LookAt");
        jokerMenu.SetActive(false);
        Time.timeScale = 1;

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
        if(collision.CompareTag("coinWeapon") == true)
        {
            foreach(PlayerWeapon weapon in everyWeapon)
            {
                if (weapon.weaponName == "Coin")
                {
                    playerShooting.weapons.Add(weapon);
                    Destroy(collision.gameObject);
                }
            }
        }
    }
}
