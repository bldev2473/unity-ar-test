using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARSubsystems;
using System;

using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ARTapToPlaceObject : MonoBehaviour
{
    public GameObject objectToPlace;
    public GameObject placementIndicator;
    private ARRaycastManager arManager;
    private Pose placementPose;
    public List<GameObject> dominos;
    public GameObject domino;

    private bool onClick = false;

    public Button placeObjectButton;

    private bool placementPoseIsValid = false;

    // Start is called before the first frame update
    void Start()
    {
        arManager = FindObjectOfType<ARRaycastManager>();
        dominos = new List<GameObject>();

        //// Apply EventTrigger to GameObject
        //EventTrigger trigger = placeObjectButton.AddComponent<EventTrigger>();
        //EventTrigger.Entry entry = new EventTrigger.Entry();
        //entry.eventID = EventTriggerType.PointerClick;

        //// Callback
        //entry.callback.AddListener((eventData) => { ButtonClickEvent(); });

        placeObjectButton.onClick.AddListener(ButtonClickEvent);
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();

        if (placementPoseIsValid && onClick)
        {
            PlaceObject();
            onClick = false;
        }
    }

    private void ButtonClickEvent()
    {
        onClick = true;
    }

    private void PlaceObject()
    {
        Debug.Log("objectToPlace" + objectToPlace.ToString());
        //GameObject domino = Instantiate(objectToPlace, placementPose.position, placementPose.rotation);
        domino = Instantiate(objectToPlace, placementPose.position, placementPose.rotation);
        Debug.Log("domino" + domino.ToString());
        dominos.Add(domino);
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
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
