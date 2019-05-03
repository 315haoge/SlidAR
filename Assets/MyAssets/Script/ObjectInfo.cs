using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInfo : MonoBehaviour {


	private Vector3 initCam;
	private Vector3 initPos;
	private string text;

    [SerializeField]
    private bool ScalingOrNot = true;

    //Scale Relative To Cam

    public float objectScale = 0.01f;
    [SerializeField]
    private Vector3 initialScale;
    private Plane plane;
    [SerializeField]
    private Camera cam;
	// Use this for initialization
	void Start () {
        initialScale = transform.localScale;
        if(cam == null)
        {
            cam = Camera.main;
        }
        
    }
	


	// Update is called once per frame
	void Update () {
        if (ScalingOrNot)
        {
            plane = new Plane(cam.transform.forward, cam.transform.position);
            float dist = plane.GetDistanceToPoint(transform.position);
            transform.localScale = initialScale * dist * objectScale;
        }
    }

	public Vector3 GetInitCam(){
		return initCam;
	}
	public void SetInitCam(Vector3 pos){
		initCam = pos;
	}

	public Vector3 GetInitPos(){
		return initPos;
	}
	public void SetInitPos(Vector3 pos){
		initPos = pos;
	}
}
