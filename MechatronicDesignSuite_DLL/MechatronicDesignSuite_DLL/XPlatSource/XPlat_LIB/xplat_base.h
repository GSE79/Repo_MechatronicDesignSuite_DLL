/* /////////////////////////////////////////////////////////
*	InMechaSol non-profit license for (re)use.
*	- This source code is made available as a reference
*	- InMechaSol does not guarantee an applications
*		performance nor provide any warranties what so ever
*	- This source code can be (re)used as long as this
*		license text remains in place at the top of the file
*/ /////////////////////////////////////////////////////////
#ifndef XPLAT_BASE_H
#define XPLAT_BASE_H
/* /////////////////////////////////////////////////////////
*	xplat_base.h 
*	- Header file to be used in 1 of 2 ways
*		1) As a source header file
*		2) As a library reference header file
#	- xplat features depend on "#define's" for configuration
*		XPLAT_DLL_LIBRARY: for use in dll export if defined
*		XPLAT_NO_DLL: to fully exclude all dll import/export
*		DONTUSE_STDINT: to use xplat types, not stdint.h
*		INT16BITS: to build for systems with 16 bit integers
*		!There are more...The list above is not exhuastive!
*/ /////////////////////////////////////////////////////////


/* ////////////////////////////////////////////////////////
 * Compiler Constant and Macro Definitions
 */ ///////////////////////////////////////////////////////
#define TRUE					            1u
#define FALSE					            0u
#define PckHDRSize				            4       // Size in Bytes of Packet Header
#define MessageTypeByteIndex                2
#define HDRPCKOFFSETINDEX		            3       // Index in Bytes of the "Packet Offset" within the Packet Header
#define SysBigEndian			            TRUE    // true for big endian
// DLL Import/Export Compiler Settings
#ifdef XPLAT_DLL_LIBRARY
	#define XPLAT_DLL_API		            __declspec(dllexport)
#else
	#ifndef XPLAT_NO_DLL
		#define XPLAT_DLL_API	            __declspec(dllimport)
	#else
		#define XPLAT_DLL_API
	#endif
#endif
// Bit Constants for Modules Entry Points
#define EXEFlags_mainInit                           0x01
#define EXEFlags_mainLoop                           0x02
// Naming Convention for Modules Entry Points
#define MainInit(ModuleName)                        mainInit_##ModuleName
#define MainLoop(ModuleName)                        mainLoop_##ModuleName
// Function Prototypes for Modules Entry Points
#define MainInitFunc(ModuleName)                    void MainInit(ModuleName) (xplatModuleDataStruct*)
#define MainLoopFunc(ModuleName)                    void MainLoop(ModuleName) (xplatModuleDataStruct*)
#define ModuleFunctionProtos(ModuleName)            \
                                                    MainInitFunc(ModuleName);\
                                                    MainLoopFunc(ModuleName)
// Function Definitions for Modules Entry Points
#define MainInitFuncDef(ModuleName)                 void MainInit(ModuleName) (xplatModuleDataStruct* moduleData)
#define MainLoopFuncDef(ModuleName)                 void MainLoop(ModuleName) (xplatModuleDataStruct* moduleData)
// Static Instantiation of Module Structure
#define ModuleInstanceConstant(ModuleName)          {0, MainInit(ModuleName), MainLoop(ModuleName)}

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

typedef struct
{
   // Auto-Gen::XPLAT_DLL_API_Comm Packet Structures
}XPLAT_DLL_API BaseSMStruct;

// xplatAPI.Data structure
typedef struct
{
    // Pointers to input/output buffers
    U8*                 inputBuffer;
	U8*		            inputPacket;
    U8*                 outputBuffer;
	U8*		            outputPacket;

    // Buffer Sizes and Counts
    U32                 inputBufferSize;
    U32                 inputBufferCount;
    U32                 outputBufferSize;
    U32                 outputBufferCount;

	// Temp Pointers
	U8*		            outPackBuffPtr;
	U8*		            inPackBuffPtr;

	// Temp Indecies
	U32		            outIndex;
	U32		            inIndex;

    BaseSMStruct*       SMPtr;
	

}XPLAT_DLL_API xplatAPI_DATAstruct;

// Top Level xplatAPI Structure
typedef struct
{
    // Data structure Pointer
    xplatAPI_DATAstruct*    Data;

    // Function Pointers
    void                    (*ProcessRxPacket)(xplatAPI_DATAstruct*);
    void                    (*PrepareTxPacket)(xplatAPI_DATAstruct*);

}XPLAT_DLL_API xplatAPIstruct;

// Module Data Structure
typedef struct
{
    // Module Execution Flags
    U8      ExeFlags;                   // Flags to inhibit execution of particular entry points
    U8      RunOnceFlags;               // Flags aligned with ExeFlags used to enforce run once policy
    U8      ErrorFlags;                 // Flags aligned with ExeFlags used to indicate error during entry point execution
}XPLAT_DLL_API xplatModuleDataStruct;

// Module Structure
typedef struct
{
    // Pointer to Instance Data
    xplatModuleDataStruct*  Data;
    
    // Module Entry Point Functions (pointers)
    void                    (*mainInit)(xplatModuleDataStruct*);
    void                    (*mainLoop)(xplatModuleDataStruct*);
    
}XPLAT_DLL_API xplatModulestruct;

// Execution System Data Structure
typedef struct
{
    // Pre/Post Entry Point Function Pointers
    void                    (*mainInit_preTasks)();
    void                    (*mainInit_postTasks)();
    void                    (*mainLoop_preTasks)();
    void                    (*mainLoop_postTasks)();
    
    // Module Execution Array Pointer
    xplatModulestruct*      exeModuleArrayPtr;
    xplatModuleDataStruct*  DataArrayPtr;
    
    // Indicies
    U8                      indexMainInit;
    U8                      indexMainLoop;
    // Module Count
    U8                      moduleCount;
    // Option Flags
    U8                      optionFlags;
    // runOnce Flags
    U8                      runOnceFlags;
    
    
}XPLAT_DLL_API xplatExeSysstruct;


/* ////////////////////////////////////////////////////////
* Packaging Macros, links to Packaging Functions...
*/ ///////////////////////////////////////////////////////
#define package8BIG(outPackPtr, Value)			packageBytesBIG(    (U8**)(&outPackPtr), (U8*)(&Value), 1)
#define package16BIG(outPackPtr, Value)			packageBytesBIG(    (U8**)(&outPackPtr), (U8*)(&Value), 2)
#define package32BIG(outPackPtr, Value)			packageBytesBIG(    (U8**)(&outPackPtr), (U8*)(&Value), 4)
#define package64BIG(outPackPtr, Value)			packageBytesBIG(    (U8**)(&outPackPtr), (U8*)(&Value), 8)
#define package8little(outPackPtr, Value)		packageByteslittle( (U8**)(&outPackPtr), (U8*)(&Value), 1)
#define package16little(outPackPtr, Value)		packageByteslittle( (U8**)(&outPackPtr), (U8*)(&Value), 2)
#define package32little(outPackPtr, Value)		packageByteslittle( (U8**)(&outPackPtr), (U8*)(&Value), 4)
#define package64little(outPackPtr, Value)		packageByteslittle( (U8**)(&outPackPtr), (U8*)(&Value), 8)
#define unpack8BIG(inPackPtr, Value)			unpackBytesBIG(     (U8**)(&inPackPtr), (U8*)(&Value), 1)
#define unpack16BIG(inPackPtr, Value)			unpackBytesBIG(     (U8**)(&inPackPtr), (U8*)(&Value), 2)
#define unpack32BIG(inPackPtr, Value)			unpackBytesBIG(     (U8**)(&inPackPtr), (U8*)(&Value), 4)
#define unpack64BIG(inPackPtr, Value)			unpackBytesBIG(     (U8**)(&inPackPtr), (U8*)(&Value), 8)
#define unpack8little(inPackPtr, Value)			unpackByteslittle(  (U8**)(&inPackPtr), (U8*)(&Value), 1)
#define unpack16little(inPackPtr, Value)		unpackByteslittle(  (U8**)(&inPackPtr), (U8*)(&Value), 2)
#define unpack32little(inPackPtr, Value)		unpackByteslittle(  (U8**)(&inPackPtr), (U8*)(&Value), 4)
#define unpack64little(inPackPtr, Value)		unpackByteslittle(  (U8**)(&inPackPtr), (U8*)(&Value), 8)

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
											U8*                     outBuff,            // Pointer to Output Data Buffer
                                            BaseSMStruct*           SMPtrIn);           // Pointer to Global Shared Memory Structure
// Package to BIG Endian
XPLAT_DLL_API void packageBytesBIG(			U8*						*outBuff,			// Pointer to Pointer to Output Data Buffer
											U8*						ValueAddr,			// Pointer to Values bits
											U8						numBytes);			// Number of bytes to package

// Package to little Endian
XPLAT_DLL_API void packageByteslittle(		U8*						*outBuff,			// Pointer to Pointer to  Output Data Buffer
											U8*						ValueAddr,			// Pointer to Values bits
											U8						numBytes);			// Number of bytes to package	

// Un-Pack from BIG Endian
XPLAT_DLL_API void unpackBytesBIG(			U8*						*inBuff,			// Pointer Pointer to  to input Data Buffer
											U8*						ValueAddr,			// Pointer to Values bits
											U8						numBytes);			// Number of bytes to package

// Un-Pack from little Endian
XPLAT_DLL_API void unpackByteslittle(		U8*						*inBuff,			// Pointer to Pointer to  Input Data Buffer
											U8*						ValueAddr,			// Pointer to Values bits
											U8						numBytes);			// Number of bytes to package	

// Auto-Gen::XPLAT_DLL_API_PackagePacket Function Prototypes

// Auto-Gen::XPLAT_DLL_API_Comm Function Prototypes


// Execution System Structure Initialization
XPLAT_DLL_API void initExeSysStruct(        xplatExeSysstruct*      ExeSys,                 // Execution System to be initialized
                                            xplatModulestruct*      ModuleArrayPtr,         // Pointer to the Module Execution Array
                                            void                    (*initPretasks)(),      // Function pointer to init pre tasks
                                            void                    (*initPosttasks)(),     // Function pointer to init post tasks
                                            void                    (*loopPretasks)(),      // Function pointer to loop pre tasks
                                            void                    (*loopPosttasks)());    // Function pointer to loop post tasks

// Execution System Entry Points
XPLAT_DLL_API void mainInit_PreTasks(       xplatExeSysstruct*      ExeSys); 
XPLAT_DLL_API void exeSysMainInit(          xplatExeSysstruct*      ExeSys);  
XPLAT_DLL_API void mainInit_PostTasks(      xplatExeSysstruct*      ExeSys); 
XPLAT_DLL_API void mainLoop_PreTasks(       xplatExeSysstruct*      ExeSys); 
XPLAT_DLL_API void exeSysMainLoop(          xplatExeSysstruct*      ExeSys);
XPLAT_DLL_API void mainLoop_PostTasks(      xplatExeSysstruct*      ExeSys); 





#endif // XPLAT_BASE_H
