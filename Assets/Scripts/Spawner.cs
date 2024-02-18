using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using Input = UnityEngine.Input;
using Newtonsoft.Json;
using System.IO;
using Application = UnityEngine.Application;
using File = System.IO.File;

public class Spawner : MonoBehaviour
{
    public GameObject Dot;

    public bool place;

    public List<GameObject> _Dots;
    public List<DOT> points;
    public Spline line;
    private List<String> saves;

    public float threshold;
    // Start is called before the first frame update
    void Start()
    {
        points = new List<DOT>();
        _Dots = new List<GameObject>();
        line = GetComponent<SplineContainer>()[0];

        if (!Directory.Exists(Application.dataPath + "/Saves"))
        {
            Directory.CreateDirectory(Application.dataPath+"/Saves");
            if (File.Exists(Application.dataPath + "/Saves/settings.json"))
            {
                saves = JsonConvert.DeserializeObject <List<String>>(File.ReadAllText(Application.dataPath + "/Saves/settings.json"));
            }
            else
            {
                saves = new List<String>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            place = !place;
            if (place)
            {
                _Dots.Add(Instantiate(Dot, Camera.main.ScreenToWorldPoint(Input.mousePosition)+ new Vector3(0,0,1) ,Quaternion.identity));
            }
            else
            {
                foreach (var obj in _Dots)
                {
                    points.Add(new DOT(obj.transform.position,obj.transform.rotation));
                    
                }
                LoadCurve(false);

            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveCurve();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            line.Clear();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadCurve(true);
        }
    }

    public void FixedUpdate()
    {
        if (place)
        {
            Vector3 InputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector3.Distance(_Dots[_Dots.Count-1].transform.position,InputPos) > threshold)
            {
               _Dots.Add(Instantiate(Dot, InputPos+ new Vector3(0,0,1) ,Quaternion.identity));
                var Diff =_Dots[_Dots.Count - 1].transform.InverseTransformPoint(_Dots[_Dots.Count - 2].transform.position);
                var angle = Mathf.Atan2(Diff.x, Diff.y)*Mathf.Rad2Deg;
                _Dots[_Dots.Count - 1].transform.Rotate(0,0, -angle + 180);
            }
        
        }
    }

    public void SaveCurve()
    {
        String curveJson = JsonConvert.SerializeObject(points);
        Debug.Log(curveJson);
        File.WriteAllText(Application.dataPath + "/Saves/Curve.json",curveJson);
    }

    public void LoadCurve(bool load)
    {
        List<DOT> P = new List<DOT>();
        if (load)
        {
            var json = File.ReadAllText(Application.dataPath + "/Saves/Curve.json");
            Debug.Log(json);
            P = JsonConvert.DeserializeObject<List<DOT>>(json);
        }
        else
        {
            P = points;
        }
        line.Clear();

        foreach (var point in P)
        {
            
            var knot = new BezierKnot(point.position);
            knot.Rotation = point.rotation;
            line.Add(knot);
        }
                
        line.SetTangentMode(TangentMode.AutoSmooth);
        foreach (var dots in _Dots)
        {
            Destroy(dots);
        }
        _Dots.Clear();
    }
}
