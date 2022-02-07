using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



/* Finite StateMachine
 * A finite statemachine is an artificial intelligence system for storing state data and transition between them.
 * Animation system is an example for it.
 * A finite statemachine that defines all the states they can be in and how they can get from one state to another state.
 * First determine the states of the character.
 * In zombie character we determined five states
 * 1)Idle 2)wander 3)Chase 4)Attack 5)Dead
 */
public class ZombieController : MonoBehaviour
{
    Animator anim;
    public GameObject targetPlayer;
    NavMeshAgent enemyAgent;

    enum STATE
    {
        IDLE,WANDER,CHASE,ATTACK,DEAD
    };
    STATE state = STATE.IDLE;


    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        enemyAgent = GetComponent<NavMeshAgent>();
        //anim.SetBool("isWalking", true);

    }
    void TurnOffAnimTriggers()
    {
        anim.SetBool("isWalking", false);
        anim.SetBool("isAttacking", false);
        anim.SetBool("isRunning", false);
        anim.SetBool("isDead", false);

    }

    // Update is called once per frame
    void Update()
    {
        //enemyAgent.SetDestination(targetPlayer.transform.position);
        //if (enemyAgent.remainingDistance > enemyAgent.stoppingDistance)
        //{
        //    anim.SetBool("isWalking", true);
        //    anim.SetBool("isAttacking", false);

        //}
        //else
        //{
        //    anim.SetBool("isWalking", false);
        //    anim.SetBool("isAttacking", true);
        //}
        switch (state)
        {
            case STATE.IDLE:
                break;
            case STATE.WANDER:
                break;
            case STATE.CHASE:
                break;
            case STATE.ATTACK:
                break;
            case STATE.DEAD:
                break;
            default:
                break;
        }


    }
}
