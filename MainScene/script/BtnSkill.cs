using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnSkill : MonoBehaviour
{
    public void sendBullet()
    {
        PlayerManager.myPlayer.sendBullet();
    }
    public void changeWeapon()
    {
        Debug.Log("changeweapn btnskill");
        PlayerManager.myPlayer.changeWeapon();
    }
    public void skillDown()
    {
        PlayerManager.myPlayerGO.GetComponent<Player>().skillDown();
    }
    public void skillUp()
    {
        PlayerManager.myPlayerGO.GetComponent<Player>().skillUp(); 
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
