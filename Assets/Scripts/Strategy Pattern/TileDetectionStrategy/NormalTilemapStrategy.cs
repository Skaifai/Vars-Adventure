using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Tilemaps;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.Strategy_Pattern.TileDetectionStrategy
{
    class NormalTilemapStrategy : ITileStrategy
    {
        public void GetTilePositions(List<Vector3Int> listToEdit, Vector3Int playerPositionRounded, int heightOfTheArea, int widthOfTheArea)
        {
            for (int j = 0; j < heightOfTheArea; j++)
            {
                for (int i = 0; i < widthOfTheArea; i++)
                {
                    listToEdit[i + 3 * j] = new Vector3Int(playerPositionRounded.x - 1 + i, playerPositionRounded.y - j, 0);
                }
            }
        }
    }
}
