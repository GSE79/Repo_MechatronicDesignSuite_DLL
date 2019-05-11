#-------------------------------------------------
#
# Project created by QtCreator 2019-05-08T10:38:54
#
#-------------------------------------------------

QT -=       core gui

TARGET =    XPlat_LIB
TEMPLATE =  lib

DEFINES +=  XPLAT_DLL_LIBRARY \
            DONTUSE_STDINT

SOURCES +=  \
    xplat_base.c \
    xplat_class.cpp

HEADERS +=  \
    xplat_base.h \
    xplat_class.hpp

unix {
    target.path = /usr/lib
    INSTALLS += target
}
