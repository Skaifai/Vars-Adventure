using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Strategy_Pattern.TileDetectionStrategy
{
    internal class TileStrategyController : MonoBehaviour
    {
        ITileStrategy _tileStrategy;

        public TileStrategyController()
        {

        }

        // Setter method. Allows us to change strategies at runtime
        public void SetStrategy(ITileStrategy tileStrategy)
        {
            this._tileStrategy = tileStrategy;
            //Console.WriteLine(strategy.SortingType + " sorting is set.\n");
        }

        public void GetTilePositions(List<Vector3Int> listToEdit, Vector3Int playerPositionRounded, int heightOfTheArea, int widthOfTheArea)
        {
            this._tileStrategy.GetTilePositions(listToEdit, playerPositionRounded, heightOfTheArea, widthOfTheArea);
        }
    }
}
