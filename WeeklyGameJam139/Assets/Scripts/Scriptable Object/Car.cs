using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Car", menuName = "ScriptableObject/Car", order = 1)]
public class Car : ScriptableObject
{
    public float speed = 5f;
    public int dir = 2;
}
