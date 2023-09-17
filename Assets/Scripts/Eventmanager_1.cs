using UnityEngine;
using System;

public class Eventmanager : MonoBehaviour
{
    public static event Action gameOver;
    public static event Action gameUp;
    public static event Action enemyDead;
}
