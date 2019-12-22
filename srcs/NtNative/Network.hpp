//
// Created by Frozen on 21/12/2019.
//

#ifndef NTCORE_NETWORK_HPP
#define NTCORE_NETWORK_HPP

#include <unordered_map>

#include "Module.hpp"
#include "PacketType.hpp"
#include "Types.hpp"

class Network {
public:
    void initialize(Module module);
    void setCallback(PacketType packetType, PacketCallback callback);
    void recvPacket(const char* packet) const;
    void sendPacket(const char* packet) const;

private:
    DWORD _recvPacketAddress;
    DWORD _sendPacketAddress;
    DWORD _callerObject;

    static Network* instance()
    {
        static Network instance;
        return reinterpret_cast<Network*>(&instance);
    }

    std::unordered_map<PacketType, PacketCallback> _callbacks;

    static void onPacketReceived();
    static void onPacketSend();

    bool executeCallback(PacketType packetType, const char* packet) const;
};


#endif //NTCORE_NETWORK_HPP
