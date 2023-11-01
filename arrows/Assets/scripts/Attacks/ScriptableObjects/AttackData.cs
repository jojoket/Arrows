using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnumBulletType {Ball, Burst, Bomb, Boomerang};

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Attack", order = 1)]
public class AttackData : ScriptableObject
{
    public string AttackName;
    public GameObject AttackChargePrefab;
    public GameObject BulletPrefab;
    public float Charge;
    public float ChargeMax;
    public float ChargeBySec;
    public EnumBulletType BulletType;
    public AttackData NextBullet;
    public bool isTargetMouse;
    public float LifeTime;
    public float InitialSpeed;
    public int NumBulletMax;
    public float Frequency;
    public float CoolDown;
}
