using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    
    public float speed = 0.01f;
    public float maxBound, minBound;
    public float firerate;


    public GameObject shot;
    public Transform shotSpawn;

    private Transform player;
    private bool turbo_flag;
    private int PointerDown;
    private float nextFire;
    private float nextCooldown;
    private float multiplier;
    private float tick;


    // Start is called before the first frame update
    void Start()
    {
        turbo_flag = false;
        tick = 0;
        multiplier = 1.0f;
        nextCooldown = 40f;
        
        player = GetComponent<Transform>();
    }

    void Update()
    {
        if (nextCooldown - Time.timeSinceLevelLoad > 0) Turbo.turboCooldown = nextCooldown - Time.timeSinceLevelLoad;
        else Turbo.turboCooldown = 0;

        if(PointerDown == 1) PlayerMoveLeft();
        if(PointerDown == 2) PlayerMoveRight();

        if (Time.timeSinceLevelLoad > nextCooldown && turbo_flag == true) //branching this if would not be transparent at all. Making changes to it would be quite impossible.
        {
            if(tick == 0)
            {
               tick = Time.timeSinceLevelLoad;
            }
            if (Time.timeSinceLevelLoad - tick < 5.0f) 
            {
                Turbo.discharge = true;
                multiplier = 2.0f;
            }
            else
            {
                Turbo.discharge = false;
                multiplier = 1.0f;
                tick = 0;
                nextCooldown += Time.timeSinceLevelLoad;
                turbo_flag = false;
            }


        }

        if ( Time.time > nextFire)
        {
           nextFire = Time.time + (firerate / multiplier);
           Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        }

    }

    void PlayerMoveLeft()
    {
        int h = -1;
        if (player.position.x < minBound && h < 0) h = 0;
        else if (player.position.x > maxBound && h > 0) h = 0;
        player.position += Vector3.right * h * speed;

    }
    void PlayerMoveRight()
    {
        int h = 1;
        if (player.position.x < minBound && h < 0) h = 0;
        else if (player.position.x > maxBound && h > 0) h = 0;
        
        player.position += Vector3.right * h * speed;

    }

    public void PointerDownLeft()
    {
        PointerDown = 1;
    }
    public void PointerDownRight()
    {
        PointerDown = 2;
    }
    public void PointerUp()
    {
        PointerDown = 0;
    }

    public void TurboOnClick()
    {   
        /*
        if (nextCooldown <= Time.time)
        {
            turbo_flag = true;
        }
        */


        //Branchless conversion=========================================================
        turbo_flag = nextCooldown <= Time.time;

        //==============================================================================
        
    }
}
