
using Cinemachine;
using UnityEngine;
using UnityEngine.AI;

public class Player_Movement : MonoBehaviour
{
    //Public variables
    public NavMeshAgent agent;
    public GameObject playerdataServer;
    public GameObject interactable;
    public GameObject tempInteractable;
    public Camera cam;

    //Private variables.
    private float playerSpeed = 5.0f;
    private float interactRange = 100.0f;
    private float interactRadius = 10.0f;
    private float m_camSpeed_XAxis = 4.0f;
    private float m_camSpeed_YAxis = 450.0f;

    //Protected Variables and Refereneses
    protected RaycastHit hitinfo;
    protected Ray rayinfo;
    protected Vector2 turn;
    protected int isZoom = -1;

    // Start is called before the first frame update
    void Start()
    {
        //Set the Navigation Mesh Agent to Player Speed;
        agent.speed = playerSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
        //Player Interactable and Movement by left-click
        if (Input.GetMouseButtonDown(0))
        {
            rayinfo = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(rayinfo, out hitinfo, interactRange))
            {
                tempInteractable = hitinfo.transform.gameObject;
                Transform m_tempPosition = tempInteractable.GetComponent<Transform>();  
                float p_x = this.transform.position.x - m_tempPosition.position.x;
                float p_y = this.transform.position.y - m_tempPosition.position.y;
                float p_z = this.transform.position.z - m_tempPosition.position.z;
                float m_tempRadius = Mathf.Sqrt(Mathf.Pow(p_x,2) + Mathf.Pow(p_y, 2) + Mathf.Pow(p_z, 2));
                if (m_tempRadius <= interactRadius)
                {
                    if (tempInteractable.GetComponent<Interactable>() != null)
                    {
                        interactable = tempInteractable;
                    }
                }
                else
                {
                    if(Physics.Raycast(rayinfo, out hitinfo))
                    {
                        agent.SetDestination(hitinfo.point);
                    }
                }
            }
        }
    }
    void OnGUI()
    {
        //Rotation the Camera Function with change is speed
        Event cur = Event.current;
        if (cur.button == 1 && isZoom == -1)
        {
            cam.GetComponent<CinemachineFreeLook>().m_XAxis.m_MaxSpeed = m_camSpeed_XAxis;
            cam.GetComponent<CinemachineFreeLook>().m_YAxis.m_MaxSpeed = m_camSpeed_YAxis;
        }
        else
        {
            cam.GetComponent<CinemachineFreeLook>().m_XAxis.m_MaxSpeed = 0;
            cam.GetComponent<CinemachineFreeLook>().m_YAxis.m_MaxSpeed = 0;
        }
        if (cur.button == 1 && isZoom == 1)
        {
            turn.x += Input.GetAxis("Mouse X");
            turn.y += Input.GetAxis("Mouse Y");

        }
    }
}