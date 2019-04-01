#ifndef MEMSAVE_H_
#define MEMSAVE_H_

#include "main.h"
#include "eepromEx.h"
#include "backlight.h"

#define EEPROM_CUSTOM_REALIZATION

#define SAVE_ADDRESS_ETX_ENABLE 0
#define SAVE_ADDRESS_ETX_MODE 1
#define SAVE_ADDRESS_ETX_CALIBRATION 2
#define SAVE_ADDRESS_LED_ENABLE 3
#define SAVE_ADDRESS_LED_MODE 4
#define SAVE_ADDRESS_LED_R 5
#define SAVE_ADDRESS_LED_G 6
#define SAVE_ADDRESS_LED_B 7
#define SAVE_ADDRESS_LED_BRIGHTNESS 8
#define SAVE_ADDRESS_LED_BRIGHTNESS_MULTIPLIER 9
#define SAVE_ADDRESS_LED_USER_MODE_START 10


uint16_t saveFloat(uint16_t pos, float value);
uint16_t loadFloat(uint16_t pos, float *value);
uint16_t saveInt(uint16_t pos, uint16_t value);
uint16_t loadInt(uint16_t pos, uint16_t *value);
void backlightSaveUserPreset();
uint8_t backlightLoadUserPreset();
void saveSettings(int8_t addr);
void loadSettings();
void saveDefaultSettings();

#endif /* MEMSAVE_H_ */
