using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Movement : MonoBehaviour
{
    public HpSystem hpSys;
    LevelSystem lvlSys;
    PowerUpActivator pUA;

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

    public GameObject chipsInventory;
    public GameObject rouletteStuff;
    public GameObject chipSlots;
    public ChipWeapon normalChip;

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
        pUA = GetComponent<PowerUpActivator>();
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
            if (!rouletteStuff.activeSelf)
            {
                rouletteStuff.SetActive(true);
                chipsInventory.SetActive(true);
                chipSlots.SetActive(false);
                Time.timeScale = 0f;
            }
            else
            {
                rouletteStuff.SetActive(false);
                chipsInventory.SetActive(false);
                chipSlots.SetActive(true);
                Time.timeScale = 1f;
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
                    UIChip.GetComponent<DragDropChips>().chipAttributes = normalChip; 
                    Destroy(collision.gameObject);
                }
            }
        }
        if(collision.CompareTag("roulette") == true)
        {
            inInteractableRange = true;
            pUA.powerUpEnabled = false;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (inInteractableRange)
        {
            inInteractableRange = false;
            rouletteStuff.SetActive(false);
            chipSlots.SetActive(true);
            pUA.powerUpEnabled = true;
        }
    }
}
