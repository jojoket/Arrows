using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public List<AttackData> AttackData =  new List<AttackData>();

    public int CurrentAttack = 0;
    private GameObject CurrentChargeInstance;
    public float CurrentCharge = 0;

    private bool isLoading = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        AttackData curAttack = AttackData[CurrentAttack];
        if (isLoading)
        {
            curAttack.Charge = Mathf.Clamp(curAttack.Charge + curAttack.ChargeBySec * Time.deltaTime, 0, curAttack.ChargeMax);
            CurrentCharge = curAttack.Charge;
        }

        if (CurrentChargeInstance)
        {
            CurrentChargeInstance.transform.position = transform.position;
        }
    }

    public void ChangeCurrentAttack(InputAction.CallbackContext input)
    {
        Vector2 offSetAdded = input.ReadValue<Vector2>();
        if (offSetAdded.y == 0)
            return;
        CurrentAttack = Mathf.Clamp(CurrentAttack + (int)Mathf.Sign(offSetAdded.y), 0, AttackData.Count-1);
    }

    void StartLoading()
    {
        isLoading = true;
        CurrentChargeInstance = Instantiate(AttackData[CurrentAttack].AttackChargePrefab);
        AttackCharge Attack = CurrentChargeInstance.GetComponent<AttackCharge>();
        Attack.AttackData = AttackData[CurrentAttack];
    }

    void FillBulletBehaviorData(BulletBehavior bulletBehavior)
    {
        AttackData curAttack = AttackData[CurrentAttack];
        Vector2 MousePos = Mouse.current.position.ReadValue();
        MousePos = Camera.main.ScreenToWorldPoint(MousePos);
        bulletBehavior.Owner = gameObject;
        bulletBehavior.From = gameObject;
        bulletBehavior.direction = (MousePos - new Vector2(transform.position.x, transform.position.y)).normalized;
        bulletBehavior.TargetPosition = MousePos;
        bulletBehavior.Charge = curAttack.Charge;
        bulletBehavior.ChargeMax = curAttack.ChargeMax;
        bulletBehavior.LifeTime = curAttack.LifeTime;
        bulletBehavior.InitialSpeed = curAttack.InitialSpeed;
        bulletBehavior.NumBulletMax = curAttack.NumBulletMax;
        bulletBehavior.Frequency = curAttack.Frequency;
        bulletBehavior.BulletType = AttackData[CurrentAttack].BulletPrefab;
        bulletBehavior.NextBullet = AttackData[CurrentAttack].NextBullet;
    }

    void StopLoading()
    {
        AttackData curAttack = AttackData[CurrentAttack];
        isLoading = false;
        GameObject Bullet = Instantiate(AttackData[CurrentAttack].BulletPrefab);
        BulletBehavior bulletBehavior = Bullet.GetComponent<BulletBehavior>();
        FillBulletBehaviorData(bulletBehavior);
        bulletBehavior.Fire();
        Destroy(CurrentChargeInstance);
        curAttack.Charge = 0;
    }

    public void InputLoading(InputAction.CallbackContext input)
    {
        bool isLoading = input.ReadValue<float>() == 1;
        if (isLoading)
        {
            StartLoading();
        }
        else
        {
            StopLoading();
        }
    }
}
