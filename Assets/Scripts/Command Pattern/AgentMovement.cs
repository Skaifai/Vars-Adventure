using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

internal class AgentMovement : ISubject
{
    public bool IsMoving { get; private set; }

    public float? CurrentCommandXCoord { get; private set; }

    private List<IObserver> _observers = new List<IObserver>();

    NavMeshAgent _agent;

    private Queue<QueableMovementCommand> _commands = new Queue<QueableMovementCommand>();

    private QueableMovementCommand _currentCommand;

    // Subscription method
    public void Subscribe(IObserver observer)
    {
        this._observers.Add(observer);
    }

    // Unsubscription method
    public void Unsub(IObserver observer)
    {
        this._observers.Remove(observer);
    }

    // Alert all subscribed observers
    public void Alert()
    {
        foreach (var observer in _observers)
        {
            observer.Check(this);
        }
    }

    // Object constructor.
    public AgentMovement(NavMeshAgent agent)
    {
        _agent = agent;
    }

    public void ProcessCommands()
    {
        // If there is a command that is currently executing, then do nothing.
        if (_currentCommand != null && _currentCommand.IsFinished == false)
        {
            if (_currentCommand.GetType() == typeof(QueableMovementCommand))
            {
                CurrentCommandXCoord = _currentCommand.DestinationXCoord;
                IsMoving = true;
            }
            else CurrentCommandXCoord = null;

            Alert();

            return;
        }

        IsMoving = false;

        Alert();

        // If there are no commands in the queue, then do nothing.
        if (_commands.Any() == false) return;

        // Dequeing the command and assigning it to the current command field.
        _currentCommand = _commands.Dequeue();

        // Executing current command. The command includes the boolean field which determines when that command is considered as finished.
        _currentCommand.Execute();
    }

    public void ListenForCommands()
    {
        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftShift))
        {
            // Getting the position of the mouse. This is where the player character will go
            Vector3 pointToMove = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Adding this command to the queue
            QueableMovementCommand queableCommand = new QueableMovementCommand(pointToMove, _agent);
            _commands.Enqueue(queableCommand);
        }
        else if (Input.GetMouseButtonDown(0))
        {
            // Getting the position of the mouse. This is where the player character will go
            Vector3 pointToMove = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Clearing the queue, because this is an instant command
            _commands.Clear();
            
            // Setting the current command to null, to overwrite it
            _currentCommand = null;

            // Adding this command to the queue. Update method will then deque this command into the _currentComand field
            QueableMovementCommand instantCommand = new QueableMovementCommand(pointToMove, _agent);
            _commands.Enqueue(instantCommand);
        }
    }
}
