using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UR5RealController : MonoBehaviour
{
    private BoolPublisher BoolPublisherHandle;
    private BoolSubscriber BoolSubscriberHandle;

    private bool _toSend;
    // Start is called before the first frame update
    void Start()
    {
        BoolPublisherHandle = transform.GetComponent<BoolPublisher>();
        BoolSubscriberHandle = transform.GetComponent<BoolSubscriber>();
    }

    // Update is called once per frame
    void Update()
    {
        if (BoolPublisherHandle.accessBool() && BoolSubscriberHandle.accessMessage())
        {
            BoolPublisherHandle.UpdateBool(false);
        }
    }
}
