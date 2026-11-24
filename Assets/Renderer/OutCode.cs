using UnityEngine;

public class OutCode
{
    // one bit per edge of the viewport (-1 to 1 in x and y)
    public bool up, down, left, right;

    public OutCode(Vector2 v)
    {
        up = v.y > 1;
        down = v.y < -1;
        left = v.x < -1;
        right = v.x > 1;
    }

    public OutCode() // produces 0000, ie inside the viewport
    {
        up = false;
        down = false;
        left = false;
        right = false;
    }

    public OutCode(OutCode oc) // copy of the given OutCode
    {
        up = oc.up;
        down = oc.down;
        left = oc.left;
        right = oc.right;
    }

    public OutCode(bool up, bool down, bool left, bool right)
    {
        this.up = up;
        this.down = down;
        this.left = left;
        this.right = right;
    }

    //Logical OR
    public static OutCode operator +(OutCode a, OutCode b)
    {
        return new OutCode(a.up || b.up, a.down || b.down, a.left || b.left, a.right || b.right);
    }

    //Logical AND
    public static OutCode operator *(OutCode a, OutCode b)
    {
        return new OutCode(a.up && b.up, a.down && b.down, a.left && b.left, a.right && b.right);
    }

    //Equality
    public static bool operator ==(OutCode a, OutCode b)
    {
        return (a.up == b.up) && (a.down == b.down) && (a.left == b.left) && (a.right == b.right);
    }

    //Not equal
    public static bool operator !=(OutCode a, OutCode b)
    {
        return !(a == b);
    }

    // C# wants these whenever == and != are overloaded, otherwise you get warnings
    public override bool Equals(object obj)
    {
        return obj is OutCode other && this == other;
    }

    public override int GetHashCode()
    {
        return (up ? 8 : 0) + (down ? 4 : 0) + (left ? 2 : 0) + (right ? 1 : 0);
    }

    public override string ToString()
    {
        return (up ? "1" : "0") + (down ? "1" : "0") + (left ? "1" : "0") + (right ? "1" : "0");
    }
}