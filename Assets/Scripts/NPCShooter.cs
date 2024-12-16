using UnityEngine;

public class NPCShooter : MonoBehaviour
{
    public Transform player; // Assign the player GameObject here
    public GameObject bulletPrefab; // Assign a bullet prefab
    public Transform shootPoint; // Assign the point where bullets spawn
    public float shootingRange = 15f; // Range at which the NPC starts shooting
    public float shootCooldown = 2f; // Time between shots
    private float shootTimer = 0f; // Timer to track cooldown

    private Animator animator; 

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null)
        {
            Debug.LogWarning("Player reference is missing!");
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        Debug.Log("Distance to player: " + distanceToPlayer);

        if (distanceToPlayer <= shootingRange)
        {
            FacePlayer();
            HandleShooting();

            // Optional: Play shooting animation
            if (animator != null)
            {
                animator.SetBool("isShooting", true);
            }
        }
        else
        {
            if (animator != null)
            {
                animator.SetBool("isShooting", false);
            }
        }
    }

    void FacePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0; // Prevent tilting
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
    }

    void HandleShooting()
    {
        shootTimer += Time.deltaTime;

        if (shootTimer >= shootCooldown)
        {
            Shoot();
            shootTimer = 0f; // Reset the timer
        }
    }

    void Shoot()
    {
        if (shootPoint == null || bulletPrefab == null) return;

        // Spawn a bullet
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);

        // Apply force to the bullet
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = shootPoint.forward * 20f; // Use velocity for consistent movement
        }

        Debug.Log("Bullet fired!");
    }
}
