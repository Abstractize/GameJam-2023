using Data;
using Player;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/InvetoryObject", order = 1)]
public class InventoryObject : ScriptableObject
{
    public Texture2D Icon;
    public string Name;
    public int Amount;
    public int Cost;
    public Interaction Stat;

    public void UseObject(PlayerController player)
        => player.BuyItem(Stat, Cost, Amount);
}

