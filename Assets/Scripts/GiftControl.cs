using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftControl : MonoBehaviour
{

    [SerializeField]private GameObject wheelNew;
    [SerializeField]private WheelNew wheelNewScript;
    [SerializeField]private GameManager gameManager;
    public GameObject gift;
     Transform newparent;
    private bool oneShot;

    float time;
    float timer = 0.5f;

    private void Start()
    {

        oneShot = true;
        wheelNew = transform.parent.transform.GetChild(0).gameObject;
        wheelNewScript = wheelNew.GetComponent<WheelNew>();
        newparent = transform.parent.transform.parent;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

    }

    private void Update()
    {
        float finalAngle = wheelNew.transform.localEulerAngles.z;


        if (Input.GetKeyDown(KeyCode.F1))
        {
            addList(wheelNewScript.ChildObj[0]);
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            addList(wheelNewScript.ChildObj[1]);
        }

        else if (Input.GetKeyDown(KeyCode.F3))
        {
            addList(wheelNewScript.ChildObj[2]);
        }
        else if (Input.GetKeyDown(KeyCode.F4))
        {
            addList(wheelNewScript.ChildObj[3]);
        }


        if (wheelNewScript.IsDone)
        {
            

            if (oneShot)
            {
                switch (finalAngle)
                {
                        
                    case  0:
                        addList(wheelNewScript.ChildObj[0]);
                        break;
                    case 45:
                        addList(wheelNewScript.ChildObj[1]);
                        break;
                    case 90:
                        addList(wheelNewScript.ChildObj[2]);
                        break;
                    case 135:
                        addList(wheelNewScript.ChildObj[3]);
                        break;
                    case 180:
                        addList(wheelNewScript.ChildObj[4]);
                        break;
                    case 225:
                        addList(wheelNewScript.ChildObj[5]);
                        break;
                    case 270:
                        addList(wheelNewScript.ChildObj[6]);
                        break;
                    case 315:
                        addList(wheelNewScript.ChildObj[7]);
                        break;

                }   
                



            }

        }






    }


    public void addList(GameObject obj)
    {

        var objNew = Instantiate(obj, obj.transform.position, Quaternion.identity);
        objNew.GetComponent<ItemSlot>().ItemSatete = itemState.move;
        Debug.Log(objNew.GetComponent<ItemSlot>().ItemSatete);

        gameManager.addItemList(objNew.GetComponent<ItemSlot>().ItemImage, objNew.GetComponent<ItemSlot>().ItemName, objNew.GetComponent<ItemSlot>().ItemCount);
        objNew.transform.parent = newparent;
        
        oneShot = false;
        
        
    }



    





}
