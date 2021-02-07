using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turbo : MonoBehaviour
{
    public static float turboCooldown;
    private Text turboText;
    public static bool discharge = false;

    // Start is called before the first frame update
    void Start()
    {
        turboCooldown = 40f;
        turboText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (turboCooldown <= 40 && turboCooldown > 0)
        {
            turboText.text = ""+ turboCooldown;
        }else if(turboCooldown == 0)
        {
            if (discharge == true)
            {
                turboText.text = "...";
            }
            else turboText.text = "!";
            
        }
    }
}
