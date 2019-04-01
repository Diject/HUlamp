#ifndef EEPROMEX_H_
#define EEPROMEX_H_

#include "stm32f0xx_hal.h"

#define PAGE_SIZE (uint32_t)FLASH_PAGE_SIZE
#define EEPROM_START_PAGE_NUMBER 27
#define EEPROM_END_PAGE_NUMBER 31
#define EEPROM_BUFFER_LENGTH (PAGE_SIZE / 4)
#define EEPROM_VIRTUAL_ADDRESS_PROTECTION_MASK (EEPROM_BUFFER_LENGTH - 1)

uint16_t EE_Init();
uint16_t EE_ReadVariable(uint16_t VirtAddress, uint16_t* Data);
uint16_t EE_WriteVariable(uint16_t VirtAddress, uint16_t Data);

#endif
