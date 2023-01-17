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
    private GameObject _pose;
    [SerializeField]
    private String navigationGoalTopic = "/nav_goal";
    [SerializeField]
    private String navigationGoalFrame = "map";

    private PoseStampedMsg _goal;

    private ROSConnection ros;

    [SerializeField]
    private bool _constPublish = false;
    
    // Start is called before the first frame update
    void Start()
    {
        ros = ROSConnection.GetOrCreateInstance();
        ros.RegisterPublisher<PoseStampedMsg>(navigationGoalTopic);
        this._goal.header.frame_id = navigationGoalFrame;
        this._goal.header.stamp = new TimeMsg(0, 0);
    }

    private void Update()
    {
        SetPosition(_pose);
        SetRotation(_pose);
        if (_constPublish)
        {
            GoalPublish();
        }
    }

    public void GoalPublish()
    {
        ros.Publish(navigationGoalTopic, this._goal);
    }
    
    public void SetPosition(GameObject pose)
    {
        this._goal.pose.position.x = pose.transform.position.z;
        this._goal.pose.position.y = -pose.transform.position.x;
        this._goal.pose.position.z = pose.transform.position.y;
        //different coordinate systems
    }
    public void SetPosition(float UnityX, float UnityY, float UnityZ)
    {
        this._goal.pose.position.x = UnityZ;
        this._goal.pose.position.y = -UnityX;
        this._goal.pose.position.z = UnityY;
        //different coordinate systems
    }
    
    public void SetRotation(GameObject pose)
    {
        Quaternion UnityRotation = pose.transform.rotation;

        Quaternion ROSRotation = Quaternion.identity;
        ROSRotation.eulerAngles = new Vector3(
            UnityRotation.eulerAngles.z,
            -UnityRotation.eulerAngles.x,
            -UnityRotation.eulerAngles.y);

        this._goal.pose.orientation.x = UnityRotation.x;
        this._goal.pose.orientation.y = UnityRotation.y;
        this._goal.pose.orientation.z = UnityRotation.z;
        this._goal.pose.orientation.w = UnityRotation.w;
    }
    public void SetOrientation(float UnityX, float UnityY, float UnityZ, float UnityW)
    {
        Quaternion UnityRotation = new Quaternion(UnityX, UnityY, UnityZ, UnityW);

        Quaternion ROSRotation = Quaternion.identity;
        ROSRotation.eulerAngles = new Vector3(
            UnityRotation.eulerAngles.z,
            -UnityRotation.eulerAngles.x,
            -UnityRotation.eulerAngles.y);

        this._goal.pose.orientation.x = UnityRotation.x;
        this._goal.pose.orientation.y = UnityRotation.y;
        this._goal.pose.orientation.z = UnityRotation.z;
        this._goal.pose.orientation.w = UnityRotation.w;
    }
    
    public void SetTime(TimeMsg time)
    {
        this._goal.header.stamp = time;
    }
}
