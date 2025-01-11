using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Ghost : MonoBehaviour
{
    public float orbEatDistance = 0.3f;
    public NavMeshAgent agent;
    public float speed = 1f;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!agent.enabled) 
            return;

        GameObject closest = GetClosestOrb();

        Vector3 targetPosition = Vector3.zero;
        
        if (closest)
        {
            targetPosition = closest.transform.position;
        }
        else
            targetPosition = Camera.main.transform.position;

        agent.SetDestination(targetPosition);
        agent.speed = speed;
    }

    public void Kill()
    {
        agent.enabled = false;
        animator.SetTrigger("Death");
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public GameObject GetClosestOrb()
    {
        GameObject closest = null;
        float minDistance = Mathf.Infinity;

        List<GameObject> orbList = OrbSpawner.instance.spawnedOrbs;

        foreach (GameObject orb in orbList)
        {
            Vector3 ghostPosition = transform.position;
            ghostPosition.y = 0;
            Vector3 orbPosition = orb.transform.position;
            orbPosition.y = 0;

            float d = Vector3.Distance(ghostPosition, orbPosition);

            if (d < minDistance)
            {
                minDistance = d;
                closest = orb;
            }
        }

        if (minDistance < orbEatDistance) 
            OrbSpawner.instance.DestroyOrb(closest);

        return closest;
    }
}
