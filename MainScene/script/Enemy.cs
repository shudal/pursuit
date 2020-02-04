using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
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
    public static Vector3 upLibraryLeft = new Vector3(141.8F, 62.97F, -66.6F);
    public static Vector3 upLibraryRight = new Vector3(167.8F, 62.97F, -66.6F);
    public static Vector3 upLibraryDown = new Vector3(155.5F, 62.97F, -36.6F);
    public static Vector3 upLibraryUp = new Vector3(155.3F, 62.97F, -76.6F);

    public static float maxX = 463F;
    public static float minX = 6F;
    public static float maxZ = 198;
    public static float minZ = -353;
    public static float maxY = 62.97F;
    //public static float minY = 3.9F;
    public static float minY = 1.2F;

    private float blood = 100F;
    private int score = 20; 
    IEnumerator setPlayer()
    {
        while (true)
        {
            yield return new WaitForSeconds(5.0f);

            int minI = 0;
            minDis = Vector3.Distance(transform.position, PlayerManager.playerGOs[0].transform.position);
            for (int i=1; i<PlayerManager.playerGOs.Length; ++i)
            {
                float dis = Vector3.Distance(transform.position, PlayerManager.playerGOs[i].transform.position);
                if (dis < minDis) {
                    minI = i;
                    minDis = dis;
                }                
            }
            playerGO = PlayerManager.playerGOs[minI];

            //transform.LookAt(playerGO.transform);
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
            } catch (Exception e)
            {

            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        minDistance = 8F;
        status = silent;
        speed = 0.1F;
        StartCoroutine(setPlayer());
        StartCoroutine(actRandom());
        blood = 100F;
    }
    void act()
    {
        if (status == run)
        {
            /*
            if (Physics.Raycast(transform.position, playerGO.transform.position, out RaycastHit hit, 20F)) {
                Debug.Log("hit");
                transform.position = transform.position + transform.position + Vector3.Normalize(playerGO.transform.position - transform.position + hit.normal) * speed;
            } else
            { 
                transform.position = transform.position + Vector3.Normalize(playerGO.transform.position - transform.position) * speed;
            }
            */

            transform.position = transform.position + Vector3.Normalize(playerGO.transform.position - transform.position) * speed;

        }
        else if (status == attack)
        {
            // Debug.Log("attack");
        }

    }

    public void getHarm(float harm, int playerid)
    {
        subBlood(harm);
        if (blood <= 0)
        {
            selfLive = false;
            for (int i = 0; i < PlayerManager.playerGOs.Length; ++i)
            {
                if (PlayerManager.playerGOs[i].GetComponent<Player>().id == playerid)
                {
                    PlayerManager.playerGOs[i].GetComponent<Player>().addScore(score);
                }
            }
            // gameObject.GetComponent<Rigidbody>().AddForce(0, 200000, 0);
            Destroy(gameObject);
        }
    }
    void updateStatus()
    {
        theDis = Vector3.Distance(transform.position, playerGO.transform.position);
        if (theDis < minDistance)
        {
            status = attack;
        } else
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
            if (live && selfLive)
            {
                updateStatus();
                act();
            } 
            if (transform.position.y <= -10 || transform.position.x <= -273 || transform.position.x >= 735 || transform.position.z >= 436 || transform.position.z <= -470)
            {
                Destroy(gameObject);
            }
        } catch (Exception e)
        {
            // Debug.Log(e.Message);
        }
    }

    public void subBlood(float b)
    {
        blood -= b;
        // Debug.Log("enemy blood:" + blood); 
    }
    private void OnCollisionEnter(Collision collision)
    {
        try
        {
            if (collision.collider.tag == "player")
            {
                status = attack;
                collision.collider.GetComponent<Player>().subBlood(10 / 2);
            }
            else if (collision.collider.tag == "weapon")
            {
                subBlood(collision.collider.GetComponent<Weapon>().harm);
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
                subBlood(35);
                if (blood <= 0)
                {
                    Destroy(gameObject);
                }
            }
        } catch (Exception e) {

        }
    } 
}
