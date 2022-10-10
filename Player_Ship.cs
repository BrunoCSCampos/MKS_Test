using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Ship : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed;

    public bool canMove = true;
    public bool canShoot  = true;
    public bool shotLeft = false;
    public bool shotRight = false;
    public bool isDestroyed = false;

    public int maxHealth = 3;
    public int currentHealth = 3;

    public GameObject forwardShot;
    public GameObject leftShot;
    public GameObject rightShot;
    public GameObject flames;

    public Animator shipAnimator;
    public Animator healthAnimator;

    public Game_Manager gameManager;
   
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<Game_Manager>();
        shipAnimator = transform.GetChild(0).GetComponent<Animator>();
        healthAnimator = transform.GetChild(1).GetComponent<Animator>();
        canMove = true;
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Rotate();
        ForwardShooting();
        SideShooting();
        SetBoundaries();
        KillShip();

    }

    public void Movement()
    {
        if (canMove == true)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                Rigidbody shipRigidBody = gameObject.GetComponent<Rigidbody>();
                shipRigidBody.AddForce(transform.up * moveSpeed * Time.deltaTime);
            }
        }
        
    }

    public void Rotate()
    {
        if (canMove == true)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Rotate(new Vector3(0, 0, 1) * rotateSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Rotate(new Vector3(0, 0, -1) * rotateSpeed * Time.deltaTime);
            }
        }
    }
    public void Damage()
    {
        if (currentHealth >= 1)
        {
            currentHealth = currentHealth - 1;
            DegradeShip();
        }
            
       
       
    }

    public void ForwardShooting()
    {
        if(canShoot == true)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine("ForwardShootingRoutine");
            }
        }
    }

    public void SideShooting()
    {
        if(canShoot == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                shotRight = false;
                shotLeft = true;
                StartCoroutine("SideShootingRoutine");
            }
            else if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                shotLeft = false;
                shotRight = true;
                StartCoroutine("SideShootingRoutine");
            }
        }
    }

    public IEnumerator ForwardShootingRoutine()
    {
        canShoot = false;
        Instantiate(forwardShot, transform.position, transform.rotation);
        yield return new WaitForSeconds(0.5f);
        canShoot = true;
        StopCoroutine("ForwardShootingRoutine");
    }

    public IEnumerator SideShootingRoutine()
    {
        if (shotLeft == true)
        {
            canShoot = false;
            Instantiate(leftShot, transform.position, transform.rotation);
            yield return new WaitForSeconds(0.5f);
            shotLeft = false;
            canShoot = true;
            StopCoroutine("SideShootingRoutine");
        }
        else if(shotRight == true)
        {
            canShoot = false;
            Instantiate(rightShot, transform.position, transform.rotation);
           
            yield return new WaitForSeconds(0.5f);
            shotRight = false;
            canShoot = true;
            StopCoroutine("SideShootingRoutine");
        }
    }

  

    public void SetBoundaries()
    {
        if(transform.position.x >= 16)
        {
            transform.position = new Vector3(16, transform.position.y, transform.position.z);
        }
        else if(transform.position.x <= -16)
        {
            transform.position = new Vector3(-16, transform.position.y, transform.position.z);
        }
        if(transform.position.y >= 5)
        {
            transform.position = new Vector3(transform.position.x, 5, transform.position.z);
        }
        else if(transform.position.y <= -5)
        {
            transform.position = new Vector3(transform.position.x, -5, transform.position.z);
        }
    }

    public void DegradeShip()
    {
        if(currentHealth == 2)
        {
            shipAnimator.SetInteger("currentHealth", 2);
            healthAnimator.SetInteger("currentHealth", 2);
        }
        else if(currentHealth == 1)
        {
            shipAnimator.SetInteger("currentHealth", 1);
            healthAnimator.SetInteger("currentHealth", 1);
        }
        else if(currentHealth == 0)
        {
            shipAnimator.SetInteger("currentHealth", 0);
            healthAnimator.SetInteger("currentHealth", 0);
        }
    }

    public void DeathAnimation()
    {
        Instantiate(flames, transform.position, Quaternion.identity);
    }

    public void KillShip()
    {
        if (currentHealth <= 0 && isDestroyed == false)
        {
            canMove = false;
            canShoot = false;
            DeathAnimation();
            gameManager.GameOver();
            isDestroyed = true;
        }
    }

    public void DestroyShip()
    {
        GameObject flames = GameObject.FindGameObjectWithTag("Effect");
        Destroy(flames.gameObject);
        Destroy(this.gameObject);
        
    }
    

}
