using UnityEngine;

public class ChestController : MonoBehaviour
{
    private ChestSlotManager chestSlotManager;

    void Awake()
    {
        chestSlotManager = ChestSlotManager.Instance;
    }

    public void GenerateChest()
    {
        chestSlotManager.AddChest();
    }
}