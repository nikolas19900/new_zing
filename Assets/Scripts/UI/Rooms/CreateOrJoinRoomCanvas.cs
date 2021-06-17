using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateOrJoinRoomCanvas : MonoBehaviour
{
    //[SerializeField]
    //private CreateRoom _createRoom;

    [SerializeField]
    private GameScript _gameScript;

    private RoomsCanvases _roomsCanvases;

    public void FirstInitialize(RoomsCanvases roomsCanvases)
    {
        _roomsCanvases = roomsCanvases;
        //_createRoom.FirstInitialize(_roomsCanvases);
    }



    public void Show()
    {

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
