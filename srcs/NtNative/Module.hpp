//
// Created by Frozen on 21/12/2019.
//

#ifndef NTCORE_MODULE_HPP
#define NTCORE_MODULE_HPP

#include <Windows.h>

class Module {
public:
    template<typename T>
    T findPattern(const char* signature, const char* mask) const
    {
        return (T) find(signature, mask);
    }
    void initialize();

private:
    DWORD _base;
    DWORD _size;
    DWORD* find(const char* signature, const char* mask) const;
    bool isMatch(const DWORD* data, const char* signature, const char* mask) const;
};


#endif //NTCORE_MODULE_HPP
