//
// Created by Frozen on 21/12/2019.
//

#ifndef NTCORE_CHARACTER_HPP
#define NTCORE_CHARACTER_HPP

#include <Windows.h>
#include "Module.hpp"

class Character
{
public:
    void initialize(Module module);
    void walk(short x, short y);
};

#endif //NTCORE_CHARACTER_HPP

