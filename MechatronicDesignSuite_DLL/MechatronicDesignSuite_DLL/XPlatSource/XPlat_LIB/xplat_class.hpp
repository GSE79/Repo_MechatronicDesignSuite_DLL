#ifndef XPLAT_CLASS_HPP
#define XPLAT_CLASS_HPP

extern "C"{
#include "xplat_base.h"
}

class XPLAT_DLL_API XPlat_Class
{

public:
    // Defualt Constructor
    XPlat_Class();
    // Buffer Links Constructor
    XPlat_Class(U8* inBuff, U8* outBuff);
    // Top Level xplatAPI Structure
    xplatAPIstruct xplatAPI;
    // xplatAPI Data Structure
    xplatAPI_DATAstruct Data;

    // Auto-Gen::XPLAT_CLASS_PackagePacket Method Prototypes

    // Auto-Gen::XPLAT_CLASS_Comm Method Prototypes

};

#endif // XPLAT_CLASS_HPP
