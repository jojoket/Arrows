using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnumTargetType {Closest, Farthest, Random};
public enum EnumMovingType {Straight, Flee, Random, Boid};


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EnemyData", order = 1)]
public class EnemyData : ScriptableObject
{
    public string EnemyName;
    public float Speed;
    public EnumTargetType TargetType;
    public EnumMovingType MovingType;
}
