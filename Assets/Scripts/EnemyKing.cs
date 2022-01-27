using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKing : Enemy
{
    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        skipMove = false;
        base.AttemptMove<T>(xDir, yDir);
    }
    protected override void OnCantMove<T>(T component)
    {
        base.OnCantMove<T>(component);
        base.OnCantMove<T>(component);
    }

}
