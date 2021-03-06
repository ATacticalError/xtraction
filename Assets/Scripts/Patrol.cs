using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    private MapManager manager;

    public List<GameObject> patrolPath = new List<GameObject>();
    public CapsuleCollider detectionCol;

    public int pointsInPath = 3;

    public bool canMove = true;
    public float moveSpeed = 4.0f;

    public Light spotLight;

    public bool canSee = true;
    public float detectionRadius = 3;
    public bool isAlerted = false;
    public float alertPhaseLength = 20.0f;
    public float alertDetectionRadius = 6;
    public bool alertPhaseEnded = false;

    public MeshCollider raycastPlane;
    public LayerMask layermask;
    Ray ray;
    RaycastHit hit;
    public float heightOverPlane;
    public GameObject detectionVisualiser;
    public Animator animator;
    public GameObject[] pieces;

    void Awake()
    {
        detectionCol = GetComponent<CapsuleCollider>();
        detectionCol.radius = detectionRadius;
    }

    void Start()
    {
        for (int p = 0; p < pointsInPath; p++)
        {
            patrolPath.Add(manager.GetRandomPatrolPoint());
        }
        raycastPlane = GameObject.FindGameObjectWithTag("RaycastPlane").GetComponent<MeshCollider>();
    }

    void Update()
    {
        if (canMove)
        {
            Pathing();
        }
        DetectionLight();
    }

    void DetectionLight()
    {
        ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layermask))
        {
            heightOverPlane = hit.distance;
            spotLight.range = heightOverPlane + 1;
            spotLight.innerSpotAngle = Mathf.Atan(detectionCol.radius / heightOverPlane) * Mathf.Rad2Deg;
        }
        detectionVisualiser.transform.localScale = new Vector3(1,1,1) * (detectionCol.radius*2);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isAlerted && other.gameObject.tag == "Player")
        {
            if(!canSee) {
                Debug.Log("Detected player, but whilst cannot see");
                return;
            }
            // Change to alert phase.
            isAlerted = true;
            animator.SetBool("Alert", true);
            Debug.Log("PlayerDetected");
            StartCoroutine(AlertPhaseRoutine(alertPhaseLength));
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (alertPhaseEnded && other.gameObject.tag == "Player")
        {
            alertPhaseEnded = false;
            // TODO : link to gameover event
            other.gameObject.GetComponentInParent<Player>().AddGameOver();
            Debug.Log("Game ended");
        }
    }

    public IEnumerator AlertPhaseRoutine(float seconds)
    {
        isAlerted = true;
        animator.SetBool("Alert", true);
        canMove = false;
        detectionCol.radius = alertDetectionRadius;
        manager.PlayerSpotted(isAlerted, alertPhaseLength);
        // TODO : Add Alert effects
        yield return new WaitForSeconds(seconds);
        alertPhaseEnded = true;
        detectionCol.radius = detectionRadius;
        isAlerted = false;
        animator.SetBool("Alert", false);
        canMove = true;
        manager.PlayerSpotted(isAlerted, alertPhaseLength);
    }

    void Pathing()
    {
        //move towards next point if we haven't 
        int patrolCount = patrolPath.Count;
        float step = moveSpeed * Time.deltaTime;
        GameObject targetPoint = patrolPath[0];

        Vector3 ylockedpoint = new Vector3(targetPoint.transform.position.x, transform.position.y, targetPoint.transform.position.z);
        Vector3 direction = (ylockedpoint - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        if (Vector3.Distance(transform.position, targetPoint.transform.position) > 0.0001f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 3);
            transform.position = Vector3.MoveTowards(transform.position, targetPoint.transform.position, step);
        }
        else
        {
            patrolPath.RemoveAt(0);
            patrolPath.Add(manager.GetRandomPatrolPoint());
        }

    }

    public void SetManager(MapManager m) { manager = m; }

    public IEnumerator InvisibleRoutine(float seconds)
    {
        print("InvisibleRoutine Started");
        canSee = false;
        yield return new WaitForSeconds(seconds);
        canSee = true;
        print("Invisible Routine Ended");
    }

    public void Blindspot(float percentage)
    {
        detectionCol.radius *= (1 - percentage);
    }

    public IEnumerator PocketSandRoutine(float minutes)
    {
        canMove = false;
        yield return new WaitForSeconds(minutes * 60);
        canMove = true;
    }

    public void Roadwork(GameObject patrolPoint)
    {
        if (patrolPath.Contains(patrolPoint))
        {
            patrolPath.Remove(patrolPoint);
            GameObject newPoint = manager.GetRandomPatrolPoint();
            while (patrolPath.Contains(newPoint))
            {
                newPoint = manager.GetRandomPatrolPoint();
            }
            patrolPath.Add(newPoint);
        }
    }

    public void Destroyed()
    {
        StartCoroutine(DestroyedRoutine());
    }

    private IEnumerator DestroyedRoutine()
    {
        // TODO: play patrol death animation, wait for length
        foreach (GameObject part in pieces)
        {
            Rigidbody rigidbody = part.GetComponent<Rigidbody>();
            rigidbody.isKinematic = false;
            rigidbody.useGravity = true;
            rigidbody.velocity = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
            rigidbody.angularVelocity = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1));
            ParticleSystem fireparticles = part.GetComponent<ParticleSystem>();
            var emission = fireparticles.emission;
            emission.rateOverTime = 100;
        }
        yield return new WaitForSeconds(3.0f);
        manager.RemovePatrol(this);
    }
}
