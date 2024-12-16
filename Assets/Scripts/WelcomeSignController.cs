using UnityEngine;

public class WelcomeSignController : MonoBehaviour
{
    public GameObject welcomeSign; // Drag the WelcomeSign GameObject here in the Inspector
    public float displayDuration = 3.0f; // Duration to show the sign in seconds

    void Start()
    {
        // Make sure the welcome sign is active at the start
        if (welcomeSign != null)
        {
            welcomeSign.SetActive(true);
            // Start a coroutine to hide the sign after a delay
            StartCoroutine(HideSignAfterDelay());
        }
    }

    System.Collections.IEnumerator HideSignAfterDelay()
    {
        // Wait for the specified duration
        yield return new WaitForSeconds(displayDuration);

        // Deactivate the welcome sign
        if (welcomeSign != null)
        {
            welcomeSign.SetActive(false);
        }
    }
}
