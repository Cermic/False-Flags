using UnityEngine;

public class MenuScreenController : MonoBehaviour
{
	void Start()
    {
        FindObjectOfType<AudioManager>().Play("Getting_it_Done");
    }
}
