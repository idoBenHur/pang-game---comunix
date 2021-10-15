using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball_script : MonoBehaviour
{

    // ATTACHED TO: all of the balls' prefabs. and to the first balls in a scene


    // THIS SCRIPT IS IN CHARGE OF:

    //  - Giving the staring force/movement to the first balls in scene (the prefabs of the balls have 0 starting force)
    //  - Freezing the ball in the scene once the player is dead
    //  -Spliting the ball into 2 different balls' prefab



    // Standard variable
    public Vector2 starting_force;
    private Rigidbody2D ball_rb;

    // This variable store different prefabs according to the prefab that the script its attached to.
    // For example, in the 'huge ball' prefab, The 'nextBall' variable will store the 'big ball' prefab.
    // And in the 'big ball' prefab, the 'nextball' variable will store the 'ball' prefab. And so on until the 'XS ball' that store 'null' in the 'nextBall' variable.
    public GameObject nextBall;





    //          INITIAL FORCE PART:


    void Start()
    {
        ball_rb = GetComponent<Rigidbody2D>();

        // Adding force to the ball (wont effect prefab balls because the 'starting_force' is equal to 0 in those prefabs)
        ball_rb.AddForce(starting_force, ForceMode2D.Impulse);
    }




    //          FREEZING THE BALL PART:



    void Update()
    {
        // Getting the isPlayerDead value from the 'player_script'. if its true it will freeze the ball in place.

        if (player_script.isPlayerDead == true)
        {
            ball_rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }







    //          SPLITTING THE BALL INTO 2 BALLS PART:   


    //  This function is called by the 'Laser_collision' script.
    // If there is a prefab stored in the 'nextBall' variable, instantiate 2 copies of this prefab. One slightly to the left and one slightly to the right.
    // Then give each copy a 'starting_force' value. positive X value to the right copy and negative X value to the left copy (causing them to start moving to this dirction). (give them both positive Y value for a small "jump" effect)
    // Destroy the original ball (this gameobject)

    public void Split()
    {

        if(nextBall != null)
        {
            GameObject ball1 = Instantiate(nextBall, ball_rb.position + Vector2.right / 4, Quaternion.identity);
            GameObject ball2 = Instantiate(nextBall, ball_rb.position + Vector2.left / 4, Quaternion.identity);

            ball1.GetComponent<ball_script>().starting_force = new Vector2(2f, 5f);
            ball2.GetComponent<ball_script>().starting_force = new Vector2(-2f, 5f);


        }

        Destroy(gameObject);
    }

}
