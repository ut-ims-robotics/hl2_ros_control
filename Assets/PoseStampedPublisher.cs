using System;
using System.Collections;
using System.Collections.Generic;
using RosMessageTypes.BuiltinInterfaces;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Geometry;

public class PoseStampedPublisher : MonoBehaviour
{
    [SerializeField]
    private GameObject _navGoal;
    [SerializeField]
    private String navigationGoalTopic = "/nav_goal";

    private PoseStampedMsg _goal;
    
    // Start is called before the first frame update
    void Start()
    {
        ROSConnection ros = ROSConnection.GetOrCreateInstance();
        ros.RegisterPublisher<PoseStampedMsg>(navigationGoalTopic);
        this._goal.header.frame_id = "map";
        this._goal.header.stamp.sec = 0;
        this._goal.header.stamp.nanosec = 0;
    }

    void Update()
    {
        // throw new NotImplementedException();
        this._goal.pose.position = new PointMsg((float)_navGoal.transform.localPosition.z,
            -(float)_navGoal.transform.localPosition.y, (float)_navGoal.transform.localPosition.y);
        // this._goal.pose.orientation = new
        //check quaternion converison
    }
}
