using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public static int PISTOL_TYPE = 1;
    public static int BOMB_TYPE = 2;
    public int type;
    public long count;

    public float harm = 0;
    public int playerid; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static Weapon newWeapon(int type, long count)
    {
        Weapon w = new Weapon();
        w.type = type;
        w.count = count;
        return w;
    }
}
