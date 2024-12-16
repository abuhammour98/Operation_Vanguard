using UnityEngine;
using System;
using System.Linq;

public class FSM : MonoBehaviour
{
    [Serializable]
    public enum FSMState
    {
        Chase,
        Flee,
        SelfDestruct,
    }

    [Serializable]
    public struct FSMProbability
    {
        public FSMState state;
        public int weight;
    }

    public FSMProbability[] states; // Array for states and their weights
    private FSMState currentState;  // Current state being performed

    public float speed = 5f;        // Movement speed
    private Transform player;       // Reference to the player

    void Start()
    {
        // Find the player in the scene
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Set an initial state
        currentState = SelectState();
        Debug.Log($"Initial State: {currentState}");
    }

    void Update()
    {
        // Update the FSM state if spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentState = SelectState();
            Debug.Log($"Selected State: {currentState}");
        }

        // Perform the behavior based on the current state
        PerformAction(currentState);
    }

    // Randomly select a state based on weights
    FSMState SelectState()
    {
        int weightSum = states.Sum(state => state.weight);

        if (weightSum <= 0)
        {
            Debug.LogWarning("Total weight is zero or negative. Cannot select a state.");
            throw new InvalidOperationException("Invalid state weights configuration.");
        }

        int randomNumber = UnityEngine.Random.Range(0, weightSum);

        foreach (var state in states)
        {
            randomNumber -= state.weight;
            if (randomNumber < 0)
            {
                return state.state;
            }
        }

        throw new Exception("Something is wrong in the SelectState algorithm!");
    }

    // Perform the selected behavior
    void PerformAction(FSMState state)
    {
        switch (state)
        {
            case FSMState.Chase:
                ChasePlayer();
                break;

            case FSMState.Flee:
                FleeFromPlayer();
                break;

            case FSMState.SelfDestruct:
                SelfDestruct();
                break;
        }
    }

    // Move toward the player
    void ChasePlayer()
    {
        if (player == null) return;

        Debug.Log("Chasing the Player...");
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    // Move away from the player
    void FleeFromPlayer()
    {
        if (player == null) return;

        Debug.Log("Fleeing from the Player...");
        Vector3 direction = (transform.position - player.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    // Destroy the enemy (self-destruct)
    void SelfDestruct()
    {
        Debug.Log("Self-Destruct Activated!");
        Destroy(gameObject);
    }
}
