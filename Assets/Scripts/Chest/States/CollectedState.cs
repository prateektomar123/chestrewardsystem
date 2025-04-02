using UnityEngine;

public class CollectedState : IChestState
{
    public void EnterState(Chest chest)
    {
        Debug.Log($"CollectedState: Entered for chest in slot {chest.slotIndex}");
    }

    public void UpdateState(Chest chest)
    {
        
    }

    public void OnStartTimer(Chest chest)
    {
       
    }

    public void OnUnlockWithGems(Chest chest)
    {
        
    }
}