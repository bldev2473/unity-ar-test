using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DominoAddForce : MonoBehaviour
{
    public GameObject[] dominos;
    public GameObject firstDomino;

    public void AddForceToDomino()
    {
        dominos = GameObject.FindGameObjectsWithTag("Dominos");
        Debug.Log("dominos" + dominos.Length);

        //firstDomino = dominos[0];
        firstDomino = dominos[0].transform.GetChild(0).gameObject;
        Debug.Log("firstDomino" + firstDomino.ToString());

        Vector3 direction = firstDomino.transform.position - transform.position;
        //firstDomino.GetComponent<Rigidbody>().AddForceAtPosition(direction.normalized, transform.position);
        Debug.Log("Rigidbody" + firstDomino.GetComponent<Rigidbody>());
        var cameraForward = Camera.current.transform.forward;
        var cameraBearing = new Vector3(cameraForward.x, 0, 0).normalized;
        firstDomino.GetComponent<Rigidbody>().AddForce(cameraBearing * 200);
    }
}
