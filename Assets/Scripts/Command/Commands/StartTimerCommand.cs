using UnityEngine;

public class StartTimerCommand : ICommand
{
    private Chest chest;

    public StartTimerCommand(Chest chest)
    {
        this.chest = chest;
    }

    public void Execute()
    {
        Debug.Log($"Executing StartTimerCommand for chest in slot {chest.slotIndex}");
        chest.StartTimer();
    }

    public void Undo()
    {
        
    }
}