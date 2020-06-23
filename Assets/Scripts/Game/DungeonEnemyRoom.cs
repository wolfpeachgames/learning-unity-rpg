using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEnemyRoom : DungeonRoom
{
    public LockedDoor[] doors;


    // Start is called before the first frame update
    void Start()
    {

    }


    public void CheckEnemies()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].gameObject.activeInHierarchy && i < enemies.Length - 1)
            {
                return;
            }
            else
            {
                OpenDoors();
            }
        }
    }


    public void CloseDoors()
    {
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].Close();
        }
    }


    public void OpenDoors()
    {
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].Open();
        }
    }


    public override void OnTriggerEnter2D(Collider2D other)
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
            CloseDoors();
        }
    }


    //public override void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.CompareTag("Player") && !other.isTrigger)
    //    {
    //        // deactivate all enemies and breakables
    //        for (int i = 0; i < enemies.Length; i++)
    //        {
    //            ChangeActivation(enemies[i], false);
    //        }
    //        for (int i = 0; i < breakables.Length; i++)
    //        {
    //            ChangeActivation(breakables[i], false);
    //        }
    //    }
    //}
}
