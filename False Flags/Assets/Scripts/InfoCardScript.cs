using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoCardScript : MonoBehaviour
{
    public GameObject infoCardPanel;
    private Animator anim;

    void Start()
    {
        //time scale is positive on start
        Time.timeScale = 1;
        //get the animator component
        anim = GetComponent<Animator>(); 
        //disable it on start to stop it from playing the default animation
        anim.enabled = false;
    }

    // Update is called once per frame
    public void Update()
    {

    }
    //function to pause the game
    public void SlideIn()
    {
        //enable the animator component
        anim.enabled = true;
        //play the Slidein animation
        anim.Play("InfoCardSlideIn");
        //freeze the timescale
        Time.timeScale = 0;
    }
    //function to unpause the game
    public void SlideOut()
    {
        FindObjectOfType<AudioManager>().Play("Button_Click");
        //play the SlideOut animation
        anim.Play("InfoCardSlideOut");
        //set back the time scale to normal time scale
        Time.timeScale = 1;
    }

}