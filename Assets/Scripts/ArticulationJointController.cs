//code heavily "inspired" by https://github.com/Unity-Technologies/articulations-robot-demo

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArticulationJointController : MonoBehaviour
{
    private ArticulationBody _articaltionJoint;

    public float rotationSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        _articaltionJoint = transform.GetComponent<ArticulationBody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void RotateTo(float primaryAxisRotation)
    {
        var drive = _articaltionJoint.xDrive;
        drive.target = primaryAxisRotation;
        _articaltionJoint.xDrive = drive;
    }
}
