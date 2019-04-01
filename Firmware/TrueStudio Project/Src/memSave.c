
#include "memSave.h"

#ifndef EEPROM_CUSTOM_REALIZATION
uint16_t saveFloat(uint16_t pos, float value)
{
	uint16_t ret = 0xAA;
	float16 v = value, rfv;
	uint16_t rv;
	HAL_FLASH_Unlock();
	EE_ReadVariable(pos, &rv);
	rfv = *(float16 *)&rv;
	if (rfv != v)
	{
		uint16_t sv = *(uint16_t *)&v;
		ret = EE_WriteVariable(pos, sv);
	}
	HAL_FLASH_Lock();
	return ret;
}
#else
uint16_t saveFloat(uint16_t pos, float value)
{
	uint16_t ret = 0xAA;
	float16 v = value;
	HAL_FLASH_Unlock();
	uint16_t sv = *(uint16_t *)&v;
	ret = EE_WriteVariable(pos, sv);
	HAL_FLASH_Lock();
	return ret;
}
#endif

uint16_t loadFloat(uint16_t pos, float *value)
{
	uint16_t v, res;
	HAL_FLASH_Unlock();
	res = EE_ReadVariable(pos, &v);
	HAL_FLASH_Lock();
	if (res) return res;
	float16 lv = *(float16 *)&v;
	*value = lv;
	return 0;
}

#ifndef EEPROM_CUSTOM_REALIZATION
uint16_t saveInt(uint16_t pos, uint16_t value)
{
	uint16_t ret = 0xAA;
	HAL_FLASH_Unlock();
	uint16_t v;
	EE_ReadVariable(pos, &v);
	if (v != value) ret = EE_WriteVariable(pos, value);
	HAL_FLASH_Lock();
	return ret;
}
#else
uint16_t saveInt(uint16_t pos, uint16_t value)
{
	uint16_t ret = 0xAA;
	HAL_FLASH_Unlock();
	ret = EE_WriteVariable(pos, value);
	HAL_FLASH_Lock();
	return ret;
}
#endif

uint16_t loadInt(uint16_t pos, uint16_t *value)
{
	HAL_FLASH_Unlock();
	uint16_t ret = EE_ReadVariable(pos, value);
	HAL_FLASH_Lock();
	return ret;
}

void backlightSaveUserPreset()
{
	HAL_FLASH_Unlock();
	EE_WriteVariable(SAVE_ADDRESS_LED_USER_MODE_START, backlight_userdata_steps);
	uint32_t k = 0;
	for (uint16_t i = SAVE_ADDRESS_LED_USER_MODE_START + 1; i <= (backlight_userdata_steps + 1) * 8 + SAVE_ADDRESS_LED_USER_MODE_START; i++)
	{
		float16 v = ((float *)(backlight_userdata))[k++];
		uint16_t sv = *(uint16_t *)&v;
		EE_WriteVariable(i, sv);
	}
	HAL_FLASH_Lock();
}

uint8_t backlightLoadUserPreset()
{
	uint16_t steps;
	HAL_FLASH_Unlock();
	if (EE_ReadVariable(SAVE_ADDRESS_LED_USER_MODE_START, &steps)){ HAL_FLASH_Lock(); return 1;}
	backlight_userdata_steps = steps;
	uint32_t k = 0;
	for (uint16_t i = SAVE_ADDRESS_LED_USER_MODE_START + 1; i <= (backlight_userdata_steps + 1) * 8 + SAVE_ADDRESS_LED_USER_MODE_START; i++)
	{
		uint16_t v;
		if (EE_ReadVariable(i, &v)) return 1;
		float16 lv = *(float16 *)&v;
		float f = lv;
		((float *)(backlight_userdata))[k++] = f;
	}
	HAL_FLASH_Lock();
	return 0;
}

void saveSettings(int8_t addr)
{
	switch (addr)
	{
		switchStart:
//	case SAVE_ADDRESS_TX_ENABLE:
//		saveInt(SAVE_ADDRESS_TX_ENABLE, txEnableState);
//		if (addr != -1) break;

	case SAVE_ADDRESS_ETX_MODE:
		saveInt(SAVE_ADDRESS_ETX_MODE, etxControl);
		if (addr != -1) break;

	case SAVE_ADDRESS_ETX_CALIBRATION:
		saveFloat(SAVE_ADDRESS_ETX_CALIBRATION, etxNoLoadCurrentMax);
		if (addr != -1) break;

	case SAVE_ADDRESS_LED_ENABLE:
		saveInt(SAVE_ADDRESS_LED_ENABLE, getLEDState());
		if (addr != -1) break;

	case SAVE_ADDRESS_LED_MODE:
		saveInt(SAVE_ADDRESS_LED_MODE, backlight_mode);
		if (addr != -1) break;

	case SAVE_ADDRESS_LED_R:
		saveFloat(SAVE_ADDRESS_LED_R, backlight_data.rgb_red);
		if (addr != -1) break;

	case SAVE_ADDRESS_LED_G:
		saveFloat(SAVE_ADDRESS_LED_G, backlight_data.rgb_green);
		if (addr != -1) break;

	case SAVE_ADDRESS_LED_B:
		saveFloat(SAVE_ADDRESS_LED_B, backlight_data.rgb_blue);
		if (addr != -1) break;

	case SAVE_ADDRESS_LED_BRIGHTNESS:
		saveFloat(SAVE_ADDRESS_LED_BRIGHTNESS, backlight_data.brightness);
		if (addr != -1) break;

	case SAVE_ADDRESS_LED_BRIGHTNESS_MULTIPLIER:
		saveFloat(SAVE_ADDRESS_LED_BRIGHTNESS_MULTIPLIER, brightnessMul);
		if (addr != -1) break;

	case SAVE_ADDRESS_LED_USER_MODE_START:
		backlightSaveUserPreset();
		break;

	default:
		addr = -1;
		goto switchStart;
	}
}

void saveDefaultSettings()
{
		saveInt(SAVE_ADDRESS_ETX_MODE, Auto);
		saveFloat(SAVE_ADDRESS_ETX_CALIBRATION, 0.5);
		saveInt(SAVE_ADDRESS_LED_ENABLE, 1);
		saveInt(SAVE_ADDRESS_LED_MODE, DefaultPresetMode);
		saveFloat(SAVE_ADDRESS_LED_R, 100);
		saveFloat(SAVE_ADDRESS_LED_G, 100);
		saveFloat(SAVE_ADDRESS_LED_B, 100);
		saveFloat(SAVE_ADDRESS_LED_BRIGHTNESS, 5);
		saveFloat(SAVE_ADDRESS_LED_BRIGHTNESS_MULTIPLIER, 1);
		saveInt(SAVE_ADDRESS_LED_USER_MODE_START, 0);
}

void loadSettings()
{
	uint16_t res, vali;
	float valf;
//	res = loadInt(SAVE_ADDRESS_TX_ENABLE, &vali);
//	if (!res)
//	{
//		txEnableState = vali;
//	}
	res = loadInt(SAVE_ADDRESS_ETX_MODE, &vali);
	if (!res)
	{
		etxControl = vali;
	}
	res = loadFloat(SAVE_ADDRESS_ETX_CALIBRATION, &valf);
	if (!res)
	{
		etxNoLoadCurrentMax = valf;
	}
	res = loadInt(SAVE_ADDRESS_LED_ENABLE, &vali);
	if (!res)
	{
		LEDEnable(vali);
	}
	backlightLoadUserPreset();
	res = 0;
	float rgb[3] = {0};
	res |= loadFloat(SAVE_ADDRESS_LED_R, &rgb[0]);
	res |= loadFloat(SAVE_ADDRESS_LED_G, &rgb[1]);
	res |= loadFloat(SAVE_ADDRESS_LED_B, &rgb[2]);
	if (!res)
	{
		setRGBColour(rgb[0], rgb[1], rgb[2]);
	}
	res = loadFloat(SAVE_ADDRESS_LED_BRIGHTNESS, &valf);
	if (!res)
	{
		setRGBBrightness(valf);
	}
	res = loadFloat(SAVE_ADDRESS_LED_BRIGHTNESS_MULTIPLIER, &valf);
	if (!res)
	{
		setBrightnessMultiplier(valf);
	}
	res = loadInt(SAVE_ADDRESS_LED_MODE, &vali);
	if (!res)
	{
		initBacklightMode(vali);
	}
}


