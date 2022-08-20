using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


[CreateAssetMenu]
public class TileData : ScriptableObject
{
    public TileBase[] tiles;


    public float walkingSpeed;

    public bool canBurn;

    public float spreadChance, spreadInterval, burnTime;

    public float indicatorInterval;
    public float numberOfFire;
}
