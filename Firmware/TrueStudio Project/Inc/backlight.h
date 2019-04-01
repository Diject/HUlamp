
#ifndef BACKLIGHT_H_
#define BACKLIGHT_H_

#include "main.h"
#include "stdint.h"
#include "stm32f0xx_hal.h"

#define RGB_RED_COEFFICIENT 1
#define RGB_BLUE_COEFFICIENT 0.75
#define RGB_GREEN_COEFFICIENT 0.75

#define PWM_CHANNEL_RED TIM_CHANNEL_1
#define PWM_CHANNEL_GREEN TIM_CHANNEL_3
#define PWM_CHANNEL_BLUE TIM_CHANNEL_2

#define PWM_CHANNEL_MAX_VALUE 2000
#define PWM_CHANNEL_MAX_VALUE_RED (uint16_t)((float)PWM_CHANNEL_MAX_VALUE * (float)RGB_RED_COEFFICIENT)
#define PWM_CHANNEL_MAX_VALUE_GREEN (uint16_t)((float)PWM_CHANNEL_MAX_VALUE * (float)RGB_GREEN_COEFFICIENT)
#define PWM_CHANNEL_MAX_VALUE_BLUE (uint16_t)((float)PWM_CHANNEL_MAX_VALUE * (float)RGB_BLUE_COEFFICIENT)

typedef enum
{
	DirectMode,
	DefaultPresetMode,
	UserPresetMode,
	FlashMode1,
	FlashMode2,
}backlightMode;

typedef struct
{
	float target_brightness;
	float step_brightness;
	float target_red;
	float step_red;
	float target_green;
	float step_green;
	float target_blue;
	float step_blue;
	float brightness;
	float rgb_red;
	float rgb_green;
	float rgb_blue;
}backlightData;

typedef struct
{
	float target_brightness;
	float step_brightness;
	float target_red;
	float step_red;
	float target_green;
	float step_green;
	float target_blue;
	float step_blue;
}backlightStepData;

backlightData backlight_data;
backlightMode backlight_mode;

float brightnessMul;
uint8_t backlight_userdata_steps;
backlightStepData backlight_userdata[61];

void LEDEnable(uint32_t val);
uint8_t getLEDState();

void setButtonPressDetectMode(uint8_t val, uint16_t r, uint16_t g, uint16_t b);

void backlightSetModel(const backlightStepData* const model, uint8_t steps);
void backlightSetMode(backlightMode mode);
void initBacklightMode(backlightMode mode);

void setBrightnessMultiplier(float brightness);
void setRGBBrightness(float brightness);
void setRGBColour(float red, float green, float blue);

void configPWM(uint32_t channel, uint16_t value);

void backlightUpdate();
void backlightCallback();

void backlightUserDataPut(const uint8_t pos, const backlightStepData data);
void backlightUserDataSetup(const uint8_t count);
void backlightUserDataCopy(const uint8_t from, const uint8_t to);

#endif /* BACKLIGHT_H_ */
