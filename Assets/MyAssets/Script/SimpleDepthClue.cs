using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDepthClue : MonoBehaviour
{

    private LineRenderer lr;
    public Material lineMat;
    public GameObject shadowPlane;
    // Start is called before the first frame update
    void Start()
    {
        lr = gameObject.AddComponent<LineRenderer>();
        lr.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject != null&& shadowPlane != null)
        {
            shadowPlane.transform.position = new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z);
            shadowPlane.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
            DrawDepthClue();
        }
        
    }

    private void DrawDepthClue()
    {
        lr.material = lineMat;
        /*
        lr.startColor = Color.red;
        lr.endColor = Color.red;
        */      
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        lr.positionCount = 2;
        lr.SetPosition(0, gameObject.transform.position);
        lr.SetPosition(1, shadowPlane.transform.position);
    }
}
