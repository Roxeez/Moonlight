//
// Created by Frozen on 21/12/2019.
//

#include "Character.hpp"

void Character::initialize(Module module)
{
    _walkObject = module.findPattern<unsigned int>("\x33\xC9\x8B\x55\xFC\xA1\x00\x00\x00\x00\xE8\x00\x00\x00\x00", "xxxxxx????x????") + 0x6;
    _walkFunction = module.findPattern<unsigned int>("\x55\x8B\xEC\x83\xC4\xEC\x53\x56\x57\x66\x89\x4D\xFA", "xxxxxxxxxxxxx");
}

void Character::walk(short x, short y)
{
    DWORD position = (y << 16) | x;
    __asm
    {
        push 1
        xor ecx, ecx
        mov edx, position
        mov eax, dword ptr ds : [this._walkObject]
        mov eax, dword ptr ds : [eax]
        call this._walkFunction
    }
}