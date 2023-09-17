using UnityEngine;
using System;
public class EventManager : MonoBehaviour
{
    public static  Action gameOver;
    public static  Action gameUp;
    public static  Action enemyDead;
    public static  Action enemyMoreThanTarget;
    public static  Action StartEnemyAttack;
}
