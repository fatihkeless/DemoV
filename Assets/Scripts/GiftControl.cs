using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftControl : MonoBehaviour
{

    [SerializeField]private WheelNew wheelNew;
    [SerializeField]private GameManager gameManager;
    public GameObject gift;
     Transform newparent;
    private bool oneShot;

    private void Start()
    {

        oneShot = true;
        wheelNew = transform.parent.transform.GetChild(0).GetComponent<WheelNew>();
        newparent = transform.parent.transform.parent;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

    }

    private void Update()
    {
       
            //Get the mouse position on the screen and send a raycast into the game world from that position.
            Vector2 worldPoint = transform.position;
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.down);

            Debug.DrawRay(worldPoint, Vector3.down, Color.blue);

            //If something was hit, the RaycastHit2D.collider will not be null.
            if (hit.collider != null)
            {
            if (hit.collider.transform.tag == "Gift")
            {
                gift = hit.collider.gameObject.transform.parent.gameObject;
                Debug.Log(gift.GetComponent<ItemSlot>().ItemName);
                if (wheelNew.IsDone)
                {

                    if (oneShot)
                    {
                        addList(gift);
                    }

                }
            }
                
            }

        
        
    }


    public void addList(GameObject obj)
    {

        var objNew = Instantiate(obj, obj.transform.position, Quaternion.identity);
        objNew.transform.parent = newparent;
        ItemSlot objItemSlot = objNew.GetComponent<ItemSlot>();
        gameManager.addItemList(objNew, objItemSlot.ItemImage, objItemSlot.ItemName, objItemSlot.ItemCount);
       
        oneShot = false;
        
        
    }




}
