using UnityEngine;

public class ChestController : MonoBehaviour
{
    private ChestSlotManager chestSlotManager;

    void Awake()
    {
        
    }

    public void GenerateChest()
    {
        if (chestSlotManager == null)
        {
            chestSlotManager = ChestSlotManager.Instance;
            if (chestSlotManager == null)
            {
                Debug.LogError("ChestSlotManager instance is null!");
                return;
            }
        }
        chestSlotManager.AddChest();
    }

    public void StartTimer(int slotIndex)
    {
        if (chestSlotManager == null)
        {
            chestSlotManager = ChestSlotManager.Instance;
            if (chestSlotManager == null)
            {
                Debug.LogError("ChestSlotManager instance is null!");
                return;
            }
        }

        Chest chest = chestSlotManager.GetChest(slotIndex);
        if (chest != null)
        {
            chest.StartTimer();
        }
    }

    public void CollectRewards(int slotIndex)
    {
        if (chestSlotManager == null)
        {
            chestSlotManager = ChestSlotManager.Instance;
            if (chestSlotManager == null)
            {
                Debug.LogError("ChestSlotManager instance is null!");
                return;
            }
        }

        Chest chest = chestSlotManager.GetChest(slotIndex);
        if (chest != null)
        {
            var (coins, gems) = chest.GetRewards();
            PlayerData.Instance.AddCoins(coins);
            PlayerData.Instance.AddGems(gems);
            chestSlotManager.RemoveChest(slotIndex);
        }
    }
}