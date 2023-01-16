using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RosMessageTypes.Sensor;
using Unity.Robotics.ROSTCPConnector;

public class JointSubscriber : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField]
    private string topicName = "/joint_states";

    private ManipulatorJointControl jointControlHandler;
    //TODO: make a game object parser to get the joint objects from arm
    
    void Start()
    {
        jointControlHandler = transform.GetComponent<ManipulatorJointControl>();
        ROSConnection ros = ROSConnection.GetOrCreateInstance();
        ros.Subscribe<JointStateMsg>(topicName, JointStateCallback);
        

    }
    
    void JointStateCallback(JointStateMsg msg)
    {
        int msgLength = msg.name.Length;
        for (int i = 0; i < msgLength; i++)
        {
            jointControlHandler.MoveJoint(msg.name[i], (float)msg.position[i]);
        }
    }
}
