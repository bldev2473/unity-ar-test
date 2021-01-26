using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewScene : MonoBehaviour
{
    public string sceneName;

    public void OnClick()
    {
        SceneManager.LoadScene(sceneName);
    }
}
