using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UIElements;

public class ChargePunch : MonoBehaviour
{
    float heldTime = 0f;
    float chargeStart = 0.1f;
    float chargeDiff = 0.15f;
    float maxCharge = 0.8f;

    bool charging = false;
    bool attacking = false;

    private Animator anim;
    private SpriteRenderer sr;

    private void Start()
    {
        Debug.Log("Charge Upper Bounds: " + (maxCharge + chargeDiff) + "s");
        Debug.Log("Charge Lower Bounds: " + (maxCharge - chargeDiff) + "s");

        anim = GetComponentInParent<Animator>();
        sr = GetComponentInParent<SpriteRenderer>();
    }

    void Update()
    {
        //Check if button held or button tapped
        if (Input.GetButton("Fire1") && !attacking) {
            heldTime += Time.deltaTime;

            if (heldTime > chargeStart && !charging)
            {
                charging = true;
                anim.SetBool("Charge", true);
                StartCoroutine(ChargeTimer());
                Debug.Log("Charge Attack");
            }

            Debug.Log("Held button for:" + heldTime + " seconds");
        }

        if (Input.GetButtonUp("Fire1") || heldTime > (maxCharge + chargeDiff) && !attacking)
        {
            anim.SetBool("Charge", false);
            attacking = true;

            //If tapped
            if (heldTime <= chargeStart)
            {
                //Debug.Log("Regular Attack");
            }
            //If held
            else if (heldTime <= (maxCharge + chargeDiff) && heldTime >= (maxCharge - chargeDiff)) {
                Debug.Log("CRITICAL HIT");
            }
            //If held, but past charge time
            else
            {
                Debug.Log("Normal Hit");
            }

            heldTime = 0;
            anim.SetTrigger("Attack");
            StartCoroutine(ChargeAttack());
        } 
    }

    IEnumerator ChargeTimer()
    {
        yield return new WaitForSeconds(maxCharge - chargeDiff);
        sr.color = Color.magenta;

        yield return new WaitForSeconds(0.2f);
        sr.color = Color.white;

        charging = false;
    }

    IEnumerator ChargeAttack()
    {
        yield return new WaitForSeconds(0.7f);
        attacking = false;
    }
}
