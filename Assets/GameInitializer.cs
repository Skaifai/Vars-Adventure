using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField]
    GameObject playerCharacterPrefab;

    [SerializeField]
    int playerSpawnXCoordinate = 0;

    [SerializeField]
    int playerSpawnYCoordinate = 0;

    [SerializeField]
    GameObject levelPrefab;

    private void Start()
    {
        GameObject level = Instantiate(levelPrefab) as GameObject;
        level.transform.position = new Vector3(0, 0, 0);
        
        GameObject player = Instantiate(playerCharacterPrefab) as GameObject;
        player.transform.position = new Vector3(playerSpawnXCoordinate, playerSpawnYCoordinate, 0);
        player.transform.eulerAngles = new Vector3(0, 0, 0);
    }
}
