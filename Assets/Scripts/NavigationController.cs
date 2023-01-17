using System.Collections;
using System.Collections.Generic;
using RosMessageTypes.BuiltinInterfaces;
using UnityEngine;
using RosMessageTypes.Geometry;

public class NavigationController : MonoBehaviour
{
    
    private PoseStampedMsg _navGoal;
    
    [SerializeField]
    private GameObject _goalMarker;
    
    private PoseStampedPublisher _navGoalPublisher;

    private TimeMsg _timeStamp;

    // Start is called before the first frame update
    void Start()
    {
        float stime = Time.time;
        _timeStamp.sec = (uint)stime;
        Debug.Log("float time " + stime + " Time.time" + _timeStamp.sec);
        _navGoalPublisher = transform.GetComponent<PoseStampedPublisher>();
    }

    // Update is called once per frame
    void Update()
    {
        _navGoalPublisher.SetPosition(
            _goalMarker.transform.position.x,
            _goalMarker.transform.position.y,
            _goalMarker.transform.position.z);
        
        _navGoalPublisher.SetOrientation(
            _goalMarker.transform.rotation.x,
            _goalMarker.transform.rotation.y,
            _goalMarker.transform.rotation.z,
            _goalMarker.transform.rotation.w);
        
        // _navGoalPublisher.SetTime();
    }
    
    
    public void PublishNavGoal(GameObject NavGoal)
    {
        
    }
}
