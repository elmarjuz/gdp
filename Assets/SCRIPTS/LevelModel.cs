using UnityEngine;
using System.Collections;

public class LevelModel : MonoBehaviour
{
    public static float[,] shieldSpawnPoints = new float[,]{
        {20,60,110,140,170,210,260,320},
        {1,40,80,125,170,220,270,310},
        {20,60,110,140,170,210,160,320},
        {20,60,110,140,170,210,160,320}
    };
    public static float[,] laserSpawnPoints = new float[,]{
        {30,50,115,145,175,205,250,310},
        {30,60,110,140,170,210,160,320},
        {20,60,110,140,170,210,160,320},
        {20,60,110,140,170,210,160,320}
    };
    public static float[,] gunSpawnPoints = new float[,]{
        {10,40,78,100,150,190,230,280,300,330},
        {10,40,80,90,150,190,230,280,300,330},
        {10,40,80,90,150,190,230,280,300,330},
        {10,40,80,90,150,190,230,280,300,330}
    };


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
