public interface ICommand
{
    void Execute(Character character);
}

public class MoveCommand : ICommand
{
    int distance;

    public MoveCommand(string distance)
    {
        this.distance = int.Parse(distance);
    }

    public void Execute(Character character)
    {
        character.Move(distance);
    }
}

public class TurnCommand : ICommand
{
    string direction;

    public TurnCommand(string direction)
    {
        this.direction = direction;
    }

    public void Execute(Character character)
    {
        character.Turn(direction);
    }
}
