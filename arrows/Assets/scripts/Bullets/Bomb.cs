using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bomb : BulletBehavior
{
    public GameObject bulletPrefab;
    private Rigidbody2D RB2D;
    public ParticleSystem ExplosionParticlesPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FillBulletBehaviorData(BulletBehavior bulletBehavior)
    {
        Vector2 MousePos = Mouse.current.position.ReadValue();
        MousePos = Camera.main.ScreenToWorldPoint(MousePos);
        bulletBehavior.Owner = Owner;
        bulletBehavior.From = gameObject;
        bulletBehavior.direction = (MousePos - new Vector2(transform.position.x, transform.position.y)).normalized;
        bulletBehavior.TargetPosition = MousePos;
        bulletBehavior.Charge = Charge;
        bulletBehavior.ChargeMax = ChargeMax;
        bulletBehavior.LifeTime = NextBullet.LifeTime;
        bulletBehavior.InitialSpeed = NextBullet.InitialSpeed;
        bulletBehavior.NumBulletMax = NextBullet.NumBulletMax;
        bulletBehavior.Frequency = NextBullet.Frequency;
        bulletBehavior.BulletType = NextBullet.BulletPrefab;
        bulletBehavior.NextBullet = NextBullet.NextBullet;
    }

    IEnumerator WaitForExplosion()
    {
        yield return new WaitForSeconds(LifeTime);
        RB2D.velocity = Vector2.zero;
        ParticleSystem bombParticles = Instantiate(ExplosionParticlesPrefab);
        bombParticles.transform.position = transform.position;
        bombParticles.Play();
        int numOfBullets = Mathf.FloorToInt((Charge / ChargeMax) * NumBulletMax);
        for (int i = 0; i < numOfBullets; i++)
        {
            GameObject Bullet = Instantiate(bulletPrefab);
            BulletBehavior bulletBehavior = Bullet.GetComponent<BulletBehavior>();
            FillBulletBehaviorData(bulletBehavior);
            bulletBehavior.direction = new Vector2(Random.Range(-10,10), Random.Range(-10, 10)).normalized;
            bulletBehavior.Fire();
        }
        yield return new WaitForSeconds(LifeTime);
        Destroy(gameObject);
    }


    public override void Fire()
    {
        RB2D = gameObject.GetComponent<Rigidbody2D>();
        transform.position = From.transform.position;
        bulletPrefab = NextBullet.BulletPrefab;
        base.Fire();
        gameObject.SetActive(true);
        RB2D.AddForce(direction * InitialSpeed * 50);
        StartCoroutine(WaitForExplosion());


    }
}
