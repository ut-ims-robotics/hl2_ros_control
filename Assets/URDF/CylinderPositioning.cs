using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderPositioning : MonoBehaviour
{

    [SerializeField]
    private GameObject _cylinder;

    [SerializeField]
    private float _localYCoordinate = -0.37f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _cylinder.transform.localPosition = new Vector3(
            _cylinder.transform.localPosition.x,
            _localYCoordinate,
            _cylinder.transform.localPosition.z);
    }
}
