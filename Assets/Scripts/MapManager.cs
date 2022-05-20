using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Map;

public class MapManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> patrolPoints = new List<GameObject>();
    [SerializeField]
    private List<GameObject> disabledPatrolPoints = new List<GameObject>();

    public int patrolAmount = 3;

    public GameObject patrolPrefab;

    [SerializeField]
    private List<Patrol> patrols = new List<Patrol>();
    [SerializeField]
    private List<Patrol> disabledPatrols = new List<Patrol>();

    public HotspotManager hsManager;

    public Player player;

    public void InitialiseMapManager(Player pl)
    {
        player = pl;
        patrolPoints.AddRange(GameObject.FindGameObjectsWithTag("PatrolPoint"));
        Debug.Log("patrol points found");
        print(patrolAmount);
        for (int i = patrolAmount; i > 0; i--)
        {
            Patrol p;
            p = Instantiate(patrolPrefab, Vector3.zero, Quaternion.identity).GetComponent<Patrol>();
            print(p);
            p.transform.position = patrolPoints[Random.Range(0, patrolPoints.Count)].transform.position;
            p.SetManager(this);
            patrols.Add(p);
        }
    }

    void Start()
    {
#if UNITY_EDITOR
        // InitialiseMapManager();
#endif
    }

    public void ApplyInvisibility(float seconds)
    {
        foreach (Patrol p in patrols)
        {
            if (p)
                StartCoroutine(p.InvisibleRoutine(seconds));
        }
    }

    public void ApplyBlindspot(float percentage)
    {
        foreach (Patrol p in patrols)
        {
            if (p)
                p.Blindspot(percentage);
        }
    }

    public void ApplyRoadwork(float minutes, GameObject patrolPoint)
    {
        StartCoroutine(RoadworkRoutine(minutes, patrolPoint));
    }

    public void ApplyPocketSand(float minutes)
    {
        foreach (Patrol p in patrols) { if (p) StartCoroutine(p.PocketSandRoutine(minutes)); }
    }

    public void RemovePatrol(Patrol patrol)
    {
        disabledPatrols.Add(patrol);
        patrols.Remove(patrol);
        patrol.gameObject.SetActive(false);
    }

    public void PlayerSpotted(bool detected, float alertPhaseLength)
    {
        player.Spotted(detected, alertPhaseLength);
    }

    private IEnumerator RoadworkRoutine(float minutes, GameObject patrolPoint)
    {
        disabledPatrolPoints.Add(patrolPoint);
        if (patrolPoints.Contains(patrolPoint)) { patrolPoints.Remove(patrolPoint); }

        foreach (Patrol p in patrols) { if (p) p.Roadwork(patrolPoint); }

        patrolPoint.SetActive(false);
        // TODO : play some kind of particle effect
        yield return new WaitForSeconds(minutes * 60);
        patrolPoint.SetActive(true);

        patrolPoints.Add(patrolPoint);
        if (disabledPatrolPoints.Contains(patrolPoint)) { disabledPatrolPoints.Remove(patrolPoint); }
    }

    public GameObject GetRandomPatrolPoint() { return patrolPoints[Random.Range(0, patrolPoints.Count)]; }

}
