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

    private ROSConnection ros;
    
    // Start is called before the first frame update
    void Start()
    {
        ros = ROSConnection.GetOrCreateInstance();
        ros.RegisterPublisher<PoseStampedMsg>(navigationGoalTopic);
        this._goal.header.frame_id = "map";
        this._goal.header.stamp = new TimeMsg(0, 0);
    }

    // void Update()
    // {
    //     throw new NotImplementedException();
    //     this._goal.pose.position = new PointMsg((float)_navGoal.transform.localPosition.z,
    //         -(float)_navGoal.transform.localPosition.x, (float)_navGoal.transform.localPosition.y);
    //     //convert unity frame to ROS frame as they differ
    //     this._goal.pose.orientation = new QuaternionMsg((float)_navGoal.transform.localRotation.x,
    //         (float)_navGoal.transform.localRotation.y, -(float)_navGoal.transform.localRotation.z,
    //         -(float)_navGoal.transform.localRotation.w);
    //
    //     ros.Publish(navigationGoalTopic, this._goal);
    // }
}
