using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    //Bullet Parameters
    public GameObject Owner;
    public GameObject From;
    public GameObject BulletType;
    public AttackData NextBullet;
    public Vector2 direction;
    public Vector2 TargetPosition;
    public Vector3 InitialDirForce;
    public float InitialSpeed;
    public float Charge;
    public float ChargeMax;
    public float LifeTime;
    public int NumBulletMax;
    public float Frequency;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Fire()
    {

    }
}
