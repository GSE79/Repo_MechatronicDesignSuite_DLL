#include "xplat_class.hpp"

XPlat_Class::XPlat_Class()
{
    initXPlatAPIStruct(&xplatAPI, &Data, FALSE, FALSE);
}
XPlat_Class::XPlat_Class(U8* inBuff, U8* outBuff)
{
    initXPlatAPIStruct(&xplatAPI, &Data, inBuff, outBuff);
}
// Auto-Gen::XPLAT_CLASS_PackagePacket Method Definitions

// Auto-Gen::XPLAT_CLASS_Comm Method Definitions
