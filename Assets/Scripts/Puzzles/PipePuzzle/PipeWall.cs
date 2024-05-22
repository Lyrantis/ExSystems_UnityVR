using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public struct Node
{
    public Vector2 positionOnGrid;
    public List<Node> neighbours;
    public List<int> neighbourCosts;

    public Node(Vector2 initPos)
    {
        positionOnGrid = initPos;
        neighbours = new List<Node>();
        neighbourCosts = new List<int>();
    }

}

public class PipeWall : MonoBehaviour
{
    [SerializeField]
    public int PuzzleWidth = 5;
    
    [SerializeField]
    public int PuzzleHeight = 5;

    private List<List<Node>> grid = new List<List<Node>>();

    private void GridSetup()
    {
        for (int i = 0; i < PuzzleHeight; i++)
        {
            grid.Add(new List<Node>());
            for (int j = 0; j < PuzzleWidth; j++)
            {
                grid[i].Add(new Node(new Vector2(i, j)));
            }
        }

        for (int i = 0; i < PuzzleHeight; i++)
        {
            grid.Add(new List<Node>());
            for (int j = 0; j < PuzzleWidth; j++)
            {
                if (i > 0)
                {
                    grid[i][j].neighbours.Add(grid[i - 1][j]);
                }
                if (i < PuzzleHeight - 1)
                {
                    grid[i][j].neighbours.Add(grid[i + 1][j]);
                }
                if (j > 0)
                {
                    grid[i][j].neighbours.Add(grid[i][j - 1]);
                }
                if (j < PuzzleHeight - 1)
                {
                    grid[i][j].neighbours.Add(grid[i][j + 1]);
                }
                for (int count = 0; count < grid[i][j].neighbours.Count; count++)
                {
                    grid[i][j].neighbourCosts.Add(UnityEngine.Random.Range(1, 4));
                }
            }
        }
    }

    private void GeneratePuzzle()
    {
        //Start at [0][0]
        //End at [Height][Width]
        //A* pathfindng
        //Iterate through path, assigning appropriate pipe pieces for connection type (T's and +'s can be added anywhere, randomly)
        //Fill rest of grid with random pipes 

    }
}
