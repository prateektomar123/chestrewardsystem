public class UnlockWithGemsCommand : ICommand
{
    private Chest chest;

    public UnlockWithGemsCommand(Chest chest)
    {
        this.chest = chest;
    }

    public void Execute()
    {
        chest.UnlockWithGems();
    }

    public void Undo()
    {
        
    }
}