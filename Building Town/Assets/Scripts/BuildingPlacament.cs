using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacament : MonoBehaviour
{
    private bool currentlyPlacing;
    private bool currentlyDeleting;

    private BuildingPresets currentlyBuildingPreset;

    private float indicatorUpdateTime = 0.05f;
    private float lastUpdateTime;
    private Vector3 curIndicatorPos;

    public GameObject placementIndicator;
    public GameObject deleteIndicator;

    public void BeginNewBuildingPlacament(BuildingPresets preset)
    {
        if (City.instance.money < preset.cost)
        {
            return;
        }
        currentlyPlacing = true;
        currentlyBuildingPreset = preset;
        placementIndicator.SetActive(true);
    }
    private void CancelBuildingPlacement()
    {
        currentlyPlacing = false;
        placementIndicator.SetActive(false);
    }
    public void ToggleDelete()
    {
        currentlyDeleting = !currentlyDeleting;
        deleteIndicator.SetActive(currentlyDeleting);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            CancelBuildingPlacement();
        if(Time.time-lastUpdateTime > indicatorUpdateTime)
        {
            lastUpdateTime = Time.time;

            curIndicatorPos = Selector.Instance.GetCurTilePosition();

            if(currentlyPlacing)
                placementIndicator.transform.position = curIndicatorPos;
            else if(currentlyDeleting)
                deleteIndicator.transform.position = curIndicatorPos;
        }
        if(Input.GetMouseButtonDown(0) && currentlyPlacing)
        {
            PlaceBuilding();
        }
        else if (Input.GetMouseButtonDown(0) && currentlyDeleting)
        {
            DeleteBuilding();
        }
    }
    void PlaceBuilding()
    {
        GameObject buildingObj = Instantiate(currentlyBuildingPreset.prefab, curIndicatorPos, Quaternion.identity);
        City.instance.OnPlaceBuilding(buildingObj.GetComponent<Building>());
        CancelBuildingPlacement();
    }
    void DeleteBuilding()
    {
        Building buildingToDestroy = City.instance.buildings.Find(x => x.transform.position == curIndicatorPos);
        if(buildingToDestroy != null)
        {
            City.instance.OnRemoveBuilding(buildingToDestroy);
        }
    }
}
