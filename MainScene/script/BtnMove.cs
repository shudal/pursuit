using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnMove : MonoBehaviour
{
    public void moveHor(int _d)
    {
        // Debug.Log("1");
        PlayerManager.myPlayer.moveHor(_d);
    }
    public void moveVer(int _d)
    {
        PlayerManager.myPlayer.moveVer(_d);
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
