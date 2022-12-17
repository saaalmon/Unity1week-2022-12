using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IManagerable
{
    void InitStart();
    void InitUpdate();

    void GameStart();
    void GameUpdate();

    void ResultStart();
    void ResultUpdate();

    void EndStart();
    void EndUpdate();
}

