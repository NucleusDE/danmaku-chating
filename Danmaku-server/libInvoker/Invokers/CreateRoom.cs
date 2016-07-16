using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using Model.Structs;
using libNetwork.Sockets;
using MemoryData;

namespace libInvoker.Invokers
{
    internal class CreateRoom
    {
        Room_mod room = new Room_mod();
        System.Net.Sockets.TcpClient client;
        MainBLL BASE;

        internal CreateRoom(DataPackage data, MainBLL BASE)
        {
            room.FromBytes(data.Data);
            client = data.Client;
            this.BASE = BASE;
        }

        internal void Response()
        {
            try
            {
                SockSender sender = new SockSender();
                if (BASE.user.User_id == "" || BASE.user.Room_id == "")
                {
                    sender.SendMessage(new DataPackage
                    {
                        Client = client,
                        Data = room.ToBytes(libData.ConstData.OP_code_CreateRoom_Failed, room.Uid)
                    });
                    return;
                }
                if (RoomControl.Rooms.ContainsKey(room.Room_name))
                {
                    sender.SendMessage(new DataPackage
                    {
                        Client = client,
                        Data = room.ToBytes(libData.ConstData.OP_code_CreateRoom_NameTaken,room.Uid)
                    });
                    return;
                }

                lock (RoomControl.Rooms)
                {
                    RoomControl.Rooms.Add(room.Room_name, new Room
                    {
                        EoomInfo = room,
                    });
                    Room r = RoomControl.Rooms[room.Room_name];
                    r.Users.Add(BASE.user.User_id, BASE.user);
                }
                ExitRoom exit = new ExitRoom();
                exit.Exit(BASE);

                sender.SendMessage(new DataPackage
                {
                    Client = client,
                    Data = room.ToBytes(libData.ConstData.OP_code_CreateRoom_Succeed, room.Uid)
                });
                return;
            }
            catch
            {
                SockSender sender = new SockSender();
                sender.SendMessage(new DataPackage
                {
                    Client = client,
                    Data = room.ToBytes(libData.ConstData.OP_code_CreateRoom_Failed, room.Uid)
                });
            }
        }
    }
}
