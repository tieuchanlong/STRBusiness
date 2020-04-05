using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class Definitions contains the basic data for direction
/// This is a good practice for Singleton
/// </summary>
public class Definitions
{
    public static Definitions Instance = new Definitions();
    public float speedScale = 1;
    public List<KeyValuePair<int, int>> Directions = new List<KeyValuePair<int, int>>() { new KeyValuePair<int, int>(0, 1), new KeyValuePair<int, int>(1, 1), new KeyValuePair<int, int>(1, 0), new KeyValuePair<int, int>(1, -1), new KeyValuePair<int, int>(0, -1), new KeyValuePair<int, int>(-1, -1), new KeyValuePair<int, int>(-1, 0), new KeyValuePair<int, int>(-1, 1) };

    private Definitions()
    {

    }
}
