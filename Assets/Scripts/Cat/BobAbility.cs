using UnityEngine;

public class BobAbility : MonoBehaviour
{
    // Variables to control the motion
    [SerializeField]
    public float amplitude; // Height of the bobbing
    [SerializeField]
    public float frequency;   // Speed of the bobbing

    // Offset to ensure the motion is relative
    private float initialYOffset;

    void Start()
    {
        // Calculate the initial vertical offset relative to the GameObject's position
        initialYOffset = transform.localPosition.y;
    }

    void Update()
    {
        // Calculate the new local Y position using a sine wave
        float bobbingOffset = Mathf.Sin(Time.time * frequency) * amplitude;
        float newY = initialYOffset + bobbingOffset;

        // Update the local position without affecting X or Z
        transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.localPosition.z);
    }
}