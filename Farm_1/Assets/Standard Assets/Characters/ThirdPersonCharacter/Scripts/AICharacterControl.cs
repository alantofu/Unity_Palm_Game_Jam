using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class AICharacterControl : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; } // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        public Transform target;// target to aim for
        public Rigidbody rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.detectCollisions = false;
            rb.constraints = RigidbodyConstraints.FreezePositionY;
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();
            agent.updateRotation = false;
	        agent.updatePosition = true;
            StartCoroutine(GoingToTarget());
        }

        IEnumerator GoingToTarget()
        {
            while (Vector3.Distance(target.position, character.transform.position) > agent.stoppingDistance)
            {
                if (target != null)
                    agent.SetDestination(RandomNavmeshLocation(Random.Range(4f,10f)));

                if (agent.remainingDistance > agent.stoppingDistance)
                    character.Move(agent.desiredVelocity, false, false);
                else
                    character.Move(Vector3.forward, false, false);
                yield return new WaitForSeconds(100f);
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
            return finalPosition;
        }

        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }
}
