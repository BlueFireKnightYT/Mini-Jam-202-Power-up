using UnityEngine;
using UnityEngine.UIElements;

public class ChipCarrier : MonoBehaviour
{
    public ChipWeapon chip;
    Image image;

    private void Start()
    {
        image.sprite = chip.chipSprite;
    }
}
