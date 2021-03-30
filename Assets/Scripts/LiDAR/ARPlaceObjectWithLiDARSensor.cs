using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARPlaceObjectWithLiDARSensor : MonoBehaviour
{
    [SerializeField]
    private ARMeshManager meshManager = null;

    [SerializeField]
    private Camera arCamera = null;

    [SerializeField]
    private LayerMask layersToInclude;

    [SerializeField]
    private GameObject objectToPlace;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            var touch = Input.GetTouch(i);
            var touchPhase = touch.phase;

            if (touchPhase == TouchPhase.Began || touchPhase == TouchPhase.Moved)
            {
                var ray = arCamera.ScreenPointToRay(touch.position);
                print("ray: " + ray.ToString());

                var hasHit = Physics.Raycast(ray, out var hit, float.PositiveInfinity, layersToInclude);
                print("hasHit: " + hasHit.ToString());

                if (hasHit)
                {
                    Quaternion objectRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

                    var cameraForward = -1 * arCamera.GetComponent<Camera>().transform.forward;
                    var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
                    //objectRotation = Quaternion.LookRotation(cameraBearing);

                    GameObject newObject = GameObject.Instantiate(objectToPlace, hit.point, objectRotation);
                    newObject.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                }
            }
        }
    }
}
