
using System.Numerics;

public class Character
{
    Point position;
    public Point Position => position;
    Direction direction;
    public Direction Direction => direction;

    public void Move(int distance)
    {
        switch (direction)
        {
            case Direction.North: position.Y += distance; break;
            case Direction.East: position.X += distance; break;
            case Direction.South: position.Y -= distance; break;
            case Direction.West: position.X -= distance; break;
        }
    }

    public void Turn(string direction)
    {
        switch (direction)
        {
            case "right": this.direction = (Direction)(((int)this.direction + 1) % 4); break;
            case "left": this.direction = (Direction)(((int)this.direction + 3) % 4); break;
            default: throw new ArgumentException();
        }
    }
}

public enum Direction { North, East, South, West}
public struct Point
{
    public int X;
    public int Y;

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
}
