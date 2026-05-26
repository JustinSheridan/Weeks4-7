using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class Trainer : MonoBehaviour
{
    public SpriteRenderer creatureRenderer; // Kept for reference, but we will use the list
    public Camera gameCamera;
    public Color caughtColour;

    public Hider creatureHider;

    public List<SpriteRenderer> uncaughtCreatures;
    public List<SpriteRenderer> caughtCreatures;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Ensure lists are initialized to avoid null reference errors
        if (uncaughtCreatures == null) uncaughtCreatures = new List<SpriteRenderer>();
        if (caughtCreatures == null) caughtCreatures = new List<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isClicked = Mouse.current.leftButton.wasPressedThisFrame;

        Vector3 mousePosition = Mouse.current.position.ReadValue();
        Vector3 worldMousePosition = gameCamera.ScreenToWorldPoint(mousePosition);
        worldMousePosition.z = 0f;

        if(isClicked)
        {
            // Output caught creatures to console
            for(int i = 0; i < caughtCreatures.Count; i++)
            {
                if (caughtCreatures[i] != null)
                    Debug.Log(caughtCreatures[i].gameObject.name + " is caught.");
            }

            // Check mouse position against all uncaught creatures
            // We iterate backwards because we will remove items from the list
            for (int i = uncaughtCreatures.Count - 1; i >= 0; i--)
            {
                SpriteRenderer currentCreature = uncaughtCreatures[i];

                // Check if the creature is still valid (hasn't been destroyed in scene)
                if (currentCreature == null)
                {
                    uncaughtCreatures.RemoveAt(i);
                    continue;
                }

                if (currentCreature.bounds.Contains(worldMousePosition))
                {
                    // 1. Change color
                    currentCreature.color = caughtColour;

                    // 2. Use the Hider script
                    if (creatureHider != null)
                    {
                        creatureHider.Hide();
                    }
                    else
                    {
                        Debug.LogWarning("Creature Hider reference is missing!");
                    }

                    // 3. Update lists
                    caughtCreatures.Add(currentCreature);
                    uncaughtCreatures.RemoveAt(i); // Remove from uncaught since we just processed it

                    Debug.Log("Caught the creature!");
                    
                    // Break to only catch one at a time (remove break to catch multiple at once)
                    break; 
                }
            }
        }
    }
}