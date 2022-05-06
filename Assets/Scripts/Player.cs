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

    public SelectionType selectionType;
    public LayerMask patrolLayermask;
    public LayerMask patrolPointLayermask;

    public AbilityManager abManager;
    public MapManager mapManager;

    public Text mapManagerStatus;

    public void FindMapManager() { 
        mapManager = GameObject.FindGameObjectWithTag("MapManager").GetComponent<MapManager>();
        if(mapManager)
            mapManagerStatus.text = "Found";
        mapManager.InitialiseMapManager();
     }

    void Awake()
    {
        targetLocation = transform.position;
        selectionType = SelectionType.Movement;
        abManager = this.GetComponent<AbilityManager>();
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
        if(canMove){
        float step = moveSpeed * Time.deltaTime;
            if (Vector3.Distance(transform.position, targetLocation) > 0.0001f)
                transform.position = Vector3.MoveTowards(transform.position, targetLocation, step);
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