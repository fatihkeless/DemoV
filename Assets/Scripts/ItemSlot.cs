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

    private Vector2 endPos = new Vector2(-830, 84);

    private Vector2 firstPos;
    
    RectTransform rectTransform;






    private itemState _itemState;
    public itemState ItemState { get => _itemState; set => _itemState = value; }

    private void Awake()
    {
        _itemState = itemState.normal;
    }

    private void Start()
    {

        firstPos = GetComponent<RectTransform>().anchoredPosition;
        rectTransform = GetComponent<RectTransform>();
        


    }


    private void Update()
    {


        if (_itemState == itemState.move)
        {
            StartCoroutine(move());

        }


    }


    IEnumerator move()
    {
        rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, endPos, Time.deltaTime * 2);
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
    

}
