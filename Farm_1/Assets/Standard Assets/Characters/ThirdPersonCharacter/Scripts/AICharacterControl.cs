using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class AICharacterControl : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; } // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        public Transform target;// target to aim for (must be assigned on inspector?)
        private Vector3 targetdirection;
        private Vector3 aimeddirection;
        private Quaternion lookRotation;

        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();
            agent.updateRotation = false;
            agent.updatePosition = true;
            StartCoroutine(GoingToTarget());
            StartCoroutine(RotatetoTargetDirection());
        }

        IEnumerator GoingToTarget()
        {
            transform.Rotate(0, 50 * Time.deltaTime, 0);
            while (Vector3.Distance(target.position, character.transform.position) > agent.stoppingDistance)
            {
                if (target != null)
                    agent.SetDestination(RandomNavmeshLocation(Random.Range(1f, 5f))); // original max 20f

                if (agent.remainingDistance > agent.stoppingDistance)
                    character.Move(agent.desiredVelocity, false, false);
                else
                    character.Move(Vector3.forward, false, false);
                yield return new WaitForSeconds(Random.Range(0.5f, 5f)); // original max 10f
            }
        }

        IEnumerator RotatetoTargetDirection()
        {
            while (true)
            {
                if (targetdirection != null)
                {
                    aimeddirection = (targetdirection - transform.position).normalized;
                    if (aimeddirection == Vector3.zero)
                    { // unknown bug, "Look rotation viewing vector is zero"
                        aimeddirection = (targetdirection);
                    }
                    lookRotation = Quaternion.LookRotation(aimeddirection);
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 360);
                    //transform.LookAt(target.transform);
                    yield return new WaitForSeconds(0.1f);
                }
            }
        }

        public Vector3 RandomNavmeshLocation(float radius)
        {
            Vector3 randomDirection = Random.insideUnitSphere * radius;
            randomDirection += transform.position;
            NavMeshHit hit;
            Vector3 finalPosition = Vector3.zero;
            if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
            {
                finalPosition = hit.position;
            }
            targetdirection = finalPosition;
            return finalPosition;
        }

        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }
}
