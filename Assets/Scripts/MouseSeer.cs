using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSeer : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 MousePos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.Log(r.origin);
            RaycastHit2D hit = Physics2D.Raycast(r.origin,r.direction);

            
            Debug.DrawRay(r.origin, r.direction * 1000, Color.yellow);
            if (hit.collider != null)
            {
                hit.transform.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
        

    }
}
