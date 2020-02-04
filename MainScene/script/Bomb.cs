using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Weapon
{

    public AudioClip bombBoomAC;
    int collisionTimes = 0;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        harm = 90;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision col)
    {
       
        if (col.collider.tag == "player" || col.collider.tag == "Untagged")
        {
            return;
        }
        //定义爆炸半径
        float radius = 10.0f;
        //定义爆炸位置为炸弹位置
        Vector3 explosionPos = transform.position;
        //这个方法用来反回球型半径之内（包括半径）的所有碰撞体collider[]
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);

        Debug.Log("播放爆炸声音");
        PlayerManager.myPlayerGO.GetComponent<Player>().playAc(bombBoomAC);
        //遍历返回的碰撞体，如果是刚体，则给刚体添加力
        foreach (Collider hit in colliders)
        {  
            if (hit.GetComponent<Rigidbody>())
            { 
                hit.GetComponent<Rigidbody>().AddExplosionForce(4600, explosionPos, radius);
            } 
            if (hit.tag == "enemy")
            {
                hit.GetComponent<Enemy>().getHarm(harm, playerid);
            } 
        }

        Destroy(gameObject);
    }

}
