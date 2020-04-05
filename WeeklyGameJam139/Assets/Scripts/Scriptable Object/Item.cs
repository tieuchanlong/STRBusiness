using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObject/Item")]
public class Item : ScriptableObject
{
    public string name;
    public float speed_up = 1f;
    public float effect_time = 10f;
}
