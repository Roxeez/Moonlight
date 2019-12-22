#include "Nostale.hpp"

Nostale* nostale;

extern "C"
{
    __declspec(dllexport) void __stdcall initialize()
    {
        nostale = new Nostale();
        nostale->initialize();
    }

    __declspec(dllexport) void __stdcall clean()
    {
        delete nostale;
    }

    __declspec(dllexport) void __stdcall setSendCallback(PacketCallback callback)
    {
        nostale->network().setCallback(PacketType::SEND, callback);
    }

    __declspec(dllexport) void __stdcall setRecvCallback(PacketCallback callback)
    {
        nostale->network().setCallback(PacketType::RECV, callback);
    }

    __declspec(dllexport) void __stdcall sendPacket(const char* packet)
    {
        nostale->network().sendPacket(packet);
    }

    __declspec(dllexport) void __stdcall recvPacket(const char* packet)
    {
        nostale->network().recvPacket(packet);
    }

    __declspec(dllexport) void __stdcall walk(short x, short y)
    {
        nostale->walk(x, y);
    }
}