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
    public GameObject objectToPlace;
    public GameObject placementIndicator;
    private ARRaycastManager arManager;
    private Pose placementPose;

    public List<GameObject> dominos;
    public GameObject domino;

    public GameObject placeObjectButton;

    private bool placementPoseIsValid = false;

    private GameObject[] modelBtns;
    private bool isModelBtnLoaded = false;

    // Touch
    private Vector2 touchPosition;
    private GameObject spawnedObject;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    // Start is called before the first frame update
    void Start()
    {
        arManager = FindObjectOfType<ARRaycastManager>();
        Debug.Log("arManager: " + arManager.ToString());

        //dominos = new List<GameObject>();           

        //// Apply EventTrigger to GameObject
        //EventTrigger trigger = placeObjectButton.AddComponent<EventTrigger>();
        //EventTrigger.Entry entry = new EventTrigger.Entry();
        //entry.eventID = EventTriggerType.PointerClick;

        //// Callback
        //entry.callback.AddListener((eventData) => { ButtonClickEvent(); });

        //Debug.Log("placeObjectButton: " + placeObjectButton.ToString());
        //placeObjectButton.onClick.AddListener(ButtonClickEvent);
    }

    // Update is called once per frame
    void Update()
    {
        // Load model button
        if (!isModelBtnLoaded) {
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

        //if (placementPoseIsValid)
        //{
        //    PlaceObject();
        //}

        //if (!TryGetTouchPosition(out Vector2 touchposition))
        //    return;

        //bool arManagerRaycast = arManager.Raycast(touchposition, hits, TrackableType.PlaneWithinPolygon);
        //Debug.Log("arManagerRaycast: " + arManagerRaycast.ToString());

        //if (arManagerRaycast)
        //{
        //    var hitPose = hits[0].pose;
        //    Debug.Log("hitPose: " + hitPose.ToString());

        //    Debug.Log("spawnedObject: " + spawnedObject.ToString());
        //    Debug.Log("objectToPlace: " + objectToPlace.ToString());

        //    if (spawnedObject == null)
        //    {
        //        spawnedObject = Instantiate(objectToPlace, hitPose.position, hitPose.rotation);
        //    }
        //    else
        //    {
        //        spawnedObject.transform.position = hitPose.position;
        //    }
        //}
    }

    private void ButtonClickEvent()
    {
        Debug.Log("ButtonClickEvent()");
        if (placementPoseIsValid)
        {
            PlaceObject();
        }
    }

    bool TryGetTouchPosition(out Vector2 touchposition)
    {
        if (Input.touchCount > 0)
        {
            touchposition = Input.GetTouch(0).position;
            Debug.Log("touchPosition: " + touchPosition.ToString());
            return true;
        }

        touchposition = default;
        return false;
    }

    private void PlaceObject()
    {
        Debug.Log("objectToPlace: " + objectToPlace.ToString());

        /*
        // Domino
        ////GameObject domino = Instantiate(objectToPlace, placementPose.position, placementPose.rotation);
        //domino = Instantiate(objectToPlace, placementPose.position, placementPose.rotation);
        //Debug.Log("domino" + domino.ToString());
        //dominos.Add(domino);

        //// Throw Object
        //GameObject newObj = Instantiate(objectToPlace, placementPose.position, placementPose.rotation);
        //float force = 5f;
        //newObj.GetComponent<Rigidbody>().velocity = new Vector3(0, force, 0);

        // Create and move Object
        //spawnedObject = Instantiate(objectToPlace, placementPose.position, placementPose.rotation);
        */

        // Sprint#1 create model
        GameObject[] respawn = GameObject.FindGameObjectsWithTag("ModelName");
        foreach (var t in respawn)
        {
            Destroy(t);
        }

        Debug.Log("placeObjectButton.ToString(): " + placeObjectButton.ToString());
        Debug.Log("placeObjectButton.GetComponent<Button>(): " + placeObjectButton.GetComponent<Button>().ToString());
        Debug.Log("placeObjectButton.GetComponent<Image>(): " + placeObjectButton.GetComponent<Image>().ToString());
        Debug.Log("placeObjectButton.GetComponent<Image>().name: " + placeObjectButton.GetComponent<Image>().name);
        Debug.Log("placeObjectButton.GetComponent<Image>().sprite.name: " + placeObjectButton.GetComponent<Image>().sprite.name);
        Debug.Log("placeObjectButton.GetComponent<Button>().GetComponent<Image>().name: " + placeObjectButton.GetComponent<Button>().GetComponent<Image>().name);

        string dir = "Models/" + placeObjectButton.GetComponent<Button>().GetComponent<Image>().name + "/" + placeObjectButton.GetComponent<Button>().GetComponent<Image>().name;
        dir = "Models/DragonSD_A/DragonSD_A";
        string aniDir = "Animations/Walking";
        Debug.Log("dir: " + dir);

        Animator animator;
        GameObject instance;

        if (Resources.LoadAll<GameObject>(dir).Length != 0)
        {
            Debug.Log("Resources.Load(dir, typeof(GameObject)): " + Resources.Load(dir, typeof(GameObject)).ToString());
            instance = Instantiate(Resources.Load(dir, typeof(GameObject)), placementPose.position, placementPose.rotation) as GameObject;
            Debug.Log("instance: " + instance);
            instance.tag = "ModelName";
            animator = instance.GetComponent<Animator>();
            Debug.Log(animator);

            instance.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(aniDir);
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

            var cameraForward = Camera.current.transform.forward;
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
