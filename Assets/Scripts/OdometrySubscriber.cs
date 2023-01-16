using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RosMessageTypes.Nav;
using Unity.Robotics.ROSTCPConnector;

public class OdometrySubscriber : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private string topicName = "/odom";

    private GameObject _robotBaseLink;
    
    
    void Start()
    {
        ROSConnection ros = ROSConnection.GetOrCreateInstance();
        ros.Subscribe<OdometryMsg>(topicName, ReceiveMessage);
    }
    
    void ReceiveMessage(OdometryMsg msg)
    {
        Debug.Log("Received message: " + msg);
        _robotBaseLink.transform.localPosition = new Vector3(
            (float)msg.pose.pose.position.y*-1,
            (float)msg.pose.pose.position.z,
            (float)msg.pose.pose.position.x);
        
        Quaternion ROSRotation = new Quaternion(
            (float)msg.pose.pose.orientation.x,
            (float)msg.pose.pose.orientation.y,
            (float)msg.pose.pose.orientation.z,
            (float)msg.pose.pose.orientation.w);
        Quaternion UnityRotation = Quaternion.identity;
        UnityRotation.eulerAngles = new Vector3(0.0f, -ROSRotation.eulerAngles.z, 0.0f);
        _robotBaseLink.transform.localRotation = UnityRotation;
    }
}
