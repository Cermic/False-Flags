using UnityEngine;
using UnityEngine.SceneManagement;

public class Interact : MonoBehaviour {

	public void ChanageToScene (string sceneName) {
		SceneManager.LoadScene(sceneName); // Load Appropriate Scene.
        if (sceneName == "Game")
        {
            FindObjectOfType<AudioManager>().Stop("Getting_it_Done"); // Play button click sound effect.
            FindObjectOfType<AudioManager>().Play("Button_Click"); // Play button click sound effect.
            FindObjectOfType<AudioManager>().Play("Werq"); // Play button click sound effect.
        }
    }

    public void ClickAnswer()
    {
        FindObjectOfType<AudioManager>().Play("Button_Click"); // Play button click sound effect.
    }
}
