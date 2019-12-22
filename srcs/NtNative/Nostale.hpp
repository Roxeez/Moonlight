//
// Created by Frozen on 21/12/2019.
//

#ifndef NTCORE_NOSTALE_HPP
#define NTCORE_NOSTALE_HPP

#include "Module.hpp"
#include "Network.hpp"

class Nostale {
public:
    Network network();
    void initialize();
    void walk(short x, short y);
private:
    Module _module;
    Network _network;

    DWORD _walkObject;
    DWORD _walkFunction;

    static void onNetworkPacketReceived();
    static void onNetworkPacketSend();
};

#endif //NTCORE_NOSTALE_HPP
