using UnityEngine;
using UnityEngine.UI;

public class ClickAnswer : MonoBehaviour {

    public void ButtonClicked()
    {
        FindObjectOfType<AudioManager>().Play("Button_Click"); // Play button click sound effect.
    }
}
