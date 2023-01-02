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
        winRestartbtn.onClick.AddListener(restart);

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

    }


    void restart()
    {
        int x = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadSceneAsync(x);


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
            winRestartbtn.GetComponent<GameObject>().SetActive(false);
        }




    }



}
