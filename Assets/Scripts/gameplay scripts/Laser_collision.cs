using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_collision : MonoBehaviour
{
    // ATTACHED TO: the 'laser' child IN the laser parent's prefab 



    // THIS SCRIPT IS IN CHARGE OF:

    //  - Stretching the laser up
    //  - Calling the 'Split' function from the 'ball_script'
    //  - Destroying the laser


    private float speed = 1f;


    // Scaling the empty parent object over time, causing the child object to stretch upwards.
    private void Update()
    {
        transform.parent.localScale = transform.parent.localScale + Vector3.up * Time.deltaTime * speed;
    }



    // If the child gameobject hit an object with a "Ball" tag, call the 'Split' function from the 'ball_script' that attached to the specific ball that the laser hit.
    // Then decrease the static variable 'fire_laser_limit' by 1 and destroy this gameobject's parent.
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Ball")
        {
            collision.GetComponent<ball_script>().Split();
            laser.fire_laser_limit -= 1;

             Destroy(transform.parent.gameObject);
        }

        else
        {
            laser.fire_laser_limit -= 1;
            Destroy(transform.parent.gameObject);

        }
    }
}
