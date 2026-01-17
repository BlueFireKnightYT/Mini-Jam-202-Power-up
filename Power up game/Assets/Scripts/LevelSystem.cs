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

    private void Update()
    {
        if(xpCount >= xpLevelAmount)
        {
            xpLevel++;
            xpCount = 0;
        }
        xpLevelAmount = xpLevel * 10 + 100;

        slider.maxValue = xpLevelAmount;
        slider.value = xpCount;
        levelText.text = (xpLevel.ToString());
    }
}
