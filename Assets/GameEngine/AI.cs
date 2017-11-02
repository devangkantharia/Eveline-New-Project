using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshAgent))]
public class AI : MonoBehaviour {
    private NavMeshAgent aiAgent;
    public PlayerController player;
    public float distanceToInteract;
    public float distanceToKill;
    public PauseMenu pauseMenu;

    public Image gameOverImage;

    public bl_SceneLoader reloadLevelScreen;

    [Tooltip("In secondi")]
    public float tempoGameOver = 3f;
    public AudioClip suonoGameOver;
    public string levelName;
    private float tempoPassato = 0f;

    public float wanderRadius;
    public float wanderTimer;

    private Transform target;
    private float timer;

    public bool isAlreadyLoading = false;
    bool gameOver;

    // Use this for initialization
    void OnEnable()
    {
       aiAgent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }

    public void Start()
    {
        gameOverImage.enabled = false;
    }

    public void FixedUpdate()
    {
        float distance = Vector3.Distance(aiAgent.transform.position, player.transform.position);

        if (distance > distanceToInteract)
        {
            Idle();
        } else if(distance <=distanceToInteract && distance > distanceToKill)
        {
            Follow();
        } else if(distance <= distanceToKill)
        {
            Attack();
        }
    }

    public void Idle()
    {
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            aiAgent.SetDestination(newPos);
            timer = 0;
        }
    }

    public void Follow()
    {
        aiAgent.destination = player.transform.position;
    }

    public void Attack()
    {
        if (!gameOver)
        {
            player.enabled = false;
            gameOverImage.enabled = true;
            pauseMenu.enabled = false;
            gameOver = true;
        }

        if (tempoPassato <= tempoGameOver)
        {
            tempoPassato += Time.deltaTime;
        }
        else
        {
            gameOverImage.enabled = false;
            if (!isAlreadyLoading)
            {
                reloadLevelScreen.LoadLevel(levelName);
                isAlreadyLoading = true;
            }
            tempoPassato = 0f;
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
}