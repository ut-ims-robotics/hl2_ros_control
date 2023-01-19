using System;
using System.Collections;
using System.Collections.Generic;
using RosMessageTypes.BuiltinInterfaces;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Geometry;
using RosMessageTypes.Std;

public class PoseStampedPublisher : MonoBehaviour
{
    [SerializeField]
    private String navigationGoalTopic = "/nav_goal";

    private PoseStampedMsg _goal;

    private ROSConnection ros;
    
    
    // Start is called before the first frame update
    void Start()
    {
        ros = ROSConnection.GetOrCreateInstance();
        ros.RegisterPublisher<PoseStampedMsg>(navigationGoalTopic);
        _goal = new PoseStampedMsg();
        this._goal.header.stamp = new TimeMsg(0, 0);
    }

    public void GoalPublish()
    {
        ros.Publish(navigationGoalTopic, this._goal);
    }
    
    public void SetPosition(Vector3 unityCoords)
    {
        this._goal.pose.position.x = unityCoords.z;
        this._goal.pose.position.y = -unityCoords.x;
        this._goal.pose.position.z = unityCoords.y;
        //different coordinate systems
    }
    
    public void SetOrientation(Quaternion unityRotation)
    {

        Quaternion rosRotation = Quaternion.identity;
        rosRotation.eulerAngles = new Vector3(
            unityRotation.eulerAngles.z,
            -unityRotation.eulerAngles.x,
            -unityRotation.eulerAngles.y);

        this._goal.pose.orientation.x = unityRotation.x;
        this._goal.pose.orientation.y = unityRotation.y;
        this._goal.pose.orientation.z = unityRotation.z;
        this._goal.pose.orientation.w = unityRotation.w;
    }
    
    public void SetTfFrame(string frame)
    {
        this._goal.header.frame_id = frame;
    }
    public void SetTime(TimeMsg time)
    {
        this._goal.header.stamp = time;
    }
}
