using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float moveSpeed;
    public float shotDistance;
    public int shotId;
    public bool hasConnected = false;
    public Player_Ship playerScript;
    public Animator cannonballAnimator;
    // Start is called before the first frame update
    void Start()
    {
        var playerObject = GameObject.FindGameObjectWithTag("Player");
        if(playerObject != null)
        {
            playerObject.GetComponent<Player_Ship>();
        }
        cannonballAnimator = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        if (hasConnected == false)
        {
            if (shotId == 0 || shotId == 3)
            {
                transform.Translate(Vector3.up * moveSpeed * Time.deltaTime, Space.Self);
                Destroy(this.gameObject, shotDistance);
            }
            else if (shotId == 1)
            {
                transform.Translate(Vector3.left * moveSpeed * Time.deltaTime, Space.Self);
                Destroy(this.gameObject, shotDistance);
            }
            else if (shotId == 2)
            {
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.Self);
                Destroy(this.gameObject, shotDistance);
            }
           
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(shotId == 3 && other.tag == "Player")
        {
            Player_Ship playerScript = other.GetComponent<Player_Ship>();
            playerScript.Damage();
            CannonballExplode();
            Destroy(this.gameObject, 0.5f);
            
        }
        else if(shotId != 3 && other.tag == "Enemy")
        {
            Enemy_Ship enemyScript = other.GetComponent<Enemy_Ship>();
            if (enemyScript != null)
            {
                if (enemyScript.enemyAlive == true)
                {
                    enemyScript.Damage();
                    CannonballExplode();
                    Destroy(this.gameObject, 0.5f);
                }
            }
        }
    }

    public void CannonballExplode()
    {
        hasConnected = true;
        cannonballAnimator.SetBool("hasHit", true);
    }

    
}
