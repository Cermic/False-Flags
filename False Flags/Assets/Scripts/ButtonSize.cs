using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSize : MonoBehaviour
{

    public GameObject container;

    // Update is called once per frame
    void Update()
    {

        float width = container.GetComponent<RectTransform>().rect.width;
        float height = container.GetComponent<RectTransform>().rect.height;
        Vector2 newSize = new Vector2(width / 2, width / 3);
        container.GetComponent<GridLayoutGroup>().cellSize = newSize;
    }
}
