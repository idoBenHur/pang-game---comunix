using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_script : MonoBehaviour
{

    // ATTACHED TO: 'the player' gameobject


    // THIS SCRIPT IS IN CHARGE OF:

    //  -The player movement (using a joystick prefab from the asset store )
    //  -Death/Gameover conditions and after death effects.


    //Standard variable
    private float player_speed = 5f;
    private float dirction_of_movement;
    private Rigidbody2D player_rb;
    private Transform player_transform; 
    public GameObject death_explosion_particle_effect;
    
    // Other scripts attached
    public Joystick joystick;
    public GameManager GameManager;

    // Static value, used in the 'ball_script' as well.
    public static bool isPlayerDead;
    
 
    


    void Start()
    {
        player_transform = GetComponent<Transform>();
        player_rb = GetComponent<Rigidbody2D>();
        isPlayerDead = false;

    }



    //            PLAYER MOVEMENT PART -

    void Update()
    {
        joystick_movement();

    }

    // In charge of converting the user input from the joystick to the movement of the player.
    // Joystick.Horizontal is returning a number between -1 to 1 depending on the user input (full drag to the left == -1, full drag to the right == 1).
    // Using only numbers bigger then 0.1 or smaller then -0.1 to control the joystick's sensitivity. 
    // Changing the player localscale so he would face the right dirction
    private void joystick_movement()
    {

        if(joystick.Horizontal >= 0.1f)
        {
            dirction_of_movement = player_speed;
            player_transform.localScale = new Vector2(-0.3223443f, player_transform.localScale.y);
        }
        else if( joystick.Horizontal <= -0.1f)
        {
            dirction_of_movement = -player_speed;
            player_transform.localScale = new Vector2(0.3223443f, player_transform.localScale.y);
        }
        else
        {
            dirction_of_movement = 0f;
        }


    }

    private void FixedUpdate()
    {
        player_rb.MovePosition(player_rb.position + new Vector2(dirction_of_movement * Time.deltaTime, 0f));
    }





    //          DEATH / GAMEOVER CONDITIONS, AND AFTER DEATH PART-



    // If the player's collider collide with an object with the tag "Ball" it means that the player has lost, causing 3 things:
    //  -The player will freeze in place.
    //  -The 'Death_Explosion' function will be called
    //  -The static value 'isPlayerDead' will become true.

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ball")
        {

            player_rb.constraints = RigidbodyConstraints2D.FreezeAll;
            isPlayerDead = true;        
            Death_Explosion();
        }

    }



    // Will hide the player's gameobject, instantiate particle effect at the player position and will call the 'pop_GameOver_screen' after 2 seconds.

    private void Death_Explosion()
    {
        gameObject.SetActive(false);
        Instantiate(death_explosion_particle_effect, transform.position, Quaternion.identity);
        Invoke("pop_GameOver_screen", 2f);

    }

    //calling the GameOver_Screen from the GameManger script
    private void pop_GameOver_screen()
    {
        GameManager.GameOver_Screen();
    }


}
