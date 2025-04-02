using UnityEngine;

public class UnlockingState : IChestState
{
    public void EnterState(Chest chest)
    {
        Debug.Log($"UnlockingState: Entered for chest in slot {chest.slotIndex}");
    }

    public void UpdateState(Chest chest)
    {
        if (chest.remainingTime > 0)
        {
            //Debug.Log($"UnlockingState: Updating timer for chest in slot {chest.slotIndex}. Remaining time: {chest.remainingTime}");
            chest.remainingTime -= Time.deltaTime;
            if (chest.remainingTime <= 0)
            {
                chest.remainingTime = 0;
                chest.SetState(new CollectedState());
                TimerManager.Instance.StopTimer(chest);
            }
        }
    }

    public void OnStartTimer(Chest chest)
    {
        
    }

    public void OnUnlockWithGems(Chest chest)
    {
        
        chest.remainingTime = 0;
        chest.SetState(new CollectedState());
        TimerManager.Instance.StopTimer(chest);
    }
}