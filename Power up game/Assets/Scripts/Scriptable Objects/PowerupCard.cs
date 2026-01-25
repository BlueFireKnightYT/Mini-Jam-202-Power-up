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
    public int projectileAmount;
    public int retryAmount;

    public virtual void onActivate(Vector2 center)
    {

    }
}
