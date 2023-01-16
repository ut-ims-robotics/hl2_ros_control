using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RosMessageTypes.Geometry;

public class NavigationController : MonoBehaviour
{
    [SerializeField]
    private PoseStampedMsg _navGoal;
    
    private PoseStampedPublisher _navGoalPublisher;

    // Start is called before the first frame update
    void Start()
    {
        _navGoalPublisher = transform.GetComponent<PoseStampedPublisher>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void PublishNavGoal(GameObject NavGoal)
    {
    }
}
