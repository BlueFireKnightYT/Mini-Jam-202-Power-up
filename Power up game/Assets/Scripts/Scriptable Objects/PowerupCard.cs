using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum CardType
{
    Card,
    Coin,
    Slotgun
}

public abstract class PowerupCard : ScriptableObject
{
    public string cardName;
    public string cardDescription;
    public Sprite cardSprite;
    public Sprite inventorySprite;
    public CardType type;
    public float projectileAmount;
    public float projectileSpeed;
    public bool isSpecialAbility;
}
