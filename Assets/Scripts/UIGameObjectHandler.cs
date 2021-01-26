using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UIGameObjectHandler
{
    public static void setActiveStatus(GameObject[] gos, bool status)
    {
        for (int i = 0; i < gos.Length; i++)
        {
            try
            {
                gos[i].SetActive(status);
            }
            catch
            {
                Debug.Log(i);
            }
        }
    }
}
