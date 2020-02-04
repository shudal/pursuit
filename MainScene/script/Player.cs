using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int id;

    public int horDirection = 0;
    public int lastHorDir = 0;
    public int m_times = 25;

    public int verDirection = 0;
    public int lastVerDir = 0;

    private static int lastDirHor = 1;
    private static int lastDirVer = 2;
    public int lastDir = 0;

    public float bulletForce;
    public float bombForce;
    public float bombSendForce;
    public GameObject shotFromGO;
    public GameObject shotToGO;
    public GameObject up45GO;

    private int score = 0;
    private int blood = 100;

    private bool sended = false;

    public AudioClip pistolShotAC;
    public AudioClip toss_bombAC; 
    public AudioSource audiosSource;
    public List<Weapon> weaponList = new List<Weapon>();
    public int nowWeaponIndex;

    public DateTime skillDwonDT; 
    public void moveHor(int _direction)
    {
        horDirection = _direction;
        if (_direction != 0)
        {
            lastHorDir = _direction;
            lastDir = lastDirHor;
            if (_direction < 0)
            {
                // 向右
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            else
            {
                // 向左
                transform.rotation = Quaternion.Euler(0, -90, 0);
            }
        }
    }
    public void moveVer(int _d)
    {
        verDirection = _d;
        if (_d != 0)
        {
            lastVerDir = _d;
            lastDir = lastDirVer;
            if (_d > 0)
            {
                // 向下
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    public void setWeaponName(string name)
    {

    }
    public void changeWeapon()
    {
        Debug.Log("改变武器");
        ++nowWeaponIndex;
        if (nowWeaponIndex >= weaponList.Count)
        {
            nowWeaponIndex = 0;
        }
        if (weaponList[nowWeaponIndex].type == Weapon.PISTOL_TYPE)
        {
            PlayerManager.weaponNameGO.GetComponent<Text>().text = "当前武器：手枪";
        } else if (weaponList[nowWeaponIndex].type == Weapon.BOMB_TYPE)
        {
            PlayerManager.weaponNameGO.GetComponent<Text>().text = "当前武器：炸弹";
        } 
        PlayerManager.weaponCountGO.GetComponent<Text>().text = "弹药数：" + weaponList[nowWeaponIndex].count;

    }
    public void sendBullet()
    {
       
    }
    public void skillDown()
    {
        try
        {
            Debug.Log("skill down");
            if (weaponList[nowWeaponIndex].count <= 0)
            {
                return;
            }

            if (weaponList[nowWeaponIndex].type == Weapon.PISTOL_TYPE)
            {
                GameObject g = (GameObject)Resources.Load("prefab/bullet");
                g = Instantiate(g);
                // g.transform.parent = transform;
                g.GetComponent<Transform>().position = shotFromGO.transform.position;
                g.GetComponent<Weapon>().playerid = id;
                Vector3 v = (shotToGO.transform.position - transform.position);
                audiosSource.PlayOneShot(pistolShotAC);
                g.GetComponent<Rigidbody>().AddForce(bulletForce * v);
                Debug.Log(v);
            }
            else if (weaponList[nowWeaponIndex].type == Weapon.BOMB_TYPE)
            {
                skillDwonDT = DateTime.Now;
            }
        } catch (Exception e)
        {

        }
    }
    public void skillUp()
    {
        try
        {
            Debug.Log("skill up");
            if (weaponList[nowWeaponIndex].count <= 0)
            {
                return;
            }
            if (weaponList[nowWeaponIndex].type == Weapon.BOMB_TYPE)
            {
                TimeSpan gapT = DateTime.Now - skillDwonDT;
                float f = (float)gapT.TotalMilliseconds / 1000;
                Debug.Log(f);

                GameObject g = (GameObject)Resources.Load("prefab/bomb");
                g = Instantiate(g);
                // g.transform.parent = transform;
                g.GetComponent<Transform>().position = shotFromGO.transform.position;
                g.GetComponent<Weapon>().playerid = id;
                Vector3 v = (up45GO.transform.position - shotFromGO.transform.position);
                audiosSource.PlayOneShot(toss_bombAC);
                g.GetComponent<Rigidbody>().AddForce(bombSendForce * v * f);
                Debug.Log(v);
                Debug.Log("发送炸弹");
            }

            decBulletCount(1);
        } catch (Exception e)
        {

        }
    }
    void decBulletCount(int count)
    {
        --weaponList[nowWeaponIndex].count;
        PlayerManager.weaponCountGO.GetComponent<Text>().text = "弹药数：" + weaponList[nowWeaponIndex].count;
    }
    public void playAc(AudioClip a)
    {
        audiosSource.PlayOneShot(a);
    }
    IEnumerator autoAddBlood()
    {
        while (true)
        {
            yield return new WaitForSeconds(3F);
            if (blood <= 95)
            {
                subBlood(-5);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        m_times = 25;
        bulletForce = 350;
        bombSendForce = 500;
        bombForce = 500;
        blood = 100;
        // PlayerManager.bloodTextGO.GetComponent<Text>().text = blood + "";
        StartCoroutine(autoAddBlood());

        sended = false;

        audiosSource = GetComponent<AudioSource>();
        weaponList.Add(Weapon.newWeapon(Weapon.PISTOL_TYPE, 99999999999));
        weaponList.Add(Weapon.newWeapon(Weapon.BOMB_TYPE, 20));
        nowWeaponIndex = 0; 
        PlayerManager.weaponCountGO.GetComponent<Text>().text = "弹药数：" + weaponList[nowWeaponIndex].count;

    }

    // Update is called once per frame
    void Update()
    {
        if (horDirection != 0)
        {
            transform.position = transform.position + new Vector3(horDirection * m_times * Time.deltaTime, 0, 0);
        }
        if (verDirection != 0)
        {
            transform.position = transform.position + new Vector3(0, 0, verDirection * m_times * Time.deltaTime);
        }

        keepTransform();
    }
    void keepTransform()
    {
        try
        {
            transform.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            /*
            if (!(Math.Abs(GetComponent<Rigidbody>().velocity.x) <= 1) || !(Math.Abs(GetComponent<Rigidbody>().velocity.z) <= 1))
            {
                GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            }
            if (!(Math.Abs(GetComponent<Rigidbody>().velocity.y) <= 10))
            {
                GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            }
            */
        } catch (Exception e)
        {

        }
        if (transform.localPosition.y <= PlayerManager.minY)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, PlayerManager.minY, transform.localPosition.z);
        } 
    }
    public void addScore(int _s)
    {
        score += _s;
        PlayerManager.scoreTextGO.GetComponent<Text>().text = "分数:" +  score + "";
    }
    public void subBlood(int _b)
    {
        blood -= _b;
        PlayerManager.bloodTextGO.GetComponent<Text>().text = "生命:" + blood + "";

        if (blood <= 0)
        {

            // UnityWebRequest result = new UnityWebRequest();
            if (!sended)
            {
                StartCoroutine(Get());
                sended = true;
            }
            PlayerManager.endGOs.SetActive(true);
        }
    }
    public void addWeapon(int type, long count)
    {
        Debug.Log("add wepon, type=" + type + ", count=" + count);
        for (int i=0; i<weaponList.Count; ++i)
        {
            if (weaponList[i].type == type)
            {
                weaponList[i].count += count;
                if (nowWeaponIndex == i)
                {
                    PlayerManager.weaponCountGO.GetComponent<Text>().text = "弹药数：" + weaponList[nowWeaponIndex].count;
                }
                return;
            }
        }
    }
    IEnumerator Get()
    {
        UnityWebRequest webRequest = UnityWebRequest.Get("https://qqapp.heing.fun/index/dan/score?score=" + score);

        yield return webRequest.SendWebRequest();
        //异常处理，很多博文用了error!=null这是错误的，请看下文其他属性部分
        if (webRequest.isHttpError || webRequest.isNetworkError)
            Debug.Log(webRequest.error);
        else
        {
            Debug.Log(webRequest.downloadHandler.text);
        }
    }
}
