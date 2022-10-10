using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Side_Shots : MonoBehaviour
{
    public Projectile projectile;
    // Start is called before the first frame update
    void Start()
    {
       projectile = transform.GetChild(0).GetComponent<Projectile>();
        Destroy(this.gameObject, projectile.shotDistance);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
