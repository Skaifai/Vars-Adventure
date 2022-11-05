using System;

public interface ICommand
{
    bool IsFinished { get; }

    void Execute();
}
