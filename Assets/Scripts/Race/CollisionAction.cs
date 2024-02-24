using UnityEngine;


// player collision control system
public class CollisionAction
{
    public delegate void Action(PlayerControl player, CollisionAction collisionAction, Collision2D collision);
    
    public string Tag { set; get; }
    public bool Repeat { set; get; }

    private Action _action;

    public CollisionAction(string tag, Action action, bool repeat)
    {
        Tag = tag;
        _action = action;
        Repeat = repeat;
    }

    public void Invoke(PlayerControl player, Collision2D collision)
    {
        _action.Invoke(player, this, collision);
    }
}