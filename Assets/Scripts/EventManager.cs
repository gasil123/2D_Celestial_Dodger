using UnityEngine;
using System;
public class EventManager : MonoBehaviour
{
    public static  Action gameOver;
    public static  Action enemyDestroyed;
    public static  Action planeDestroyed;
    public static  Action enemyMoreThanTarget;
    public static  Action bulletfired;
}
