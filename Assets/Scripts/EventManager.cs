using UnityEngine;
using System;
public class EventManager : MonoBehaviour
{
    public static event Action gameOver;
    public static event Action gameUp;
    public static event Action enemyDead;
}
