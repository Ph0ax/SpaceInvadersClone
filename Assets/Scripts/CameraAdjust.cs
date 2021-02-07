using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraAdjust : MonoBehaviour
{
    public const float Ratio4_3 = 1.333333333333333f;
    public const float Ratio16_9 = 1.777777777777778f;
    public const float Ratio21_9 = 2.333333333333333f;

    public float sceneWidth = 25;

    Camera _camera;

    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

        _camera = GetComponent<Camera>();
        float ratio = (float)Screen.height / Screen.width;
        //print(ratio);
        float diff = Mathf.Abs(Screen.height - Screen.width);

        if(diff > 900)
        {
            float unitsPerPixel = sceneWidth / Screen.width;
            float desiredHalfHeight = 0.5f * unitsPerPixel * Screen.height;
            _camera.orthographicSize = desiredHalfHeight;
        }


        //if (ratio <= Ratio4_3 || ratio >= Ratio16_9)
       // {
       //     float unitsPerPixel = sceneWidth / Screen.width;
         //   float desiredHalfHeight = 0.5f * unitsPerPixel * Screen.height;
          //  _camera.orthographicSize = desiredHalfHeight;
        //}
       
        

        
    }
}
