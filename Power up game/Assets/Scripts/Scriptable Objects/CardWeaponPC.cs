using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CardWeaponPowerupCard", menuName = "Scriptable Objects/PowerupCard/CardWeaponPowerupCard")]
public class CardWeaponPC : PowerupCard
{
    private void Awake()
    {
        type = CardType.Card;
    }
}
