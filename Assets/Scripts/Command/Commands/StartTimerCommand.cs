public class StartTimerCommand : ICommand
{
    private Chest chest;

    public StartTimerCommand(Chest chest)
    {
        this.chest = chest;
    }

    public void Execute()
    {
        chest.StartTimer();
    }

    public void Undo()
    {
        
    }
}