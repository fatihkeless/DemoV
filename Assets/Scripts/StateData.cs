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
    
    public List<ItemDatas> ýtemData = new List<ItemDatas>();

}




[System.Serializable]
public class ItemDatas
{

    [Range(0, 100)] public float DropChance;




    public string itemName;

    public int itemCount;


    public ItemData itemData;






}





