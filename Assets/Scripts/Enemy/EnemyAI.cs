using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    enum State {Patrol, Idle, Attack};
    State myState;
    public float speed = 0.01f;
    public float distance = 5f;
    private Vector3 currentPos;
    private Animator animator;
    private float timer;

    private void Start()
    {
        
        myState = State.Patrol;
        currentPos = this.transform.position;
        animator = GetComponent<Animator>();
        timer = 0;
    }

    private void Update()
    {

        switch(myState)
        {
            case State.Patrol:
                Patrol();
                break;
            case State.Idle:
                Idle();
                break;

        }
    }

    private void Idle()
    {
        timer += 1;
        animator.SetFloat("speed", 0);
        if(timer > 300)
        {
            timer = 0;
            myState = State.Patrol;
        }
    }
    private void Patrol()
    {
        Vector3 target;
        switch (gameObject.tag)
        {
            case "MeleeEnemy":
                //move left first then right
                //Debug.Log("MeleeEnemy");
                animator.SetFloat("speed", speed);             
                target = currentPos - new Vector3(distance, 0, 0);
                this.transform.position = Vector3.MoveTowards(this.transform.position, target, speed);
                if(Vector3.Distance(this.transform.position, target) < 0.1)
                {
                    //if distance is negative it moves right
                    distance = -distance;
                    currentPos = this.transform.position;
                    if (distance < 0)
                    {
                        this.transform.localRotation = Quaternion.Euler(0, 180, 0);
                    }
                    else
                    {
                        this.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    }
                    myState = State.Idle;
                }
                
                break;

            case "RangedEnemy":
                //Debug.Log("rangedEnemy");
                animator.SetFloat("speed", speed);
               target = currentPos - new Vector3(0, distance, 0);
                this.transform.position = Vector3.MoveTowards(this.transform.position, target, speed);
                if (Vector3.Distance(this.transform.position, target) < 0.1)
                {
                    //if distance is negative it moves right
                    distance = -distance;
                    currentPos = this.transform.position;
                    myState = State.Idle;
                }

                break;
        }
    }

}
