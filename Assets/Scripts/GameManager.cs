using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public List<Inventory> items = new List<Inventory>();


    [SerializeField] GameObject objWheel;
    Vector3 firstPos;
    [SerializeField]  Transform wheelParent;


    [Header("Spin Image")]
    public Sprite currentSpin;
    public Sprite currentSpinTooth;
    public Sprite silverSpin;
    public Sprite silverSpinTooth;
    public Sprite goldSpin;
    public Sprite goldSpinTooth;
    bool itemFound;


    [Header("Level Image")]
    [SerializeField] GameObject levelImage;
    Vector3 ImagePos; 

    [SerializeField] Sprite safeImage;
    [SerializeField] Sprite bonusImage;
    [SerializeField] Sprite currenImage;

    private List<GameObject> levelSlideList = new List<GameObject>();

    private int levelCount;
    public int LevelCount { get => levelCount; set => levelCount = value; }



    // Start is called before the first frame update
    void Start()
    {
        itemFound = false;

        levelCount = 1;

        firstPos = objWheel.GetComponent<RectTransform>().localPosition;
        ImagePos = levelImage.GetComponent<RectTransform>().localPosition;

        for(int i = 0; i< 91; i++)
        {
            int n = levelCount + i;

            Transform objParent = levelImage.transform.parent;

            Vector3 newObjVector = levelImage.GetComponent<RectTransform>().localPosition;


            var objImage = Instantiate(levelImage, objParent);

            levelSlideList.Add(objImage);

            objImage.GetComponent<RectTransform>().localPosition = new Vector3(newObjVector.x + (130 * i), newObjVector.y, newObjVector.z);

            objImage.transform.GetChild(0).gameObject.GetComponent<Text>().text = n.ToString();


            if (n % 30 == 0 && n != 0)
            {
                objImage.GetComponent<Image>().sprite = bonusImage;
            }
            else if (n % 5 == 0)
            {
                objImage.GetComponent<Image>().sprite = safeImage;
            }
            else
            {
                objImage.GetComponent<Image>().sprite = currenImage;
            }

            if (n == 90)
            {
                Destroy(levelImage);
            }


        }




    }

    // Update is called once per frame
    void Update()
    {
       

        if (Input.GetKeyDown(KeyCode.A))
        {
            levelCount++;

            spawnWheel();
            for (int i = 0; i < levelSlideList.Count; i++)
            {


                Vector3 lastPos = levelSlideList[i].GetComponent<RectTransform>().localPosition;
                levelSlideList[i].GetComponent<RectTransform>().localPosition = new Vector3(lastPos.x - 130, lastPos.y, lastPos.z);

                if(i + 1 == levelCount )
                {

                    levelSlideList[i].transform.GetChild(1).gameObject.SetActive(true);


                }
                else
                {
                    levelSlideList[i].transform.GetChild(1).gameObject.SetActive(false);
                }

            }
        }





    }

    void spawnWheel()
    {
        int n = levelCount;
        var newObj = Instantiate(objWheel);
        newObj.transform.parent = wheelParent;
        newObj.GetComponent<RectTransform>().localPosition =firstPos;
        newObj.transform.name = objWheel.transform.name;

        Image spin = newObj.transform.GetChild(0).GetComponent<Image>();
        Image tooth = newObj.transform.GetChild(2).GetComponent<Image>();

        if (n % 30 == 0 && n != 0)
        {
            spin.sprite = goldSpin;
            tooth.sprite = goldSpinTooth;
        }
        else if (n % 5 == 0)
        {
            spin.sprite = silverSpin;
            tooth.sprite = silverSpinTooth;
        }
        else
        {
            spin.sprite = currentSpin;
            tooth.sprite = currentSpinTooth;
        }

        Destroy(objWheel);
        objWheel = newObj;
    }


    public void addItemList(Image image,string itemName,int itemCount)
    {

        Inventory newInventory = new Inventory();

        newInventory.ItemImage = image;
        newInventory.ItemName = itemName;
        newInventory.ItemCount = itemCount;


        if (!itemFound)
        {
            items.Add(newInventory);
            itemFound = true;

        }

        else
        {
            foreach (Inventory inven in items)

            {

                if (inven.ItemName == itemName)
                {

                    inven.ItemCount += itemCount;



                    break;


                }
                else if (inven.ItemName != itemName)
                {
                    items.Add(newInventory);

                    break;
                }

            }
        }

        
            
        

    }


}

[System.Serializable]
public class Inventory
{
    public Image ItemImage;
    public string ItemName;
    public int ItemCount;

    
    
}




