//Thanks to this: http://www.unknowncheats.me/forum/1064672-post23.html

#include <stdint.h>

bool DataCompare(uint8_t* data, uint8_t* signature, const char* mask)
{
    for (; *mask; ++mask, ++data, ++signature)
    {
        if (*mask == 'x' && *data != *signature)
            return false;
    }
    return (*mask) == NULL;
}

uint8_t* FindPattern(uint8_t* address, uint32_t size, uint8_t* signature, const char* mask)
{
    for (uint32_t i = NULL; i < size; i++)
    {
        if (DataCompare(address + i, signature, mask))
            return address + i;
    }
    return nullptr;
}