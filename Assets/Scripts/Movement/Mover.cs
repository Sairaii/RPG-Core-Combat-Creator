using RPG.Core;
using UnityEngine;
using UnityEngine.AI;
using RPG.Saving;
using RPG.Attributes;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField] float maxSpeed = 6f;
        [SerializeField] float maxNavPathLength = 40f;

        NavMeshAgent playerNavMeshAgent;
        Animator playerAnimator;
        Health health;

        void Awake()
        {
            playerNavMeshAgent = GetComponent<NavMeshAgent>();
            playerAnimator = GetComponentInChildren<Animator>();
            health = GetComponent<Health>();
        }

        void Update()
        {
            playerNavMeshAgent.enabled = !health.IsDead();

            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedFraction);
        }

        public bool CanMoveTo(Vector3 destination)
        {
            NavMeshPath path = new NavMeshPath();
            bool hasPath = NavMesh.CalculatePath(transform.position, destination, NavMesh.AllAreas, path);
            if (!hasPath) return false;
            if (path.status != NavMeshPathStatus.PathComplete) return false;
            if (GetPathLength(path) > maxNavPathLength) return false;

            return true;
        }

        public void MoveTo(Vector3 destination, float speedFraction) //hit.point is vector 3
        {
            playerNavMeshAgent.destination = destination; //take to the collision destination
            playerNavMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            playerNavMeshAgent.isStopped = false;
        }

        public void Cancel()
        {
            playerNavMeshAgent.isStopped = true;
        }

        void UpdateAnimator()
        {
            playerAnimator.SetFloat("forwardSpeed", Mathf.Abs(this.transform.InverseTransformDirection(playerNavMeshAgent.velocity).z)); // see [tip] performance and animation blending question
            // Vector3 velocity = GetComponent<NavMeshAgent>().velocity; //get the velocity from navmesh agent
            // Vector3 localVelocity = transform.InverseTransformDirection(velocity); //Inversetransformdirection is to convert velocity to local velocity
            // float speed = localVelocity.z;
            // GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }

        private float GetPathLength(NavMeshPath path)
        {
            float total = 0;
            if (path.corners.Length < 2) return total;
            for (int i = 0; i < path.corners.Length - 1; i++)
            {
                total += Vector3.Distance(path.corners[i], path.corners[i + 1]);
            }

            return total;
        }

        public object CaptureState()
        {
            return new SerializableVector3(transform.position);
        }

        public void RestoreState(object state)
        {
            SerializableVector3 position = (SerializableVector3)state;
            playerNavMeshAgent.enabled = false;
            transform.position = position.ToVector();
            playerNavMeshAgent.enabled = true;
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
    }
}