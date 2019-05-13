#
# Generated Makefile - do not edit!
#
# Edit the Makefile in the project folder instead (../Makefile). Each target
# has a -pre and a -post target defined where you can add customized code.
#
# This makefile implements configuration specific macros and targets.


# Include project Makefile
ifeq "${IGNORE_LOCAL}" "TRUE"
# do not include local makefile. User is passing all local related variables already
else
include Makefile
# Include makefile containing local settings
ifeq "$(wildcard nbproject/Makefile-local-default.mk)" "nbproject/Makefile-local-default.mk"
include nbproject/Makefile-local-default.mk
endif
endif

# Environment
MKDIR=gnumkdir -p
RM=rm -f 
MV=mv 
CP=cp 

# Macros
CND_CONF=default
ifeq ($(TYPE_IMAGE), DEBUG_RUN)
IMAGE_TYPE=debug
OUTPUT_SUFFIX=a
DEBUGGABLE_SUFFIX=a
FINAL_IMAGE=dist/${CND_CONF}/${IMAGE_TYPE}/XPlat_LIB.X.${OUTPUT_SUFFIX}
else
IMAGE_TYPE=production
OUTPUT_SUFFIX=a
DEBUGGABLE_SUFFIX=a
FINAL_IMAGE=dist/${CND_CONF}/${IMAGE_TYPE}/XPlat_LIB.X.${OUTPUT_SUFFIX}
endif

ifeq ($(COMPARE_BUILD), true)
COMPARISON_BUILD=-mafrlcsj
else
COMPARISON_BUILD=
endif

# Object Directory
OBJECTDIR=build/${CND_CONF}/${IMAGE_TYPE}

# Distribution Directory
DISTDIR=dist/${CND_CONF}/${IMAGE_TYPE}

# Source Files Quoted if spaced
SOURCEFILES_QUOTED_IF_SPACED=../xplat_base.c

# Object Files Quoted if spaced
OBJECTFILES_QUOTED_IF_SPACED=${OBJECTDIR}/_ext/1472/xplat_base.o
POSSIBLE_DEPFILES=${OBJECTDIR}/_ext/1472/xplat_base.o.d

# Object Files
OBJECTFILES=${OBJECTDIR}/_ext/1472/xplat_base.o

# Source Files
SOURCEFILES=../xplat_base.c


CFLAGS=
ASFLAGS=
LDLIBSOPTIONS=

############# Tool locations ##########################################
# If you copy a project from one host to another, the path where the  #
# compiler is installed may be different.                             #
# If you open this project with MPLAB X in the new host, this         #
# makefile will be regenerated and the paths will be corrected.       #
#######################################################################
# fixDeps replaces a bunch of sed/cat/printf statements that slow down the build
FIXDEPS=fixDeps

.build-conf:  ${BUILD_SUBPROJECTS}
ifneq ($(INFORMATION_MESSAGE), )
	@echo $(INFORMATION_MESSAGE)
endif
	${MAKE}  -f nbproject/Makefile-default.mk dist/${CND_CONF}/${IMAGE_TYPE}/XPlat_LIB.X.${OUTPUT_SUFFIX}

MP_PROCESSOR_OPTION=33EP128GM304
MP_LINKER_FILE_OPTION=,--script=p33EP128GM304.gld
# ------------------------------------------------------------------------------------
# Rules for buildStep: compile
ifeq ($(TYPE_IMAGE), DEBUG_RUN)
${OBJECTDIR}/_ext/1472/xplat_base.o: ../xplat_base.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}/_ext/1472" 
	@${RM} ${OBJECTDIR}/_ext/1472/xplat_base.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/xplat_base.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../xplat_base.c  -o ${OBJECTDIR}/_ext/1472/xplat_base.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/xplat_base.o.d"      -g -D__DEBUG -D__MPLAB_DEBUGGER_PK3=1  -mno-eds-warn  -omf=elf -DXPRJ_default=$(CND_CONF)  -legacy-libc  $(COMPARISON_BUILD)  -O0 -DDONTUSE_STDINT -DXPLAT_NO_DLL -DINT16BITS -msmart-io=1 -Wall -msfr-warn=off  
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/xplat_base.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
else
${OBJECTDIR}/_ext/1472/xplat_base.o: ../xplat_base.c  nbproject/Makefile-${CND_CONF}.mk
	@${MKDIR} "${OBJECTDIR}/_ext/1472" 
	@${RM} ${OBJECTDIR}/_ext/1472/xplat_base.o.d 
	@${RM} ${OBJECTDIR}/_ext/1472/xplat_base.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ../xplat_base.c  -o ${OBJECTDIR}/_ext/1472/xplat_base.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MMD -MF "${OBJECTDIR}/_ext/1472/xplat_base.o.d"      -mno-eds-warn  -g -omf=elf -DXPRJ_default=$(CND_CONF)  -legacy-libc  $(COMPARISON_BUILD)  -O0 -DDONTUSE_STDINT -DXPLAT_NO_DLL -DINT16BITS -msmart-io=1 -Wall -msfr-warn=off  
	@${FIXDEPS} "${OBJECTDIR}/_ext/1472/xplat_base.o.d" $(SILENT)  -rsi ${MP_CC_DIR}../ 
	
endif

# ------------------------------------------------------------------------------------
# Rules for buildStep: assemble
ifeq ($(TYPE_IMAGE), DEBUG_RUN)
else
endif

# ------------------------------------------------------------------------------------
# Rules for buildStep: assemblePreproc
ifeq ($(TYPE_IMAGE), DEBUG_RUN)
else
endif

# ------------------------------------------------------------------------------------
# Rules for buildStep: archive
ifeq ($(TYPE_IMAGE), DEBUG_RUN)
dist/${CND_CONF}/${IMAGE_TYPE}/XPlat_LIB.X.${OUTPUT_SUFFIX}: ${OBJECTFILES}  nbproject/Makefile-${CND_CONF}.mk    
	@${MKDIR} dist/${CND_CONF}/${IMAGE_TYPE} 
	@${RM} dist/${CND_CONF}/${IMAGE_TYPE}/XPlat_LIB.X.${OUTPUT_SUFFIX} 
	${MP_AR} $(MP_EXTRA_AR_PRE)  -omf=elf -r dist/${CND_CONF}/${IMAGE_TYPE}/XPlat_LIB.X.${OUTPUT_SUFFIX}  ${OBJECTFILES_QUOTED_IF_SPACED}      
	
else
dist/${CND_CONF}/${IMAGE_TYPE}/XPlat_LIB.X.${OUTPUT_SUFFIX}: ${OBJECTFILES}  nbproject/Makefile-${CND_CONF}.mk   
	@${MKDIR} dist/${CND_CONF}/${IMAGE_TYPE} 
	@${RM} dist/${CND_CONF}/${IMAGE_TYPE}/XPlat_LIB.X.${OUTPUT_SUFFIX} 
	${MP_AR} $(MP_EXTRA_AR_PRE)  -omf=elf -r dist/${CND_CONF}/${IMAGE_TYPE}/XPlat_LIB.X.${OUTPUT_SUFFIX}  ${OBJECTFILES_QUOTED_IF_SPACED}      
	
endif


# Subprojects
.build-subprojects:


# Subprojects
.clean-subprojects:

# Clean Targets
.clean-conf: ${CLEAN_SUBPROJECTS}
	${RM} -r build/default
	${RM} -r dist/default

# Enable dependency checking
.dep.inc: .depcheck-impl

DEPFILES=$(shell mplabwildcard ${POSSIBLE_DEPFILES})
ifneq (${DEPFILES},)
include ${DEPFILES}
endif
