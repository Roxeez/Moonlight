#include "Nostale.hpp"
#include "NtString.hpp"

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
        Network::instance()->setCallback(PacketType::SEND, callback);
    }

    __declspec(dllexport) void __stdcall setRecvCallback(PacketCallback callback)
    {
        Network::instance()->setCallback(PacketType::RECV, callback);
    }

    __declspec(dllexport) void __stdcall sendPacket(const char* packet)
    {
        Network::instance()->sendPacket(NtString(packet).toString());
    }

    __declspec(dllexport) void __stdcall recvPacket(const char* packet)
    {
        Network::instance()->recvPacket(NtString(packet).toString());
    }

    __declspec(dllexport) void __stdcall walk(short x, short y)
    {
        nostale->character().walk(x, y);
    }
}