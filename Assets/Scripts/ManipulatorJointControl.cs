using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Unity.Robotics;
using Unity.Robotics.UrdfImporter;

public class ManipulatorJointControl : MonoBehaviour
{
    [System.Serializable]
    public enum RotationAxis
    {
        X,
        Y,
        Z
    };
    
    //TODO: automate script to read all the joints from robot (apparently there is high chance it is possible
    //TODO: and attach their object to the controller here.
    
    //write code to automatically fill the array of joint names based on object names used

    [SerializeField]
    private GameObject _manipulatorBaseLink;
    
    [System.Serializable]
    public struct JointDescription
    {
        public GameObject jointObject;
        public RotationAxis axis;
        public string jointName;
    }
    
    public void SetSubscribeJointStates(bool publish) //adaptation of ROS# function
    {
        if (publish)
        {
            _addJoints(_manipulatorBaseLink.transform);
        }
        else
        {
            _manipulatorJoints = new JointDescription[0];
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
                jointDescriptionStruct.axis = RotationAxis.X;
                _manipulatorJoints = AppendToArray(_manipulatorJoints, jointDescriptionStruct);
                // _addJoints(child.transform);
            }
        }
        // Debug.Log("list of joints: " + _manipulatorJoints);
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
        ArticulationBody[] _allBodies = Resources.FindObjectsOfTypeAll<ArticulationBody>();
        foreach (var VARIABLE in _allBodies)
        {
            VARIABLE.enabled = false;
        }

        SetSubscribeJointStates(true); //test
    }

    public void MoveJoint(string JointName, float angle)
    {
        foreach (var VARIABLE in _manipulatorJoints)
        {
            if (VARIABLE.jointName != JointName)
            {
                continue;
            }
            switch (VARIABLE.axis)
            {
                case RotationAxis.X:
                    VARIABLE.jointObject.transform.rotation = Quaternion.Euler(angle, 0f, 0f);
                    break;
                
                case RotationAxis.Y:
                    VARIABLE.jointObject.transform.rotation = Quaternion.Euler(0f, angle, 0f);
                    break;
                
                case RotationAxis.Z:
                    VARIABLE.jointObject.transform.rotation = Quaternion.Euler(0f, 0f, angle);
                    break;
                
                default:
                    break;
            }
        }
    }
}
