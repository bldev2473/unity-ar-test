using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SizeController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RectTransform rt = this.GetComponent(typeof(RectTransform)) as RectTransform;
        //rt.sizeDelta = new Vector2(rt.sizeDelta.x, Screen.height - 400);

        Canvas canvas = FindObjectOfType<Canvas>();

        float h = canvas.GetComponent<RectTransform>().rect.height;
        float w = canvas.GetComponent<RectTransform>().rect.width;

        rt.sizeDelta = new Vector2(rt.sizeDelta.x, h - 500);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
