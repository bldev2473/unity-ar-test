using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ModelController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private GameObject player;
    public string actionName;
    private float force = 5f;
    private bool buttonPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player") as GameObject;
        Debug.Log("Player" + player.ToString());
    }

    void Update()
    {
        if (buttonPressed)
        {
            if (actionName.Equals("Forward"))
            {
                //player.transform.position = player.transform.position + -1 * player.transform.forward * 1;
                player.GetComponent<Rigidbody>().velocity = Vector3.forward * force;
            }

            if (actionName.Equals("Backward"))
            {
                //player.transform.position = player.transform.position + -1 * player.transform.forward * 1;
                player.GetComponent<Rigidbody>().velocity = Vector3.back * force;
            }

            if (actionName.Equals("Left"))
            {
                //player.transform.position = player.transform.position + -1 * player.transform.right * 1;
                player.GetComponent<Rigidbody>().velocity = Vector3.left * force;
            }

            if (actionName.Equals("Right"))
            {
                //player.transform.position = player.transform.position + player.transform.right * 1;
                player.GetComponent<Rigidbody>().velocity = Vector3.right * force;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        player = GameObject.FindGameObjectWithTag("Player") as GameObject;
        Debug.Log("Player" + player.ToString());
        buttonPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonPressed = false;
    }
}
