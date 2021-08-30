using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class scr_playerVars : MonoBehaviour
{
    // Start is called before the first frame update
    public int health = 100;

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {

            RestartScene();
        }
    }

    public void gotHitP()
    {
        health -= 10;
    
    }
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
