using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public  enum GameStage { play,win,lose}


public class GameManager : MonoBehaviour
{
    public  GameStage gameStage;
    [SerializeField] private List<Inventory> items = new List<Inventory>();
    public List<Inventory> Items { get => items; }


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

        gameStage = GameStage.play;

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

    


    void nextStage()
    {
        levelCount++;
        StartCoroutine(spawnWheel());
        for (int i = 0; i < levelSlideList.Count; i++)
        {


            Vector3 lastPos = levelSlideList[i].GetComponent<RectTransform>().localPosition;
            levelSlideList[i].GetComponent<RectTransform>().localPosition = new Vector3(lastPos.x - 130, lastPos.y, lastPos.z);

            if (i + 1 == levelCount)
            {

                levelSlideList[i].transform.GetChild(1).gameObject.SetActive(true);


            }
            else
            {
                levelSlideList[i].transform.GetChild(1).gameObject.SetActive(false);
            }

        }
    }



    IEnumerator spawnWheel()
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

        newObj.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        Destroy(objWheel);
        yield return new WaitForSeconds(0.5f);
        newObj.SetActive(true);
        objWheel = newObj;
    }


    public void addItemList(Image image, string itemName, int itemCount)
    {
        Inventory newInventory = new Inventory();
        newInventory.ItemImage = image;
        newInventory.ItemName = itemName;
        newInventory.ItemCount = itemCount;

        bool itemFound = false;


        if(itemName == "Death")
        {
            gameStage = GameStage.lose;
        }

        if(itemName != "Death")
        {

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ItemName == itemName)
                {
                    items[i].ItemCount += itemCount;
                    itemFound = true;
                    nextStage();
                    break;
                }
            }

            if (!itemFound)
            {
                items.Add(newInventory);
                nextStage();

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




