using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public int playerScore = 0;
    public int spawnLocation;
    public int enemyRoll;

    public float spawnTimer;
    public float gameTimer;


    public bool hasTimedOut = false;
    public bool hasStarted = false;
    public bool gameOver = false;
    public bool timerOnMedium = true;
    public bool timerOnHigh = false;
    public bool timerOnLow = false;
    public bool spawnerOnMedium = true;
    public bool spawnerOnHigh = false;
    public bool spawnerOnLow = false;

    public GameObject playerPrefab;
    public GameObject shooterEnemy;
    public GameObject chaserEnemy;

    public List<GameObject> spawnedEnemies = new List<GameObject>();

    public UI_Manager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        uiManager = GameObject.FindGameObjectWithTag("UI Manager").transform.GetComponent<UI_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
      GameTimer();
      SpawnerControl();
      
     
    }

    public void StartGame()
    {
        var player = GameObject.FindGameObjectWithTag("Player");

        if(player != null)
        {
            player.GetComponent<Player_Ship>().DestroyShip();
        }

        gameOver = false;
        playerScore = 0;
        hasStarted = false;
        Instantiate(playerPrefab, playerPrefab.transform.position, Quaternion.identity);
        ChangeSpawner();
    }
    public void UpdateScore()
    {
        playerScore = playerScore + 1;
    }

    public void SpawnEnemies()
    {
        spawnLocation = Random.Range(0, 4);
        enemyRoll = Random.Range(0, 2);

        if (spawnLocation == 0)
        {
            if (enemyRoll == 0)
            {
                Instantiate(chaserEnemy, new Vector3(Random.Range(-9, 9), 6, chaserEnemy.transform.position.z), Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 180));
            }
            else if (enemyRoll == 1)
            {
                Instantiate(shooterEnemy, new Vector3(Random.Range(-9, 9), 6, shooterEnemy.transform.position.z), Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 180));

            }
        }
        else if (spawnLocation == 1)
        {
            if (enemyRoll == 0)
            {
                Instantiate(chaserEnemy, new Vector3(Random.Range(-9, 9), -6, chaserEnemy.transform.position.z), Quaternion.identity);
            }
            else if (enemyRoll == 1)
            {
                Instantiate(shooterEnemy, new Vector3(Random.Range(-9, 9), -6, shooterEnemy.transform.position.z), Quaternion.identity);
            }
        }
        else if (spawnLocation == 2)
        {
            if (enemyRoll == 0)
            {
                Instantiate(chaserEnemy, new Vector3(20, Random.Range(-4.5f, 4.5f), chaserEnemy.transform.position.z), Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 90));
            }
            else if (enemyRoll == 1)
            {
                Instantiate(shooterEnemy, new Vector3(20, Random.Range(-4.5f, 4.5f), shooterEnemy.transform.position.z), Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 90));
            }

        }
        else if (spawnLocation == 3)
        {
            if (enemyRoll == 0)
            {
                Instantiate(chaserEnemy, new Vector3(-20, Random.Range(-4.5f, 4.5f), chaserEnemy.transform.position.z), Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, -90)); ;
            }
            else if (enemyRoll == 1)
            {
                Instantiate(shooterEnemy, new Vector3(-20, Random.Range(-4.5f, 4.5f), shooterEnemy.transform.position.z), Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, -90));
            }
        }
    }

    public IEnumerator SpawnRoutine()
    {
        while (true)
        {
            
            SpawnEnemies();
            yield return new WaitForSeconds(spawnTimer);

        }
        
    }
   
    public void SpawnerControl()
    {
       if(gameOver == true)
        {
            StopCoroutine("SpawnRoutine");
        }
       else
        {
            if(hasStarted == false)
            {
                StartCoroutine("SpawnRoutine");
                hasStarted = true;
            }
            else if(spawnedEnemies.Count >= 40)
            {
                StopCoroutine("SpawnRoutine");
            }

        }
        
    }

    public void GameTimer()
    {
        if (hasTimedOut == false && gameOver == false)
        {
            if(gameTimer > 0)
            {
                gameTimer -= Time.deltaTime;
            }
            else
            {
                hasTimedOut = true;
                gameTimer = 0;
                GameOver();

            }
        }
    }

    public void ChangeTimer()
    {
        if(timerOnMedium == true)
        {
            gameTimer = 120f;

        }
        else if(timerOnHigh == true)
        {
            gameTimer = 180f;
        }
        else if(timerOnLow == true)
        {
            gameTimer = 60f;
        }
    }

    public void ChangeSpawner()
    {
        if(spawnerOnMedium == true)
        {
            spawnTimer = 6f;
        }
        else if(spawnerOnHigh == true)
        {
            spawnTimer = 3f;
        }
        else if(spawnerOnLow == true)
        {
            spawnTimer = 9f;
        }
    }

    public void GameOver()
    {
        gameOver = true;

        foreach (GameObject enemy in spawnedEnemies)
        {
            if (enemy != null)
            {
                enemy.GetComponent<Enemy_Ship>().DestroyShip();
            }
        }
        spawnedEnemies.Clear();
        uiManager.GameOverMenu();
    }

}
