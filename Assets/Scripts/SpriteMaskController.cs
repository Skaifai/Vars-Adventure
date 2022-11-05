using Assets.Scripts.Strategy_Pattern.TileDetectionStrategy;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

// EN : The Singleton should always be a 'sealed' class to prevent class
// inheritance through external classes and also through nested classes.
public sealed class SpriteMaskController : MonoBehaviour
{
    // Strategy support
    private TileStrategyController _tileStrategyController;

    // Singleton support. We hide the constructor to make sure there are no instances created with the 'new' operator.
    private SpriteMaskController() { }

    // Storing a single instance in the static variable.
    private static SpriteMaskController s_instance;

    #region Fields
    // Player's position support.
    int x, y;

    [SerializeField]
    const int HeightOfTheArea = 3;
    [SerializeField]
    const int WidthOfTheArea = 3;

    Vector3 playerPosition = new Vector3();

    [SerializeField]
    private SpriteRenderer _playerSpriteRenderer;

    [SerializeField]
    private SpriteMask _spriteMask;

    private Collider2D _spriteMaskCollider;

    [SerializeField]
    private List<Tilemap> _otherNormalTilemaps = new List<Tilemap>();

    [SerializeField]
    private List<Tilemap> _otherSmallTilemaps = new List<Tilemap>();

    // Tile pos that is below the player.
    Vector3Int playerPositionRounded = new Vector3Int();

    // List for multiple positions to check.
    [SerializeField]
    private List<Vector3Int> _normalTilePositions = new List<Vector3Int>(HeightOfTheArea * WidthOfTheArea);

    [SerializeField]
    private List<Vector3Int> _smallTilePositions = new List<Vector3Int>(HeightOfTheArea * WidthOfTheArea * 2);

    public bool Checking { get; set; }

    #endregion

    // Static method, which controls the access to the class.
    public static SpriteMaskController GetInstance()
    {
        if (s_instance == null)
        {
            s_instance = new SpriteMaskController();
        }
        return s_instance;
    }

    // Awake is called once when the script object is initialised.
    private void Awake()
    {
        // Strategy controller.
        _tileStrategyController = gameObject.AddComponent<TileStrategyController>();

        _spriteMaskCollider = GetComponent<Collider2D>();
        _spriteMaskCollider.isTrigger = true;

        // Default parameters for sprite mask and player sprite.
        _spriteMask.enabled = false;
        _playerSpriteRenderer.maskInteraction = SpriteMaskInteraction.None;
    }

    private void Start()
    {
        // Fetching the player's position.
        playerPosition = gameObject.transform.position;

        // Rounding the player's coordinates (flooring).
        x = Mathf.RoundToInt(playerPosition.x - 0.5f);
        y = Mathf.RoundToInt(playerPosition.y - 0.5f);

        // Player position rounded (floored).
        playerPositionRounded = new Vector3Int(x, y, 0);
        Debug.Log("Player position rounded: " + playerPositionRounded);

        // Populating the list of tile positions. Adds the number of records equal to the number of tiles,
        // in a specified rectangular height and width.
        PopulateTilePositionsList(HeightOfTheArea * WidthOfTheArea, _normalTilePositions);
        PopulateTilePositionsList(HeightOfTheArea * 2 * WidthOfTheArea, _smallTilePositions);
    }

    // Update is called once per frame.
    void Update()
    {
        // Fetching the current player position.
        playerPosition = gameObject.transform.position;

        // Rounding the player's coordinates (flooring).
        x = Mathf.RoundToInt(playerPosition.x - 0.5f);
        y = Mathf.RoundToInt(playerPosition.y - 0.5f);

        // Player position rounded (floored).
        playerPositionRounded = new Vector3Int(x, y, 0);
        //Debug.Log("Player position rounded: " + playerPositionRounded);

        // Acquiring nine tiles' positions below and around the character.
        // Using the strategy pattern.
        _tileStrategyController.SetStrategy(new NormalTilemapStrategy());
        _tileStrategyController.GetTilePositions(_normalTilePositions, playerPositionRounded, HeightOfTheArea, WidthOfTheArea);

        _tileStrategyController.SetStrategy(new SmallTilemapStrategy());
        _tileStrategyController.GetTilePositions(_smallTilePositions, playerPositionRounded, HeightOfTheArea, WidthOfTheArea);

        if (Checking)
        {
            // Checking tiles in normal sized (1x1) tilemaps.
            CheckTilesInTilemaps(_otherNormalTilemaps, _normalTilePositions);

            // Checking tiles in small sized (1x0.5) tilemaps.
            CheckTilesInTilemaps(_otherSmallTilemaps, _smallTilePositions);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Only reacts to trigger colliders.
        if (collision.isTrigger == false) return;

        // Getting the tilemap component of the game object with the trigger collider.
        Tilemap tilemap = collision.GetComponent<Tilemap>();

        // If there is a tilemap component, then add it to the list. Depending on the cell size,
        // the tilemap is added to an apropriate list.
        if (tilemap != null)
        {
            //Debug.Log("Entered!");
            //tilemap.GetComponent<TilemapRenderer>().maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;

            if (tilemap.GetComponentInParent<Grid>().cellSize.y == 1) _otherNormalTilemaps.Add(tilemap);
            else if (tilemap.GetComponentInParent<Grid>().cellSize.y < 1) _otherSmallTilemaps.Add(tilemap);

            // Setting the boolean to true.
            Checking = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Only reacts to trigger colliders.
        if (collision.isTrigger == false) return;

        // Getting the tilemap component of the game object with the trigger collider.
        Tilemap tilemap = collision.GetComponent<Tilemap>();

        // If there is a tilemap component, then remove it from the list. Depending on the cell size,
        // the tilemap is removed from an apropriate list.
        if (tilemap != null)
        {
            //Debug.Log("Exited!");
            //tilemap.GetComponent<TilemapRenderer>().maskInteraction = SpriteMaskInteraction.None;

            if (tilemap.GetComponentInParent<Grid>().cellSize.y == 1) _otherNormalTilemaps.Remove(tilemap);
            else if (tilemap.GetComponentInParent<Grid>().cellSize.y < 1) _otherSmallTilemaps.Remove(tilemap);

            // If there are no more members in the lists, then we need to turn off the sprite mask component 
            // and set its interaction to none.
            if (_otherNormalTilemaps.Count <= 0 && _otherSmallTilemaps.Count <= 0)
            {
                // Setting the boolean to false.
                Checking = false;

                // Disabling interactio
                _spriteMask.enabled = false;
                _playerSpriteRenderer.maskInteraction = SpriteMaskInteraction.None;
            }
        }
    }

    private void PopulateTilePositionsList(int numberofPositions, List<Vector3Int> listToPopulate)
    {
        for (int i = 0; i < numberofPositions; i++)
        {
            listToPopulate.Add(new Vector3Int(0, 0, 0));
        }
    }

    private void GetTilePositions(List<Vector3Int> listToEdit, int xPosition, int yPosition)
    {
        for (int j = 0; j < HeightOfTheArea; j++)
        {
            for (int i = 0; i < WidthOfTheArea; i++)
            {
                listToEdit[i + 3 * j] = new Vector3Int(xPosition - 1 + i, yPosition - j, 0);
            }
        }
    }

    private void CheckTilesInTilemaps(List<Tilemap> tilemaps, List<Vector3Int> tilePositions)
    {
        foreach(Tilemap tilemap in tilemaps)
        {
            //Debug.Log(tilemap);

            //if (_playerSpriteRenderer.sortingLayerName == tilemap.GetComponent<TilemapRenderer>().sortingLayerName) Debug.Log("First condition is true.");

            //if (_playerSpriteRenderer.sortingOrder <= tilemap.GetComponent<TilemapRenderer>().sortingOrder) Debug.Log("Second condition is true.");

            for (int i = 0; i < tilePositions.Count;  i++)
            {
                Tile tile = tilemap.GetTile<Tile>(tilePositions[i]);

                //if (tile != null) Debug.Log("Third condition is true.");

                if (
                    _playerSpriteRenderer.sortingLayerName == tilemap.GetComponent<TilemapRenderer>().sortingLayerName
                    && _playerSpriteRenderer.sortingOrder <= tilemap.GetComponent<TilemapRenderer>().sortingOrder
                    && tile != null)
                {
                    _spriteMask.enabled = true;
                    _playerSpriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                    return;
                }
                else
                {
                    // Else disable the sprite mask
                    _spriteMask.enabled = false;
                    _playerSpriteRenderer.maskInteraction = SpriteMaskInteraction.None;
                }
            }
        }
    }
}
