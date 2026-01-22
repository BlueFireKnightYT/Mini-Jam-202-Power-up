using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{
    public float xpCount;
    public float xpLevel;
    public float xpLevelAmount;

    public Slider slider;
    public TextMeshProUGUI levelText;
    public GameObject upgradeMenu;

    public bool inMenu = false;


    private void Start()
    {
        upgradeMenu.SetActive(false);
    }
    private void Update()
    {
        if(xpCount >= xpLevelAmount)
        {
            xpLevel++;
            xpCount = 0;
            xpLevelAmount = xpLevelAmount / 100 * 110;
            Upgrade();
        }
        slider.maxValue = xpLevelAmount;
        slider.value = xpCount;
        levelText.text = (xpLevel.ToString());
    }

    private void Upgrade()
    {
        // Open upgrade Menu
        if (inMenu == false)
        {
            //time stop
            Time.timeScale = 0;
            inMenu = true;
            // menu met 3 choices
            upgradeMenu.SetActive(true);

        }
        // Sluit upgrade Menu
        else if (inMenu == true)
        {
            //menu sluiten en time door zetten
            Time.timeScale = 1;
            inMenu = false;
            upgradeMenu.SetActive(false);
        }
    }
}
