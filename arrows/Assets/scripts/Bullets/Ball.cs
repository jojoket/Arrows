using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : BulletBehavior
{
    private Rigidbody2D RB2D;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(LifeTime);
        Destroy(gameObject);
    }

    public override void Fire()
    {
        transform.position = From.transform.position;
        transform.localScale += new Vector3((Charge / ChargeMax)*transform.localScale.x, (Charge / ChargeMax)*transform.localScale.x, (Charge / ChargeMax)*transform.localScale.x);
        RB2D = gameObject.GetComponent<Rigidbody2D>();
        base.Fire();
        gameObject.SetActive(true);
        RB2D.AddForce(direction * InitialSpeed*100);
        StartCoroutine(Death());
    }
}
