using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneAwake : MonoBehaviour
{
    public bool setActiveBool = true;
    public bool dontDestroyBool = false;

    void Awake()
    {
        if (setActiveBool)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }

        if (dontDestroyBool)
        {
            DontDestroyOnLoad(this);
        }

    }
}
