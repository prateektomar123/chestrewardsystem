using UnityEngine;

public class ChestController : MonoBehaviour
{
    [SerializeField] private ChestSlotManager chestSlotManager;

    public void GenerateChest()
    {
        if (chestSlotManager == null)
        {
            Debug.LogError("ChestSlotManager is not assigned in the Inspector!");
            return;
        }
        chestSlotManager.AddChest();
    }
}