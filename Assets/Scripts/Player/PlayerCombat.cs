using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    //Hitboxes
    public GameObject attackHitBox;
    public GameObject kickHitbox;

    //Damage Numbers
    public int attackDamage = 20;
    public int kickDamage = 40;

    //Timers
    public float attackActive = 0.7f;
    public float startUp = 0.2f;
    public float reset = 1f;

    public float attackSpeed = 0.2f;
    public float kickSpeed = 0.7f;

    //Coroutine Controllers
    public bool isAttacking = false;
    public bool isReset = false;

    //camera
    public CameraShake cameraShake;
    public float magnitude = 0.05f;
    public float duration = 0.05f;
    //Misc
    public int attackCount = 0;
    private Animator animator;
    private PlayerMovement pm;

  
    private void Start()
    {
        attackHitBox.SetActive(false);
        kickHitbox.SetActive(false);
        animator = GetComponent<Animator>();
        pm = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        //Primary Attack
        if (Input.GetButtonDown("Fire1") && !isAttacking && pm.groundCheck)
        {
            animator.SetFloat("Speed", 0);
            pm.canMove = false;

            isAttacking = true;
            StartCoroutine(Attack());
           
        }
        else if (Input.GetButtonDown("Fire2") && !isAttacking && pm.groundCheck)
        {
            animator.SetFloat("Speed", 0);
            pm.canMove = false;

            isAttacking = true;
            attackCount = 2;
            StartCoroutine(Attack());
            
        }
    }

    private void LateUpdate()
    {
        if (isReset)
        {
            StartCoroutine(Reset());
        }
    }

    IEnumerator Attack()
    {
        //Coroutine Limiter and Startup Window
        isAttacking = true;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(startUp);
   
        //Do the attack
        if (attackCount < 2)
        {
            attackCount++;
            
            attackHitBox.SetActive(true);
            yield return new WaitForSeconds(attackActive);
            attackHitBox.SetActive(false);
            StartCoroutine(cameraShake.Shake(duration, magnitude));
            yield return new WaitForSeconds(attackSpeed);
        }
        else
        {
            kickHitbox.SetActive(true);
            attackCount = 0;
            yield return new WaitForSeconds(attackActive);
            kickHitbox.SetActive(false);
            StartCoroutine(cameraShake.Shake(duration, magnitude));
            yield return new WaitForSeconds(kickSpeed);
        }
        
        //Resetting
        isAttacking = false;
        pm.canMove = true;
        isReset = true;
    }

    IEnumerator Reset()
    {
        isReset = false;
        //Debug.Log("Reset");

        yield return new WaitForSeconds(reset);

        if (isAttacking)
        {
            yield break;
        }
        else
        {
            //Debug.Log("Reset Attack Counter");
            attackCount = 0;
        }
    }
}
