using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Photon.Realtime;

public class RoomListingMenu :  MonoBehaviourPunCallbacks, IMatchmakingCallbacks

{
    [SerializeField]
    private Transform _content;

    [SerializeField]
    private RoomListing _roomListing;

    private LoadBalancingClient loadBalancingClient;


    private List<RoomListing> _listings = new List<RoomListing>();


    public static RoomListingMenu instance;

    public static RoomListingMenu Instance
    {
        get
        {
            
            return instance;
        }
    }



public  override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
          Debug.Log(" updatelist"+roomList.Count);
        foreach (var info in roomList)
        {

            //if (info.RemovedFromList)
            //{
            //    int index = _listings.FindIndex(x => x.RoomInfo.Name == info.Name);
            //    if (index != -1)
            //    {
            //        Destroy(_listings[index].gameObject);
            //        _listings.RemoveAt(index);
            //    }
            //}
            //else
            //{

            //int index = _listings.FindIndex(x => x.RoomInfo.Name == info.Name);

            //if (index == -1)
            //{

            //    RoomListing listing = Instantiate(_roomListing, _content);
            //    if (listing != null)
            //    {
            //        listing.SetRoomInfo(info);
            //        _listings.Add(listing);
            //    }
            //}
           
                RoomListing listing = Instantiate(_roomListing, _content);
                if (listing != null)
                {
                    listing.SetRoomInfo(info);
                    _listings.Add(listing);
                
               
                
                }
           // }

        }
        
    }


    private void Start()
    {
        //Debug.Log("radi");
        instance = this;
    }

    void Update()
    {

        //OnRoomListUpdate(new List<RoomInfo>());
        
        
    }

   

    public List<RoomListing> GetRoomListings()
    {
        return _listings;
    }
}
