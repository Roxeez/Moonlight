//
// Created by Frozen on 21/12/2019.
//

#include "Nostale.hpp"
#include <detours.h>

void Nostale::initialize()
{
    _module.initialize();
    _network.initialize(_module);

    _walkObject = _module.findPattern<DWORD>("\x33\xC9\x8B\x55\xFC\xA1\x00\x00\x00\x00\xE8\x00\x00\x00\x00", "xxxxxx????x????") + 0x6;
    _walkFunction = _module.findPattern<DWORD>("\x55\x8B\xEC\x83\xC4\xEC\x53\x56\x57\x66\x89\x4D\xFA", "xxxxxxxxxxxxx");
}

Network Nostale::network() 
{
    return _network;
}

void Nostale::walk(short x, short y)
{
    DWORD position = (y << 16) | x;
    __asm
    {
        PUSH 1
        XOR ECX, ECX
        MOV EDX, position
        MOV EAX, DWORD PTR DS : [this._walkObject]
        MOV EAX, DWORD PTR DS : [EAX]
        CALL this._walkFunction
    }
}
