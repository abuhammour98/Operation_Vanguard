using UnityEngine;

public class UnityFlockController : MonoBehaviour
{
    public Transform targetObject;  // The object the birds will fly above
    public float speed = 10.0f; // Movement speed
    public float rotationSpeed = 5.0f; // Smooth rotation speed
    public float heightAboveTarget = 5.0f; // Height the birds will maintain above the target

    // Update is called once per frame
    void Update()
    {
        if (targetObject != null)
        {
            // Calculate the target position above the target object
            Vector3 targetPosition = targetObject.position;
            targetPosition.y += heightAboveTarget; // Adjust height above target

            // Calculate the direction towards the target position
            Vector3 direction = targetPosition - transform.position;

            // Normalize the direction to ensure consistent movement speed
            direction.Normalize();

            // Rotate smoothly to face the target position
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(direction),
                rotationSpeed * Time.deltaTime
            );

            // Move towards the target position
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}
