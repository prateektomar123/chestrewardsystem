using UnityEngine;

[CreateAssetMenu(fileName = "NewChestType", menuName = "Chest System/Chest Type", order = 1)]
public class ChestType : ScriptableObject
{
    public string chestName; 
    public int timerInMinutes; 
    public int minCoins; 
    public int maxCoins; 
    public int minGems; 
    public int maxGems; 
    public Sprite chestSprite; 
}