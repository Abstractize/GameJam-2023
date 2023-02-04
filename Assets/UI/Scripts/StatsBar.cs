using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsBar : MonoBehaviour
{
    public int hunger { get; set; } = 20;
    public int sleep { get; set; } = 20;
    public int hygiene { get; set; } = 20;
    public int fun { get; set; } = 20;

    public void Start()
    {
        Mathf.Clamp(hunger, 0, 20);
        Mathf.Clamp(sleep, 0, 20);
        Mathf.Clamp(hygiene, 0, 20);
        Mathf.Clamp(fun, 0, 20);
    }
}
