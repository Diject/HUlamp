#include "eepromEx.h"

#define PAGE_RECEIVE_DATA       (0xEEEE)     /* Page is marked to receive data */

#define PAGE_CELL_ERASED        (0xFFFFFFFF)
#define PAGE_HALF_CELL_ERASED   (0xFFFF)

static uint16_t buffer[EEPROM_BUFFER_LENGTH];
static uint8_t bufferPages[EEPROM_BUFFER_LENGTH];
static uint8_t startPageNumber = EEPROM_START_PAGE_NUMBER;
static uint8_t endPageNumber = EEPROM_END_PAGE_NUMBER;
static uint8_t readyPage = 0xFF;
static uint16_t readyPageLastValueAddress = 0xFFFF;

static uint32_t getPageStartAddress(uint32_t pageNumber)
{
	return (uint32_t)(pageNumber * PAGE_SIZE) + (0x08000000);
}

static uint32_t getPageEndAddress(uint32_t pageNumber)
{
	return ((uint32_t)(pageNumber + 1) * PAGE_SIZE - 4) + (0x08000000);
}

static HAL_StatusTypeDef prepareClearPage(uint32_t pageAddress)
{
	FLASH_EraseInitTypeDef s_eraseinit;
	uint32_t page_error = 0;
	s_eraseinit.TypeErase   = FLASH_TYPEERASE_PAGES;
	s_eraseinit.PageAddress = pageAddress;
	s_eraseinit.NbPages     = 1;
	HAL_StatusTypeDef res = HAL_FLASHEx_Erase(&s_eraseinit, &page_error);
	if (res == HAL_OK)
	{
		return HAL_FLASH_Program(FLASH_TYPEPROGRAM_WORD, pageAddress, ((uint32_t)PAGE_RECEIVE_DATA) << 16);
	}
	return HAL_ERROR;
}

static void parsePage(uint8_t page)
{
	uint32_t pageStartAddr = getPageStartAddress(page);
	for (uint32_t j = getPageEndAddress(page); j > pageStartAddr; j -= 4)
	{
		uint32_t cellValue = *(__IO uint32_t*)j;
		if (cellValue != (uint32_t)PAGE_CELL_ERASED)
		{
			uint16_t addr = ((uint32_t)cellValue >> 16) & (uint16_t)(EEPROM_VIRTUAL_ADDRESS_PROTECTION_MASK);
			uint16_t value = (cellValue & (uint32_t)PAGE_HALF_CELL_ERASED);
			if ((buffer[addr] == 0) && (bufferPages[addr] == 0xFF))
			{
				buffer[addr] = value;
				bufferPages[addr] = page;
			}
		}
	}
}

uint16_t EE_Init()
{
	uint8_t currentPage = 0xFF;
	uint8_t freePage = 0xFF;
	for (uint16_t i = 0; i < EEPROM_BUFFER_LENGTH; i++)
	{
		buffer[i] = 0;
		bufferPages[i] = 0xFF;
	}
	for (uint8_t i = endPageNumber; i >= startPageNumber; i--)
	{
		uint32_t pageStartAddress = getPageStartAddress(i);
		uint32_t pageEndAddress = getPageEndAddress(i);
		uint32_t pageStatus1 = *(__IO uint32_t*)pageStartAddress;
		uint32_t pageStatus2 = *(__IO uint32_t*)pageEndAddress;
		if ((pageStatus1 == ((uint32_t)PAGE_RECEIVE_DATA << 16)) && (pageStatus2 == (uint32_t)PAGE_CELL_ERASED))
		{
			currentPage = i;
			break;
		}
		else if ((pageStatus1 == (uint32_t)PAGE_CELL_ERASED) && (pageStatus2 == (uint32_t)PAGE_CELL_ERASED) && (freePage == 0xFF)) freePage = i;
	}

	if ((freePage != 0xFF) && (currentPage == 0xFF))
	{
		uint32_t pageStartAddress = getPageStartAddress(freePage);
		if (HAL_FLASH_Program(FLASH_TYPEPROGRAM_WORD, pageStartAddress, ((uint32_t)PAGE_RECEIVE_DATA) << 16) == HAL_OK)
		{
			currentPage = freePage;
		}else return 1;
	}

	if (currentPage != 0xFF)
	{
		for(uint8_t i = currentPage; i <= endPageNumber; i++)
		{
			parsePage(i);
		}
		for(uint8_t i = startPageNumber; i < currentPage; i++)
		{
			parsePage(i);
		}
		readyPage = currentPage;
		uint32_t startAddr = getPageStartAddress(currentPage);
		for (uint32_t i = getPageEndAddress(currentPage); i >= startAddr; i -= 4)
		{
			uint32_t cellValue = (*(__IO uint32_t*)i);
			if (cellValue != (uint32_t)PAGE_CELL_ERASED)
			{
				readyPageLastValueAddress = i - startAddr;
				break;
			}
		}
		return 0;
	}else
	{
		readyPage = endPageNumber;
		readyPageLastValueAddress = getPageEndAddress(endPageNumber);
		return 0;
	}
	return 1;
}

uint16_t EE_ReadVariable(uint16_t VirtAddress, uint16_t* Data)
{
	if (bufferPages[VirtAddress] == 0xFF) return 1;
	*Data = buffer[VirtAddress];
	return 0;
}

static uint16_t prepareNextPage()
{
	uint8_t erasePageNumber;
	uint8_t lastReadyPageNumber = readyPage;
	if (lastReadyPageNumber == startPageNumber) erasePageNumber = endPageNumber;
	else erasePageNumber = lastReadyPageNumber - 1;
	if (prepareClearPage(getPageStartAddress(erasePageNumber)) != HAL_OK) return 1;
	readyPage = erasePageNumber;
	readyPageLastValueAddress = 0;
	for (uint16_t i = 0; i < EEPROM_BUFFER_LENGTH; i++)
	{
		if (erasePageNumber == bufferPages[i])
		{
			bufferPages[i] = 0xFF;
			EE_WriteVariable(i, buffer[i]);
		}
	}
	return 0;
}

uint16_t EE_WriteVariable(uint16_t VirtAddress, uint16_t Data)
{
	uint32_t freeAddress = PAGE_CELL_ERASED;
	if ((buffer[VirtAddress] == Data) && (bufferPages[VirtAddress] != 0xFF)) return 0;
	for (uint32_t i = getPageStartAddress(readyPage) + readyPageLastValueAddress; i <= getPageEndAddress(readyPage); i += 4)
	{
		uint32_t val = (*(__IO uint32_t*)i);
		if (val == (uint32_t)PAGE_CELL_ERASED){ freeAddress = i; break; }
	}
	if (freeAddress == (uint32_t)PAGE_CELL_ERASED)
	{
		if (prepareNextPage()) return 1;
		return EE_WriteVariable(VirtAddress, Data);
	}
	uint32_t programValue = (((uint32_t)VirtAddress) << 16) | ((uint32_t)Data);
	if (HAL_FLASH_Program(FLASH_TYPEPROGRAM_WORD, freeAddress, programValue) != HAL_OK) return 1;
	buffer[VirtAddress] = Data;
	bufferPages[VirtAddress] = readyPage;
	readyPageLastValueAddress = freeAddress - getPageStartAddress(readyPage);
	if (readyPageLastValueAddress >= (PAGE_SIZE - 4))
	{
		return prepareNextPage();
	}
	return 0;
}









