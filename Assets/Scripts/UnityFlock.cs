using UnityEngine;

public class UnityFlock : MonoBehaviour
{
    public Transform leader;            // The object to follow (usually set to the UnityFlockController object)
    public float followSpeed = 8.0f;    // Speed at which flock members follow the leader
    public float rotationSpeed = 5.0f;  // Speed of rotation alignment with the leader
    public float followDistance = 5.0f; // Minimum distance to maintain from the leader

    public float separationDistance = 2.0f;  // Minimum distance to maintain from other flock members
    public float separationForce = 5.0f;     // Force used to push away from nearby objects

    private UnityFlock[] allFlockMembers;    // Array to store references to all flock members

    void Start()
    {
        // Find all UnityFlock components in the scene (all flock members)
        allFlockMembers = FindObjectsOfType<UnityFlock>();
    }

    void Update()
    {
        if (leader != null)
        {
            // Follow the leader
            Vector3 directionToLeader = leader.position - transform.position;

            // Calculate separation force to avoid merging with other flock members
            Vector3 separationVector = Vector3.zero;
            foreach (UnityFlock flockMember in allFlockMembers)
            {
                if (flockMember != this) // Don't calculate separation with itself
                {
                    Vector3 toOther = transform.position - flockMember.transform.position;
                    float distance = toOther.magnitude;

                    if (distance < separationDistance)
                    {
                        // Apply a repelling force based on how close the objects are
                        separationVector += toOther.normalized / distance;
                    }
                }
            }

            // Combine the follow and separation behaviors
            Vector3 finalDirection = directionToLeader.normalized + (separationVector * separationForce);

            // Smooth rotation to face the final direction
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(finalDirection),
                rotationSpeed * Time.deltaTime
            );

            // Move toward the final direction
            transform.position += finalDirection.normalized * followSpeed * Time.deltaTime;
        }
    }
}
