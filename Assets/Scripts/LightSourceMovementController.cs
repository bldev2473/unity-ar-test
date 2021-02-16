using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LightSourceMovementController : MonoBehaviour
{
    public Slider slider;
    public Light directionalLight;

    private float timeCounter;

    // Start is called before the first frame update
    void Start()
    {
        timeCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter = Time.deltaTime;

        float x = Mathf.Cos(timeCounter);
        float y = Mathf.Sin(timeCounter);
        float z = 0;

        directionalLight.transform.position = new Vector3(x, y, z);
    }
}
