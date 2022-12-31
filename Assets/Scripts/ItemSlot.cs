using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private ItemData _itemData;
    public ItemData ItemData { get => _itemData; set => _itemData = value; }

    [SerializeField] private int _itemCount;

    public int ItemCount { get => _itemCount; set => _itemCount = value; }

    [SerializeField] private string _itemName;
    public string ItemName { get => _itemName; set => _itemName = value; }

    [SerializeField] private Image _itemImage;

    public Image ItemImage { get => _itemImage; set => _itemImage = value; }




}
