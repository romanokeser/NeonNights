using UnityEngine;

public class CarFloatingBehavior : MonoBehaviour
{
    [SerializeField] private float _currentDistanceFromGround;  //distance from the track
    [SerializeField] private float _desiredDistanceFromGround;
    [SerializeField] private float _forceMultiplier;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, -Vector2.up, out hit))
        {
            if (hit.transform.tag == "Track")
            {
                // Get the distance between the car and the ground hit by the raycast
                _currentDistanceFromGround = hit.distance;

                // Calculate the difference between the desired distance and the current distance
                float distanceDiff = _desiredDistanceFromGround - _currentDistanceFromGround;
                _rigidbody.AddForce(distanceDiff * _forceMultiplier * Vector2.up);
            }
            else
            {
                Debug.Log("Car isn't above the track");
            }
        }
    }
}
