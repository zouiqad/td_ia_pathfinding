using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PathFindingSettings : ScriptableObject
{
    // Settings
    [Range(0.0f, 25.0f)] public float GroundCost = 1.0f;
    [Range(0.0f, 25.0f)] public float TreeCost = 3.0f;
    [Range(0.0f, 25.0f)] public float HillCost = 5.0f;

}
