using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    [Header("Components to be used")]
    GameObject player;
    [SerializeField] Transform PlayerTransform;
    [SerializeField] Transform LimbsSpawnPos;
    [SerializeField] Animator animator;
    [SerializeField] GameManager gameManager;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] AudioSource ZombieSound;
    [SerializeField] GameObject Effect;
    [SerializeField] GameObject[] ZombieParts;
    [SerializeField] ParticleSystem DeathVFX;

    [Header("Enemy Properties")]
    public float ChaseRange;
    public float Health;
    public float Damage;
    public float ForceAmount;
    public int pointsOnKill;
    public float AttackCooldown = 1f;
    private float lastAttackTime;





    void Start()
    {
        //Storing the desired components into variables
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        PlayerTransform = GameObject.FindWithTag("MainCamera").transform;
        player = GameObject.FindGameObjectWithTag("Player");


        //Playing the zombie sound
        /*if (!ZombieSound.isPlaying)
        {
            ZombieSound.Play();
        }*/
    }

    void Update()
    {
        //Calling action functions
        AiLogic();
        HealthSystem();
    }

    void AiLogic()
    {


        //Calculating the distance between the player and the enemy
        float Distance = Vector3.Distance(transform.position, PlayerTransform.position);
        //Debug.Log(Distance);

        if (Distance <= agent.stoppingDistance)
        {
            //Calling the attack function
            Attack();
        }

        //If any of the prevoius conditions is not valid chase the player
        else
        {
            //Setting the destination of the ai to the player position
            agent.SetDestination(PlayerTransform.transform.position);
            animator.SetBool("Attack", false);
        }
    }

    void Attack()
    {
        //Setting the attack bool to true
        animator.SetBool("Attack", true);
        // Debug.Log("Attck");
        //Checking if the current time is equal or larger
        //Than the current time + the attack cool down value 
        if (Time.time >= lastAttackTime + AttackCooldown)
        {
            //Decreasing the player health
            player.GetComponent<VRPlayer>().DecreasePlayerHealth(Damage);
            // Reset attack cooldown
            lastAttackTime = Time.time;
        }
    }

    void HealthSystem()
    {
        //Chekcing if the enemy died or not 
        if (Health <= 0)
        {
            //Removing the zombie from the list
            //gameManager.ZombiesInTheScene.Remove(this.GetComponent<EnemyAi>());
            //Gift the player 100 points for killing the zombie
            //PlayerTransform.GetComponent<Player>().Points += 100;
            //Death Effect
            //GameObject effect = Instantiate(Effect, transform.position, Quaternion.identity);
            //Edit the effect properties
            //effect.GetComponent<Transform>().localScale *= 3f;
            //Apply death effects
            DeathEffect();
            //Destroy gameobject
            Destroy(gameObject);
        }
    }

    void DeathEffect()
    {

        foreach (GameObject limb in ZombieParts)
        {
            GameObject LimbPart = Instantiate(limb, transform.position, Quaternion.identity);
            Rigidbody rb = LimbPart.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(ForceAmount, LimbsSpawnPos.position, 5f);
            }
            ParticleSystem Effect = Instantiate(DeathVFX, transform.position, Quaternion.identity);
            Effect.Play();
            Destroy(Effect, 3f);
            Destroy(LimbPart, 3f);
            player.GetComponent<VRPlayer>().AddPoints(pointsOnKill);
        }
    }

    public void TakeDamage(float Damage)
    {
        //Decreasing the zombie health
        Health -= Damage;
        //Adding points to the player per shot
        //PlayerTransform.GetComponent<Player>().Points = PlayerTransform.GetComponent<Player>().Points + 10;
        //updating the UI for the player
        //PlayerTransform.GetComponent<Player>().DisplayPoints();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(this.transform.position, ChaseRange);
    }
}
