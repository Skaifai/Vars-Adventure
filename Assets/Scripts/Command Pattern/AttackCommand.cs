using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts
{
    public class AttackCommand : ICommand
    {
        private Vector3 _destination;
        Timer _timer;
        public float DestinationXCoord;

        public bool IsFinished 
        {
            get
            {
                return _timer.Finished;
            }
        }

        public void Execute()
        {
            _timer.Duration = 1.0f;
            _timer.Run();
        }

        public AttackCommand(Vector3 targetPosition, Timer timer)
        {
            this._destination = targetPosition;
            DestinationXCoord = _destination.x;
            this._timer = timer;
            _timer.Duration = 1.0f;
        }
    }
}
