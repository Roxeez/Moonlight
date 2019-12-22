//
// Created by Frozen on 21/12/2019.
//

#ifndef NTCORE_NETWORK_HPP
#define NTCORE_NETWORK_HPP

#include "Module.hpp"
#include "PacketType.hpp"
#include "Types.hpp"

class Network {
    Network(Network const&) = delete;
    Network(Network&&) = delete;
    Network operator=(Network const&) = delete;
    Network operator=(Network&&) = delete;

public:
    void initialize(Module module);
    void setCallback(PacketType packetType, PacketCallback callback);
    void recvPacket(const char* packet) const;
    void sendPacket(const char* packet) const;

    static Network* instance()
    {
        static Network instance;
        return reinterpret_cast<Network*>(&instance);
    }

private:
    unsigned int _recvPacketAddress;
    unsigned int _sendPacketAddress;
    unsigned int _callerObject;

    PacketCallback _recvCallback;
    PacketCallback _sendCallback;

    static void onPacketReceived();
    static void onPacketSend();

    bool executeCallback(PacketType packetType, const char* packet) const;

protected:
    Network() = default;
};


#endif //NTCORE_NETWORK_HPP
