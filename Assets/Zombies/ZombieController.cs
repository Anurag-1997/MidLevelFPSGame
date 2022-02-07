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

    bool ZombieCanSeePlayer()
    {
        //logic for zombie to see the player and chase
        //need to calculate distance to the player
        if(DistanceToPlayer()<10)
        {
            return true;
        }
        return false;

    }

    private float DistanceToPlayer()
    {
        return Vector3.Distance(targetPlayer.transform.position, this.transform.position);
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
                if(ZombieCanSeePlayer())
                {
                    state = STATE.CHASE;
                }
                else
                {
                    state = STATE.WANDER;
                }
                
                
                break;
            case STATE.WANDER:
                if(!enemyAgent.hasPath)
                {
                    float newRandomPositionX = this.transform.position.x + Random.Range(-10, 10);
                    float newRandomPositionZ = this.transform.position.z + Random.Range(-10, 10);
                    float newRandomPositionY = Terrain.activeTerrain.SampleHeight(new Vector3(newRandomPositionX, 0, newRandomPositionZ));
                    Vector3 finalDestination = new Vector3(newRandomPositionX, newRandomPositionY, newRandomPositionZ);
                    enemyAgent.SetDestination(finalDestination);
                    enemyAgent.stoppingDistance = 0.0f;
                    TurnOffAnimTriggers();
                    anim.SetBool("isWalking", true);
                }
                else if(ZombieCanSeePlayer())
                {
                    state = STATE.CHASE;
                }
                
                break;
            case STATE.CHASE:
                enemyAgent.SetDestination(targetPlayer.transform.position);
                enemyAgent.stoppingDistance = 0.5f;
                TurnOffAnimTriggers();
                anim.SetBool("isRunning", true);
                if(enemyAgent.remainingDistance<=enemyAgent.stoppingDistance )//&& !enemyAgent.pathPending)
                {
                    state = STATE.ATTACK;
                }
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
