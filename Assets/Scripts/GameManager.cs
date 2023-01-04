using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public  enum GameStage { play,win,lose}


public class GameManager : MonoBehaviour
{
    public GameStage gameStage;
    [SerializeField] private List<Inventory> items = new List<Inventory>();


    public List<Inventory> Items { get => items; }

    private List<Inventory> awardsList = new List<Inventory>();

    private List<GameObject> awardsImage = new List<GameObject>();

    public List<GameObject> collectAwardsImage = new List<GameObject>();

    [SerializeField] GameObject objWheel;
    Vector3 firstPos;
    [SerializeField] private Transform wheelParent;
    public Transform WheelParent { get => wheelParent; set => wheelParent = value; }



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

    [SerializeField]private int levelCount;
    public int LevelCount { get => levelCount; set => levelCount = value; }


    [SerializeField] GameObject awards;
    [SerializeField] Transform awardsTransform;

    [SerializeField] GameObject collectAwards;
    [SerializeField] Transform collectAwardsTransform;


    public Vector2 awardsPos;

    // y pos farký 120


    private void Awake()
    {
        levelCount = 1;
    }


    // Start is called before the first frame update
    void Start()
    {

        gameStage = GameStage.play;

        itemFound = false;

        

        firstPos = objWheel.GetComponent<RectTransform>().localPosition;
        ImagePos = levelImage.GetComponent<RectTransform>().localPosition;
        
        // level slide listi     oluþturalým

        for (int i = 0; i < 45; i++)
        {
            int n = levelCount + i;

            Transform objParent = levelImage.transform.parent;

            Vector3 newObjVector = levelImage.GetComponent<RectTransform>().localPosition;


            var objImage = Instantiate(levelImage, objParent);

            levelSlideList.Add(objImage);

            objImage.GetComponent<RectTransform>().localPosition = new Vector3(newObjVector.x + (130 * i), newObjVector.y, newObjVector.z);

            objImage.transform.GetChild(0).gameObject.GetComponent<Text>().text = n.ToString();

            // resimlerin renklerini düzenliyelim

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

            if (n == 44)
            {
                Destroy(levelImage);
            }


        }

        awardsPos = awards.GetComponent<RectTransform>().anchoredPosition;


        for(int j = 0;j < 45; j++)
        {
            // sol taraftaki item toplama yerini oluþturalým

            var awardsObj = Instantiate(awards);

            awardsObj.transform.parent = awardsTransform;

            awardsObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(awards.GetComponent<RectTransform>().anchoredPosition.x, awards.GetComponent<RectTransform>().anchoredPosition.y - (j * 120));

            awardsObj.transform.GetChild(0).gameObject.GetComponent<Image>().enabled = false;


            awardsImage.Add(awardsObj);


            // win panelindeki scroolbarý oluþturalým

            var collect = Instantiate(collectAwards);

            collect.transform.parent = collectAwardsTransform;

            collect.GetComponent<RectTransform>().anchoredPosition = new Vector2(collectAwards.GetComponent<RectTransform>().anchoredPosition.x + ( j * 100), collectAwards.GetComponent<RectTransform>().anchoredPosition.y);

            collect.transform.GetChild(0).gameObject.GetComponent<Image>().enabled = false;


            collectAwardsImage.Add(collect);


            if (j == 44)
            {
                Destroy(awards);
                Destroy(collectAwards);
            }

        }




    }


    
    // yeni stage geçme
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


    // yeni çark oluþturma

    IEnumerator spawnWheel()
    {
        int n = levelCount;
        var newObj = Instantiate(objWheel);
        newObj.transform.parent = wheelParent;
        newObj.GetComponent<RectTransform>().localPosition = firstPos;
        newObj.transform.name = objWheel.transform.name;

        Image spin = newObj.transform.GetChild(0).GetComponent<Image>();
        Image tooth = newObj.transform.GetChild(2).GetComponent<Image>();

        // levellara göre çarkýmýzýn resimlerini ayarlýyalým

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


    // item ekleme ve durumlarý

    public void addItemList(Image image, string itemName, int itemCount)
    {
        Inventory newInventory = new Inventory();
        newInventory.ItemImage = image;
        newInventory.ItemName = itemName;
        newInventory.ItemCount = itemCount;

        bool itemFound = false;


        if (itemName == "Death")
        {
            gameStage = GameStage.lose;
        }

        if (itemName != "Death")
        {

            addAwardsList(newInventory);

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


    //awardsControl
    private void addAwardsList(Inventory newAwards)
    {

        bool awardsFound = false;
        
        for(int i = 0;i < awardsList.Count; i++)
        {
          
            if(awardsList[i].ItemName == newAwards.ItemName)
            {
                awardsFound = true;
                
                break;
            }
            
        }
        if (!awardsFound)
        {
            awardsList.Add(newAwards);
        }

        StartCoroutine(imageControl());

    }


    // eklenen ödüllerden sonra panellerdeki resimleri düzenliyelim

    private IEnumerator imageControl()
    {
        yield return new WaitForSeconds(2);


        for(int i = 0; i < awardsList.Count; i++)
        {
            awardsImage[i].transform.GetChild(0).gameObject.GetComponent<Image>().enabled = true;
            awardsImage[i].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = awardsList[i].ItemImage.sprite;
            awardsImage[i].transform.GetChild(1).gameObject.GetComponent<Text>().text = "X" + awardsList[i].ItemCount.ToString();



            collectAwardsImage[i].transform.GetChild(0).gameObject.GetComponent<Image>().enabled = true;
            collectAwardsImage[i].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = awardsList[i].ItemImage.sprite;
            collectAwardsImage[i].transform.GetChild(1).gameObject.GetComponent<Text>().text = "X" + awardsList[i].ItemCount.ToString();

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







