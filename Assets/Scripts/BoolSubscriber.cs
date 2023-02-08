using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RosMessageTypes.Std;
using Unity.Robotics.ROSTCPConnector;

public class BoolSubscriber : MonoBehaviour
{
    private BoolMsg _boolMSG;
    private ROSConnection ros;
    [SerializeField]
    private String boolTopic = "/is_done";

    // Start is called before the first frame update
    void Start()
    {
        _boolMSG.data = false;
        ros = ROSConnection.GetOrCreateInstance();
    }

    void ReceiveMessage(BoolMsg msg)
    {
        _boolMSG = msg;
    }

    public bool accessMessage()
    {
        return _boolMSG.data;
    }
}
