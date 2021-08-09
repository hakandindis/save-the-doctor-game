using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using System;

namespace Worq
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(AWSEntityIdentifier))]
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Animation))]
    public class AWSPatrol : MonoBehaviour
    {
        //group settings
        [Header("Group")] 
        [Space(10)]
        public WaypointRoute group;
        [HideInInspector] public int groupID = 0;

        //patrol settings
        [Header("Patrol")] [Space(10)] [Tooltip("Minimum amount of time to wait before moving to next patrol point")]
        public float minPatrolWaitTime = 1f;

        [Tooltip("Maximum amount of time to wait before moving to next patrol point")]
        public float maxPatrolWaitTime = 3f;

        [Tooltip("If or not all entities patrol waypoints at random or in sequence")]
        public bool randomPatroler = false;

//		[Tooltip("When you drag in any active gameObject into this slot the patrol entity will abandon current patrol" +
//		         "and go to this position. Can also be set from script by calling the static method " +
//		         "AWSPatrol.GoTo(position); The patrol entity will stop upon arriving this position. " +
//		         "Please tick the resetPatrol checkbox in the inspector, or call ")] 
//		public Transform goTo;

        //NavMesh Agent settings
        [Space(10)] [Header("Agent")] [Space(10)] [Tooltip("Speed by which the NavMesh agent moves")]
        public float moveSpeed = 3f;

        [Tooltip("The distance from destination the Navmesh agent stops")]
        public float stoppingDistance = 1f;

        [Tooltip("Turning speed of the NavMesh  agent")]
        public float angularSpeed = 500f;

        [Tooltip("Defines how high up the entity is. This is useful for creating flying entities")]
        public float distanceFromGround = 0.5f;

        //Animations
        [Space(10)]
        [Header("Animations")]
        [Space(10)]
        [Tooltip(
            "Animations to play when the agent is in idle position (Selects one at random if multiple are inserted)")]
        public AnimationClip[] idleAnimations;

        [Tooltip(
            "Animations to play when the agent is walking/moving position (Selects one at random if multiple are inserted)")]
        public AnimationClip[] walkAnimations;

        //Debug
        [Space(10)] [Header("Debug")] [Space(10)]
        public bool resetPatrol;

        public bool interruptPatrol;
        public static bool reset;

        //private variables
        private AWSManager mAWSManager;
        private NavMeshAgent agent;
        private Animation anim;
        private AudioSource src;
        private Transform[] patrolPoints;
        private bool isWaiting;
        private bool hasPlayedDetectSound;
        private bool hasReachedGoTo;
        private int waypointCount;
        private int destPoint;

        void Awake()
        {
            mAWSManager = GameObject.FindObjectOfType<AWSManager>();

            agent = GetComponent<NavMeshAgent>();
            anim = GetComponent<Animation>();
            src = GetComponent<AudioSource>();

            try
            {
                waypointCount = 0;
                Transform groupTransform = group.transform;
                int childrenCount = groupTransform.childCount;

                for (int i = 0; i < childrenCount; i++)
                {
                    if (groupTransform.GetChild(i).GetComponent<WaypointIdentifier>())
                    {
                        waypointCount += 1;
                    }
                }

                patrolPoints = new Transform[waypointCount];
                int curIndex = 0;
                for (int i = 0; i < childrenCount; i++)
                {
                    if (groupTransform.GetChild(i).GetComponent<WaypointIdentifier>())
                    {
                        patrolPoints[curIndex] = groupTransform.GetChild(i);
                        if (patrolPoints[curIndex].gameObject.GetComponent<MeshRenderer>())
                            patrolPoints[curIndex].gameObject.GetComponent<MeshRenderer>().enabled = false;
                        if (patrolPoints[curIndex].gameObject.GetComponent<Collider>())
                            patrolPoints[curIndex].gameObject.GetComponent<Collider>().enabled = false;
                        curIndex++;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning("Group not assigned for " + gameObject.name);
            }
        }

        void Start()
        {
            anim = GetComponent<Animation>();
            if (anim == null)
                anim = gameObject.AddComponent<Animation>();

            agent.autoBraking = false;
            agent.stoppingDistance = stoppingDistance;
            agent.speed = moveSpeed;
            agent.angularSpeed = angularSpeed;
            agent.baseOffset = distanceFromGround;

            string newName;
            if (idleAnimations != null && idleAnimations.Length > 0)
            {
                for (int i = 0; i < idleAnimations.Length; i += 1)
                {
                    newName = idleAnimations[i].name;
                    idleAnimations[i].legacy = true;
                    anim.AddClip(idleAnimations[i], newName);
                }
            }

            if (walkAnimations != null && walkAnimations.Length > 0)
            {
                for (var i = 0; i < walkAnimations.Length; i += 1)
                {
                    newName = walkAnimations[i].name;
                    walkAnimations[i].legacy = true;
                    anim.AddClip(walkAnimations[i], newName);
                }
            }

            try
            {
                GotoNextPoint();
            }
            catch (NullReferenceException e)
            {
            }
        }

        void Update()
        {
            if (resetPatrol || reset)
            {
                agent.isStopped = false;
                goToNextPointDirect();
                interruptPatrol = false;
                resetPatrol = false;
                reset = false;
            }

            if (interruptPatrol)
            {
                agent.isStopped = true;
                if (null != idleAnimations)
                    playAnimation(idleAnimations);
            }

            if (!interruptPatrol && !isWaiting && agent.remainingDistance <= stoppingDistance && null != group)
            {
                GotoNextPoint();
            }
//For future a release
//			if (goTo != null && !hasReachedGoTo)
//			{
//				interruptPatrol = true;
//				agent.SetDestination(goTo.position);
//				if (walkAnimations != null)
//					playAnimation (walkAnimations);
//
//				if (agent.remainingDistance <= stoppingDistance)
//				{
//					playAnimation (idleAnimations);
//					hasReachedGoTo = true;
//				}
//			}

            //updating variables

            agent.stoppingDistance = stoppingDistance;
            agent.speed = this.moveSpeed;
            agent.angularSpeed = angularSpeed;
            agent.baseOffset = distanceFromGround;
        }

        private void GotoNextPoint()
        {
            if (patrolPoints.Length == 0)
                return;
//			Debug.Log ("Going to next point...");
            StartCoroutine(pauseAndContinuePatrol());
        }

        IEnumerator pauseAndContinuePatrol()
        {
            isWaiting = true;
            if (idleAnimations != null)
                playAnimation(idleAnimations);

            float waitTime = UnityEngine.Random.Range(minPatrolWaitTime, maxPatrolWaitTime);
            if (waitTime < 0f)
                waitTime = 1f;

            yield return new WaitForSeconds(waitTime);

            if (randomPatroler)
            {
                agent.destination = patrolPoints[destPoint].position;
                int nextPos;
                do
                {
                    nextPos = UnityEngine.Random.Range(0, patrolPoints.Length);
                } while (nextPos == destPoint);

                destPoint = nextPos;
            }
            else
            {
                agent.destination = patrolPoints[destPoint].position;
                destPoint = (destPoint + 1) % patrolPoints.Length;
            }

            if (walkAnimations != null)
                playAnimation(walkAnimations);
            isWaiting = false;
        }

        void goToNextPointDirect()
        {
            if (randomPatroler)
            {
                agent.destination = patrolPoints[destPoint].position;
                int nextPos;
                do
                {
                    nextPos = UnityEngine.Random.Range(0, patrolPoints.Length);
                } while (nextPos == destPoint);

                destPoint = nextPos;
            }
            else
            {
                agent.destination = patrolPoints[destPoint].position;
                destPoint = (destPoint + 1) % patrolPoints.Length;
            }

            if (walkAnimations != null)
                playAnimation(walkAnimations);
        }

        void RestartPatrol()
        {
            hasPlayedDetectSound = false;
            resetPatrol = false;
            agent.speed = moveSpeed;

            agent.stoppingDistance = 1f;
            if (walkAnimations != null)
                playAnimation(walkAnimations);
            goToNextPointDirect();
        }

        void playAnimation(AnimationClip clip)
        {
            anim.Play(clip.name);
        }

        void playAnimation(AnimationClip[] clips)
        {
            if (clips.Length > 0 && null != clips)
            {
                int rand = UnityEngine.Random.Range(0, clips.Length);
                if (clips[rand] != null)
                    anim.Play(clips[rand].name);
//				Debug.Log ("Now Playing: " + clips [rand].name);
//			} else {
//				Debug.LogWarning("Some enemy animations are missing ");
            }
        }

        public void ResetPatrol()
        {
            resetPatrol = true;
        }

        public void InterruptPatrol()
        {
            interruptPatrol = true;
        }

        public void SetDeatination(Transform t)
        {
            agent.destination = t.position;
            if (walkAnimations != null)
                playAnimation(walkAnimations);
            isWaiting = false;
        }
    }
}