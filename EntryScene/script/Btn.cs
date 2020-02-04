using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Btn : MonoBehaviour
{
    public void startGame()
    {
        SceneManager.LoadSceneAsync("MainScene");
    }
    public void returnToMain()
    {
        SceneManager.LoadSceneAsync("EntryScene");
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
