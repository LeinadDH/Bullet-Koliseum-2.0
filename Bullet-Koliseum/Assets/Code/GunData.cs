using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Gun Data", menuName ="Shooter 2D/Gun Data")]

public class GunData : ScriptableObject
{
    public int bulletCapacity;
    public float bulletShotTime;
    public float bulletReloadTime;
    public GameObject bulletPrefab;

    // Gun Data
    public Sprite gunIcon;
    public Sprite gunSprite;

    public AudioClip shootClip;
    public AudioClip reloadClip;
    public AudioClip emptyClip;
}
