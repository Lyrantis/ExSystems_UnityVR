using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.ProBuilder;

public class Node
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

public class NodeInformation
{
    public Node node;
    public NodeInformation parent;
    public float gCost, hCost, fCost;

    public NodeInformation(Node node, NodeInformation parentToAdd, float gCost, int maxX, int maxY)
    {
        this.node = node;
        this.parent = parentToAdd;
        this.gCost = gCost;
        GenerateHeuristic(maxX, maxY);
        this.fCost = gCost + hCost;
    }

    private void GenerateHeuristic(int maxX, int maxY)
    {
        hCost = (maxX - node.positionOnGrid.x) + (maxY - node.positionOnGrid.y);
    }
}

public class PipeWall : MonoBehaviour
{
    [SerializeField]
    public GameObject puzzleStartCorner;

    [SerializeField]
    public int PuzzleWidth = 5;
    
    [SerializeField]
    public int PuzzleHeight = 5;

    [SerializeField]
    public GameObject startPipe;

    [SerializeField]
    public GameObject endPipe;

    [SerializeField]
    public GameObject straightPipe;
    
    [SerializeField]
    public GameObject LPipe;

    [SerializeField]
    public GameObject TPipe;

    [SerializeField]
    public GameObject XPipe;

    private List<List<Node>> grid = new List<List<Node>>();

    private Pipe endPipeInstance;

    private void GridSetup()
    {
        for (int i = 0; i < PuzzleHeight; i++)
        {
            grid.Add(new List<Node>());
            for (int j = 0; j < PuzzleWidth; j++)
            {
                grid[i].Add(new Node(new Vector2(j, i)));
            }
        }

        for (int i = 0; i < PuzzleHeight; i++)
        {
            grid.Add(new List<Node>());
            for (int j = 0; j < PuzzleWidth; j++)
            {
                if (i == j & j == 0)
                {
                    grid[i][j].neighbours.Add(grid[i][j + 1]);
                    grid[i][j].neighbourCosts.Add(UnityEngine.Random.Range(1, 4));
                    continue;
                }
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
                    grid[i][j].neighbourCosts.Add(UnityEngine.Random.Range(1, 10));
                }
            }
        }
    }

    private void GeneratePuzzle()
    {

        GridSetup();

        List<NodeInformation> OpenList = new List<NodeInformation>();
        List<NodeInformation> ClosedList = new List<NodeInformation>();
        NodeInformation current = null;
        Node end = grid[PuzzleHeight - 1][PuzzleWidth - 1];
        bool pathFound = false;

        current = new NodeInformation(grid[0][0], null, 0.0f, PuzzleWidth, PuzzleHeight);

        while (!pathFound)
        {
            if (current.node == end)
            {
                pathFound = true;

                List<List<int>> pipeGridOutput = new List<List<int>>();

                for (int i = 0; i <PuzzleHeight; i++)
                {
                    pipeGridOutput.Add(new List<int>());

                    for (int j = 0; j <PuzzleWidth; j++)
                    {
                        pipeGridOutput[i].Add(0);
                    }
                }

                while (current.parent.parent != null)
                {
                    if (Mathf.Abs(current.node.positionOnGrid.x - current.parent.parent.node.positionOnGrid.x) == 2 || Mathf.Abs(current.node.positionOnGrid.y - current.parent.parent.node.positionOnGrid.y) == 2)
                    {
                        pipeGridOutput[(int)current.parent.node.positionOnGrid.y][(int)current.parent.node.positionOnGrid.x] = 1;
                    }
                    else
                    {
                        pipeGridOutput[(int)current.parent.node.positionOnGrid.y][(int)current.parent.node.positionOnGrid.x] = 2;
                    }
                    current = current.parent;
                }
                for (int y = 0; y < pipeGridOutput.Count; y++)
                {
                    for (int x = 0; x < pipeGridOutput[y].Count; x++)
                    {
                        if (x == y & y == 0)
                        {
                            Instantiate(startPipe, puzzleStartCorner.transform.position, Quaternion.identity, null);
                            continue;
                        }
                        else if (x == y & y == PuzzleHeight - 1)
                        {
                            endPipeInstance = Instantiate(endPipe, puzzleStartCorner.transform.position + new Vector3(-1.0f * 0.3f * x, -1.0f * 0.3f * y, 0.0f), Quaternion.identity, null).GetComponent<Pipe>();
                            continue;
                        }
                        GameObject temp = new GameObject();

                        if (pipeGridOutput[y][x] == 0)
                        {
                            int prefabToSpawn = UnityEngine.Random.Range(1, 100);

                            if (prefabToSpawn <= 5)
                            {
                                temp = XPipe;
                            }
                            else if (prefabToSpawn <= 10)
                            {
                                temp = TPipe;
                            }
                            else if (prefabToSpawn <= 55)
                            {
                                temp = straightPipe;
                            }
                            else
                            {
                                temp = LPipe;
                            }
                        }
                        else
                        {
                            float randomChance = UnityEngine.Random.Range(0.0f, 1.0f);

                            if (randomChance < 0.05f)
                            {
                                int prefabToSpawn = UnityEngine.Random.Range(3, 5);

                                switch (prefabToSpawn)
                                {
                                    case 3:
                                        temp = TPipe;
                                        break;
                                    case 4:
                                        temp = XPipe;
                                        break;
                                    default:
                                        Debug.Log("Unrecognised Symbol");
                                        break;
                                }
                            }
                            else
                            {
                                switch (pipeGridOutput[y][x])
                                {
                                    case 1:
                                        temp = straightPipe;
                                        break;
                                    case 2:
                                        temp = LPipe;
                                        break;
                                    default:
                                        Debug.Log("Unrecognised Symbol");
                                        break;
                                }
                            }
                        }
                        Vector3 offset = new Vector3(-1.0f * 0.3f * x, -1.0f * 0.3f * y, 0.0f);
                        temp = Instantiate(temp, puzzleStartCorner.transform.position + offset, Quaternion.identity, null);
                        temp.GetComponent<Pipe>().OnConnectionGained += CheckForPath;
                    }
                }
            }

            for (int i = 0; i < current.node.neighbours.Count; i++)
            {
                NodeInformation neighbour = new NodeInformation(current.node.neighbours[i], current, current.gCost + current.node.neighbourCosts[i], PuzzleWidth, PuzzleHeight);

                if (OpenList.Contains(neighbour))
                {
                    int foundIndex = OpenList.IndexOf(neighbour);
                    if (foundIndex >= 0)
                    {
                        if (OpenList[foundIndex].gCost > neighbour.gCost)
                        {
                            OpenList[foundIndex] = neighbour;
                        }
                    }
                }
                else if (ClosedList.Contains(neighbour))
                {
                    int foundIndex = ClosedList.IndexOf(neighbour);
                    if (foundIndex >= 0)
                    {
                        if (ClosedList[foundIndex].gCost > neighbour.gCost)
                        {
                            ClosedList[foundIndex] = neighbour;
                        }
                    }
                }
                else
                {
                    OpenList.Add(neighbour);
                }
            }

            OpenList.Remove(current);
            ClosedList.Add(current);

            if (OpenList.Count == 0)
            {
                Debug.Log("Failed - Open List is Empty");
                break;
            }
            else
            {
                current = OpenList[0];

                for (int i = 0; i < OpenList.Count - 1; i++)
                {
                    if (OpenList[i + 1].gCost < current.gCost)
                    {
                        current = OpenList[i + 1];
                    }
                }
            }
        }
    }

    private void Start()
    {
        GeneratePuzzle();
    }

    private void CheckForPath()
    {
         if (endPipeInstance.CheckForStartConnection(null))
        {
            gameObject.GetComponent<Puzzle>().OnPuzzleCompleted();
            Destroy(endPipeInstance.gameObject);
        }
    }
}
