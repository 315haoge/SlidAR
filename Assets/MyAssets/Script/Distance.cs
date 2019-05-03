using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Distance : MonoBehaviour
{
    public GameObject worldCenter;
    private Text t;
    // Start is called before the first frame update
    void Start()
    {
        t = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        t.text = Vector3.Distance(worldCenter.transform.position,Camera.main.transform.position).ToString();
    }
}
