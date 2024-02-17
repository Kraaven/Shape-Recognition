using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Quaternion = System.Numerics.Quaternion;

public class GenerateGrids : MonoBehaviour
{
    public GameObject[,] grid;
    public GameObject Tile;

    public SpriteRenderer[,] sprites;

    // Start is called before the first frame update
    public SpriteRenderer[,] StartGen()
    {
        sprites = new SpriteRenderer[20, 20];
        grid = new GameObject[20, 20];

        for (float i = 0; i < 20; i++)
        {
            for (float j = 0; j < 20; j++)
            {
                grid[(int)i, (int)j] = Instantiate(Tile, transform.position + new Vector3(i * 0.3f, j * -0.3f, 0),
                    quaternion.identity);
                sprites[(int)i, (int)j] = grid[(int)i, (int)j].GetComponent<SpriteRenderer>();
            }
        }

        return sprites;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
