using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [Header("Target for action")]
    public Interactable target;

    [Header("State")]
    public bool active;
    public BoolValue storedValue;

    [Header("Sprite")]
    public Sprite activeSprite;
    private SpriteRenderer mySprite;


    // Start is called before the first frame update
    void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
        active = storedValue.RuntimeValue;
        
        if (active)
        {
            ActivateButton();
        }
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        // is it the player?
        if (other.CompareTag("Player"))
        {
            ActivateButton();
        }
    }


    public void ActivateButton()
    {
        // change state of button
        active = true;
        storedValue.RuntimeValue = active;
        mySprite.sprite = activeSprite;
        // activate action on target object
        target.Activate();
    }
}
