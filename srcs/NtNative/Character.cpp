//
// Created by Frozen on 21/12/2019.
//

#include "Character.hpp"
#include "PatternScanner.hpp"

DWORD _walkObject;
DWORD _walkFunction;

void Character::initialize()
{
    _walkObject = *(DWORD*)(FindPattern((uint8_t*)0x401000, 0x2d1000, (uint8_t*)"\x33\xC9\x8B\x55\xFC\xA1\x00\x00\x00\x00\xE8\x00\x00\x00\x00", "xxxxxx????x????") + 0x6);
    _walkFunction = (DWORD)FindPattern((uint8_t*)0x401000, 0x2d1000, (uint8_t*)"\x55\x8B\xEC\x83\xC4\xEC\x53\x56\x57\x66\x89\x4D\xFA", "xxxxxxxxxxxxx");
}

void Character::walk(short x, short y)
{
    DWORD position = (y << 16) | x;
    __asm
    {
        push 1
        xor ecx, ecx
        mov edx, position
        mov eax, dword ptr ds : [_walkObject]
        mov eax, dword ptr ds : [eax]
        call _walkFunction
    }
}