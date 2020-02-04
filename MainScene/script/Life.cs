using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    public float blood;
    // Start is called before the first frame update

    public void subBlood(float b)
    {
        blood -= b;
        if (blood < 0)
        {
            dead();
        }
    }
    public virtual void dead()
    {

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
