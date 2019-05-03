using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondCamera : MonoBehaviour
{
    [SerializeField]
    Camera thisCam;
    // Start is called before the first frame update
    void Start()
    {
        thisCam = gameObject.GetComponent<Camera>();
        thisCam.fieldOfView = Camera.main.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        thisCam.fieldOfView = Camera.main.fieldOfView;
    }
}
