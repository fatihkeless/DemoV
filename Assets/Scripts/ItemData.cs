using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Create Item", order = 1)]
public class ItemData : ScriptableObject
{
    public Sprite ItemSprite;
    public string ItemName;
    [Range(0,100)]public float DropChance;
    public int itemCount;

    public ItemData(string itemName, float dropChance)
    {
        
        
        itemName = ItemName;
        dropChance = DropChance;


    }

    



}












