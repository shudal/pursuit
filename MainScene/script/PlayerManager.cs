using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static Player myPlayer;
    public static GameObject myPlayerGO;
    public static int myPlayerId;
    public static int playerCount;
    public static GameObject terrainGO;
    public static GameObject[] playerGOs;
    public static GameObject scoreTextGO; 
    public static GameObject bloodTextGO;
    public static GameObject endGOs;
    public static GameObject mainCameraGO;

    public static Vector3 initPos = new Vector3(1470, 43, 850);
    public static float minY = 43;

    public static Vector3 vd;
    public static GameObject weaponNameGO;
    public static GameObject weaponCountGO;
    private int newPlayer()
    {
        int newPlayerId = playerCount + 1;

        GameObject newPlayerGO = (GameObject)Resources.Load("prefab/player");
        newPlayerGO.tag = "player";
        newPlayerGO.GetComponent<Player>().id = newPlayerId;
        newPlayerGO = Instantiate(newPlayerGO);
        return newPlayerId;
    }
    // Start is called before the first frame update
    void Start()
    {
        // 初始化playerCount
        playerCount = 0;

        // TODO 初始化其他player;

        endGOs = GameObject.FindGameObjectWithTag("end");
        endGOs.SetActive(false);
        scoreTextGO = GameObject.FindGameObjectWithTag("scoreText");
        bloodTextGO = GameObject.FindGameObjectWithTag("bloodText");
        mainCameraGO = GameObject.FindGameObjectWithTag("MainCamera");
        weaponNameGO = GameObject.FindGameObjectWithTag("weaponName");
        weaponCountGO = GameObject.FindGameObjectWithTag("weaponCount");

        terrainGO = GameObject.FindGameObjectWithTag("terrain");
        myPlayerId = newPlayer();
        playerGOs = GameObject.FindGameObjectsWithTag("player");
        for (int i=0; i<playerGOs.Length; ++i)
        {
            if (playerGOs[i].GetComponent<Player>().id == myPlayerId)
            {
                myPlayerGO = playerGOs[i];
                myPlayer = playerGOs[i].GetComponent<Player>();
                break;
            }
        }

        myPlayer.transform.parent = terrainGO.transform; 
        myPlayerGO.transform.localPosition = initPos;

        Enemy.live = true;

        vd = mainCameraGO.transform.position - myPlayerGO.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        mainCameraGO.transform.position = myPlayerGO.transform.position + vd;
    }
}
