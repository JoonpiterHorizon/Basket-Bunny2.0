using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FireManager : MonoBehaviour
{
    [SerializeField]
    private Tilemap map;


    [SerializeField]
    private MapManager mapManager;

    [SerializeField]
    private Fire firePrefab;

    private TileData data;

    private List<Vector3Int> activeFire = new List<Vector3Int>();
    private List<Vector3Int> activeIndicator = new List<Vector3Int>();

    [SerializeField]
    private Indicator indicatorPrefab;

    public void FinishedBurning(Vector3Int position)
    {
        activeFire.Remove(position);
    }

    public void FinishedIndicator(Vector3Int position)
    {
        activeIndicator.Remove(position);
    }

    public void TryToSpread(Vector3Int position, float spreadChance)
    {
        for(int i = 0; i < 10; i++)
        {
            int x = UnityEngine.Random.Range(-2, 6);
            int y = UnityEngine.Random.Range(-3, 2);
            TryToBurnTile(new Vector3Int(x, y, 0));
        }


        void TryToBurnTile(Vector3Int tilePosition)
        {
            if(activeFire.Contains(tilePosition))
            {
                return;
            }

            TileData data = mapManager.GetTileData(tilePosition);

            if(data != null && data.canBurn)
            {
                if(UnityEngine.Random.Range(0f, 100f) <= data.spreadChance){
                    if(activeIndicator.Count <= data.numberOfFire)
                    {
                        showIndicator(tilePosition, data);
                    }
                                          
                }
            }
        }
    }


    public void SetTileOnFire(Vector3Int tilePosition, TileData data)
    {
        Fire newFire = Instantiate(firePrefab);
        newFire.transform.position = map.GetCellCenterWorld(tilePosition);
        newFire.startBurning(tilePosition, data, this);

        activeFire.Add(tilePosition);
    }

    private void showIndicator(Vector3Int tilePosition, TileData data)
    {
        Indicator newIndicator = Instantiate(indicatorPrefab);
        newIndicator.transform.position = map.GetCellCenterWorld(tilePosition);
        newIndicator.startBurning(tilePosition, data, this);

        activeIndicator.Add(tilePosition);
    }

    private void Start()
    {
        Vector3Int gridPosition = new Vector3Int(0, 0, 0);

        TileData data = mapManager.GetTileData(gridPosition);

        showIndicator(gridPosition, data);
    }

    private void Update()
    {
        if(activeIndicator.Count <= 0)
        {

            int x = UnityEngine.Random.Range(-2, 6);
            int y = UnityEngine.Random.Range(-3, 2);
            Vector3Int gridPosition = new Vector3Int(x, y, 0);

            TileData data = mapManager.GetTileData(gridPosition);

            showIndicator(gridPosition, data);
        }
    }

}
