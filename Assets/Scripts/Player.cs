using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Camera cam;
    public GameObject raycastPlane;
    public LayerMask movementLayermask;
    public bool canMove = true;
    public float moveSpeed = 5.0f;
    public Vector3 targetLocation;
    Ray ray;
    RaycastHit hit;

    float clickTime;
    bool moving;

    public List<Clue> clues = new List<Clue>();
    public List<GameObject> keys = new List<GameObject>();

    public SelectionType selectionType;
    public LayerMask patrolLayermask;
    public LayerMask patrolPointLayermask;

    public GameManager manager;
    public AbilityManager abManager;
    public MapManager mapManager;
    public HotspotManager hotspotManager;
    public Inventory inventory;
    public GameObject startButton;

    public GameObject alertOverlay;
    public Text alertTimerText;
    public Animator alertAnimator;
    public float alertTimer = 30;
    public bool alertCountdown = false;

    void Awake()
    {
        targetLocation = transform.position;
        selectionType = SelectionType.Movement;
        manager = this.GetComponent<GameManager>();
        abManager = this.GetComponent<AbilityManager>();
        inventory = this.GetComponent<Inventory>();
        alertOverlay.SetActive(false);
    }

    public void InitialiseGame()
    {
        GameObject mapGO = GameObject.FindGameObjectWithTag("MapManager");

        mapManager = mapGO.GetComponent<MapManager>();
        mapManager.InitialiseMapManager(this);
        abManager.SetMapManager(mapManager);

        hotspotManager = mapGO.GetComponent<HotspotManager>();
        hotspotManager.AssignInventory(inventory);

        startButton.SetActive(false);
    }

    public void Spotted(bool detected, float alertPhaseLength)
    {
        if(detected) {
            alertOverlay.SetActive(true);
            alertAnimator.SetTrigger("On");
            alertTimer = alertPhaseLength;
            Debug.Log(alertPhaseLength);
            alertCountdown = true;
        } else {
            alertAnimator.SetTrigger("Off");
            Invoke("AlertOff", 1);
        }
    }

    public void AlertOff() {
        alertCountdown = false;
        alertOverlay.SetActive(false);
    }

    public void AddGameOver()
    {
        manager.AddGameOver();
    }

    void Update()
    {
#if UNITY_EDITOR
        if (selectionType == SelectionType.Movement)
            SelectMovement();
#endif

        if (selectionType == SelectionType.Assassin)
            SelectAssassin();

        if (selectionType == SelectionType.Roadwork)
            SelectRoadwork();
        if (canMove)
        {
            float step = moveSpeed * Time.deltaTime;
            if (Vector3.Distance(transform.position, targetLocation) > 0.0001f)
                transform.position = Vector3.MoveTowards(transform.position, targetLocation, step);
        }
        if(alertCountdown) {
            alertTimer -= Time.deltaTime;
            if(alertTimer <= 0) {
                alertTimer = 0;
            }
            alertTimerText.text = ((int)alertTimer).ToString();
        }
    }

    void SelectRoadwork()
    {
        print("selecting roadwork");
        if (Input.GetMouseButton(0))
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, patrolPointLayermask))
            {
                abManager.Roadwork(hit.transform.gameObject);
            }
        }
    }

    void SelectAssassin()
    {
        print("selecting patrol to assassinate");
        if (Input.GetMouseButton(0))
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, patrolLayermask))
            {
                abManager.Assassinate(hit.transform.gameObject.GetComponent<Patrol>());
            }
        }
    }

    void SelectMovement()
    {
        bool click = false;
        if (Input.GetMouseButtonDown(0))
        {
            clickTime = Time.time;
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (Time.time - clickTime < 0.15f)
                click = true;
        }

        if (click)
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, movementLayermask))
            {
                if (canMove)
                    targetLocation = hit.point;
            }
        }

    }
}

public enum SelectionType
{
    Movement,
    Assassin,
    Roadwork
}