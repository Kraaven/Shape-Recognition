using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Comparer : MonoBehaviour
{
    public SpriteRenderer[,] grid;
    public SpriteRenderer[,] grid2;
    public GenerateGrids G1;
    public GenerateGrids G2;
    public TMP_Text result;

    public int[,] boolgrid;

    public int InitScore;
    public int compareScore;
    public float accurate;
    
    // Start is called before the first frame update
    void Start()
    {
        boolgrid = new int[20, 20];
        
        grid = G1.StartGen();
        grid2 = G2.StartGen();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Compare()
    {
        InitScore = 0;
        compareScore = 0;
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                if (grid[i, j].color == Color.red)
                {
                    InitScore++;
                }
                if (grid[i, j].color == grid2[i,j].color && grid[i,j].color == Color.red)
                {
                    compareScore++;
                }

            }
        }

        accurate = (float)compareScore / (float)InitScore;
        result.text = "Result: " + (int)(accurate*100)+ "%";
    }

    public void ClearScreens()
    {
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                grid[i,j].color = Color.white;
                grid2[i, j].color = Color.white;
            }
        }

        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                boolgrid[i, j] = 0;
            }
        }
        result.text = "";
    }
}
