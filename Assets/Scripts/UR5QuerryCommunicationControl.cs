using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RosMessageTypes.Sensor;

public class UR5QuerryCommunicationControl : MonoBehaviour
{
    private PoseStampedPublisher PoseStampedPublisherHandle;
    private JointSubscriber JointSubscriberHandle;
    private ManipulatorJointControl ManipulatorJointControlHandle;
    private JointStateMsg JointState;

    [SerializeField]
    private GameObject _gameObject;
    
    // Start is called before the first frame update
    void Start()
    {
        PoseStampedPublisherHandle = transform.GetComponent<PoseStampedPublisher>();
        JointSubscriberHandle = transform.GetComponent<JointSubscriber>();
        ManipulatorJointControlHandle = transform.GetComponent<ManipulatorJointControl>();
    }

    // Update is called once per frame
    void Update()
    {
        PoseStampedPublisherHandle.SetPosition(_gameObject.transform.localPosition);
        PoseStampedPublisherHandle.SetOrientation(_gameObject.transform.localRotation);
        PoseStampedPublisherHandle.SetTfFrame("ur5e_base_link");
        PoseStampedPublisherHandle.GoalPublish();
        JointState = JointSubscriberHandle.GetJointState();
        int msgLength = JointState.name.Length;
        Debug.Log(JointState);
        for (int i = 0; i < msgLength; i++)
        {
            
            ManipulatorJointControlHandle.MoveJoint(JointState.name[i], (float)JointState.position[i]*Mathf.Rad2Deg);
        }
    }
}
