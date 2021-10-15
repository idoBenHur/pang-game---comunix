using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour
{
    // ATTACHED TO: 'laser parent' gameobject 


    // THIS SCRIPT IS IN CHARGE OF:

    //  - Instantiate the laser parent prefab
    //  - Limiting the number of times the player can fire/instantiate

    private Rigidbody2D player_rb;

    // A prefab with a parent and a child objects
    public GameObject laser_parent_obj;

    // Static variable, used in the 'Laser_collision' script
    public static float fire_laser_limit = 0;


    void Start()
    {
        fire_laser_limit = 0;

        player_rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();

    }

    // This function is activated by the 'fire' button.
    // If the player didnt reach the 'fire_laser_limit' , add 1 to the limint and instantiate the prefab.
    public void fire_laser_button()
    {
        if(fire_laser_limit < 2)
        {
            fire_laser_limit += 1;
            Instantiate(laser_parent_obj, player_rb.position, Quaternion.identity);
        }
   
    }
}
