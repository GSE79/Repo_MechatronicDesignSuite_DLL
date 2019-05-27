/* /////////////////////////////////////////////////////////
*	InMechaSol non-profit license for (re)use.
*	- This source code is made available as a reference
*	- InMechaSol does not guarantee an applications
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
                            U8*                     outBuff,        // Pointer to Output Data Buffer
                            BaseSMStruct*           SMPtrIn)        // Pointer to Global Shared Memory Structure
{
    // Set Rx (in Buff) Pointer
    (*Data).inputBuffer = inBuff;
    // Set Tx (out Buff) Pointer
    (*Data).outputBuffer = outBuff;
	// Set Rx (in Buff) Packet Pointer
	(*Data).inputPacket = inBuff + PckHDRSize;
	// Set Tx (out Buff) Packet Pointer
	(*Data).outputPacket = outBuff + PckHDRSize;
    // Set SM (Shared Memory) Pointer
    (*Data).SMPtr = SMPtrIn;
}

// Package to BIG Endian
void packageBytesBIG(		U8*						*outBuff,       // Pointer to Pointer to Output Data Buffer
							U8*						ValueAddr,      // Pointer to Values bits
							U8						numBytes)		// Number of bytes to package
{	
	// Package Big end first
	int i;
	for(i=0;i<numBytes;i++)
		*((*outBuff)++) = (*(ValueAddr+(numBytes-1)-i));		// Pack Addr[0] <- Value Addr[0] + numBytes

}
// Package to little Endian
void packageByteslittle(	U8*						*outBuff,       // Pointer to Pointer to Output Data Buffer
							U8*						ValueAddr,      // Pointer to Values bits
							U8						numBytes)		// Number of bytes to package
{
	// Package little end first
	int i;
	for (i = 0; i<numBytes; i++)
		*((*outBuff)++) = (*(ValueAddr + i));		// Pack Addr[0] <- Value Addr[0] 
}
// Un-Pack from BIG Endian
void unpackBytesBIG(		U8*						*inBuff,			// Pointer to Pointer to  input Data Buffer
							U8*						ValueAddr,			// Pointer to Values bits
							U8						numBytes)			// Number of bytes to package
{
	// Un-Pack Big end first
	int i;
	for (i = 0; i<numBytes; i++)
		(*(ValueAddr + (numBytes-1) - i)) = *((*inBuff)++);		// unPack Addr[0] -> Value Addr[0] + numBytes
}
// Un-Pack from little Endian
void unpackByteslittle(		U8*						*inBuff,			// Pointer to Pointer to  Input Data Buffer
							U8*						ValueAddr,			// Pointer to Values bits
							U8						numBytes)			// Number of bytes to package	
{
	// Package little end first
	int i;
	for (i = 0; i<numBytes; i++)
		(*(ValueAddr + i)) = *((*inBuff)++);		// unPack Addr[0] -> Value Addr[0] 
}

// Auto-Gen::XPLAT_DLL_API_PackagePacket Function Definitions

// Auto-Gen::XPLAT_DLL_API_Comm Function Definitions

#define LOOPALLMODULES(index)           for(ExeSys->index = 0; ExeSys->index < ExeSys->moduleCount; ExeSys->index++)
#define ACTIVEMODULE(index)             ExeSys->exeModuleArrayPtr[ExeSys->index]
#define ACTIVEMODULEDATAPTR(index)      ACTIVEMODULE(index).Data
#define ACTIVEMODULEDATAADDR(index)     &(ExeSys->DataArrayPtr[ExeSys->index])
#define ACTIVEMODULEFLAGS(index)        ACTIVEMODULEDATAPTR(index)->ExeFlags
#define DONOTHING(arbitrary)            if(ExeSys->exeModuleArrayPtr){;}

// Execution System Structure Initialization
void initExeSysStruct(      xplatExeSysstruct*      ExeSys,                 // Execution System to be initialized
                            xplatModulestruct*      ModuleArrayPtr,         // Pointer to the Module Execution Array
                            void                    (*initPretasks)(),      // Function pointer to init pre tasks
                            void                    (*initPosttasks)(),     // Function pointer to init post tasks
                            void                    (*loopPretasks)(),      // Function pointer to loop pre tasks
                            void                    (*loopPosttasks)())     // Function pointer to loop post tasks
{
    ExeSys->exeModuleArrayPtr = ModuleArrayPtr;
    ExeSys->mainInit_preTasks = initPretasks;
    ExeSys->mainInit_postTasks = initPosttasks;
    ExeSys->mainLoop_preTasks = loopPretasks;
    ExeSys->mainLoop_postTasks = loopPosttasks;
}

// The Main Init Function
void exeSysMainInit(        xplatExeSysstruct*      ExeSys)
{
    
    // It is required that ExeSys has been initialized prior to this call
    if(ExeSys->runOnceFlags & EXEFlags_mainInit)
    {
        LOOPALLMODULES(indexMainInit)
        {            
            ACTIVEMODULEDATAPTR(indexMainInit) = ACTIVEMODULEDATAADDR(indexMainInit);
            ACTIVEMODULEFLAGS(indexMainInit) = (EXEFlags_mainInit | EXEFlags_mainLoop);
        }
        ExeSys->runOnceFlags &= ~EXEFlags_mainInit;
    }
    
    
    // Execute Pre Tasks
    ExeSys->mainInit_preTasks();
    
    // Loop all Modules of the ExeSys
    LOOPALLMODULES(indexMainInit)
    {
        // IF the Modules Flag is set, 
        if( (ACTIVEMODULEFLAGS(indexMainInit) & EXEFlags_mainInit) )
        {
            // Execute the Entry Point Function
            ACTIVEMODULE(indexMainInit).mainInit(ACTIVEMODULEDATAPTR(indexMainInit));
        }
    }
    
    // Execute Post Tasks
    ExeSys->mainInit_postTasks();
}

// The Main Loop Function
void exeSysMainLoop(        xplatExeSysstruct*      ExeSys)
{
    // Execute Pre Tasks
    ExeSys->mainLoop_preTasks();
    
    // Loop all Modules of the ExeSys
    LOOPALLMODULES(indexMainLoop)
    {
        // IF the Modules Flag is set, 
        if( (ACTIVEMODULEFLAGS(indexMainLoop) & EXEFlags_mainLoop) )
        {
            // Execute the Entry Point Function
            ACTIVEMODULE(indexMainLoop).mainLoop(ACTIVEMODULEDATAPTR(indexMainLoop));
        }
    }
    
    // Execute Post Tasks
    ExeSys->mainLoop_postTasks();
}

// Pre and Post Init Tasks
void mainInit_PreTasks(     xplatExeSysstruct*      ExeSys)
{
    DONOTHING(ExeSys);        
}
void mainInit_PostTasks(    xplatExeSysstruct*      ExeSys)
{
    DONOTHING(ExeSys);
}
// Pre and Post Loop Tasks
void mainLoop_PreTasks(     xplatExeSysstruct*      ExeSys)
{
    DONOTHING(ExeSys);        
}
void mainLoop_PostTasks(    xplatExeSysstruct*      ExeSys)
{
    DONOTHING(ExeSys);
}


