using UnityEngine;

public class LockedState : IChestState
{
    public void EnterState(Chest chest)
    {
        chest.remainingTime = chest.chestType.timerInMinutes * 60f;
    }

    public void UpdateState(Chest chest)
    {
        
    }

    public void OnStartTimer(Chest chest)
    {
        if (TimerManager.Instance.CanStartTimer())
        {
            Debug.Log($"LockedState: Transitioning to UnlockingState for chest in slot {chest.slotIndex}");
            TimerManager.Instance.StartTimer(chest);
            chest.SetState(new UnlockingState());
        }
        else
        {
            Debug.Log("Cannot start timer: Another chest is already unlocking.");
        }
    }

    public void OnUnlockWithGems(Chest chest)
    {
        
        chest.remainingTime = 0;
        chest.SetState(new CollectedState());
    }
}