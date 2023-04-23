using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
class MapInfo 
{
    public GameObject Map;
    public bool busy = false;
}
public class map : MonoBehaviour
{
    [SerializeField] private MapInfo[] MapPart;
    [SerializeField] private Vector3 NextPosSetting;
    private Vector3 MapPos;
    private MapInfo MapOld;
    private void Start()
    {
        foreach (var item in MapPart)
        {
            item.Map.SetActive(false);
            item.busy = false;
        }
        InvokeRepeating("Crate_Map", 2f, 2f);
    }
    private void Crate_Map ()
    {
        while (true)
        {
            int random_number = Random.Range(0, MapPart.Length);
            if (MapPart[random_number].busy == false) 
            {
                MapPart[random_number].Map.SetActive(true);
                MapPart[random_number].Map.transform.position = MapPos + NextPosSetting;
                MapPos = MapPart[random_number].Map.transform.position;
                MapPart[random_number].busy = true;
                if(MapOld != null) 
                {
                    MapOld.busy = false;
                    MapOld.Map.SetActive(false);
                }
                MapOld = MapPart[random_number];
                break;
            }
        }
    }
}
