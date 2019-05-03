using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollowObject : MonoBehaviour
{
    private GameObject objectToFollow; 
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SetObjectToFollow(GameObject go)
    {
        objectToFollow = go;
    }
    // Update is called once per frame
    void Update()
    {
        if (objectToFollow != null)
        {
            this.transform.position = Camera.main.WorldToScreenPoint(objectToFollow.transform.position);
        }
    }
}
