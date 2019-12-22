//
// Created by Frozen on 21/12/2019.
//
#include "Network.hpp"
#include <detours.h>

void Network::initialize(Module module)
{
    _recvPacketAddress = module.findPattern<DWORD>("\x55\x8B\xEC\x83\xC4\xF4\x53\x56\x57\x33\xC9\x89\x4D\xF4\x89\x55\xFC\x8B\xD8\x8B\x45\xFC\xE8\x00\x00\x00\x00\x33\xC0","xxxxxxxxxxxxxxxxxxxxxxx????xx");
    _sendPacketAddress = module.findPattern<DWORD>("\x53\x56\x8B\xF2\x8B\xD8\xEB\x04", "xxxxxxxx");
    _callerObject = module.findPattern<DWORD>("\xA1\x00\x00\x00\x00\x8B\x00\xBA\x00\x00\x00\x00\xE8\x00\x00\x00\x00\xE9\x00\x00\x00\x00\xA1\x00\x00\x00\x00\x8B\x00\x8B\x40\x40", "x????xxx????x????x????x????xxxxx");

    if (!_recvPacketAddress)
    {
        throw "Can't found recv packet function";
    }

    if (!_sendPacketAddress)
    {
        throw "Can't found recv packet function";
    }

    if (_callerObject)
    {
        _callerObject = *reinterpret_cast<DWORD*>(_callerObject + 1);
    }

    if (!_callerObject)
    {
        throw "Caller object not found";
    }
}

void Network::setCallback(PacketType packetType, PacketCallback callback)
{
    _callbacks[packetType] = callback;
}

void Network::recvPacket(const char *packet) const
{
    __asm
    {
        MOV ESI, this
        MOV EAX, [ESI]._callerObject
        MOV EAX, DWORD PTR DS : [EAX]
        MOV EAX, DWORD PTR DS : [EAX]
        MOV EAX, DWORD PTR DS : [EAX + 34h]
        MOV EDX, packet
        CALL[ESI]._recvPacketAddress
    }
}

void Network::sendPacket(const char *packet) const
{
    __asm
    {
        MOV ESI, this
        MOV EAX, [ESI]._callerObject
        MOV EAX, DWORD PTR DS : [EAX]
        MOV EAX, DWORD PTR DS : [EAX]
        MOV EDX, packet
        CALL[ESI]._sendPacketAddress
    }
}

void Network::onPacketReceived()
{
    const char* packet = nullptr;

    __asm
    {
        PUSHAD
        PUSHFD

        MOV packet, EDX
    }

    bool isAccepted = Network::instance()->executeCallback(PacketType::RECV, packet);

    __asm
    {
        POPFD
        POPAD
    }

    if (isAccepted)
    {
        Network::instance()->recvPacket(packet);
    }
}

void Network::onPacketSend()
{
    const char* packet = nullptr;

    __asm
    {
        PUSHAD
        PUSHFD

        MOV packet, EDX
    }

    bool isAccepted = Network::instance()->executeCallback(PacketType::SEND, packet);

    __asm
    {
        POPFD
        POPAD
    }

    if (isAccepted)
    {
        Network::instance()->sendPacket(packet);
    }
}

bool Network::executeCallback(PacketType packetType, const char *packet) const
{
    auto callback = _callbacks.find(packetType);

    if (callback != _callbacks.end())
    {
        if (callback->second)
        {
            return callback->second(packet);
        }
    }

    return true;
}
