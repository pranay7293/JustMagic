using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Animator animator;   
    public float moveSpeed;
    public float timeToDieInFiringZone;

    private Vector3 targetPosition;
    private bool isDead = false;

    private bool isInFiringZone = false;
    private float firingZoneTimer = 0f;

    private int timeStart;
    void Start()
    {
        targetPosition = new Vector3(-9.7f, 0f, -7.7f);
    }

    void Update()
    {
        if (!isDead)
        {
            MoveTowardsTarget();
        }

        if(isInFiringZone) 
        {
            firingZoneTimer += Time.deltaTime;

            if (firingZoneTimer >= timeToDieInFiringZone)
            {
                Die();
            }
        }
    }

    private void MoveTowardsTarget()
    {
        float step = moveSpeed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        Vector3 direction = (targetPosition - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, step);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FiringZone"))
        {
            Debug.Log("Entered Firing Zone");
            firingZoneTimer = 0f;
            isInFiringZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("FiringZone"))
        {
            Debug.Log("Exited Firing Zone");
            isInFiringZone = false;
            firingZoneTimer = 0f;
        }
    }

    private void Die()
    {
        isDead = true;
        animator.SetTrigger("Dead");
        Destroy(gameObject, 2);
    }

}
