//
// Created by Frozen on 21/12/2019.
//

#ifndef NTCORE_CHARACTER_HPP
#define NTCORE_CHARACTER_HPP

#include "Module.hpp"

class Character
{
public:
    void initialize(Module module);
    void walk(short x, short y);
private:
    DWORD _walkObject;
    DWORD _walkFunction;
};

#endif //NTCORE_CHARACTER_HPP

