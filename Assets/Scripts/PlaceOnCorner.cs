using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceOnCorner : MonoBehaviour
{
    public enum PlacementPosition { LeftTop, RightTop, RightBottom, LeftBottom }
    public PlacementPosition placementPosition;

    // Start is called before the first frame update
    void Start()
    {
        var rect = GetComponent<RectTransform>().rect;
        var width = transform.localScale.x * rect.width;
        var height = transform.localScale.y * rect.height;

        var mainCanvas = GetComponentInParent<Canvas>();

        float minX = mainCanvas.GetComponent<RectTransform>().position.x + mainCanvas.GetComponent<RectTransform>().rect.xMin;
        float maxX = mainCanvas.GetComponent<RectTransform>().position.x + mainCanvas.GetComponent<RectTransform>().rect.xMax;
        float minY = mainCanvas.GetComponent<RectTransform>().position.y + mainCanvas.GetComponent<RectTransform>().rect.yMin;
        float maxY = mainCanvas.GetComponent<RectTransform>().position.y + mainCanvas.GetComponent<RectTransform>().rect.yMax;
        float z = mainCanvas.GetComponent<RectTransform>().position.z;

        Vector3 position = Vector3.zero;
        switch(placementPosition)
        {
            case PlacementPosition.LeftTop:
                position = new Vector3(minX + width / 2, maxY - height / 2, z);
                break;
            case PlacementPosition.RightTop:
                position = new Vector3(maxX - width / 2, maxY - height / 2, z);
                break;
            case PlacementPosition.RightBottom:
                position = new Vector3(maxX - width / 2, minY + height / 2, z);
                break;
            case PlacementPosition.LeftBottom:
                position = new Vector3(minX + width / 2, minY + height / 2, z);
                break;
        }

        transform.position = position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
