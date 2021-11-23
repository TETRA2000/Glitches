using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceOnLeftTop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var rect = GetComponent<RectTransform>().rect;
        var width = transform.localScale.x * rect.width;
        var height = transform.localScale.y * rect.height;

        var mainCanvas = GetComponentInParent<Canvas>();

        float minX = mainCanvas.GetComponent<RectTransform>().position.x + mainCanvas.GetComponent<RectTransform>().rect.xMin;
        float maxY = mainCanvas.GetComponent<RectTransform>().position.y + mainCanvas.GetComponent<RectTransform>().rect.yMax;
        float z = mainCanvas.GetComponent<RectTransform>().position.z;

        Vector3 topLeft = new Vector3(minX + width / 2, maxY - height / 2, z);
        transform.position = topLeft;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
