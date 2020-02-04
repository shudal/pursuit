using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public static float speed;
    public static bool live = false;
    public bool selfLive = true;

    private GameObject playerGO;

    public float minDistance;

    // 0->静止,1->追逐,2->攻击
    private const int silent = 0;
    private const int run = 1;
    private const int attack = 2;
    public int status = 0;
    float minDis = 0;
    private float theDis;

    public static Vector3 downRight = new Vector3(150.7F, 3.9F, 144.7F);
    public static Vector3 downLeft = new Vector3(160, 3.9F, 144.7F);
    public static Vector3 upRight = new Vector3(150.7F, 3.9F, 42.0F);
    public static Vector3 upLeft = new Vector3(160, 3.9F, 42.0F);

    private float blood = 100F;
    private int score = 100;

    public float bulletForce = 80;

    public static Vector3 leftCenter = new Vector3(273F, 4F, 88.16F);
    public static Vector3 rightCenter = new Vector3(60F, 4F, 88.16F);
    IEnumerator setPlayer()
    {
        while (true)
        {
            yield return new WaitForSeconds(5.0f);

            int minI = 0;
            minDis = Vector3.Distance(transform.position, PlayerManager.playerGOs[0].transform.position);
            for (int i = 1; i < PlayerManager.playerGOs.Length; ++i)
            {
                float dis = Vector3.Distance(transform.position, PlayerManager.playerGOs[i].transform.position);
                if (dis < minDis)
                {
                    minI = i;
                    minDis = dis;
                }
            }
            playerGO = PlayerManager.playerGOs[minI];

            // transform.LookAt(playerGO.transform);
        }
    }
    IEnumerator actRandom()
    {
        System.Random rand = new System.Random();
        while (true)
        {
            yield return new WaitForSeconds((float)0.1 * rand.Next(1, 20));
            try
            {
                transform.LookAt(playerGO.transform);
            }
            catch (Exception e)
            {

            }
        }
    }
    void sendBullet()
    { 
        GameObject g = (GameObject)Resources.Load("prefab/bossbullet");
        g = Instantiate(g); 
        g.GetComponent<Transform>().position= transform.position + 6*Vector3.Normalize(playerGO.transform.position - transform.position);
        g.transform.localScale = new Vector3(4F, 4F, 4F);
        Vector3 v = (playerGO.transform.position - transform.position); 
        g.GetComponent<Rigidbody>().AddForce(bulletForce * v);
        Debug.Log(v);
    }
    IEnumerator sendBulletIE()
    {
        System.Random rand = new System.Random();
        while (true)
        {
            yield return new WaitForSeconds(5F);
            try
            {
                sendBullet();
            }
            catch (Exception e)
            {

            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        minDistance = 0F;
        status = run;
        speed = 0.15F;
        score = 100;
        blood = 300;
        bulletForce = 80;
        selfLive = true;
        StartCoroutine(setPlayer());
        StartCoroutine(actRandom());
        StartCoroutine(sendBulletIE()); 

    }
    void act()
    {
        if (status == run)
        {
            transform.position = transform.position + Vector3.Normalize(playerGO.transform.position - transform.position) * speed;
        } 
    }

    void updateStatus()
    {
        theDis = Vector3.Distance(transform.position, playerGO.transform.position);
        if (theDis < minDistance)
        {
            status = attack;
        }
        else
        {
            status = run;
        }
        // Debug.Log(theDis);
    }
    // Update is called once per frame
    void Update()
    {
        try
        {
            if (selfLive)
            {
                updateStatus();
                act();
            }

            if (transform.position.y <= -10 || transform.position.x <= -340 || transform.position.x >= 735 || transform.position.z >= 494 || transform.position.z <= -520)
            {
                Destroy(gameObject);
            }
            /*
            if (transform.position.y < 3.9F)
            {
                Destroy(gameObject);
            }
            else if (!(transform.position.x >= Enemy.minX && transform.position.x <= Enemy.maxX) || !(transform.position.z >= Enemy.minZ && transform.position.z <= Enemy.maxZ))
            {
                Destroy(gameObject);
            }
            */
        }
        catch (Exception e)
        {
            //Debug.Log(e.Message);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        try
        {
            if (collision.collider.tag == "player")
            {
                status = attack;
            }
            else if (collision.collider.tag == "weapon")
            {
                blood -= collision.collider.GetComponent<Weapon>().harm;
                Debug.Log("blood =" + blood);
                if (blood <= 0)
                {
                    selfLive = false;
                    for (int i = 0; i < PlayerManager.playerGOs.Length; ++i)
                    {
                        if (PlayerManager.playerGOs[i].GetComponent<Player>().id == collision.collider.GetComponent<Weapon>().playerid)
                        {
                            PlayerManager.playerGOs[i].GetComponent<Player>().addScore(score);
                        }
                    }
                    // gameObject.GetComponent<Rigidbody>().AddForce(0, 200000, 0);
                    Destroy(gameObject);
                }
            }
            else if (collision.collider.tag == "bossbullet")
            {
                blood -= 90;
                if (blood < 0)
                {
                    Destroy(gameObject);
                }
            }
        } catch (Exception e)
        {

        }
    }
}
