using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotSimulationControl : MonoBehaviour
{

    private ArticulationBody _robot;

    [SerializeField] private GameObject _robotObject;

    public string armBaseLink;

    // Start is called before the first frame update
    void Start()
    {
        _robot = _robotObject.transform.GetComponent<ArticulationBody>();
    }

    private void ParseArm()
    {
        //parse the arm related joints for later usage
    }

    public void MoveRobot(Vector3 newUnityPosition, Quaternion newUnityOrientation)
    {
        
        //use teleportRoot function of articulation body
        //to move the robot around
        //make sure to check scripting API
    }

    public void MoveArm(List<float> JointValues)
    {
        //use setJointPosition function of articulation body
        //to specify arms joint state
    }
}
