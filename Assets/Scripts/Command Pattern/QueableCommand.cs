using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts
{
    public class QueableMovementCommand : ICommand
    {
        private Vector3 _destination;
        NavMeshAgent _agent;

        public bool IsFinished
        {
            get
            {
                // Returns true when the remaining distance until the destination is less than 0.1f
                return _agent.remainingDistance <= 0.1f;
            }
        }

        public float DestinationXCoord;

        public void Execute()
        {
            _agent.SetDestination(_destination);
        }

        public QueableMovementCommand(Vector3 targetPosition, NavMeshAgent navMeshAgent)
        {
            _destination = targetPosition;
            DestinationXCoord = _destination.x;
            _agent = navMeshAgent;
        }
    }
}
