
#include "backlight.h"

backlightData backlight_data = {5, 5, 100, 10, 100, 10, 100, 10, 10, 200, 200, 200};

const backlightStepData const lgbt[13] =
{
	{5, 10, 200, 20, 0, 20, 0, 20},
	{5, 10, 200, 0.1, 0, 0.2, 0, 0.1},
	{5, 10, 200, 0.1, 0, 0.2, 0, 0.1},
	{5, 10, 200, 20, 100, 0.1, 0, 20},
	{5, 10, 200, 20, 100, 0.1, 0, 20},
	{5, 10, 200, 20, 200, 0.1, 0, 20},
	{5, 10, 200, 20, 200, 0.1, 0, 20},
	{5, 10, 0, 0.2, 200, 0.1, 0, 20},
	{5, 10, 0, 0.2, 200, 0.1, 0, 20},
	{5, 10, 0, 0.2, 0, 0.2, 200, 0.2},
	{5, 10, 0, 0.2, 0, 0.2, 200, 0.2},
	{5, 10, 100, 0.1, 0, 0.2, 100, 0.1},
	{5, 10, 100, 0.1, 0, 0.2, 100, 0.1},
//	float target_brightness;
//	float step_brightness;
//	float target_red;
//	float step_red;
//	float target_green;
//	float step_green;
//	float target_blue;
//	float step_blue;
};

const backlightStepData const flashMode1Model[11] =
{
	{0, 10, 0, 0, 0, 0, 0, 0},
	{2, 0.002, 0, 0, 0, 0, 0, 0},
	{4, 0.002, 0, 0, 0, 0, 0, 0},
	{6, 0.002, 0, 0, 0, 0, 0, 0},
	{8, 0.002, 0, 0, 0, 0, 0, 0},
	{10, 0.002, 0, 0, 0, 0, 0, 0},
	{8, 0.002, 0, 0, 0, 0, 0, 0},
	{6, 0.002, 0, 0, 0, 0, 0, 0},
	{4, 0.002, 0, 0, 0, 0, 0, 0},
	{2, 0.002, 0, 0, 0, 0, 0, 0},
	{0, 0.002, 0, 0, 0, 0, 0, 0},
};

const backlightStepData const flashMode2Model[11] =
{
	{5, 10, 0, 0, 0, 0, 0, 0},
	{6, 0.001, 0, 0, 0, 0, 0, 0},
	{7, 0.001, 0, 0, 0, 0, 0, 0},
	{8, 0.001, 0, 0, 0, 0, 0, 0},
	{9, 0.001, 0, 0, 0, 0, 0, 0},
	{10, 0.001, 0, 0, 0, 0, 0, 0},
	{9, 0.001, 0, 0, 0, 0, 0, 0},
	{8, 0.001, 0, 0, 0, 0, 0, 0},
	{7, 0.001, 0, 0, 0, 0, 0, 0},
	{6, 0.001, 0, 0, 0, 0, 0, 0},
	{5, 0.001, 0, 0, 0, 0, 0, 0},
};

static const backlightStepData* backlight_model = lgbt;
static uint8_t backlight_steps = 12;
static uint8_t backlight_currentStep = 0;
backlightMode backlight_mode = DefaultPresetMode;
uint8_t backlight_userdata_steps = 0;
backlightStepData backlight_userdata[61] = {0};
float brightnessMul = 1.;
static uint8_t buttonPressDetectMode = 0;
static uint8_t ledEn = 0;


void LEDEnable(uint32_t val)
{
	HAL_GPIO_WritePin(GPIOB, StepUp_Enable_Pin, val);
	ledEn = val;
}

uint8_t getLEDState()
{
	return ledEn;
}

void configPWM(uint32_t channel, uint16_t value)
{
	switch (channel)
	{
		case PWM_CHANNEL_RED:
			TIM1->CCR1 = value;
			break;
		case PWM_CHANNEL_BLUE:
			TIM1->CCR2 = value;
			break;
		case PWM_CHANNEL_GREEN:
			TIM1->CCR3 = value;
			break;
	}
}

void setRGBBrightness(float brightness)
{
	backlight_data.brightness = brightness;
	setRGBColour(backlight_data.rgb_red, backlight_data.rgb_green, backlight_data.rgb_blue);
}

void setBrightnessMultiplier(float brightness)
{
	brightnessMul = brightness;
}

void setRGBColour(float red, float green, float blue)
{
	backlight_data.rgb_red = red;
	backlight_data.rgb_green = green;
	backlight_data.rgb_blue = blue;
	if (!buttonPressDetectMode)
	{
		TIM1->CCR1 =(uint16_t)(red * backlight_data.brightness * (float)RGB_RED_COEFFICIENT * brightnessMul);
		TIM1->CCR3 = (uint16_t)(green * backlight_data.brightness * (float)RGB_GREEN_COEFFICIENT * brightnessMul);
		TIM1->CCR2 = (uint16_t)(blue * backlight_data.brightness * (float)RGB_BLUE_COEFFICIENT * brightnessMul);
	}
}

static float calcStepResult(float cval, float step, float res)
{
	float v;
	if (cval > res)
	{
		v = cval - step;
		if (v < res) v = res;
	}else
	{
		v = cval + step;
		if (v > res) v = res;
	}
	return v;
}

void backlightUpdate()
{
	backlight_data.target_brightness = backlight_model[backlight_currentStep].target_brightness;
	backlight_data.step_brightness = backlight_model[backlight_currentStep].step_brightness;
	backlight_data.target_red = backlight_model[backlight_currentStep].target_red;
	backlight_data.step_red = backlight_model[backlight_currentStep].step_red;
	backlight_data.target_green = backlight_model[backlight_currentStep].target_green;
	backlight_data.step_green = backlight_model[backlight_currentStep].step_green;
	backlight_data.target_blue = backlight_model[backlight_currentStep].target_blue;
	backlight_data.step_blue = backlight_model[backlight_currentStep].step_blue;
	if (backlight_currentStep >= backlight_steps) backlight_currentStep = 1; else backlight_currentStep++;
}

void backlightCallback()
{
	if ((backlight_mode == DefaultPresetMode) || (backlight_mode == UserPresetMode))
	{
		backlight_data.brightness = calcStepResult(backlight_data.brightness, backlight_data.step_brightness, backlight_data.target_brightness);
		backlight_data.rgb_red = calcStepResult(backlight_data.rgb_red, backlight_data.step_red, backlight_data.target_red);
		backlight_data.rgb_green = calcStepResult(backlight_data.rgb_green, backlight_data.step_green, backlight_data.target_green);
		backlight_data.rgb_blue = calcStepResult(backlight_data.rgb_blue, backlight_data.step_blue, backlight_data.target_blue);
	}else if ((backlight_mode == FlashMode1) || (backlight_mode == FlashMode2))
	{
		backlight_data.brightness = calcStepResult(backlight_data.brightness, backlight_data.step_brightness, backlight_data.target_brightness);
	}
	if (!buttonPressDetectMode)
	{
		uint16_t k;
		//red
		k = (uint16_t)(backlight_data.rgb_red * backlight_data.brightness * (float)RGB_RED_COEFFICIENT * brightnessMul);
		if (k > PWM_CHANNEL_MAX_VALUE_RED) k = PWM_CHANNEL_MAX_VALUE_RED;
		TIM1->CCR1 = k;
		//green
		k = (uint16_t)(backlight_data.rgb_green * backlight_data.brightness * (float)RGB_GREEN_COEFFICIENT * brightnessMul);
		if (k > PWM_CHANNEL_MAX_VALUE_GREEN) k = PWM_CHANNEL_MAX_VALUE_GREEN;
		TIM1->CCR3 = k;
		//blue
		k = (uint16_t)(backlight_data.rgb_blue * backlight_data.brightness * (float)RGB_BLUE_COEFFICIENT * brightnessMul);
		if (k > PWM_CHANNEL_MAX_VALUE_BLUE) k = PWM_CHANNEL_MAX_VALUE_BLUE;
		TIM1->CCR2 = k;
	}

}

void backlightSetModel(const backlightStepData* const model, const uint8_t steps)
{
	backlight_model = model;
	backlight_steps = steps;
	backlight_currentStep = 0;
}

void backlightSetMode(backlightMode mode)
{
	backlight_mode = mode;
}

void initBacklightMode(backlightMode mode)
{
	backlight_data.brightness = 1.;
	backlight_mode = mode;
	switch (mode)
	{
		case DefaultPresetMode:
			backlightSetModel(lgbt, 12);
			break;

		case UserPresetMode:
			backlightSetModel(backlight_userdata, backlight_userdata_steps);
			break;

		case FlashMode1:
			backlightSetModel(flashMode1Model, 10);
			break;

		case FlashMode2:
			backlightSetModel(flashMode2Model, 10);
			break;

		default:
			break;
	}
}

void setButtonPressDetectMode(uint8_t val, uint16_t r, uint16_t g, uint16_t b)
{
	buttonPressDetectMode = val;
	HAL_GPIO_WritePin(GPIOB, StepUp_Enable_Pin, 1);
	if (val)
	{
		TIM1->CCR1 = (uint16_t)(r * (float)RGB_RED_COEFFICIENT);
		TIM1->CCR3 = (uint16_t)(g * (float)RGB_GREEN_COEFFICIENT);
		TIM1->CCR2 = (uint16_t)(b * (float)RGB_BLUE_COEFFICIENT);
	}
}

void backlightUserDataPut(const uint8_t pos, const backlightStepData data)
{
	backlight_userdata[pos] = data;
}

void backlightUserDataSetup(const uint8_t count)
{
	backlight_userdata_steps = count;
}

void backlightUserDataCopy(const uint8_t from, const uint8_t to)
{
	backlight_userdata[to] = backlight_userdata[from];
}
