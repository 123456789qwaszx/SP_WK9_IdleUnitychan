using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MonsterController : MonoBehaviour
{
    public Animator animator { get { return m_Animator; } }
    public NavMeshAgent navmeshAgent { get { return m_NavMeshAgent; } }
    public bool grounded { get { return m_Grounded; } }

    protected Animator m_Animator;
    protected NavMeshAgent m_NavMeshAgent;

    
    protected bool m_ExternalForceAddGravity = true;
    protected Vector3 m_ExternalForce;

    protected bool m_Grounded;
    protected bool m_UnderExternalForce;

    protected Rigidbody m_Rigidbody;

    const float k_GroundedRayDistance = .8f;

    void OnEnable()
    {
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        m_Animator = GetComponent<Animator>();

        m_NavMeshAgent.updatePosition = false;

        m_Rigidbody = GetComponentInChildren<Rigidbody>();
        if (m_Rigidbody == null)
            m_Rigidbody = gameObject.AddComponent<Rigidbody>();

        m_Rigidbody.isKinematic = true;
        m_Rigidbody.useGravity = false;

    }

    private void FixedUpdate()
    {
        //animator.speed = PlayerInput.Instance != null && PlayerInput.Instance.HaveControl() ? 1.0f : 0.0f;
        animator.speed = 1.5f;

        CheckGrounded();

        if (m_UnderExternalForce)
            ForceMovement();
    }

    void CheckGrounded()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position + Vector3.up * k_GroundedRayDistance * 0.5f, -Vector3.up);
        m_Grounded = Physics.Raycast(ray, out hit, k_GroundedRayDistance, Physics.AllLayers,
            QueryTriggerInteraction.Ignore);
    }

    void ForceMovement()
    {
        if (m_ExternalForceAddGravity)
            m_ExternalForce += Physics.gravity * Time.deltaTime;

        RaycastHit hit;
        Vector3 movement = m_ExternalForce * Time.deltaTime;
        if (!m_Rigidbody.SweepTest(movement.normalized, out hit, movement.sqrMagnitude))
        {
            m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
        }
    }

    // used to disable position being set by the navmesh agent, for case where we want the animation to move the enemy instead (e.g. Chomper attack)
    public void SetFollowNavmeshAgent(bool follow)
    {
        if (!follow && m_NavMeshAgent.enabled)
        {
            m_NavMeshAgent.ResetPath();
        }
        else if (follow && !m_NavMeshAgent.enabled)
        {
            m_NavMeshAgent.Warp(transform.position);
        }

        m_NavMeshAgent.enabled = follow;
    }

    public void AddForce(Vector3 force, bool useGravity = true)
    {
        if (m_NavMeshAgent.enabled)
            m_NavMeshAgent.ResetPath();

        m_ExternalForce = force;
        m_NavMeshAgent.enabled = false;
        m_UnderExternalForce = true;
        m_ExternalForceAddGravity = useGravity;
    }

    public void ClearForce()
    {
        m_UnderExternalForce = false;
        m_NavMeshAgent.enabled = true;
    }

    public bool SetTarget(Vector3 position)
    {
        return m_NavMeshAgent.SetDestination(position);
    }
}
