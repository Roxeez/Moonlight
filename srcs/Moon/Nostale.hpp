//
// Created by Frozen on 21/12/2019.
//

#ifndef NTCORE_NOSTALE_HPP
#define NTCORE_NOSTALE_HPP

#include "Module.hpp"
#include "Network.hpp"
#include "Character.hpp"

class Nostale {
public:
    Character character();
    void initialize();
private:
    Module _module;
    Character _character;
};

#endif //NTCORE_NOSTALE_HPP
