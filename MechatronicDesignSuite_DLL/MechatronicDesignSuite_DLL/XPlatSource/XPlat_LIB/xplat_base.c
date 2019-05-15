/* /////////////////////////////////////////////////////////
*	InMechaSol non-profit license for (re)use.
*	- This source code is made available as a reference
*	- This source code does not guarantee an applications
*		performance nor provide any warranties what so ever
*	- This source code can be (re)used as long as this
*		license text remains in place at the top of the file
*/ /////////////////////////////////////////////////////////
#include "xplat_base.h"
/* /////////////////////////////////////////////////////////
*	xplat_base.c
*	- Source file to provide cross platform framework for
*		packet comm system interoperability
*	- xplat features depend on "#define's" for configuration
*		XPLAT_DLL_LIBRARY: for use in dll export if defined
*		XPLAT_NO_DLL: to fully exclude all dll import/export
*		DONTUSE_STDINT: to use xplat types, not stdint.h
*		INT16BITS: to build for systems with 16 bit integers
*
*/ /////////////////////////////////////////////////////////


//////////////////////////////////////////////////////////////////////
//
// API Structure Initialization Function
//
// Used to link Data and Function pointers of xplatAPI structure
//////////////////////////////////////////////////////////////////////
void initXPlatAPIStruct(xplatAPIstruct*           xplatAPI,				// Pointer to XPlat API structure to be initialized
						xplatAPI_DATAstruct*      Data,					// Pointer to XPlat API Data structure to link
						void                      (*ProcessRxPacket),	// Function Pointer to Process Data Received
						void                      (*PrepareTxPacket))	// Function Pointer to Package Data to Send
{
    // Set Data Pointer
    (*xplatAPI).Data = Data;
    // Set Rx Function Pointer
    (*xplatAPI).ProcessRxPacket = ProcessRxPacket;
    // Set Tx Function Pointer
    (*xplatAPI).PrepareTxPacket = PrepareTxPacket;
	
}

//////////////////////////////////////////////////////////////////////
//
// API Data Structure Initialization Function
//
// Used to link Input/Output buffer pointers of xplatAPI.Data structure
// *Note - The input and output buffer pointers can be identical*
//////////////////////////////////////////////////////////////////////
void initXplatAPIDataStruct(xplatAPI_DATAstruct*    Data,			// Pointer to Data Structure to be initialized
                            U8*                     inBuff,			// Pointer to Input Data Buffer
                            U8*                     outBuff)		// Pointer to Ooutput Data Buffer
{
    // Set Rx (in Buff) Pointer
    (*Data).inputBuffer = inBuff;
    // Set Tx (out Buff) Pointer
    (*Data).outputBuffer = outBuff;
	// Set Rx (in Buff) Packet Pointer
	(*Data).inputPacket = inBuff + PckHDRSize;
	// Set Tx (out Buff) Packet Pointer
	(*Data).outputPacket = outBuff + PckHDRSize;
}

// Package to BIG Endian
void packageBytesBIG(		U8*						outBuff,        // Pointer to Output Data Buffer
							U8*						ValueAddr,      // Pointer to Values bits
							U8						numBytes)		// Number of bytes to package
{	
	// Package Big end first
	int i;
	for(i=0;i<numBytes;i++)
		(*(outBuff++)) = (*(ValueAddr+numBytes-i));		// Pack Addr[0] <- Value Addr[0] + numBytes

}
// Package to little Endian
void packageByteslittle(	U8*						outBuff,        // Pointer to Output Data Buffer
							U8*						ValueAddr,      // Pointer to Values bits
							U8						numBytes)		// Number of bytes to package
{
	// Package little end first
	int i;
	for (i = 0; i<numBytes; i++)
		(*(outBuff++)) = (*(ValueAddr + i));		// Pack Addr[0] <- Value Addr[0] 
}
// Un-Pack from BIG Endian
void unpackBytesBIG(		U8*						inBuff,				// Pointer to input Data Buffer
							U8*						ValueAddr,			// Pointer to Values bits
							U8						numBytes)			// Number of bytes to package
{
	// Un-Pack Big end first
	int i;
	for (i = 0; i<numBytes; i++)
		(*(ValueAddr + numBytes - i)) = (*(inBuff++));		// unPack Addr[0] -> Value Addr[0] + numBytes
}
// Un-Pack from little Endian
void unpackByteslittle(		U8*						inBuff,				// Pointer to Input Data Buffer
							U8*						ValueAddr,			// Pointer to Values bits
							U8						numBytes)			// Number of bytes to package	
{
	// Package little end first
	int i;
	for (i = 0; i<numBytes; i++)
		(*(ValueAddr + i)) = (*(inBuff++));		// unPack Addr[0] -> Value Addr[0] 
}

// Auto-Gen::XPLAT_DLL_API_PackagePacket Function Definitions

// Auto-Gen::XPLAT_DLL_API_Comm Function Definitions
