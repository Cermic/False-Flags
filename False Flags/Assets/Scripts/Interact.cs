using UnityEngine;
using UnityEngine.SceneManagement;

public class Interact : MonoBehaviour {

	public void ChanageToScene (string sceneName) {
		SceneManager.LoadScene(sceneName); // Load Appropriate Scene.
        if (sceneName == "MenuScreen")
        {
            FindObjectOfType<AudioManager>().Stop("Werq"); 
            FindObjectOfType<AudioManager>().Play("Button_Click"); 
            FindObjectOfType<AudioManager>().Play("Getting_it_Done"); 
        }
        if (sceneName == "Game")
        {
            FindObjectOfType<AudioManager>().Stop("Getting_it_Done"); 
            FindObjectOfType<AudioManager>().Play("Button_Click"); 
            FindObjectOfType<AudioManager>().Play("Werq");
        }
    }

    public void ClickAnswer()
    {
        FindObjectOfType<AudioManager>().Play("Button_Click"); // Play button click sound effect.
    }
}
