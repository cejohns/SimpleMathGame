using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LogoToGame : MonoBehaviour {


    float Clock = 5;
    void Start()
    {
        
    }

    
    void Update()
    {
        GameClock();
    }

    void GameClock()
    {
        if (Clock > 0)
        {

            Clock -= Time.deltaTime;
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }


}
