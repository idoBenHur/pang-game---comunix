using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // ATTACHED TO: 'Game manager' gameobject


    // THIS SCRIPT IS IN CHARGE OF:

    //  - The scenes transition (moving from one level to the next level or to the menu).
    //  - The scenes transition's windows (game over / next level / pause / win). 
    //  - Seraching for win condition (0 balls on the screen)



    //Standard variable
    private bool cheack_for_active_balls;
    private GameObject active_ball_in_scene;
    private Scene active_scene;

    // Transition's windows gameobject
    public GameObject Next_level_screen_obj;
    public GameObject GameOver_screen_obj;
    public GameObject pause_screen_obj;
    public GameObject winning_screen_obj;

    // Particle effects and their spawn position (empty gameobject prefab) 
    public GameObject[] win_confetti_particle_effect;
    public GameObject[] confetti_spawn_points;



    //          WIN CONDITION PART:


    void Start()
    {
        active_scene = SceneManager.GetActiveScene();
        cheack_for_active_balls = true;
    }

    void Update()

    // If 'cheack_for_active_balls' is still true, and the current scene is not the main menu - call 'active_balls_check()' function

    {
        if (cheack_for_active_balls == true && active_scene.buildIndex != 0 )
        {
            active_balls_check();
        }    
    }


    // This function checks if there are any balls in the scene by searching for an object with the 'ball' tag.
    //If the function does not find a 'ball', the function will stop being called, and will call the 'next_level_screen()' function.
    //If there are no balls in the scene, and the active scene is the third level, the function will call 'winning_screen()' instead
    private void active_balls_check()
    {
        active_ball_in_scene = GameObject.FindGameObjectWithTag("Ball");
        if (active_ball_in_scene == null)
        {
            cheack_for_active_balls = false;

            if(active_scene.buildIndex == 3)
            {
                winning_screen();
            }
            else
            {
                next_level_screen();
            }
        }
    }








    //          SCENES TRANSITION'S WINDOWS' PART:
    


    //Will show a screen with a button that takes the player to the next level
    public void next_level_screen()
    {
        Next_level_screen_obj.SetActive(true);
    }



    // Pop the winning screen and instantiate particle effects
    public void winning_screen()
    {
        winning_screen_obj.SetActive(true);
        Instantiate(win_confetti_particle_effect[0], confetti_spawn_points[0].transform.position, Quaternion.identity);
        Instantiate(win_confetti_particle_effect[1], confetti_spawn_points[1].transform.position, Quaternion.identity);
    }



    //This function is being called from the 'player_script' Once a ball touches a player, it will activated the game over screen.
    public void GameOver_Screen()
    {
        GameOver_screen_obj.SetActive(true);
    }









    //          BUTTONS PART:



    public void next_level_button()
    {
        SceneManager.LoadScene(active_scene.buildIndex + 1);
    }


    public void play_button()
    {
        SceneManager.LoadScene(1);
    }

    public void main_menu_button()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }


    public void pause_button()
    {
        pause_screen_obj.SetActive(true);
        Time.timeScale = 0f;
    }

    public void resume_button()
    {
        pause_screen_obj.SetActive(false);
        Time.timeScale = 1f;
    }

    public void restart_button()
    {
        SceneManager.LoadScene(active_scene.buildIndex);
    }

}




