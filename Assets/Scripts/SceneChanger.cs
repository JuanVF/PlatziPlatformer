using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void changeSceneTo(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void changeToNextScene()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
            
        SceneManager.LoadScene(nextScene);
    }
}
