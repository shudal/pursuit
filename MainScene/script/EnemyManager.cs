using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static int times = 1;

    void sendAEnemy(Vector3 pos)
    {
        GameObject aEnemy = (GameObject)Resources.Load("prefab/enemy");
        Instantiate(aEnemy);
        aEnemy.transform.position = pos;
    }
    
    void sendGift(Vector3 pos)
    { 
        GameObject aEnemy = (GameObject)Resources.Load("prefab/gift");
        Instantiate(aEnemy);
        int type = Random.Range(2, 3); 
        aEnemy.GetComponent<Gift>().type = type;
        if (type == Weapon.BOMB_TYPE)
        {
            aEnemy.GetComponent<Gift>().count = 25 + times*5;
        } else if (type == Weapon.PISTOL_TYPE)
        {
            aEnemy.GetComponent<Gift>().count = 10;
        }
        aEnemy.transform.position = pos;
    }
    void sendFourGift()
    {
        for (int i = 0; i <= 4; ++i)
        {
            sendGift(new Vector3(Random.Range(Enemy.minX, Enemy.maxX), Random.Range(Enemy.minY, Enemy.maxY), Random.Range(Enemy.minZ, Enemy.maxZ)));
        }
    }
    void sendFourEnemy()
    { 
        sendAEnemy(Enemy.downRight);
        sendAEnemy(Enemy.downLeft);
        sendAEnemy(Enemy.upRight);
        sendAEnemy(Enemy.upLeft);
        sendAEnemy(Enemy.upLibraryLeft);
        sendAEnemy(Enemy.upLibraryRight);
        sendAEnemy(Enemy.upLibraryDown);
        sendAEnemy(Enemy.upLibraryUp); 
        for (int i=0; i<4 + times*2; ++i)
        {
            sendAEnemy(new Vector3(Random.Range(Enemy.minX, Enemy.maxX), 3.9F, Random.Range(Enemy.minZ, Enemy.maxZ)));
        }

    }
    void sendTwoBoss()
    {
        GameObject g = (GameObject)Resources.Load("prefab/boss");
        g = Instantiate(g);
        g.transform.position = Boss.leftCenter;


        g = (GameObject)Resources.Load("prefab/boss");
        g = Instantiate(g);
        g.transform.position = Boss.rightCenter;

        for (int i=0; i<4; ++i)
        { 
            GameObject g2 = (GameObject)Resources.Load("prefab/boss");
            g2 = Instantiate(g);
            g2.transform.position = new Vector3(Random.Range(Enemy.minX, Enemy.maxX), 3.9F, Random.Range(Enemy.minZ, Enemy.maxZ));
        }
    } 
    IEnumerator sendEnemy()
    {
        while (true)
        {  
            if (GameObject.FindGameObjectsWithTag("enemy").Length + GameObject.FindGameObjectsWithTag("boss").Length == 0)
            {
                Debug.Log("times=" + times);
                for (int i = 0; i < 5 + times * 2; ++i)
                {
                    // sendTwoBoss();
                    // sendFourGift();
                    if (i % 5 == 0)
                    {
                        sendFourGift();
                    }
                    sendFourEnemy();
                    yield return new WaitForSeconds(5F);
                }
                for (int i = 0; i < times; ++i)
                {
                    sendFourGift();
                    sendFourEnemy();
                    sendTwoBoss();
                    yield return new WaitForSeconds(5F);
                }
                ++times;
            } 
            yield return new WaitForSeconds(5F); 
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        times = 1;
        StartCoroutine(sendEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
