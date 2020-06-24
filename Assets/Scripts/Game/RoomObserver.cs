using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomObserver : MonoBehaviour
{
    public Enemy[] enemies;
    public Pot[] breakables;
    public GameObject virtualCamera;


    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            // activate all enemies and breakables
            for (int i = 0; i < enemies.Length; i++)
            {
                ChangeActivation(enemies[i], true);
            }
            for (int i = 0; i < breakables.Length; i++)
            {
                ChangeActivation(breakables[i], true);
            }
            virtualCamera.SetActive(true);
        }
    }


    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            // deactivate all enemies and breakables
            for (int i = 0; i < enemies.Length; i++)
            {
                ChangeActivation(enemies[i], false);
            }
            for (int i = 0; i < breakables.Length; i++)
            {
                ChangeActivation(breakables[i], false);
            }
            virtualCamera.SetActive(false);
        }
    }


    public void ChangeActivation(Component component, bool activation)
    {
        component.gameObject.SetActive(activation);
    }
}
