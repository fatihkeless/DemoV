using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum itemState{  move,normal,scaleup,scaledown}



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

    private Vector2 endPos = new Vector3(-1111,11,0);
    private Vector2 firstPos;
    RectTransform rectTransform;

    float duration = 0.5f;
    float t = 0f;



   [SerializeField] private itemState _itemState;
    public itemState ItemSatete { get => _itemState; set => _itemState = value; }

    private void Start()
    {
        _itemState = itemState.normal;
        firstPos = GetComponent<RectTransform>().anchoredPosition;
        rectTransform = GetComponent<RectTransform>();


    }


    private void Update()
    {


        if (_itemState == itemState.move)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.position, endPos, 0.05f);
        }








    }





}
