/*using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System.IO;

public class BoardManager : MonoBehaviour
{
    [Serializable]
    public class Count
    {
        public int minimum;
        public int maximum;

        public Count(int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    public string[,] Map;
    public string[,] Map1;
    public string[,] Map2;
    public string[,] Map3;
    public string[,] Map4;
    public string[,] Map5;
    public string[,] Map6;

    public int columns = 8;
    public int rows = 8;
    public Count wallCount = new Count(5, 9);
    public Count foodCount = new Count(1, 5);
    public GameObject exit;
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] foodTiles;
    public GameObject[] enemyTiles;
    public GameObject[] outerWallTiles;
    public GameObject[] Door;
    public GameObject[] Key;            // the exit will show up if the player got the key
    public GameObject[] PointItems;     //collect those item to get points.

    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        gridPositions.Clear();

        for (int x = 1; x < columns - 1; x++)
        {
            for (int y = 1; y < rows - 1; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }

    void BoardSetup(string[,] Map)
    {
        boardHolder = new GameObject("Board").transform;

        for (int x = -1; x < columns + 1; x++)
        {
            for (int y = -1; y < rows + 1; y++)
            {
                //INSTANTIATE FLOOR OR OUTER WALL
                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];

                if (Map[y + 1, x + 1] == "X")
                {
                    toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];
                }

                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHolder);
            }
        }
    }

    void LayoutObjects(string[,] Map)
    {
        GameObject tileChoice;

        for (int x = -1; x < columns + 1; x++)
        {
            for (int y = -1; y < rows + 1; y++)
            {

                if (Map[y + 1, x + 1] == "F")
                {
                    tileChoice = foodTiles[Random.Range(0, foodTiles.Length)];
                    Instantiate(tileChoice, new Vector3(x, y, 0f), Quaternion.identity);
                }

                else if (Map[y + 1, x + 1] == "E")
                {
                    tileChoice = enemyTiles[Random.Range(0, enemyTiles.Length)];
                    Instantiate(tileChoice, new Vector3(x, y, 0f), Quaternion.identity);
                }

                else if (Map[y + 1, x + 1] == "K")
                {
                    tileChoice = Key[0];
                    Instantiate(tileChoice, new Vector3(x, y, 0f), Quaternion.identity);
                }

                else if (Map[y + 1, x + 1] == "W")
                {
                    tileChoice = wallTiles[Random.Range(0, wallTiles.Length)];
                    Instantiate(tileChoice, new Vector3(x, y, 0f), Quaternion.identity);
                }

                else if (Map[y + 1, x + 1] == "D")
                {
                    tileChoice = Door[0];
                    Instantiate(tileChoice, new Vector3(x, y, 0f), Quaternion.identity);
                }
            }
        }
    }

    string[,] ReadMapFromFile(string filename)
    {
        Array.Clear(Map, 0, Map.Length);

        string somePath = @"C:\Users\jairl\Documents\GitHub\Scavenger-main\Assets\Scripts";
        string path = Path.Combine(somePath, filename);

        string[] all_lines = System.IO.File.ReadAllLines(path);  //Read each line, store as "all_lines" array

        //Create 2D array
        char[] delim_chars = { ' ' };  //Delimiter set to a PIPE
        Map = new string[10, 10];  // Initialize 2D Array

        int i = 0; int j = 0;
        foreach (string l in all_lines)
        {
            j = 0;
            string[] words = all_lines[i].Split(delim_chars);
            foreach (string s in words)
            {
                Map[i, j] = s;
                j++;
            }
            i++;
        }

        return Map;
    }

    void ReadAllMaps()
    {
        Map1 = ReadMapFromFile("Map1.txt");
    }

    public void SetupScene(int level)
    {
        Debug.Log(level);
        ReadAllMaps();

        BoardSetup(Map1);
        LayoutObjects(Map1);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
*/