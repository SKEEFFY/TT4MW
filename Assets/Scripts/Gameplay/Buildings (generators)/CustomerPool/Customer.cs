using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    [SerializeField] private float _distanceToTarget = 0.5f;

    private float _stayTime;
    private NavMeshAgent _agent;
    private Transform[] _wayPoints;
    private int _currentWaypointIndex;
    private bool _isMoving = false; 


    public NavMeshAgent Agent { get { return _agent; } }
    public bool IsMoving { set { _isMoving = value; } }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (_isMoving && _agent.remainingDistance < _distanceToTarget && !_agent.pathPending)
        {
            MoveToNextWaypoint();
        }
    }

    private void MoveToNextWaypoint()
    {
        if (_currentWaypointIndex == _wayPoints.Length - 1)
        {
            _isMoving = false;
            CustomerPool.Instance.ReturnCustomer(this); 
            return;
        }

        _currentWaypointIndex++;

        if (_currentWaypointIndex < _wayPoints.Length)
        {
            _agent.SetDestination(_wayPoints[_currentWaypointIndex].position);
            _isMoving = true;
            
            if (_currentWaypointIndex == 1)
            {
                StartCoroutine(StayAtWaypoint());
            }
        }
    }
    
    public void SetWaypoints(Transform[] waypoints, float stayTime)
    {
        _wayPoints = waypoints;
        _stayTime = stayTime;
        _currentWaypointIndex = -1; 
        _isMoving = true;
        MoveToNextWaypoint(); 
    }

    public void ResetCustomer(Vector3 spawnPosition)
    {
        _agent.ResetPath();
        transform.position = spawnPosition; 
        _currentWaypointIndex = -1; 
        _isMoving = false; 
    }
    
    private IEnumerator StayAtWaypoint()
    {
        _isMoving = false; 
        yield return new WaitForSeconds(_stayTime);
        _isMoving = true;
        MoveToNextWaypoint();
    }
}