using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Create level", order = 2)]
public class StateData : ScriptableObject
{

    public List<LevelDatas> levelData = new List<LevelDatas>();

   


}

[System.Serializable]
public class LevelDatas
{
    public int levelIndex;
    
    public List<ItemDatas> ýtemData = new List<ItemDatas>();

}




[System.Serializable]
public class ItemDatas
{

    [Range(0, 100)] public float DropChance;

    [InspectorName("Item Name")]
    public string itemName;

    public string ItemName { get => itemData.ItemName; set => itemData.ItemName = value; }


    public ItemData itemData;






}




