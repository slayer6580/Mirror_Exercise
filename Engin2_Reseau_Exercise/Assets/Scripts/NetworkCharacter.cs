using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Mirror;

public class NetworkCharacter : NetworkBehaviour
{
    private const float MIN_MOUVEMENT_SPEED = 0.3f;
    [SerializeField]
    private float m_forwardMaxSpeed = 8.0f;
    [SerializeField]
    private NavMeshAgent m_navMeshAgent;
    [SerializeField]
    private Animator m_animator;

    [SerializeField]
    private Camera m_camera;

    private void Start()
    {
        if (isLocalPlayer) 
        {
            m_camera.gameObject.SetActive(false);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer) 
        {
            return;
        }
        float currentSpeed = m_navMeshAgent.velocity.magnitude;
        m_animator.SetBool("Moving", currentSpeed > MIN_MOUVEMENT_SPEED);

        if (Input.GetKeyDown(KeyCode.Mouse1)) 
        {
            int layerMask = LayerMask.NameToLayer("NavMesh");

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray ,out hit, Mathf.Infinity, ~layerMask)) 
            {
                Debug.LogWarning("Hit");
                m_navMeshAgent.SetDestination(hit.point);
            }
        }
        
    }
}
