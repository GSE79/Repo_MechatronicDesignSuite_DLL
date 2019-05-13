#ifndef XPLAT_BASE_H
#define XPLAT_BASE_H

/* ////////////////////////////////////////////////////////
 * Compiler Constant and Macro Definitions
 */ ///////////////////////////////////////////////////////
#define TRUE					1u
#define FALSE					0u
#define NULL					0u
#define PckHDRSize				4
#define SysBigEndian			TRUE
#define HDRPCKOFFSETINDEX		3
#ifdef XPLAT_DLL_LIBRARY
	#define XPLAT_DLL_API		__declspec(dllexport)
#else
	#ifndef XPLAT_NO_DLL
		#define XPLAT_DLL_API	__declspec(dllimport)
	#else
		#define XPLAT_DLL_API
	#endif
#endif
/* ////////////////////////////////////////////////////////
 * Global Type Definitions
 */ ///////////////////////////////////////////////////////
#ifndef DONTUSE_STDINT
    #include <stdint.h>
#else
    typedef unsigned char       uint8_t;
    typedef char                int8_t;
    #ifdef INT16BITS
        typedef unsigned int    uint16_t;
        typedef int             int16_t;
        typedef unsigned long   uint32_t;
        typedef long            int32_t;
    #else
        typedef unsigned short  uint16_t;
        typedef short           int16_t;
        typedef unsigned int    uint32_t;
        typedef int             int32_t;
        typedef unsigned long   uint64_t;
        typedef long            int64_t;
    #endif
#endif

// Standard size integer types
typedef uint8_t             U8;
typedef int8_t              I8;
typedef uint16_t            U16;
typedef int16_t             I16;
typedef uint32_t            U32;
typedef int32_t             I32;

#ifndef INT16BITS
typedef uint64_t            U64;
typedef int64_t             I64;
#endif

// Auto-Gen::XPLAT_DLL_API_Packets Structures

// xplatAPI.Data structure
typedef struct
{
    // Pointers to input/output buffers
    U8*     inputBuffer;
	U8*		inputPacket;
    U8*     outputBuffer;
	U8*		outputPacket;

    // Buffer Sizes and Counts
    U32     inputBufferSize;
    U32     inputBufferCount;
    U32     outputBufferSize;
    U32     outputBufferCount;

	// Temp Pointers
	U8*		outPackBuffPtr;
	U8*		inPackBuffPtr;

	// Auto-Gen::XPLAT_DLL_API_Comm Packet Structure Pointers

}XPLAT_DLL_API xplatAPI_DATAstruct;

// Top Level xplatAPI Structure
typedef struct
{
    // Data structure Pointer
    xplatAPI_DATAstruct*    Data;

    // Function Pointers
    void                    (*ProcessRxPacket)();
    void                    (*PrepareTxPacket)();

	// Temp Indecies
	U32		outIndex;
	U32		inIndex;


}XPLAT_DLL_API xplatAPIstruct;





/* ////////////////////////////////////////////////////////
* Packaging Macros, links to Packaging Functions...
*/ ///////////////////////////////////////////////////////
#define package8BIG(outPackPtr, Value)			packageBytesBIG((U8*)outPackPtr, (U8*)(&Value), 1)
#define package16BIG(outPackPtr, Value)			packageBytesBIG((U8*)outPackPtr, (U8*)(&Value), 2)
#define package32BIG(outPackPtr, Value)			packageBytesBIG((U8*)outPackPtr, (U8*)(&Value), 4)
#define package64BIG(outPackPtr, Value)			packageBytesBIG((U8*)outPackPtr, (U8*)(&Value), 8)
#define package8little(outPackPtr, Value)		packageByteslittle((U8*)outPackPtr, (U8*)(&Value), 1)
#define package16little(outPackPtr, Value)		packageByteslittle((U8*)outPackPtr, (U8*)(&Value), 2)
#define package32little(outPackPtr, Value)		packageByteslittle((U8*)outPackPtr, (U8*)(&Value), 4)
#define package64little(outPackPtr, Value)		packageByteslittle((U8*)outPackPtr, (U8*)(&Value), 8)
#define unpack8BIG(inPackPtr, Value)			unpackBytesBIG((U8*)inPackPtr, (U8*)(&Value), 1)
#define unpack16BIG(inPackPtr, Value)			unpackBytesBIG((U8*)inPackPtr, (U8*)(&Value), 2)
#define unpack32BIG(inPackPtr, Value)			unpackBytesBIG((U8*)inPackPtr, (U8*)(&Value), 4)
#define unpack64BIG(inPackPtr, Value)			unpackBytesBIG((U8*)inPackPtr, (U8*)(&Value), 8)
#define unpack8little(inPackPtr, Value)			unpackByteslittle((U8*)inPackPtr, (U8*)(&Value), 1)
#define unpack16little(inPackPtr, Value)		unpackByteslittle((U8*)inPackPtr, (U8*)(&Value), 2)
#define unpack32little(inPackPtr, Value)		unpackByteslittle((U8*)inPackPtr, (U8*)(&Value), 4)
#define unpack64little(inPackPtr, Value)		unpackByteslittle((U8*)inPackPtr, (U8*)(&Value), 8)

/* ////////////////////////////////////////////////////////
 * Global Static Functions
 */ ///////////////////////////////////////////////////////

// API Initialization Function
XPLAT_DLL_API void initXPlatAPIStruct(		xplatAPIstruct*         xplatAPI,           // Pointer to XPlat API structure to be initialized
											xplatAPI_DATAstruct*    Data,               // Pointer to XPlat API Data structure to link
											void                    (*ProcessRxPacket), // Function Pointer to Process Data Received
											void                    (*PrepareTxPacket));// Function Pointer to Package Data to Send
// Data Initialization Function
XPLAT_DLL_API void initXplatAPIDataStruct(	xplatAPI_DATAstruct*    Data,               // Pointer to Data Structure to be initialized
											U8*                     inBuff,             // Pointer to Input Data Buffer
											U8*                     outBuff);           // Pointer to Ooutput Data Buffer
// Package to BIG Endian
XPLAT_DLL_API void packageBytesBIG(			U8*						outBuff,			// Pointer to Output Data Buffer
											U8*						ValueAddr,			// Pointer to Values bits
											U8						numBytes);			// Number of bytes to package

// Package to little Endian
XPLAT_DLL_API void packageByteslittle(		U8*						outBuff,			// Pointer to Output Data Buffer
											U8*						ValueAddr,			// Pointer to Values bits
											U8						numBytes);			// Number of bytes to package	

// Un-Pack from BIG Endian
XPLAT_DLL_API void unpackBytesBIG(			U8*						inBuff,				// Pointer to input Data Buffer
											U8*						ValueAddr,			// Pointer to Values bits
											U8						numBytes);			// Number of bytes to package

// Un-Pack from little Endian
XPLAT_DLL_API void unpackByteslittle(		U8*						inBuff,				// Pointer to Input Data Buffer
											U8*						ValueAddr,			// Pointer to Values bits
											U8						numBytes);			// Number of bytes to package	

// Auto-Gen::XPLAT_DLL_API_PackagePacket Function Prototypes

// Auto-Gen::XPLAT_DLL_API_Comm Function Prototypes

#endif // XPLAT_BASE_H
