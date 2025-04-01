using UnityEngine;
using System.Collections.Generic;

public class ChestSlotManager : MonoBehaviour
{
    public static ChestSlotManager Instance { get; private set; }

    [SerializeField] private ChestType[] chestTypes;
    [SerializeField] private int maxSlots = 4;
    [SerializeField] private Popup slotsFullPopup;
    private List<Chest> chestSlots;
    private List<ChestSlotUI> chestSlotUIs;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        chestSlots = new List<Chest>(new Chest[maxSlots]);
        chestSlotUIs = new List<ChestSlotUI>();
    }

    public void RegisterSlotUI(ChestSlotUI slotUI)
    {
        chestSlotUIs.Add(slotUI);
    }

    public bool HasEmptySlot()
    {
        return chestSlots.Contains(null);
    }

    private int GetFirstEmptySlotIndex()
    {
        return chestSlots.IndexOf(null);
    }

    public bool AddChest()
    {
        if (!HasEmptySlot())
        {
            Debug.Log("All slots are full!");
            slotsFullPopup.Show();
            return false;
        }

        ChestType randomChestType = chestTypes[Random.Range(0, chestTypes.Length)];
        int slotIndex = GetFirstEmptySlotIndex();
        Chest newChest = new Chest(randomChestType, slotIndex);
        chestSlots[slotIndex] = newChest;
        chestSlotUIs[slotIndex].SetChest(newChest);
        return true;
    }

    public void RemoveChest(int slotIndex)
    {
        chestSlots[slotIndex] = null;
        chestSlotUIs[slotIndex].Clear();
    }

    public Chest GetChest(int slotIndex)
    {
        return chestSlots[slotIndex];
    }
}