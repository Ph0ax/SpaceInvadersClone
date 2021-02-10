using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EnemyController : MonoBehaviour
{

    private Transform enemyHolder;
    private GameObject[] Enemies;
    private int LastChilds;

    public int WidthOfMap = 21;
    public int EnemiesInRow = 8;
    public int EnemySquishIndex = 4;

    
    public GameObject Enemy1;
    public GameObject Enemy2;
    public GameObject shot;

    public float speed = 0.4f;
    public int NumberOfEnemies = 40;
    public static double Columns;


    
    public float firesuppresor= 0.9f;
    public float height = 10;

    float timer;
    float acceleration;
    bool Fire;
    bool[] shotflag;
    int iterator;

    

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;

        shotflag = new bool[NumberOfEnemies];
        for(int i = 0; i <NumberOfEnemies; i++)
        {
            shotflag[i] = false;
        }
        Spawner();
        
        InvokeRepeating("MoveEnemy", 0.1f, 0.1f);
        enemyHolder = GetComponent<Transform>();
        acceleration = speed / enemyHolder.childCount;
        LastChilds = enemyHolder.childCount;


    }


    void Update()
    {
        timer += Time.deltaTime;
    }
    void MoveEnemy()
    {
        iterator = 0;


        /* this is the best example of how efficiently we can avoid branching the code.
        if (timer >= 5.0f && timer <= 7.0f) Fire = true;

        else
        {
            if(Fire != false)
            {
                for (int i = 0; i < NumberOfEnemies; i++)
                {
                    shotflag[i] = false;
                }
            }
            Fire = false;
            if (timer > 7.0f) timer = 0;
        }
        */

        //branchless Fire if statements conversion ===============
        Fire = (timer >= 5.0f) && (timer <= 7.0f);
        timer = timer * Convert.ToSingle(!(timer > 7.0f)) + 0; // if (timer > 7.0f) timer = 0;

        if (Fire == true) 
            for (int i = 0; i < NumberOfEnemies; i++) 
                shotflag[i] = false;

        //=============================================




        enemyHolder.position += Vector3.right * speed;

        /* Another part of code for potentiall branchless code implement
        if(LastChilds != enemyHolder.childCount)
        {
            firesuppresor -= 0.002f;
            if (speed > 0) speed += acceleration;
            else speed -= acceleration;
            LastChilds = enemyHolder.childCount;
        }
        */


        //Branchless conversion ========================================================
        float ChildsEnemyBool = Convert.ToSingle(LastChilds != enemyHolder.childCount);
        firesuppresor -= 0.002f * ChildsEnemyBool;
        speed += acceleration * Convert.ToSingle(speed > 0) * ChildsEnemyBool;
        speed -= acceleration * Convert.ToSingle(!(speed > 0)) * ChildsEnemyBool;
        LastChilds = (LastChilds * Convert.ToInt16(!(LastChilds != enemyHolder.childCount))) + (enemyHolder.childCount * (int)ChildsEnemyBool);

        //==============================================================================

        foreach (Transform enemy in enemyHolder)
        {

            if (Fire == true && enemy.tag == "EnemyShot") 
            {
                
                if ((UnityEngine.Random.value > firesuppresor) && (shotflag[iterator] == false)) //change to made specific of using UnityEngine.random, not System.random
                {
                    shotflag[iterator] = true;
                    Instantiate(shot, enemy.position, enemy.rotation);
                }
           
            }
            iterator += 1;


            if (enemy.position.x < -10.5 || enemy.position.x > 10.5)
            {
                speed = -speed;
                enemyHolder.position += Vector3.down * 0.5f;
                return; //return is crucial to proper execution of this if. Thus we will not un-branch it.
            }


            /*
            if (enemy.position.y <= -18)
            {
                UIGameControl.isPlayerDead = true;
                Time.timeScale = 0;
            }
            */


            //Branchless conversion ========================================================
            UIGameControl.isPlayerDead = enemy.position.y <= -18;
            Time.timeScale = Convert.ToInt16(!(enemy.position.y <=-18)) + 0;
            //==============================================================================

        }

        if (enemyHolder.childCount == 0) //changes to this if statement would be just cosmetic. After this if we don't have any other instructions left to do, so compiler branching it won't be any problem whatsoever.
        {
            UIGameControl.win = true;
        }
    }

    void Spawner()
    {
        float spread = ((float)WidthOfMap-EnemySquishIndex) / EnemiesInRow;

        Columns = NumberOfEnemies / EnemiesInRow;
        Columns = Mathf.Ceil((float)Columns);

        float position_x = WidthOfMap / -2.0f;
        float x_min = position_x;
        Enemies = new GameObject[NumberOfEnemies];
        for (int i = 0; i < NumberOfEnemies; i++)
        {
            /*
            if (i % 8 != 0)
            {
                position_x += spread;
            }
            else
            {
                position_x = x_min;
                height -= 1.5f;
            }
            */

            //Branchless conversion ========================================================
            var positive = Convert.ToSingle(i % 8 != 0);
            var negative = Convert.ToSingle(!(i % 8 != 0));
            position_x += (positive * spread);
            position_x = (position_x *positive ) + (x_min * negative);
            height -= (1.5f * negative);

            //==============================================================================


            if (i < 8)
            {
                GameObject x2 = Instantiate(Enemy2, new Vector3(position_x, height, 0), Quaternion.identity);
                x2.transform.parent = transform;
            }
            else
            {
                GameObject x = Instantiate(Enemy1, new Vector3(position_x, height, 0), Quaternion.identity);
                x.transform.parent = transform;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Base")
        {
            GameObject playerBase = other.gameObject;
            BaseHealth baseHealth = playerBase.GetComponent<BaseHealth>();
            baseHealth.health -= 1;
            Destroy(gameObject);
        }
    }
}
