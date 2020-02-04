using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{

    Vector3 startV;
    Vector3 endV;
    public int harm;
    public float shotDis;
    // Start is called before the first frame update
    void Start()
    {
        harm = 20;

        shotDis = 200 + 20*EnemyManager.times;
        startV = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (System.Math.Abs(transform.position.y-PlayerManager.myPlayerGO.transform.position.y) >= 5) { 
            transform.position = new Vector3(transform.position.x, PlayerManager.myPlayerGO.transform.position.y, transform.position.y);

        }

        if (Vector3.Distance(startV, transform.position) > shotDis)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "player")
        {
            collision.collider.GetComponent<Player>().subBlood(harm / 2);
            Destroy(gameObject);
        }
        else if (collision.collider.tag == "enemy")
        {
            Destroy(gameObject);
        }
        else if (collision.collider.tag == "bossbullet")
        {
            Destroy(gameObject);
        }
        else if (collision.collider.tag == "wall")
        {
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
}
