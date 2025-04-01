using UnityEngine;

public class UnlockingState : IChestState
{
    public void EnterState(Chest chest)
    {
        TimerManager.Instance.StartTimer(chest);
    }

    public void UpdateState(Chest chest)
    {
        if (chest.remainingTime > 0)
        {
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
       
    }
}