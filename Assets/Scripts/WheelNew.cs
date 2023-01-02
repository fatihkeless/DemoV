using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelNew : MonoBehaviour
{
    private float rotSpeed = 0;

    private bool isTurning;
    public bool IsTurning { get => isTurning; }

    private bool isDone;
    public bool IsDone { get => isDone; }

    [SerializeField] private Button _spinButton;


    [SerializeField] private List<GameObject> childObj = new List<GameObject>();

    public List<GameObject> ChildObj { get => childObj; set => childObj = value; }



    public List<LevelDatas> stateData = new List<LevelDatas>();



    int currentLevel;

    private void Awake()
    {
        currentLevel = 0;
        isTurning = false;
        isDone = false;
        _spinButton.GetComponent<Button>().interactable = true;

        Button btn = _spinButton.GetComponent<Button>();
        btn.onClick.AddListener(spinOnClick);

        childObj.Clear();

        foreach (Transform child in gameObject.transform)
        {
            childObj.Add(child.gameObject);
        }

    }

    void Start()
    {
        

        for (int i = 0; i < childObj.Count; i++)
        {
            
            GameObject newObj = childObj[i];

            Image NewImage = newObj.transform.GetChild(0).GetComponent<Image>();
            Text amaountText = newObj.transform.GetChild(1).GetComponent<Text>();

            ItemData newItemData = getDropStateItem(stateData);
            
            NewImage.sprite = newItemData.ItemSprite;

            newObj.GetComponent<ItemSlot>().ItemData = newItemData;
            newObj.GetComponent<ItemSlot>().ItemCount = newItemData.itemCount;
            newObj.GetComponent<ItemSlot>().ItemName = newItemData.ItemName;
            newObj.GetComponent<ItemSlot>().ItemImage = NewImage;



            amaountText.text = getDropStateItem(stateData).itemCount.ToString() + "X";


        }

    }



    void Update()
    {

        if (isTurning)
        {
            transform.Rotate(0, 0, this.rotSpeed * Time.deltaTime);

            this.rotSpeed *= 0.99f;

            if (rotSpeed <= 1)
            {

                transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Round(transform.rotation.eulerAngles.z / 45f) * 45f);

                isDone = true;

            }

        }

       



    }





    private void spinOnClick()
    {
        this.rotSpeed = 1000;
        isTurning = true;
        _spinButton.GetComponent<Button>().interactable = false;

    }



   

    ItemData getDropStateItem(List<LevelDatas> level)
    {

        if (level[currentLevel].ýtemData == null || level[currentLevel].ýtemData.Count == 0)
            return null;

        float randomNumber = Random.Range(1, 100f);

        for (int i = 0; i < level[currentLevel].ýtemData.Count; i++)
        {
            if (level[currentLevel].ýtemData[i] == null)
                continue;

            float dropChance = level[currentLevel].ýtemData[i].DropChance;
            if (dropChance == 0)
                dropChance = 0;

            if (randomNumber <= dropChance)
                return level[currentLevel].ýtemData[i].itemData;
        }

        return null;




    }



}

 

