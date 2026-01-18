using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HpSystem : MonoBehaviour
{
    public int hp = 100;
    public int maxHp = 100;
    public int shield;
    public int maxShield;
    public int neededHp = 75;
    public bool inMenu = false;
    public Slider hpSlider;
    public Slider shieldSlider;

    public GameObject upgradeMenu;

    LevelSystem levelSys;
    int levelNeeded;

    private void Start()
    {
        upgradeMenu.SetActive(false);
        levelSys = GetComponent<LevelSystem>();
        levelNeeded = 1;
    }

    private void Update()
    {
        hpSlider.value = hp;
        hpSlider.maxValue = maxHp;
        shieldSlider.value = shield;
        shieldSlider.maxValue = maxShield;

        if(hp > maxHp)
        {
            hp = maxHp;
        }
        if(hp <= 0)
        {
            SceneManager.LoadScene("DeathScene");
        }
    }

    
    public void Upgrade(InputAction.CallbackContext context)
    {
        // Open upgrade Menu
        if (context.performed && levelSys.xpLevel >= levelNeeded && inMenu == false)
        {
            //time stop
            Time.timeScale = 0;
            inMenu = true;
            // menu met 3 choices
            upgradeMenu.SetActive(true);
            levelSys.xpLevel -= 1;;

        }
        // Sluit upgrade Menu
        else if (context.performed && inMenu == true)
        {
            //menu sluiten en time door zetten
            Time.timeScale = 1;
            inMenu = false;
            upgradeMenu.SetActive(false);
        }
    }
}
