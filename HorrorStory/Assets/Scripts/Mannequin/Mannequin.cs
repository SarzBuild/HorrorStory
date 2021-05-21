using UnityEngine;
using UnityEngine.AI;
using UnityCore.Audio;

public class Mannequin : MonoBehaviour
{

    private Transform goal;
    private NavMeshAgent agent;
    private float timeToNextSoundEffect; 
    private float timeBetweenSoundEffects = 4.0f;
    public float speed;

    public UnityCore.Audio.AudioType[] soundEffects; 
    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        goal = GameObject.FindGameObjectWithTag("Player").transform;
        AudioAction.StartMannequinSound();
    }
    public void SpeedUp()
    {
        speed += 0.1f * speed;
    }
    private void OnTriggerEnter(Collider other)
    {
        //player dies 
        AudioAction.PlaySound(UnityCore.Audio.AudioType.GhostCatchesPlayer);
        timeToNextSoundEffect = 10000f;
    }
    void Update()
    {
        if(timeToNextSoundEffect < 0)
        {
            timeToNextSoundEffect = timeBetweenSoundEffects;
            int nextSoundEffect = (int)Mathf.Floor(Random.Range(0.0f, 3.99f));
            AudioAction.PlaySound(soundEffects[nextSoundEffect]);
        } else
        {
            timeToNextSoundEffect -= Time.deltaTime;
        }
        //transform.position =  Vector3.MoveTowards(transform.position, goal.position, speed * Time.deltaTime);
        //transform.LookAt(goal);
        float adjustedSpeed = speed; 
        if (Vector3.Distance(transform.position, goal.position) > 10)
        {
            adjustedSpeed += .5f * adjustedSpeed;
        } else if (Vector3.Distance(transform.position, goal.position) > 5)
        {

        }
        else
        {
            adjustedSpeed -= .5f * adjustedSpeed;

        }
        agent.speed = adjustedSpeed;
        agent.SetDestination(goal.position);

    }
}

