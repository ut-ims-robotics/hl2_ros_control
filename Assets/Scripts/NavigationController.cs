using System.Collections;
using System.Collections.Generic;
using RosMessageTypes.BuiltinInterfaces;
using UnityEngine;
using RosMessageTypes.Geometry;
using System;

public class NavigationController : MonoBehaviour
{
    
    private PoseStampedMsg _navGoal;
    
    [SerializeField]
    private GameObject goalMarker;
    
    [SerializeField]
    private string tfFrame = "map";
    
    private PoseStampedPublisher _navGoalPublisher;

    private TimeMsg _timeStamp;

    // Start is called before the first frame update
    void Start()
    {
        double timeSinceEpoch =
            (DateTime.UtcNow -
             new DateTime(
                 1970,
                 1,
                 1,
                 0,
                 0,
                 0,
                 DateTimeKind.Utc)).TotalSeconds;
        _timeStamp = new TimeMsg();
        _timeStamp.sec = (uint)timeSinceEpoch;
        Debug.Log("float time " + timeSinceEpoch + " Time.time" + _timeStamp.sec);
        _navGoalPublisher = transform.GetComponent<PoseStampedPublisher>();
        if (_navGoalPublisher == null)
        {
            Debug.LogError("No PoseStampedPublisher component found on this object.");
        }

        _navGoal = new PoseStampedMsg();
        _navGoalPublisher.SetTfFrame(tfFrame);
    }

    // Update is called once per frame
    void Update()
    {
        _navGoalPublisher.SetPosition(goalMarker.transform.position);
        _navGoalPublisher.SetOrientation(goalMarker.transform.rotation);
        
        // _navGoalPublisher.SetTime();
    }
    
    
    public void PublishNavGoal(GameObject NavGoal)
    {
        
    }
}
