using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{

    private const string forwardSpeedAnimatorName = "forwardSpeed";
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;

    private Camera _mainCamera;

    void Start()
    {
        _navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        _animator = gameObject.GetComponent<Animator>();
        _mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            MoveToCursor();
        }
        UpdateAnimate();
    }

    private void MoveToCursor()
    {
        var a = Input.mousePosition;
        var b = _mainCamera.ScreenPointToRay(a);
        RaycastHit hit;
        if (Physics.Raycast(b, out hit))
        {

            _navMeshAgent.destination = hit.point;
        }
    }

    private void UpdateAnimate()
    {
        var velocity = transform.InverseTransformDirection(_navMeshAgent.velocity);
        var forwardSpeed = velocity.z;

        _animator.SetFloat(forwardSpeedAnimatorName, forwardSpeed);

    }
}
