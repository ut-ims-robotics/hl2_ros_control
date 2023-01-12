using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Unity.Robotics;
using Unity.Robotics.UrdfImporter;

public class ManipulatorJointControl : MonoBehaviour
{
    [SerializeField]
    private GameObject _manipulatorBaseLink;
    
    [System.Serializable]
    public struct JointDescription
    {
        public GameObject jointObject;
        public string jointName;
        public double rotationAngle;
    }
    
    public void SetSubscribeJointStates(bool publish) //adaptation of ROS# function
    {
        if (publish)
        {
            if (_manipulatorJoints.Length > 0)
            {
                return;
            }
            _addJoints(_manipulatorBaseLink.transform);
            ArticulationBody[] _allBodies = Resources.FindObjectsOfTypeAll<ArticulationBody>();
            foreach (var VARIABLE in _allBodies)
            {
                VARIABLE.enabled = false;
            }
        }
        else
        {
            _manipulatorJoints = new JointDescription[0];
            ArticulationBody[] _allBodies = Resources.FindObjectsOfTypeAll<ArticulationBody>();
            foreach (var VARIABLE in _allBodies)
            {
                VARIABLE.enabled = true;
            }
        }
    }
    
    JointDescription[] AppendToArray(JointDescription[] originalArray, JointDescription newElement)
    {
        JointDescription[] newArray = new JointDescription[originalArray.Length + 1];
        for (int i = 0; i < originalArray.Length; i++)
        {
            newArray[i] = originalArray[i];
        }
        newArray[newArray.Length - 1] = newElement;
        return newArray;
    }

    private void _addJoints(Transform ParentTransform)
    {
        Component[] children = ParentTransform.GetComponentsInChildren(typeof(Transform));
        foreach (var child in children)
        {
            if (child.GetComponent<ArticulationBody>() != null)
            {
                // Debug.Log("Found joint: " + child.name + " with articulation body");
                JointDescription jointDescriptionStruct = new JointDescription();
                jointDescriptionStruct.jointObject = child.gameObject;
                jointDescriptionStruct.jointName = child.name;
                jointDescriptionStruct.rotationAngle = 0;
                _manipulatorJoints = AppendToArray(_manipulatorJoints, jointDescriptionStruct);

            }
        }
    }
    
    public JointDescription[] _manipulatorJoints;

    [SerializeField]
    private bool _automaticArticulationBodyRemoval = true;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int index = 0; index < _manipulatorJoints.Length; index++)
        {
            
            _manipulatorJoints[index].jointName = _manipulatorJoints[index].jointObject.name;
        }

        if (!_automaticArticulationBodyRemoval)
        {
            return;
        }
    }

    float angle_r = 0;
    private void Update()
    {
        
        MoveJoint("upper_arm_link",angle_r);
        MoveJoint("shoulder_link",angle_r);
        MoveJoint("forearm_link",angle_r);
        angle_r += 0.1f;
    }

    public void MoveJoint(string JointName, float angle)
    {
        for (int i = 0; i < _manipulatorJoints.Length; i++)
        {
            if (_manipulatorJoints[i].jointName != JointName)
            {
                Debug.Log(JointName + " is not " + _manipulatorJoints[i].jointName);
                continue;
            }
            _manipulatorJoints[i].jointObject.transform.Rotate(Vector3.up, (float)_manipulatorJoints[i].rotationAngle-angle, Space.Self);
            _manipulatorJoints[i].rotationAngle = angle;
            break;
        }
    }
}
