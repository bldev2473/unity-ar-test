using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightSourceMovementController : MonoBehaviour
{
    public Slider slider;
    public Light directionalLight;
    public GameObject target;

    private float timeCounter;
    public float speed, axis1, axis2, height;

    // Start is called before the first frame update
    void Start()
    {
        timeCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //timeCounter += Time.deltaTime * speed;
        timeCounter = (float)(2 * Mathf.PI * (slider.value / 360.0));

        float x = Mathf.Cos(timeCounter) * axis1;
        float y = height;
        float z = Mathf.Sin(timeCounter) * axis2;

        directionalLight.transform.position = new Vector3(x, y, z);
        directionalLight.transform.LookAt(target.transform);
    }
}
