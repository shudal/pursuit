using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gift : MonoBehaviour
{
    public int type;
    public long count;
    public bool used = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (used) return;
        if (collision.collider.tag == "player")
        {
            used = true;
            PlayerManager.myPlayerGO.GetComponent<Player>().addWeapon(type, count);
            Destroy(gameObject);
        }
    }
}
