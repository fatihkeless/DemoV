using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject winCanvas;
    [SerializeField] GameObject failCanvas;
    private Button restartBtn;
    private Button collectButton;
    private Button winRestartbtn;
    [SerializeField] private GameManager gameManager;


    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        
        restartBtn = failCanvas.transform.GetChild(1).gameObject.GetComponent<Button>();
        restartBtn.onClick.AddListener(restart);
        
        collectButton = GameObject.FindGameObjectWithTag("CollectItems").GetComponent<Button>();
        collectButton.onClick.AddListener(itemCollectFunc);

        winRestartbtn = winCanvas.transform.GetChild(2).gameObject.GetComponent<Button>();
        winRestartbtn.onClick.AddListener(back);

    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.gameStage == GameStage.lose)
        {
            failCanvas.SetActive(true);
        }

        else if(gameManager.gameStage == GameStage.win)
        {
            winCanvas.SetActive(true);
        }

        else
        {
            failCanvas.SetActive(false);
            winCanvas.SetActive(false);
        }


    }


    void restart()
    {
        int x = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadSceneAsync(x);


    }

    void back()
    {
        winCanvas.SetActive(false);
        gameManager.gameStage = GameStage.play;
    }

    void itemCollectFunc()
    {
        gameManager.gameStage = GameStage.win;

        Text newtext = winCanvas.transform.GetChild(1).gameObject.GetComponent<Text>();

        if(gameManager.Items.Count == 0)
        {
            newtext.text = "Items not Found ";
        }
        else 
        {
            
            winRestartbtn.gameObject.SetActive(false);
            newtext.text = "items collected ";
        }




    }



}
