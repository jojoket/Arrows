using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCharge : MonoBehaviour
{

    public AttackData AttackData;
    public List<GameObject> ChargeVisuals = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float oneVisualCharge = AttackData.ChargeMax / ChargeVisuals.Count;
        int currentCharge = Mathf.FloorToInt(AttackData.Charge / oneVisualCharge);
        if (currentCharge >= ChargeVisuals.Count)
        {
            return;
        }
        GameObject currentVisualCharging = ChargeVisuals[currentCharge];
        float currentAlpha = (AttackData.Charge / oneVisualCharge)%1;
        Color visualChargingColor = currentVisualCharging.GetComponent<SpriteRenderer>().color;
        currentVisualCharging.GetComponent<SpriteRenderer>().color = new Color(visualChargingColor.r, visualChargingColor.g, visualChargingColor.b, currentAlpha);
    }
}
