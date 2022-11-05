using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Strategy_Pattern
{
    public interface ICommandStrategy
    {
        Tilemap getTilesInArea(Vector3 position, Queue<ICommand> commandQueue);
    }
}
