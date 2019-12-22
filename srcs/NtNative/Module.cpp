//
// Created by Frozen on 21/12/2019.
//


#include "Module.hpp"
#include <Psapi.h>

DWORD* Module::find(const char *signature, const char *mask) const
{
    for(size_t i = 0; i < _size; i++)
    {
        DWORD* data = &reinterpret_cast<DWORD*>(_base)[i];
        if (Module::isMatch(data, signature, mask))
        {
            return data;
        }
    }

    return nullptr;
}

bool Module::isMatch(const DWORD* data, const char* signature, const char* mask) const
{
    for(size_t i = 0; i < strlen(mask); i++)
    {
        if (mask[i] == 'x' && data[i] != static_cast<DWORD>(signature[i]))
        {
            return false;
        }
    }

    return true;
}

void Module::initialize()
{
    HMODULE moduleHandle = GetModuleHandleA(nullptr);

    if (!moduleHandle)
    {
        throw "Can't find module handle";
    }

    MODULEINFO moduleInfo = {};
    if (!GetModuleInformation(GetCurrentProcess(), moduleHandle, &moduleInfo, sizeof(moduleInfo)))
    {
        throw "Can't get module handle information";
    }

    _base = reinterpret_cast<DWORD>(moduleInfo.lpBaseOfDll);
    _size = moduleInfo.SizeOfImage;
}
