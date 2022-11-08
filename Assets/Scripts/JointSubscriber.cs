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
    
    [SerializeField]
    private string[] jointNames;
    
    [SerializeField]
    private GameObject[] jointObjects;
    //TODO: make a game object parser to get the joint objects from arm
    
    void Start()
    {
        ROSConnection ros = ROSConnection.GetOrCreateInstance();
        ros.Subscribe<JointStateMsg>(topicName, JointStateCallback);
        
    }
    
    void JointStateCallback(JointStateMsg msg)
    {
        for (int i = 0; i < jointNames.Length; i++)
        {
            if (jointNames[i] == msg.name[i])
            {
                jointObjects[i].transform.localRotation = Quaternion.Euler(0, 0, (float)msg.position[i]);
            }
        }
    }
}
