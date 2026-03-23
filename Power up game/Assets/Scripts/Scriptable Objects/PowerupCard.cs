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
    public int projectileAmount = 1;
    public int retryAmount;
    public float damageMultiplier = 1;
    public float projectileSize = 1;
    public float projectileForceMultiplier = 1;
    public bool isBoomerang;
    public bool chaoticMovement;
    public bool isOrbit;

    public virtual void onActivate(Vector2 center, GameObject projectilePrefab)
    {

    }
    public virtual void EnableExplosiveBullets(bool enable)
    {

    }

    public virtual void EnableHomingProjectiles(bool enable)
    {

    }
}
