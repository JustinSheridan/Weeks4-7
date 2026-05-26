using UnityEngine;

[RequireComponent(typeof(Transform))]
public class ClockhandTurner : MonoBehaviour
{
    public float degreesPerSecond = 1f;

    private void Update()
    {
        // Rotate the clock hand based on the set speed
        transform.Rotate(Vector3.forward * degreesPerSecond * Time.deltaTime);
    }
}