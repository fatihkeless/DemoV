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


    private int currentLevel;

    public int CurrenLevel { get => currentLevel; set => currentLevel = value; }


    private Button _spinButton;


    private List<GameObject> childObj = new List<GameObject>();

    public List<GameObject> ChildObj { get => childObj; set => childObj = value; }



    // oyun içi eþyalarýn düþme oranlarý ve kaç tane düþeceðiniz inspectordan bu listeden ayarlýyoruz
    [SerializeField] private List<LevelDatas> stateData = new List<LevelDatas>();


    private GameManager gameManager;


    [SerializeField] private float finalAngle;
    
    private bool oneShot;


    private void Awake()
    {

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        currentLevel = 0;
        oneShot = true;
        isTurning = false;
        isDone = false;



        _spinButton = transform.parent.gameObject.transform.GetChild(1).gameObject.GetComponent<Button>();

        _spinButton.GetComponent<Button>().interactable = true;
        Button btn = _spinButton.GetComponent<Button>();
        btn.onClick.AddListener(spinOnClick);

        // önceki state ten kalan listeyi temizliyelim
        childObj.Clear();

        // çarkýn altýndaki objeleri bir listenin altýnda toplayalým
        foreach (Transform child in gameObject.transform)
        {
            childObj.Add(child.gameObject);
        }

    }

    void Start()
    {
        
        // child listesinindeki itemleri random gelecek þekilde düzenliyelim
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
        // çark çevirme kodu ve kontrol kodu

        if (isTurning && !isDone)
        {
            transform.Rotate(0, 0, this.rotSpeed * Time.deltaTime);

            this.rotSpeed *= 0.99f;
            
            if (rotSpeed <= 1 && oneShot)
            {

                transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Round(transform.rotation.eulerAngles.z / 45f) * 45f);
                
                isDone = true;

                finalAngle = transform.localEulerAngles.z;
                giftControl();
                


                Debug.Log(isDone);

                

            }

        }

       



    }


    // hediyenin hangisi olduðunu kontrol edip sonra addListi çaðýralým
    void giftControl()
    {
        if (finalAngle < 22.5f || 337.5f <= finalAngle)
        {
            addList(childObj[0]);
        }
        else if (22.5f <= finalAngle && finalAngle < 67.5f)
        {
            addList(childObj[1]);
        }
        else if (67.5f <= finalAngle && finalAngle < 112.5f)
        {
            addList(childObj[2]);
        }
        else if (112.5f <= finalAngle && finalAngle < 157.5f)
        {
            addList(childObj[3]);
        }
        else if (157.5f <= finalAngle && finalAngle < 202.5f)
        {
            addList(childObj[4]);
        }
        else if (202.5f <= finalAngle && finalAngle < 247.5f)
        {
            addList(childObj[5]);
        }
        else if (247.5f <= finalAngle && finalAngle < 292.5f)
        {
            addList(childObj[6]);
        }
        else if (292.5f <= finalAngle && finalAngle < 337.5f)
        {
            addList(childObj[7]);
        }
    }



    // gameManagerdeki item ekleme fonksiyonunu çaðýralarým, soldaki item paneline gitcek yeni bir gameobj oluþturalým ve ItemState ini move olarak ayarlýyalým

    private void addList(GameObject obj)
    {
        
          
        var objNew = Instantiate(obj, obj.transform.position, Quaternion.identity);      
        
        ItemSlot itemSlotNew = objNew.GetComponent<ItemSlot>();


        gameManager.addItemList(objNew.GetComponent<ItemSlot>().ItemImage, objNew.GetComponent<ItemSlot>().ItemName, objNew.GetComponent<ItemSlot>().ItemCount);
          
        objNew.transform.parent = gameManager.WheelParent;

          
        if(itemSlotNew.ItemName != "Death")
        {
            itemSlotNew.ItemState = itemState.move;
        }
            
          
        oneShot = false;


    }



    // çarkýn dönmesi için hýzýný belirleyip sonra spin tuþunu týklanamaz hale getirelim
    private void spinOnClick()
    {
        this.rotSpeed = 1000;
        isTurning = true;
        _spinButton.GetComponent<Button>().interactable = false;

    }




    // LevelDatas nesneleri listesinden rastgele bir ItemData nesnesi döndürmek için kullandýðým method
    ItemData getDropStateItem(List<LevelDatas> level)
    {

        if (level[gameManager.LevelCount - 1].ýtemData == null || level[gameManager.LevelCount - 1].ýtemData.Count == 0)
            return null;

        float totalDropChance = 0;

        for(int i = 0;i < level[gameManager.LevelCount - 1].ýtemData.Count; i++)
        {
            totalDropChance += level[gameManager.LevelCount - 1].ýtemData[i].DropChance;
            
        }
        Debug.Log(totalDropChance);



        float randomNumber = Random.Range(1, totalDropChance);

        for (int i = 0; i < level[gameManager.LevelCount - 1].ýtemData.Count; i++)
        {
            if (level[gameManager.LevelCount - 1].ýtemData[i] == null)
                continue;

            float dropChance = level[gameManager.LevelCount - 1].ýtemData[i].DropChance;
            if (dropChance == 0)
                dropChance = 0;

            if (randomNumber <= dropChance)
               
            return level[gameManager.LevelCount - 1].ýtemData[i].itemData;
            level[gameManager.LevelCount - 1].ýtemData[i].itemData.itemCount = level[gameManager.LevelCount - 1].ýtemData[i].itemCount;
            randomNumber -= level[gameManager.LevelCount - 1].ýtemData[i].DropChance;

           
        }

        return null;




    }





}



