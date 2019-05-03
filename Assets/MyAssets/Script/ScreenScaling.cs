using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenScaling : MonoBehaviour
{

    public float objectScale = 0.01f;
    [SerializeField]
    private Vector3 initialScale;
    private Plane plane;
    [SerializeField]
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        initialScale = transform.localScale;
        if (cam == null)
        {
            cam = Camera.main;
        }
    }

    // Update is called once per frame
    void Update()
    {
        plane = new Plane(cam.transform.forward, cam.transform.position);
        float dist = plane.GetDistanceToPoint(transform.position);
        transform.localScale = initialScale * dist * objectScale;
    }
}
