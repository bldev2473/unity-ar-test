using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System;

using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ARDominoGame : MonoBehaviour
{
    // AR Foundation
    private Camera arCamera;
    private ARSession arSession;

    // Static singleton property
    public static ARDominoGame Instance { get; private set; }

    // Assign prefab in insepctor and place object with button click event
    public Button placeObjectButton;
    public GameObject objectToPlace;

    // Object placement
    private ARRaycastManager arManager;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    public GameObject placementIndicator;
    private Pose placementPose;
    private bool placementPoseIsValid = false;

    // Dominos
    public Button pushDominoButton;
    private List<GameObject> dominos = new List<GameObject>();
    public Button increaseForceButton;
    public Button decreaseForceButton;
    private float force = 20;
    public Text forceText;

    public Button destoryAllObjectButton;

    // Touch and move
    private Vector2 touchPosition;
    private Vector3 targetPosition;
    private GameObject spawnedObject;
    private bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        // Save a reference to the ARTapToPlaceObject component as our singleton instance
        Instance = this;

        arManager = FindObjectOfType<ARRaycastManager>();
        Debug.Log("arManager: " + arManager.ToString());

        arCamera = FindObjectOfType<ARSessionOrigin>().camera;
        Debug.Log("arCamera: " + arCamera.ToString());

        arSession = FindObjectOfType<ARSession>();
        Debug.Log("arSession: " + arSession.ToString());

        // Assign prefab in insepctor and place object with button click event
        if (placeObjectButton != null && objectToPlace != null)
        {
            placeObjectButton.onClick.AddListener(() =>
            {
                dominos.Add(Instantiate(objectToPlace, placementPose.position, placementPose.rotation));
            });
        }

        // Assign prefab in insepctor and place object with button click event
        if (pushDominoButton != null && objectToPlace != null)
        {
            pushDominoButton.onClick.AddListener(() =>
            {
                if (dominos.Count > 0)
                {
                    GameObject firstDomino = dominos[0];
                    Debug.Log("firstDomino: " + firstDomino.ToString());

                    Vector3 direction = firstDomino.transform.position - transform.position;
                    //firstDomino.GetComponent<Rigidbody>().AddForceAtPosition(direction.normalized, transform.position);
                    Debug.Log("rigidbody: " + firstDomino.GetComponentInChildren<Rigidbody>());

                    var cameraForward = arCamera.GetComponent<Camera>().transform.forward;
                    var cameraBearing = new Vector3(cameraForward.x, 0, 0).normalized;
                    firstDomino.GetComponentInChildren<Rigidbody>().AddForce(cameraBearing * force);
                }
            });
        }

        // Assign prefab in insepctor and place object with button click event
        if (increaseForceButton != null)
        {
            increaseForceButton.onClick.AddListener(() =>
            {
                force += 5;
                forceText.text = force.ToString();
            });
        }

        // Assign prefab in insepctor and place object with button click event
        if (decreaseForceButton != null)
        {
            decreaseForceButton.onClick.AddListener(() =>
            {
                force -= 5;
                forceText.text = force.ToString();
            });
        }

        // Assign prefab in insepctor and place object with button click event
        if (destoryAllObjectButton != null)
        {
            destoryAllObjectButton.onClick.AddListener(() =>
            {
                Destroy(GameObject.FindWithTag("Dominos"));
                dominos.Clear();


            });
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();

        // Move object to touched position
        if (spawnedObject != null && TryGetTouchPosition() != null)
        {
            Debug.Log("Move object to touched position");

            float dis = Vector3.Distance(spawnedObject.transform.position, targetPosition);
            Debug.Log("dis: " + dis);

            float speed = 2.0f;

            if (dis >= 0.01f)
            {
                // Rotation
                Vector3 dir = targetPosition - spawnedObject.transform.position;
                Vector3 dirXZ = new Vector3(dir.x, 0f, dir.z);
                Quaternion targetRot = Quaternion.LookRotation(dirXZ);
                spawnedObject.transform.rotation = Quaternion.RotateTowards(spawnedObject.transform.rotation, targetRot, 550.0f * Time.deltaTime);

                // Movement
                //spawnedObject.transform.localPosition = Vector3.MoveTowards(transform.position, hitPose.position, speed * Time.deltaTime);
                spawnedObject.transform.localPosition = Vector3.Lerp(spawnedObject.transform.position, targetPosition, speed * Time.deltaTime);
            }
        }
    }

    public void ButtonClickEvent(string modelName)
    {
        Debug.Log("ButtonClickEvent()");
        if (placementPoseIsValid)
        {
            PlaceObject(modelName);
        }
    }

    Vector3 TryGetTouchPosition()
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            Debug.Log("touchPosition: " + touchPosition.ToString());

            bool arManagerRaycast = arManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon);
            Debug.Log("arManagerRaycast: " + arManagerRaycast.ToString());

            if (arManagerRaycast)
            {
                var hitPose = hits[0].pose;
                targetPosition = hitPose.position;
            }
        }
        Debug.Log("targetPosition: " + targetPosition.ToString());

        return targetPosition;
    }

    private void PlaceObject(string modelName)
    {
        
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = arCamera.GetComponent<Camera>().ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        Debug.Log("screenCenter: " + screenCenter);
        //var hits = new List<ARRaycastHit>();
        arManager.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;
        Debug.Log("placementPoseIsValid: " + placementPoseIsValid.ToString());

        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;
            Debug.Log("placementPose: " + placementPose.ToString());

            var cameraForward = -1 * arCamera.GetComponent<Camera>().transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }

    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
            Debug.Log("placementIndicator: " + placementIndicator.ToString());
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }
}
