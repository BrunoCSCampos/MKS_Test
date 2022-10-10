using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public bool isOnMenu;
    public bool isOnPlay;
    public bool isOnOptions;

    public bool isOnSessionTime;
    public bool isOnSpawnTime;
    public bool isOnBack;

    public bool isOnGameOver;
    public bool isOnReplay;
    public bool isOnReturn;

    public Game_Manager gameManager;

    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject gameOverMenu;
    public GameObject scoreCounter;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<Game_Manager>();
        mainMenu = transform.GetChild(0).gameObject;
        optionsMenu = transform.GetChild(1).gameObject;
        gameOverMenu = transform.GetChild(2).gameObject;
        scoreCounter = transform.GetChild(3).gameObject;
        StartMenu();
    }

    // Update is called once per frame
    void Update()
    {
        MainMenuCursor();
        GameOverCursor();
        ScoreCounter();
        DisplayOptions();
    }

   
    public void StartMenu()
    {
        //if return to Menu or starting up
        gameManager.gameOver = true;
        mainMenu.SetActive(true);
        isOnMenu = true;
        isOnPlay = true;
    }

    public void GameOverMenu()
    {
        gameOverMenu.SetActive(true);
        isOnReplay = true;
        isOnGameOver = true;
    }

    public void MainMenuCursor()
    {
        if (isOnMenu == true)
        {
            if (isOnPlay == true)
            {
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    isOnPlay = false;
                    isOnOptions = true;
                }
                else if (Input.GetKeyDown(KeyCode.Space))
                {
                    isOnPlay = false;
                    isOnMenu = false;
                    mainMenu.SetActive(false);
                    gameManager.StartGame();
                  
                }
            }
            else if (isOnOptions == true)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    isOnOptions = false;
                    isOnPlay = true;
                }
                else if (Input.GetKeyDown(KeyCode.Space))
                {
                    mainMenu.SetActive(false);
                    optionsMenu.SetActive(true);
                    isOnOptions = false;
                    isOnSessionTime = true;
                }
            }
            else if (isOnSessionTime == true)
            {
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    isOnSessionTime = false;
                    isOnSpawnTime = true;
                }
                else if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (gameManager.timerOnMedium == true)
                    {
                        gameManager.timerOnMedium = false;
                        gameManager.timerOnHigh = true;
                        optionsMenu.transform.GetChild(0).GetComponent<Text>().text = "SESSION TIME: HIGH";
                        gameManager.ChangeTimer();
                    }
                    else if(gameManager.timerOnHigh == true)
                    {
                        gameManager.timerOnHigh = false;
                        gameManager.timerOnLow = true;
                        optionsMenu.transform.GetChild(0).GetComponent<Text>().text = "SESSION TIME: LOW";
                        gameManager.ChangeTimer();
                    }
                    else if(gameManager.timerOnLow == true)
                    {
                        gameManager.timerOnLow = false;
                        gameManager.timerOnMedium = true;
                        optionsMenu.transform.GetChild(0).GetComponent<Text>().text = "SESSION TIME: MEDIUM";
                        gameManager.ChangeTimer();
                    }
                }
            }
            else if (isOnSpawnTime == true)
            {
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    isOnSpawnTime = false;
                    isOnBack = true;
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    isOnSpawnTime = false;
                    isOnSessionTime = true;
                }
                else if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (gameManager.spawnerOnMedium == true)
                    {
                        gameManager.spawnerOnMedium = false;
                        gameManager.spawnerOnHigh = true;
                        optionsMenu.transform.GetChild(1).GetComponent<Text>().text = "SPAWN TIME: HIGH";
                        gameManager.ChangeSpawner();
                    }
                    else if (gameManager.spawnerOnHigh == true)
                    {
                        gameManager.spawnerOnHigh = false;
                        gameManager.spawnerOnLow = true;
                        optionsMenu.transform.GetChild(1).GetComponent<Text>().text = "SPAWN TIME: LOW";
                        gameManager.ChangeSpawner();
                    }
                    else if (gameManager.spawnerOnLow == true)
                    {
                        gameManager.spawnerOnLow = false;
                        gameManager.spawnerOnMedium = true;
                        optionsMenu.transform.GetChild(1).GetComponent<Text>().text = "SPAWN TIME: MEDIUM";
                        gameManager.ChangeSpawner();
                    }
                }
            }
            else if (isOnBack == true)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    isOnBack = false;
                    isOnSpawnTime = true;
                }
                else if (Input.GetKeyDown(KeyCode.Space))
                {
                    optionsMenu.SetActive(false);
                    mainMenu.SetActive(true);
                    isOnBack = false;
                    isOnOptions = true;
                }
            }
        }
    }
    public void GameOverCursor()
    {
        if(isOnGameOver == true)
        {

            if(isOnReplay == true)
            {
                if(Input.GetKeyDown(KeyCode.DownArrow))
                {
                    isOnReplay = false;
                    isOnReturn = true;
                }
                else if(Input.GetKeyDown(KeyCode.Space))
                {
                    gameOverMenu.SetActive(false);
                    isOnGameOver = false;
                    gameManager.StartGame();
                }
            }
            else if(isOnReturn == true)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    isOnReturn = false;
                    isOnReplay = true;
                }
                else if(Input.GetKeyDown(KeyCode.Space))
                {
                    gameOverMenu.SetActive(false);
                    isOnGameOver = false;
                    isOnReturn = false;
                    StartMenu();
                }
            }
        }
    }

    public void ScoreCounter()
    {
        scoreCounter.GetComponent<Text>().text = "SCORE: " + gameManager.playerScore.ToString();
    }

    public void DisplayOptions()
    {
        if (isOnPlay == true)
        {
            mainMenu.transform.GetChild(0).GetComponent<Text>().color = Color.cyan;
        }
        else
        {
            mainMenu.transform.GetChild(0).GetComponent<Text>().color = Color.white;
        }
        if (isOnOptions == true)
        {
            mainMenu.transform.GetChild(1).GetComponent<Text>().color = Color.cyan;
        }
        else
        {
            mainMenu.transform.GetChild(1).GetComponent<Text>().color = Color.white;
        }
        if (isOnSessionTime == true)
        {
            optionsMenu.transform.GetChild(0).GetComponent<Text>().color = Color.cyan;
        }
        else
        {
            optionsMenu.transform.GetChild(0).GetComponent<Text>().color = Color.white;
        }
        if (isOnSpawnTime == true)
        {
            optionsMenu.transform.GetChild(1).GetComponent<Text>().color = Color.cyan;
        }
        else
        {
            optionsMenu.transform.GetChild(1).GetComponent<Text>().color = Color.white;
        }
        if (isOnBack == true)
        {
            optionsMenu.transform.GetChild(2).GetComponent<Text>().color = Color.cyan;
        }
        else
        {
            optionsMenu.transform.GetChild(2).GetComponent<Text>().color = Color.white;
        }
        if (isOnReplay == true)
        {
            gameOverMenu.transform.GetChild(0).GetComponent<Text>().color = Color.cyan;
        }
        else
        {
            gameOverMenu.transform.GetChild(0).GetComponent<Text>().color = Color.white;
        }
        if (isOnReturn == true)
        {
            gameOverMenu.transform.GetChild(1).GetComponent<Text>().color = Color.cyan;
        }
        else
        {
            gameOverMenu.transform.GetChild(1).GetComponent<Text>().color = Color.white;
        }


    }

}

