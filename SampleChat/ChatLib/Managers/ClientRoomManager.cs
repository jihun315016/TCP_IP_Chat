using ChatLib.Handler;
using ChatLib.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatLib.Managers
{
    public class ClientRoomManager
    {
        private Dictionary<int, List<ClientHandler>> _roomHandlersDict = new();

        public void Add(ClientHandler clientHandler)
        {
            int roomId = clientHandler.InitialData.RoomId;

            if (_roomHandlersDict.ContainsKey(roomId))
                _roomHandlersDict[roomId].Add(clientHandler);
            else
                _roomHandlersDict[roomId] = new List<ClientHandler>() { clientHandler };
        }

        public void Remove(ClientHandler clientHandler)
        {
            int roomId = clientHandler.InitialData!.RoomId;

            // 종료된 사용자 하나만 제외하고 나머지는 다시 대입
            // -> 종료된 사용자 삭제
            if (_roomHandlersDict.ContainsKey(roomId))
                _roomHandlersDict[roomId] = _roomHandlersDict[roomId].FindAll(handler => !handler.Equals(clientHandler));
        }

        public void SendToMyRoom(ChatInfo chatInfo)
        {
            // RoomId가 chatInfo와 같은 방에 대해 메시지 전송(Send)
            if (_roomHandlersDict.TryGetValue(chatInfo.RoomId, out List<ClientHandler>? roomHandler))
            {
                roomHandler.ForEach(handler =>
                {
                    handler.Send(chatInfo);
                });
            }
        }
    }
}
