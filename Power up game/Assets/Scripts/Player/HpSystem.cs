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
    public bool inPauseMenu = false;
    public Slider hpSlider;
    public Slider shieldSlider;

    public GameObject pauseMenu;


    private void Start()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 0;
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
    public void PauseGame(InputAction.CallbackContext context)
    {
        if (context.performed && inPauseMenu == false)
        {
            // open pause menu en time stop
            Time.timeScale = 0;
            inPauseMenu = true;
            pauseMenu.SetActive(true);

        }
        else if (context.performed && inPauseMenu == true)
        {
            //menu sluiten en time door zetten
            Time.timeScale = 1;
            inPauseMenu = false;
            pauseMenu.SetActive(false);
        }
    }
}
