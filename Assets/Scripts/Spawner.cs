using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;

public class Spawner : MonoBehaviour
{
    public GameObject Dot;

    public bool place;

    public List<GameObject> Dots;
    public Spline line;

    public float threshold;
    // Start is called before the first frame update
    void Start()
    {
        Dots = new List<GameObject>();
        line = GetComponent<SplineContainer>()[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            place = !place;
            if (place)
            {
                Dots.Add(Instantiate(Dot, Camera.main.ScreenToWorldPoint(Input.mousePosition)+ new Vector3(0,0,1) ,Quaternion.identity));
            }
            else
            {
                line.Clear();

                foreach (var point in Dots)
                {
                    var knot = new BezierKnot(point.transform.position);
                    knot.Rotation = point.transform.rotation;
                    line.Add(knot);
                }
                
                line.SetTangentMode(TangentMode.AutoSmooth);
                
            }
        }
        
    }

    public void FixedUpdate()
    {
        if (place)
        {
            Vector3 InputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector3.Distance(Dots[Dots.Count-1].transform.position,InputPos) > threshold)
            {
                Dots.Add(Instantiate(Dot, InputPos+ new Vector3(0,0,1) ,Quaternion.identity));
                var Diff =Dots[Dots.Count - 1].transform.InverseTransformPoint(Dots[Dots.Count - 2].transform.position);
                var angle = Mathf.Atan2(Diff.x, Diff.y)*Mathf.Rad2Deg;
                Dots[Dots.Count - 1].transform.Rotate(0,0, -angle + 180);
            }
        
        }
    }
}
