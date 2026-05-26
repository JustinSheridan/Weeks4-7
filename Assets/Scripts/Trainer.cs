using UnityEngine;
using UnityEngine.InputSystem;

public class Trainer : MonoBehaviour
{
    public SpriteRenderer creatureRenderer;
    public Camera gameCamera;
    public Color caughtColour;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool isClicked = Mouse.current.leftButton.wasPressedThisFrame;

        Vector3 mousePosition = Mouse.current.position.ReadValue();
        Vector3 worldMousePosition = gameCamera.ScreenToWorldPoint(mousePosition);
        worldMousePosition.z = 0f;


        if(isClicked && creatureRenderer.bounds.Contains(worldMousePosition))
        {
            Debug.Log("Caught the creature!");
            creatureRenderer.color = caughtColour;
        }
    }
}