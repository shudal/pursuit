using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControll : MonoBehaviour
{
    KeyCode up = KeyCode.W;
    KeyCode down = KeyCode.S;
    KeyCode left = KeyCode.A;
    KeyCode right = KeyCode.D;

    KeyCode sendBullet = KeyCode.J;
    KeyCode changWeapon = KeyCode.K;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(up))
        {
            PlayerManager.myPlayer.moveVer(-1);
        }
        else if (Input.GetKeyDown(down))
        {
            PlayerManager.myPlayer.moveVer(1);
        }
        else if (Input.GetKeyUp(up) || Input.GetKeyUp(down))
        {
            PlayerManager.myPlayer.moveVer(0);
        };

        if (Input.GetKeyDown(right))
        {
            PlayerManager.myPlayer.moveHor(-1);
        } else if (Input.GetKeyDown(left))
        {
            PlayerManager.myPlayer.moveHor(1);
        } else if (Input.GetKeyUp(right) || Input.GetKeyUp(left))
        {
            PlayerManager.myPlayer.moveHor(0);
        }

        if (Input.GetKeyDown(sendBullet))
        {
            PlayerManager.myPlayer.skillDown();
        } else if (Input.GetKeyUp(sendBullet))
        {
            PlayerManager.myPlayer.skillUp();
        }
        if (Input.GetKeyDown(changWeapon))
        { 
            PlayerManager.myPlayer.changeWeapon();
        }
    }
}
