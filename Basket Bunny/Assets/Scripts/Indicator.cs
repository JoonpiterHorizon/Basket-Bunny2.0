using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    private Vector3Int position;
    private TileData data;

    private float indicatorIntervalCounter;
    private FireManager fireManager;

    public void startBurning(Vector3Int position, TileData data, FireManager fm)
    {
        this.position = position;
        this.data = data;
        fireManager = fm;

        indicatorIntervalCounter = data.indicatorInterval;
    }

    private void Update()
    {
        indicatorIntervalCounter -= Time.deltaTime;

        if (indicatorIntervalCounter <= 0)
        {
            fireManager.FinishedIndicator(position);



            fireManager.SetTileOnFire(position, data);
            Destroy(gameObject);
        }


    }
}
