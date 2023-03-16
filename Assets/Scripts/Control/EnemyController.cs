using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Memoriae.Control
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private float chaseRange = 5f;
        [SerializeField] float turnSpeed = 5f;
        [SerializeField] GameObject enemyCanvas;
        [SerializeField] AudioSource enemyScream;

        private Animator anim;
        private NavMeshAgent navMeshAgent;
        private AudioSource enemySound;
        private float distanceToTarget = Mathf.Infinity;

        private void Start()
        {
            anim = GetComponentInChildren<Animator>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            enemySound = GetComponentInChildren<AudioSource>();
        }

        private void FixedUpdate()
        {
            distanceToTarget = Vector3.Distance(target.transform.position, transform.position);

            if (distanceToTarget <= navMeshAgent.stoppingDistance)
            {
                AttackTarget();
            }
        }

        public void EngageTarget()
        {
            navMeshAgent.isStopped = false;
            FaceTarget();

            if (!enemySound.isPlaying)
            {
                enemySound.Play();
            }

            if (distanceToTarget > navMeshAgent.stoppingDistance)
            {
                anim.SetFloat("Blend", 1f);
                ChaseTarget();
            }
        }

        public void IgnoreTarget()
        {
            if (enemySound.isPlaying)
            {
                enemySound.Pause();
            }
            anim.SetFloat("Blend", 0);
            navMeshAgent.isStopped = true;
        }

        private void ChaseTarget()
        {
            navMeshAgent.SetDestination(target.transform.position);
        }

        private void AttackTarget()
        {
            StartCoroutine(ProcessAttack());
        }

        private IEnumerator ProcessAttack()
        {
            yield return target.transform.position = spawnPoint.position;
            enemyCanvas.SetActive(true);
            enemyScream.Play();
            yield return new WaitForSeconds(1);
            enemyScream.Stop();
            enemyCanvas.SetActive(false);
        }

        private void FaceTarget()
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, chaseRange);
        }
    }
}

