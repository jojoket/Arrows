using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Burst : BulletBehavior
{
    public GameObject bulletPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (From)
        {
            transform.position = From.transform.position;
        }
    }

    void FillBulletBehaviorData(BulletBehavior bulletBehavior)
    {
        Vector2 MousePos = Mouse.current.position.ReadValue();
        MousePos = Camera.main.ScreenToWorldPoint(MousePos);
        bulletBehavior.Owner = Owner;
        bulletBehavior.From = gameObject;
        bulletBehavior.direction = direction;
        bulletBehavior.TargetPosition = transform.position + new Vector3(direction.x, direction.y,0) *4;
        if (NextBullet.isTargetMouse)
        {
            bulletBehavior.direction = (MousePos - new Vector2(transform.position.x, transform.position.y)).normalized;
            bulletBehavior.TargetPosition = MousePos;
        }
        bulletBehavior.Charge = Charge;
        bulletBehavior.ChargeMax = ChargeMax;
        bulletBehavior.LifeTime = NextBullet.LifeTime;
        bulletBehavior.InitialSpeed = NextBullet.InitialSpeed;
        bulletBehavior.NumBulletMax = NextBullet.NumBulletMax;
        bulletBehavior.Frequency = NextBullet.Frequency;
        bulletBehavior.BulletType = NextBullet.BulletPrefab;
        bulletBehavior.NextBullet = NextBullet.NextBullet;
    }

    IEnumerator FireBullets()
    {
        int numOfBullets = Mathf.FloorToInt((Charge / ChargeMax) * NumBulletMax);
        for (int i = 0; i < numOfBullets; i++)
        {
            GameObject Bullet = Instantiate(bulletPrefab);
            BulletBehavior bulletBehavior = Bullet.GetComponent<BulletBehavior>();
            FillBulletBehaviorData(bulletBehavior);
            bulletBehavior.Fire();
            yield return new WaitForSeconds(1/Frequency);
        }
        Destroy(gameObject);
    }

    public override void Fire()
    {
        transform.position = From.transform.position;
        bulletPrefab = NextBullet.BulletPrefab;
        base.Fire();
        StartCoroutine(FireBullets());
    }
}
