using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Movement : MonoBehaviour
{
    public HpSystem hpSys;
    LevelSystem lvlSys;

    Rigidbody2D rb;

    Vector2 input;
    bool inInteractableRange;
    public GameObject UIChipPrefab;
    public GameObject chipsParent;
    public float baseSpeed;
    public float speed = 5f;
    public Transform weaponRotate;
    public GameObject jokerMenu;
    public PlayerWeapon[] everyWeapon;

    public GameObject rouletteMenu;

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

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (inInteractableRange)
        {
            if (!rouletteMenu.activeSelf)
            {
                rouletteMenu.SetActive(true);
            }
            else
            {
                rouletteMenu.SetActive(false);
            }
        }
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
                    GameObject UIChip = Instantiate(UIChipPrefab, chipsParent.transform);
                    Destroy(collision.gameObject);
                }
            }
        }
        if(collision.CompareTag("roulette") == true)
        {
            inInteractableRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (inInteractableRange)
        {
            inInteractableRange = false;
            rouletteMenu.SetActive(false);
        }
    }
}
