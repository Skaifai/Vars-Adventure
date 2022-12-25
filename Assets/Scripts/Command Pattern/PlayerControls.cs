using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

internal class PlayerControls : ISubject
{
    public bool IsMoving { get; private set; }

    public bool IsAttacking { get; private set; }

    private List<IObserver> _observers = new List<IObserver>();

    #region Movement Commands
    public float? _currentMoveCommandXCoord { get; private set; }
    
    NavMeshAgent _agent;

    private Queue<QueableMovementCommand> _moveCommands = new Queue<QueableMovementCommand>();

    private QueableMovementCommand _currentMoveCommand;

    #endregion

    #region Attack Commands
    public float? _currentAttackCommandXCoord { get; private set; }

    private Queue<AttackCommand> _attackCommands = new Queue<AttackCommand>();

    private AttackCommand _currentAttackCommand;

    Timer _timer;

    #endregion

    #region Observer Pattern
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
    #endregion

    // Object constructor.
    public PlayerControls(NavMeshAgent agent, Timer timer)
    {
        _agent = agent;

        _timer = timer;
    }

    public void ProcessMovingCommands()
    {
        // If there is a command that is currently executing, then do nothing.
        if (_currentMoveCommand != null && _currentMoveCommand.IsFinished == false)
        {
            if (_currentMoveCommand.GetType() == typeof(QueableMovementCommand))
            {
                _currentMoveCommandXCoord = _currentMoveCommand.DestinationXCoord;
                IsMoving = true;
            }
            else _currentMoveCommandXCoord = null;

            Alert();

            return;
        }

        IsMoving = false;

        Alert();

        // If there are no commands in the queue, then do nothing.
        if (_moveCommands.Any() == false) return;

        // Dequeing the command and assigning it to the current command field.
        _currentMoveCommand = _moveCommands.Dequeue();

        // Executing current command. The command includes the boolean field which determines when that command is considered as finished.
        _currentMoveCommand.Execute();
    }

    public void ProcessAttackCommand()
    {
        if(_currentAttackCommand != null && _currentAttackCommand.IsFinished == false)
        {
            IsAttacking = true;

            Alert();

            return;
        }

        IsAttacking = false;

        Alert();

        if (_attackCommands.Any() == false) return;

        _currentAttackCommand = _attackCommands.Dequeue();

        _currentAttackCommand.Execute();
    }

    public void ListenForMoveCommands()
    {
        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftShift))
        {
            // Getting the position of the mouse. This is where the player character will go
            Vector3 pointToMove = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Adding this command to the queue
            QueableMovementCommand queableCommand = new QueableMovementCommand(pointToMove, _agent);
            _moveCommands.Enqueue(queableCommand);
        }
        else if (Input.GetMouseButtonDown(0))
        {
            // Getting the position of the mouse. This is where the player character will go
            Vector3 pointToMove = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Clearing the queue, because this is an instant command
            _moveCommands.Clear();
            
            // Setting the current command to null, to overwrite it
            _currentMoveCommand = null;

            // Adding this command to the queue. Update method will then deque this command into the _currentComand field
            QueableMovementCommand instantCommand = new QueableMovementCommand(pointToMove, _agent);
            _moveCommands.Enqueue(instantCommand);
        }
    }

    public void ListenForAttackCommands()
    {
        if (Input.GetMouseButton(1))
        {
            Vector3 pointToAttack = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (_currentAttackCommand == null || _currentAttackCommand.IsFinished)
            {
                _attackCommands.Clear();

                AttackCommand attackCommand = new AttackCommand(pointToAttack, _timer);
                _attackCommands.Enqueue(attackCommand);
            }
        }
    }

    public void SetTimerObject(Timer timer)
    {
        this._timer = timer;
    }
}
