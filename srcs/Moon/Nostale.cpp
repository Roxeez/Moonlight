//
// Created by Frozen on 21/12/2019.
//

#include "Nostale.hpp"
#include <detours.h>

void Nostale::initialize()
{
    _module.initialize();
    _character.initialize(_module);

    Network::instance()->initialize(_module);
}

Character Nostale::character()
{
    return _character;
}
