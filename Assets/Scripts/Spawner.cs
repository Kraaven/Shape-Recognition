using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Dot;

    public bool place;

    public List<GameObject> Dots;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            place = !place;
        }
    }

    public void FixedUpdate()
    {
        if (place)
        {
            Dots.Add(Instantiate(Dot, Input.mousePosition,Quaternion.identity));
        }
    }
}
