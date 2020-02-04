using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Weapon
{
    Vector3 startV;
    Vector3 endV;

    public float shotDis = 20F;
    // Start is called before the first frame update
    void Start()
    {
        type = Weapon.PISTOL_TYPE;
        harm = 20;
        shotDis = 20F;
        startV = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        checkStatus();
    }
    void checkStatus()
    {
        if (Vector3.Distance(startV, transform.position) > shotDis)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        try
        {
            if (collision.collider.tag != "player")
            {
                Destroy(gameObject);
            }
        } catch (Exception e)
        {

        }
    }
}
