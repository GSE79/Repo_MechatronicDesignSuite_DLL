#include "xplat_base.h"

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
