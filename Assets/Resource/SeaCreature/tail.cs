using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tail 
{

    public Vector2 Spot;
    public Vector2 Dir;
    public float Angular;
    public float Speed;

    public Tail()
    {
        Spot = new Vector2();
        Dir = new Vector2();
        Angular = 0.0f;
        Speed = 0.0f;
    }

    public Tail SetSpot(Vector2 spot)
    {
        this.Spot = spot;
        this.Dir = Vector2.zero;

        return this;
    }
    public Tail SetDir(Vector2 dir)
    {
        this.Spot = Vector2.zero;
        this.Dir = dir;

        return this;
    }

    
    /*
    public Vector2 setpostionvec2(Vector3 position)
    {
        Vector2 vec2 = new Vector2(position.x, position.y);
        return vec2;
    }
    */
    public void ReDir(Vector2 here)
    {
        // Vector2 here = this.setpostionvec2(position);
        this.Dir = (this.Spot - here).normalized;
    }

    public bool IsRight()
    {
        bool isright = false;
        if (this.Dir.x>0)
        {
            isright = true;
        }

            return isright;
    }
    
}
