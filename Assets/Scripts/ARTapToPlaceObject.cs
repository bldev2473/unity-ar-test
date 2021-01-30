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
    //public GameObject objectToPlace;
    //public GameObject placeObjectButton;

    public GameObject placementIndicator;
    private ARRaycastManager arManager;
    private Pose placementPose;

    private bool placementPoseIsValid = false;

    private GameObject[] modelBtns;
    private bool isModelBtnLoaded = false;

    // Touch and move
    private Vector2 touchPosition;
    private Vector3 targetPosition;
    private GameObject spawnedObject;
    bool isMoving = false;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    // Start is called before the first frame update
    void Start()
    {
        arManager = FindObjectOfType<ARRaycastManager>();
        Debug.Log("arManager: " + arManager.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        // Load model button
        if (!isModelBtnLoaded)
        {
            modelBtns = GameObject.FindGameObjectsWithTag("ModelBtn");

            if (modelBtns.Length > 0)
            {
                Debug.Log("modelBtns: " + modelBtns.ToString());
                for (int i = 0; i < modelBtns.Length; i++)
                {
                    modelBtns[i].GetComponent<Button>().onClick.AddListener(ButtonClickEvent);
                }

                isModelBtnLoaded = true;
            }
        }

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

    private void ButtonClickEvent()
    {
        Debug.Log("ButtonClickEvent()");
        if (placementPoseIsValid)
        {
            PlaceObject();
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

    private bool PlaceObject()
    {
        Debug.Log("objectToPlace: " + objectToPlace.ToString());

        // Create model
        GameObject[] respawn = GameObject.FindGameObjectsWithTag("ModelName");
        foreach (var t in respawn)
        {
            Destroy(t);
        }

        // Get clicked button object info
        string modelName = "";
        if (ModelInfo.ModelInformation.Count > 0)
        {
            modelName = ModelInfo.ModelInformation[0];
        }

        string dir = "Models/" + modelName + "/" + modelName;
        Debug.Log("modelName: " + modelName);

        string aniDir = "Animations/Walking";
        Debug.Log("dir: " + dir);

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

        return true;
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
