using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARSubsystems;
using System;

using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ARRaycastManager))]
public class ARTapToPlaceObject : MonoBehaviour
{
    // Static singleton property
    public static ARTapToPlaceObject Instance { get; private set; }

    // Assign prefab in insepctor and place object with button click event
    public Button placeObjectButton;
    public GameObject objectToPlace;

    // Object placement
    private ARRaycastManager arManager;
    public GameObject placementIndicator;
    private Pose placementPose;
    private bool placementPoseIsValid = false;

    private GameObject[] modelBtns;
    private bool isModelBtnLoaded = false;

    // Touch and move
    private Vector2 touchPosition;
    private Vector3 targetPosition;
    private GameObject spawnedObject;
    private bool isMoving = false;

    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    // Not allow Scroll view to detect touch event
    public GameObject scrollView;

    // Start is called before the first frame update
    void Start()
    {
        // Save a reference to the ARTapToPlaceObject component as our singleton instance
        Instance = this;

        arManager = FindObjectOfType<ARRaycastManager>();
        Debug.Log("arManager: " + arManager.ToString());

        // Assign prefab in insepctor and place object with button click event
        if (placeObjectButton != null && objectToPlace != null)
        {
            placeObjectButton.onClick.AddListener(() =>
            {
                Instantiate(objectToPlace, placementPose.position, placementPose.rotation);
            });
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Get scroll view size
        
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
        // Destroy existing model
        GameObject[] respawn = GameObject.FindGameObjectsWithTag("ModelName");
        foreach (var t in respawn)
        {
            Destroy(t);
        }

        string dir = "Models/" + modelName + "/" + modelName;
        Debug.Log("modelName: " + modelName);

        string aniDir = "Animations/Walking";
        Debug.Log("dir: " + dir);

        // Create a new model with animation
        Animator animator;

        if (Resources.LoadAll<GameObject>(dir).Length != 0)
        {
            Debug.Log("Resources.Load(dir, typeof(GameObject)): " + Resources.Load(dir, typeof(GameObject)).ToString());
            spawnedObject = Instantiate(Resources.Load(dir, typeof(GameObject)), placementPose.position, placementPose.rotation) as GameObject;
            Debug.Log("spawnedObject: " + spawnedObject);
            spawnedObject.tag = "ModelName";
            animator = spawnedObject.GetComponent<Animator>();
            Debug.Log(animator);
            spawnedObject.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(aniDir);
        }
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
