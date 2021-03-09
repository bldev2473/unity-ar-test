using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(ARRaycastManager))]
public class ARTapToPlaceObject : MonoBehaviour
{
    // AR Foundation
    private Camera arCamera;
    private ARSession arSession;

    // Static singleton property
    public static ARTapToPlaceObject Instance { get; private set; }

    // Assign prefab in insepctor and place object with button click event
    public Button placeObjectButton;
    public GameObject objectToPlace;

    public Button destoryAllObjectButton;

    // Object placement
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private ARRaycastManager arManager;
    public GameObject placementIndicator;
    private Pose placementPose;
    private bool placementPoseIsValid = false;

    // Touch and move
    private Vector2 touchPosition;
    private Vector3 targetPosition;
    private GameObject spawnedObject;
    private bool isMoving = false;

    // Light
    public Light light;
    public Pose targetLightPosition;
    public Button addLightSourceButton;

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
                spawnedObject = Instantiate(objectToPlace, placementPose.position, placementPose.rotation);
            });
        }

        // Assign prefab in insepctor and place object with button click event
        if (destoryAllObjectButton != null)
        {
            destoryAllObjectButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("ARScene");
                arSession.Reset();
            });
        }

        // Assign prefab in insepctor and place object with button click event
        if (addLightSourceButton != null)
        {
            addLightSourceButton.onClick.AddListener(() =>
            {
                Instantiate(light, targetLightPosition.position, targetLightPosition.rotation);
                Debug.Log("Light: " + light.ToString());
                Debug.Log("Light Transform: " + targetLightPosition.position.ToString() + "/" + targetLightPosition.rotation.ToString());
                light.transform.LookAt(spawnedObject.transform);
            });
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();
        TryGetTouchPosition();

        //// Move object to touched position
        //if (spawnedObject != null && TryGetTouchPosition() != null)
        //{
        //    Debug.Log("Move object to touched position");

        //    float dis = Vector3.Distance(spawnedObject.transform.position, targetPosition);
        //    Debug.Log("dis: " + dis);

        //    float speed = 2.0f;

        //    if (dis >= 0.01f)
        //    {
        //        // Rotation
        //        Vector3 dir = targetPosition - spawnedObject.transform.position;
        //        Vector3 dirXZ = new Vector3(dir.x, 0f, dir.z);
        //        Quaternion targetRot = Quaternion.LookRotation(dirXZ);
        //        spawnedObject.transform.rotation = Quaternion.RotateTowards(spawnedObject.transform.rotation, targetRot, 550.0f * Time.deltaTime);

        //        // Movement
        //        //spawnedObject.transform.localPosition = Vector3.MoveTowards(transform.position, hitPose.position, speed * Time.deltaTime);
        //        spawnedObject.transform.localPosition = Vector3.Lerp(spawnedObject.transform.position, targetPosition, speed * Time.deltaTime);
        //    }
        //}
    }

    Vector3 TryGetTouchPosition()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Prevent touch input on UI object from AR feature
            if (touch.phase == TouchPhase.Began)
            {
                touchPosition = touch.position;
                Debug.Log("touchPosition: " + touchPosition.ToString());

                bool isOverUI = touchPosition.IsPointOverUIObject();

                if (!isOverUI)
                {
                    bool arManagerRaycast = arManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon);
                    Debug.Log("arManagerRaycast: " + arManagerRaycast.ToString());

                    if (arManagerRaycast)
                    {
                        var hitPose = hits[0].pose;
                        targetLightPosition = hitPose;
                        targetPosition = hitPose.position;

                        Debug.Log("targetPosition: " + targetPosition.ToString());

                        Instantiate(light, Vector3.zero, Quaternion.identity);
                        light.transform.SetPositionAndRotation(targetLightPosition.position, targetLightPosition.rotation);
                        Debug.Log("Light: " + light.ToString());
                        Debug.Log("Light Transform: " + targetLightPosition.position.ToString() + "/" + targetLightPosition.rotation.ToString());
                        light.transform.LookAt(spawnedObject.transform);
                    }
                }
            }
        }

        return targetPosition;
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        //var hits = new List<ARRaycastHit>();
        arManager.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;

        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;

            var cameraForward = -1 * Camera.current.transform.forward;
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
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }
}
