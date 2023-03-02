using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RosMessageTypes.Std;
using Unity.Robotics.ROSTCPConnector;

public class BoolPublisher : MonoBehaviour
{
    private BoolMsg _boolMSG;
    private ROSConnection ros;
    [SerializeField]
    private String boolTopic = "/to_execute";

    void Start()
    {
        _boolMSG.data = false;
        ros = ROSConnection.GetOrCreateInstance();
        ros.RegisterPublisher<BoolMsg>(boolTopic);
    }

    public void UpdateBool(bool state)
    {
        _boolMSG.data = state;
    }

    public bool accessBool()
    {
        return _boolMSG.data;
    }

    public void Publish()
    {
        ros.Publish(boolTopic,_boolMSG);
    }
}
