using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Ship : MonoBehaviour
{
    public int enemyId;
    public float detectRadius;
    public float shooterDistance;
    public float chaserDistance;
    public float moveSpeed;
    public float maxHealth = 3;
    public float currentHealth = 3;

    public bool enemyAlive = true;
    public bool canMove = true;
    public bool canShoot = true;

    public LayerMask playerMask;

    public GameObject shotPrefab;
    public GameObject chaserExplosion;

    public Game_Manager gameManager;

    public Animator shipAnimator;
    public Animator healthAnimator;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<Game_Manager>();
        gameManager.spawnedEnemies.Add(this.gameObject);
        healthAnimator = transform.GetChild(1).GetComponent<Animator>();
        shipAnimator = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        DetectPlayer();
    }

    public void DetectPlayer()
    {
        if (canMove == true)
        {
            Collider[] detectRange = Physics.OverlapSphere(transform.position, detectRadius, playerMask);
            foreach (Collider player in detectRange)
            {
                Debug.Log("Player has entered range.");
                var playerScript = player.GetComponent<Player_Ship>();
                if (playerScript != null)
                {
                    transform.up = playerScript.transform.position - transform.position;
                    if (enemyId == 1)
                    {
                        if (Vector3.Distance(transform.position, playerScript.transform.position) > shooterDistance)
                        {
                            transform.position = Vector3.MoveTowards(transform.position, playerScript.transform.position, moveSpeed * Time.deltaTime);
                        }
                        else
                        {
                            StartCoroutine("ShootingRoutine");
                        }

                    }
                    else if (enemyId == 0)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, playerScript.transform.position, moveSpeed * Time.deltaTime);
                    }

                }
            }
        }
      
    }

    public IEnumerator ShootingRoutine()
    {
        if (canShoot == true)
        {
            yield return new WaitForSeconds(0.75f);
            Instantiate(shotPrefab, transform.position, transform.rotation);
            StopCoroutine("ShootingRoutine");
        }
    }

    public void EnemyCollision(Player_Ship player)
    {
        if (enemyId == 0)
        {
            player.Damage();
            Transform.Instantiate(chaserExplosion, transform.position, transform.rotation);
            gameManager.spawnedEnemies.Remove(this.gameObject);
            DestroyShip();
        }
    }

    public void Damage()
    {
                    currentHealth = currentHealth - 1;
            DegradeShip();
        
        if(currentHealth <= 0)
        {
            gameManager.UpdateScore();
            DestroyShip();
        }

    }

    

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            var playerScript = collision.gameObject.GetComponent<Player_Ship>();
            if(playerScript != null)
            {
                EnemyCollision(playerScript);
            }
        }
    }

    public void DegradeShip()
    {
        if (currentHealth == 2)
        {
            shipAnimator.SetInteger("currentHealth", 2);
            healthAnimator.SetInteger("currentHealth", 2);
        }
        else if (currentHealth == 1)
        {
            shipAnimator.SetInteger("currentHealth", 1);
            healthAnimator.SetInteger("currentHealth", 1);
        }
        else if (currentHealth == 0)
        {
            shipAnimator.SetInteger("currentHealth", 0);
            healthAnimator.SetInteger("currentHealth", 0);
        }
    }

    public void DestroyShip()
    {
        
            canMove = false;
            canShoot = false;
            enemyAlive = false;
            Destroy(this.gameObject, 0.35f);
            
        
    }


}
