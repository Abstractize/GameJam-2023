using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsBar : MonoBehaviour
{
    public Slider hunger;
    public Slider fun;
    public Slider hygiene;
    public Slider sleep;
    public PlayerStats stats;

    public void Update()
    {
        hunger.value = stats.hunger;
        fun.value = stats.fun;
        hygiene.value = stats.hygiene;
        sleep.value = stats.sleep;
    }
}
