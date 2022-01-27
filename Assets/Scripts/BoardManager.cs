using System;
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

    public static string[,] Map1;
    public static string[,] Map2;
    public static string[,] Map3;
    public static string[,] Map4;
    public static string[,] Map5;
    public static string[,] Map6;
    public static int screen;
    int lastScreen;
    bool wereMapsRead = false;
    public static GameObject avatar;
    public static Vector3 newPlayerPos;


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
    public GameObject[] Doors;
    public GameObject[] Key;            // the exit will show up if the player got the key
    public GameObject[] PointItems;     //collect those item to get points.
    public GameObject[] EnemyKing;  //the new enemy we create. 

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
                    tileChoice = foodTiles[0];
                    Instantiate(tileChoice, new Vector3(x, y, 0f), Quaternion.identity);
                }

                if (Map[y + 1, x + 1] == "S")
                {
                    tileChoice = foodTiles[1];
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

                //Door to level 1
                else if (Map[y + 1, x + 1] == "1")
                {
                    tileChoice = Doors[0];
                    Instantiate(tileChoice, new Vector3(x, y, 0f), Quaternion.identity);
                }

                //Door to level 2
                else if (Map[y + 1, x + 1] == "2")
                {
                    tileChoice = Doors[1];
                    Instantiate(tileChoice, new Vector3(x, y, 0f), Quaternion.identity);
                }

                //Door to level 3
                else if (Map[y + 1, x + 1] == "3")
                {
                    tileChoice = Doors[2];
                    Instantiate(tileChoice, new Vector3(x, y, 0f), Quaternion.identity);
                }

                //Door to level 4
                else if (Map[y + 1, x + 1] == "4")
                {
                    tileChoice = Doors[3];
                    Instantiate(tileChoice, new Vector3(x, y, 0f), Quaternion.identity);
                }

                //Door to level 5
                else if (Map[y + 1, x + 1] == "5")
                {
                    tileChoice = Doors[4];
                    Instantiate(tileChoice, new Vector3(x, y, 0f), Quaternion.identity);
                }

                //Door to level 6
                else if (Map[y + 1, x + 1] == "6")
                {
                    tileChoice = Doors[5];
                    Instantiate(tileChoice, new Vector3(x, y, 0f), Quaternion.identity);
                }


                else if (Map[y + 1, x + 1] == "P")
                {
                    tileChoice = PointItems[Random.Range(0, PointItems.Length)];
                    Instantiate(tileChoice, new Vector3(x, y, 0f), Quaternion.identity);
                }

                else if(Map[y + 1, x + 1] == "B")
                {
                    tileChoice = EnemyKing[0];
                    Instantiate(tileChoice, new Vector3(x, y, 0f), Quaternion.identity);
                }

                else if (Map[y + 1, x + 1] == "Q")
                {
                    tileChoice = exit;
                    Instantiate(tileChoice, new Vector3(x, y, 0f), Quaternion.identity);
                }
            }
        }
    }

    string[,] ReadMapFromFile(string filename)
    {
        string somePath = "Assets/Scripts";
        string path = Path.Combine(somePath, filename);
        string[,] Map;

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
        Map2 = ReadMapFromFile("Map2.txt");
        Map3 = ReadMapFromFile("Map3.txt");
        Map4 = ReadMapFromFile("Map4.txt");
        Map5 = ReadMapFromFile("Map5.txt");
        Map6 = ReadMapFromFile("Map6.txt");
    }

    void PlayerScreenTransition(int cur, int last)
    {
        avatar = GameObject.FindWithTag("Player");
        int x = 0;
        int y = 0;

        if ((last == 1 && cur == 2) ||
            (last == 2 && cur == 3))
        {
            x = 0;
            y = 4;
        }
        else if ((last == 3 && cur == 2) ||
                (last == 2 && cur == 1))
        {
            x = 7;
            y =4;
        }
        else if ((last == 6 && cur == 5) ||
                (last == 5 && cur == 2) ||
                (last == 2 && cur == 4))
        {
            x = 3;
            y = 0;
        }
        else if ((last == 4 && cur == 2) ||
                (last == 2 && cur == 5) ||
                (last == 5 && cur == 6))
        {
            x = 3;
            y = 7;
        }


        newPlayerPos = new Vector3(x, y, 0);
        avatar.transform.position = newPlayerPos;
    }

    public void SetupScene()
    {
        lastScreen = screen;
        screen = Player.screen;
        PlayerScreenTransition(screen, lastScreen);

        //Read Maps only ones
        if (!wereMapsRead)
        {
            ReadAllMaps();
            wereMapsRead = true;
        }

        if (screen == 1)
        {
            BoardSetup(Map1);
            LayoutObjects(Map1);
        }
        else if (screen == 2)
        {
            BoardSetup(Map2);
            LayoutObjects(Map2);
        }
        else if (screen == 3)
        {
            BoardSetup(Map3);
            LayoutObjects(Map3);
        }
        else if (screen == 4)
        {
            BoardSetup(Map4);
            LayoutObjects(Map4);
        }
        else if (screen == 5)
        {
            BoardSetup(Map5);
            LayoutObjects(Map5);
        }
        else if (screen == 6)
        {
            BoardSetup(Map6);
            LayoutObjects(Map6);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
