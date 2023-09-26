using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO.Enumeration;

[CreateAssetMenu(fileName ="Building Preset", menuName ="New Building Preset")]
public class BuildingPresets : ScriptableObject
{
    public int cost;
    public int costPerTurn;
    public GameObject prefab;

    public int population;
    public int jobs;
    public int food;
}
