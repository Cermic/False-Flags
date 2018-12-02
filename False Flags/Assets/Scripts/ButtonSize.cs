using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSize : MonoBehaviour
{

    public GameObject container;
    //sets width and height values to be the same as the screen
    float width = Screen.width;
    float height = Screen.height;

    //variable for the child objects
    Transform[] children;

    //variable for the gridLayoutGroup
    GridLayoutGroup glg;

    //initialisation of variables and setup
    private void Start()
    {
        //finds the children of the container
        children = container.transform.GetComponentsInChildren<Transform>();
        //sets the GridLayoutGHroup to be a variable for ease of refference
        glg = container.GetComponent<GridLayoutGroup>();

        //initial fixing of button scale and setting of UI
        FixButtonScale();
        Rebuild();
    }
    // Update is called once per frame
    void Update()
    {
        //if the screen size changes then...
        if (Screen.width != width || Screen.height != height)
        {
            //rebuild the ui and fix the button scale
            Rebuild();
            FixButtonScale();
            
            //reset the width and height to be the current resolution
            width = Screen.width;
            height = Screen.height;

        }

    }

    //method to keep button scale consistent
    void FixButtonScale()
    {
        //for every immediate child of the container, set the x and y scale to be 1.6
        foreach (Transform child in children)
        {
            if (child.parent == container.transform)
                child.GetComponent<RectTransform>().localScale = new Vector2(1.6f, 1.6f);
        }
    }

    //rebuilds the grid ui
    void Rebuild()
    {
        //turns off the contentSizeFitter before checking if the current resolution is landscape or portrait and setting the grid scale accordingly
        glg.GetComponent<ContentSizeFitter>().enabled = true;
        if (Screen.width < Screen.height)
            container.transform.localScale = new Vector2(1.3f, 1.3f);
        else
            container.transform.localScale = new Vector2(1f, 1f);
        //rebuilds the layout then re-enables the ContentSizeFitter
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)container.transform);
        glg.GetComponent<ContentSizeFitter>().enabled = false;
    }
}