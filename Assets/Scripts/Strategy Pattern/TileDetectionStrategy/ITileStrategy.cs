using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Tilemaps;
using UnityEngine;

namespace Assets.Scripts.Strategy_Pattern.TileDetectionStrategy
{
    internal interface ITileStrategy
    {
        public void GetTilePositions(List<Vector3Int> listToEdit, Vector3Int playerPositionRounded, int heightOfTheArea, int widthOfTheArea);
    }
}
