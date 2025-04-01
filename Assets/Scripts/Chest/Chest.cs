using UnityEngine;

[System.Serializable] 
public class Chest
{
    public ChestType chestType; 
    public ChestState state;    
    public float remainingTime;
    public int slotIndex;       

    public Chest(ChestType type, int slot)
    {
        chestType = type;
        state = ChestState.Locked;
        remainingTime = type.timerInMinutes * 60f; 
        slotIndex = slot;
    }
    public (int coins, int gems) GetRewards()
    {
        int coins = Random.Range(chestType.minCoins, chestType.maxCoins + 1);
        int gems = Random.Range(chestType.minGems, chestType.maxGems + 1);
        return (coins, gems);
    }
}