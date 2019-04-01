/* USER CODE BEGIN Header */
/**
  ******************************************************************************
  * @file           : main.c
  * @brief          : Main program body
  ******************************************************************************
  * This notice applies to any and all portions of this file
  * that are not between comment pairs USER CODE BEGIN and
  * USER CODE END. Other portions of this file, whether 
  * inserted by the user or by software development tools
  * are owned by their respective copyright owners.
  *
  * Copyright (c) 2019 STMicroelectronics International N.V. 
  * All rights reserved.
  *
  * Redistribution and use in source and binary forms, with or without 
  * modification, are permitted, provided that the following conditions are met:
  *
  * 1. Redistribution of source code must retain the above copyright notice, 
  *    this list of conditions and the following disclaimer.
  * 2. Redistributions in binary form must reproduce the above copyright notice,
  *    this list of conditions and the following disclaimer in the documentation
  *    and/or other materials provided with the distribution.
  * 3. Neither the name of STMicroelectronics nor the names of other 
  *    contributors to this software may be used to endorse or promote products 
  *    derived from this software without specific written permission.
  * 4. This software, including modifications and/or derivative works of this 
  *    software, must execute solely and exclusively on microcontroller or
  *    microprocessor devices manufactured by or for STMicroelectronics.
  * 5. Redistribution and use of this software other than as permitted under 
  *    this license is void and will automatically terminate your rights under 
  *    this license. 
  *
  * THIS SOFTWARE IS PROVIDED BY STMICROELECTRONICS AND CONTRIBUTORS "AS IS" 
  * AND ANY EXPRESS, IMPLIED OR STATUTORY WARRANTIES, INCLUDING, BUT NOT 
  * LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
  * PARTICULAR PURPOSE AND NON-INFRINGEMENT OF THIRD PARTY INTELLECTUAL PROPERTY
  * RIGHTS ARE DISCLAIMED TO THE FULLEST EXTENT PERMITTED BY LAW. IN NO EVENT 
  * SHALL STMICROELECTRONICS OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT,
  * INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
  * LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, 
  * OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF 
  * LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING 
  * NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE,
  * EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
  *
  ******************************************************************************
  */
/* USER CODE END Header */

/* Includes ------------------------------------------------------------------*/
#include "main.h"
#include "touchsensing.h"
#include "usb_device.h"

/* Private includes ----------------------------------------------------------*/
/* USER CODE BEGIN Includes */
#include "usbd_cdc_if.h"
#include "usbd_core.h"
#include "eepromEx.h"
#include "backlight.h"
#include "memSave.h"
/* USER CODE END Includes */

/* Private typedef -----------------------------------------------------------*/
/* USER CODE BEGIN PTD */

/* USER CODE END PTD */

/* Private define ------------------------------------------------------------*/
/* USER CODE BEGIN PD */

#define PROGRAM_START_ADDR (uint32_t)0x08004000

#define ETX_AUTO_ENABLED_PART_START 10
#define ETX_AUTO_DISABLED_PART_START 210
#define ETX_AUTO_DISABLED_PART_END 310

#define IC_VDD 3.3
#define ADC_MAX_VALUE 0xFFF
#define VOLTS_PER_AMP 1.5
#define ADC_TO_CURRENT_VALUE_MULTIPLIER 1. / (float)ADC_MAX_VALUE * IC_VDD / VOLTS_PER_AMP
#define ETX_VOLTAGE_DAC_VALUE 0xFFF
#define VREF_VOLTAGE 1.23
#define PROTECTION_MIN_USB_VOLTAGE 3.4
#define PROTECTION_MIN_USB_VOLTAGE_BIN (uint32_t)((float)((uint32_t)1 << 18) * (float)(1. / 11.) / (float)VREF_VOLTAGE * (float)PROTECTION_MIN_USB_VOLTAGE )

#define TERMAL_PROTECTION_ON_TEMPERATURE 62
#define TERMAL_PROTECTION_OFF_TEMPERATURE 58

/* USER CODE END PD */

/* Private macro -------------------------------------------------------------*/
/* USER CODE BEGIN PM */

/* USER CODE END PM */

/* Private variables ---------------------------------------------------------*/
ADC_HandleTypeDef hadc;
DMA_HandleTypeDef hdma_adc;

DAC_HandleTypeDef hdac;

IWDG_HandleTypeDef hiwdg;

TIM_HandleTypeDef htim1;
TIM_HandleTypeDef htim2;
TIM_HandleTypeDef htim7;

TSC_HandleTypeDef htsc;

/* USER CODE BEGIN PV */

extern USBD_HandleTypeDef hUsbDeviceFS;

volatile uint32_t timer1k = 0;
volatile uint32_t timer1s = 0;

volatile uint32_t adc_data[4] = {1, 1, 1, 1};
volatile uint16_t adc_avgData[4] = {1, 1, 1, 1};
static uint32_t cc_sum = 0;
txControlType etxControl = Auto;
static uint8_t etxEnableCurrentState = 0;
static uint8_t termalProtectionState = 0;

float etxNoLoadCurrentMax = 0.5;

uint8_t calibrationMode = 0;
uint32_t calibrationOffTimer = 0;

uint8_t dataSendRequest = 0;
/* USER CODE END PV */

/* Private function prototypes -----------------------------------------------*/
void SystemClock_Config(void);
static void MX_GPIO_Init(void);
static void MX_DMA_Init(void);
static void MX_ADC_Init(void);
static void MX_DAC_Init(void);
static void MX_TSC_Init(void);
static void MX_TIM1_Init(void);
static void MX_TIM2_Init(void);
static void MX_TIM7_Init(void);
static void MX_IWDG_Init(void);
static void MX_NVIC_Init(void);
/* USER CODE BEGIN PFP */
void etxEnable(uint8_t val);
/* USER CODE END PFP */

/* Private user code ---------------------------------------------------------*/
/* USER CODE BEGIN 0 */

volatile __IO uint32_t *VectorTable = (__IO uint32_t *)0x20000000;

void Remap_Table(void)
{
    // Copy interrupt vector table to the RAM.

    for(uint16_t i = 0; i < 48; i++)
    {
    	VectorTable[i] = *(__IO uint32_t *)(PROGRAM_START_ADDR + (i << 2));
    }

    __HAL_RCC_AHB_FORCE_RESET();

    //  Enable SYSCFG peripheral clock
    __HAL_RCC_SYSCFG_CLK_ENABLE();

    __HAL_RCC_AHB_RELEASE_RESET();

    // Remap RAM into 0x0000 0000
    __HAL_SYSCFG_REMAPMEMORY_SRAM();

    __enable_irq();
}

volatile uint8_t adcready = 0;
void HAL_ADC_ConvCpltCallback(ADC_HandleTypeDef* hadc)
{
	if (hadc->Instance == ADC1)
	{
		static uint16_t adcCounter = 0;
		static uint32_t adc_avgSum[4] = {0};
		for (uint8_t i = 0; i < 4; i++) adc_avgSum[i] += adc_data[i];
		if (adc_data[3] != 0)
			if ((((uint32_t)adc_data[0] << 18) / adc_data[3]) < PROTECTION_MIN_USB_VOLTAGE_BIN)
			{
				etxEnable(0);
				etxControl = Off;
			}
		cc_sum += adc_data[1];
		adcCounter++;
		if (adcCounter >= 1000)
		{
			for (uint8_t i = 0; i < 4; i++)
			{
				adc_avgData[i] = adc_avgSum[i] / adcCounter;
				adc_avgSum[i] = 0;
			}
			adcCounter = 0;
			adcready = 1;
		}
	}
}

void HAL_GPIO_EXTI_Callback(uint16_t GPIO_Pin)
{
	if (GPIO_Pin == Button0_Pin)
	{
		saveDefaultSettings();
		NVIC_SystemReset();
	}
}

static void toByteArray(const void *val, const void *byteArray, const uint8_t size)
{
	for (uint8_t i = 0; i < size; i++)
	{
		*((uint8_t *)byteArray + i) = *((uint8_t *)val + i);
	}
}

static float calcTemp(uint16_t adcVal)
{
	volatile uint16_t *TS_CAL1 = (__IO uint16_t *)(0x1FFFF7B8U);
	volatile uint16_t *TS_CAL2 = (__IO uint16_t *)(0x1FFFF7C2U);
	float vdd = 0xFFF / (float)adc_data[3] * VREF_VOLTAGE;
	float m = (3. / vdd);
	float cal1 = *TS_CAL1 * m;
	float cal2 = *TS_CAL2 * m;
	return ((float)(110 - 30)) / (cal2 - cal1) * (adcVal * m - cal1) + 30;
}

void etxEnable(uint8_t val)
{
	if (termalProtectionState) val = 0;
	else HAL_GPIO_WritePin(GPIOB, Transmitter_Enable_Pin, val);
	etxEnableCurrentState = val;
}

void setETXMode(txControlType mode)
{
	etxControl = mode;
}

void runCalibration()
{
	calibrationOffTimer = timer1k + 180000;
	etxNoLoadCurrentMax = 0.1;
	calibrationMode = 1;
}

void setCalibrationValue(const float value)
{
	etxNoLoadCurrentMax = value;
}

/* USER CODE END 0 */

/**
  * @brief  The application entry point.
  * @retval int
  */
int main(void)
{
  /* USER CODE BEGIN 1 */
	Remap_Table();
  /* USER CODE END 1 */

  /* MCU Configuration--------------------------------------------------------*/

  /* Reset of all peripherals, Initializes the Flash interface and the Systick. */
  HAL_Init();

  /* USER CODE BEGIN Init */

  /* USER CODE END Init */

  /* Configure the system clock */
  SystemClock_Config();

  /* USER CODE BEGIN SysInit */
  HAL_Delay(1000);
  /* USER CODE END SysInit */

  /* Initialize all configured peripherals */
  MX_GPIO_Init();
  MX_DMA_Init();
  MX_ADC_Init();
  MX_DAC_Init();
  MX_TSC_Init();
  MX_TIM1_Init();
  MX_TOUCHSENSING_Init();
  MX_USB_DEVICE_Init();
  MX_TIM2_Init();
  MX_TIM7_Init();
  MX_IWDG_Init();

  /* Initialize interrupts */
  MX_NVIC_Init();
  /* USER CODE BEGIN 2 */
  LEDEnable(1);
  initBacklightMode(DefaultPresetMode);
  HAL_FLASH_Unlock();
  EE_Init();
  HAL_FLASH_Lock();
  loadSettings();
  HAL_TIM_Base_Start_IT(&htim7);
  HAL_TIM_Base_Start_IT(&htim2);
  HAL_TIM_Base_Start(&htim1);

  HAL_TIM_PWM_Start(&htim1, PWM_CHANNEL_RED);
  HAL_TIM_PWM_Start(&htim1, PWM_CHANNEL_BLUE);
  HAL_TIM_PWM_Start(&htim1, PWM_CHANNEL_GREEN);
  HAL_ADCEx_Calibration_Start(&hadc);
  HAL_ADC_Start_DMA(&hadc, adc_data, 4);
  HAL_DAC_Start(&hdac, DAC_CHANNEL_1);

  etxEnable(0);

  /* USER CODE END 2 */

  /* Infinite loop */
  /* USER CODE BEGIN WHILE */
  for (uint32_t tick = 0; ; tick++)
  {
	  HAL_IWDG_Refresh(&hiwdg);
	  static uint8_t c = 0; //big magic
	  static uint32_t tmbu = 0; // период
	  if (tmbu < timer1k)
	  {
		  if ((tsl_user_Exec() != TSL_USER_STATUS_BUSY))
		  {
			  if (c == 0) setButtonPressDetectMode(0, 0, 0, 0);
			  if (!getLEDState()) LEDEnable(0);
			  if (MyTKeys[0].p_Data->StateId == TSL_STATEID_DETECT || MyTKeys[0].p_Data->StateId == TSL_STATEID_DEB_RELEASE_DETECT)
			  {
				  if (c < 100) c++;
				  if (c > 15)
				  {
					  float brt;
					  if (c < 20) brt = 0;
					  else if (c > 70) brt = 1.;
					  	  else brt = (c - 20) * 0.02;
					  setBrightnessMultiplier(brt);
				  }
				  setButtonPressDetectMode(1, 0, 2000 * brightnessMul, 2000 * brightnessMul);
			  }else
			  {
				  if ((c > 3) && (c < 15))
				  {
					  if (getLEDState())
					  {
						  LEDEnable(0);
						  saveInt(SAVE_ADDRESS_LED_ENABLE, 0);
						  setButtonPressDetectMode(1, 2000, 0, 0);
					  }else
					  {
						  LEDEnable(1);
						  saveInt(SAVE_ADDRESS_LED_ENABLE, 1);
						  setButtonPressDetectMode(1, 0, 2000, 0);
					  }
				  }else
				  {
					  if ((c > 15) && (c < 100)) saveFloat(SAVE_ADDRESS_LED_BRIGHTNESS_MULTIPLIER, brightnessMul);
					  else
					  {
						  if (c >= 100)
						  {
							  if (etxControl == Off)
							  {
								  etxControl = Auto;
								  etxEnable(1);
								  saveInt(SAVE_ADDRESS_ETX_MODE, etxControl);
								  setButtonPressDetectMode(1, 0, 2000, 0);
							  }else
							  {
								  etxControl = Off;
								  saveInt(SAVE_ADDRESS_ETX_MODE, etxControl);
								  etxEnable(0);
								  setButtonPressDetectMode(1, 2000, 0, 0);
							  }
						  }
					  }
				  }
				  c = 0;
			  }
		  }

		  tmbu = timer1k + 200;
	  }

	  static uint32_t tmtpu = 0;
	  if (tmtpu < timer1k)
	  {
		  static uint16_t step = 0;
		  if (etxControl != Off) //magic
		  {
			  if (step == 0)
			  {
				  HAL_DAC_SetValue(&hdac, DAC_CHANNEL_1, DAC_ALIGN_12B_R, 0xFFF);
				  etxEnable(1);
				  step = 1;
			  }else if (step <= 3)
			  {
				  if (etxControl != ContinuousLowPower) HAL_DAC_SetValue(&hdac, DAC_CHANNEL_1, DAC_ALIGN_12B_R, ETX_VOLTAGE_DAC_VALUE);
				  step++;
			  }else if (step == 4)
			  {
				  if (etxControl == Auto)
				  {
					  if (((float)adc_avgData[1] * ADC_TO_CURRENT_VALUE_MULTIPLIER) < etxNoLoadCurrentMax)
					  {
						  HAL_DAC_SetValue(&hdac, DAC_CHANNEL_1, DAC_ALIGN_12B_R, ETX_VOLTAGE_DAC_VALUE);
						  step = ETX_AUTO_DISABLED_PART_START;
					  }else
					  {
						  step = ETX_AUTO_ENABLED_PART_START;
					  }
				  }else step = 3;
			  }else
				  if ((step >= ETX_AUTO_DISABLED_PART_START) && (step < ETX_AUTO_DISABLED_PART_END))
				  {
					  step++;
					  if (etxEnableCurrentState && calibrationMode)
					  {
						  float v = (float)adc_avgData[1] * ADC_TO_CURRENT_VALUE_MULTIPLIER * 1.1;
						  if (v < etxNoLoadCurrentMax) etxNoLoadCurrentMax = v;
					  }
					  etxEnable(0);
				  }else
					  if ((step >= ETX_AUTO_ENABLED_PART_START) && (step < ETX_AUTO_DISABLED_PART_START))
					  {
						  step++;
						  float v = (float)adc_avgData[1] * ADC_TO_CURRENT_VALUE_MULTIPLIER;
						  if (v < etxNoLoadCurrentMax)
						  {
							  if (calibrationMode)
							  {
								  v *= 1.1;
								  if (v < etxNoLoadCurrentMax) etxNoLoadCurrentMax = v;
							  }
							  step = 4;
						  }
					  }else step = 0;
		  }else{ etxEnable(0); step = 0; }
		  if ((calibrationOffTimer < timer1k) && calibrationMode)
		  {
			  calibrationMode = 0;
			  saveFloat(SAVE_ADDRESS_ETX_CALIBRATION, etxNoLoadCurrentMax);
		  }
		  tmtpu = timer1k + 100;
	  }

	  if (adcready)
	  {
		  uint8_t str[24] = {0xFA, 0xFE};
		  float f = adc_avgData[1] / 4095. * 3.3 / 1.5;
		  toByteArray(&f, str + 2, sizeof(float)); //Average current
		  f = calcTemp(adc_avgData[2]);

		  //Выключение при превышении температуры
		  if (f > TERMAL_PROTECTION_ON_TEMPERATURE){ etxEnable(0); termalProtectionState = 1;}
		  if ((f < TERMAL_PROTECTION_OFF_TEMPERATURE) && termalProtectionState) termalProtectionState = 0;
		  //
		  toByteArray(&f, str + 6, sizeof(float)); //IC temp
		  str[10] = c; //sens button state
		  f = (float)adc_avgData[0] / adc_avgData[3] * VREF_VOLTAGE * 11;
		  toByteArray(&f, str + 11, sizeof(float));
		  str[15] = '\r';
		  str[16] = '\n';
		  for(uint32_t i = 0; (((USBD_CDC_HandleTypeDef*)(hUsbDeviceFS.pClassData))->TxState != 0) && (i < 1000); i++);
		  if (((USBD_CDC_HandleTypeDef*)(hUsbDeviceFS.pClassData))->TxState == 0) CDC_Transmit_FS(str, 17);

		  adcready = 0;

		  if (dataSendRequest)
		  {
			  uint8_t str[32] = {0xFB, 0xFE};
			  float f;
			  str[2] = etxControl;
			  f = etxNoLoadCurrentMax;
			  toByteArray(&f, str + 3, sizeof(float));
			  str[7] = getLEDState();
			  str[8] = backlight_mode;
			  f = backlight_data.rgb_red;
			  toByteArray(&f, str + 9, sizeof(float));
			  f = backlight_data.rgb_green;
			  toByteArray(&f, str + 13, sizeof(float));
			  f = backlight_data.rgb_blue;
			  toByteArray(&f, str + 17, sizeof(float));
			  f = backlight_data.brightness;
			  toByteArray(&f, str + 21, sizeof(float));
			  f = brightnessMul;
			  toByteArray(&f, str + 25, sizeof(float));
			  str[29] = '\r';
			  str[30] = '\n';
			  for(uint32_t i = 0; (((USBD_CDC_HandleTypeDef*)(hUsbDeviceFS.pClassData))->TxState != 0) && (i < 1000); i++);
			  if (((USBD_CDC_HandleTypeDef*)(hUsbDeviceFS.pClassData))->TxState == 0) CDC_Transmit_FS(str, 31);

			  dataSendRequest = 0;
		  }
	  }
	  if (adc_avgData[1] >= 0xdff)
	  {

	  }
    /* USER CODE END WHILE */

    /* USER CODE BEGIN 3 */
  }
  /* USER CODE END 3 */
}

/**
  * @brief System Clock Configuration
  * @retval None
  */
void SystemClock_Config(void)
{
  RCC_OscInitTypeDef RCC_OscInitStruct = {0};
  RCC_ClkInitTypeDef RCC_ClkInitStruct = {0};
  RCC_PeriphCLKInitTypeDef PeriphClkInit = {0};

  /**Initializes the CPU, AHB and APB busses clocks 
  */
  RCC_OscInitStruct.OscillatorType = RCC_OSCILLATORTYPE_LSI|RCC_OSCILLATORTYPE_HSE;
  RCC_OscInitStruct.HSEState = RCC_HSE_ON;
  RCC_OscInitStruct.LSIState = RCC_LSI_ON;
  RCC_OscInitStruct.PLL.PLLState = RCC_PLL_ON;
  RCC_OscInitStruct.PLL.PLLSource = RCC_PLLSOURCE_HSE;
  RCC_OscInitStruct.PLL.PLLMUL = RCC_PLL_MUL4;
  RCC_OscInitStruct.PLL.PREDIV = RCC_PREDIV_DIV1;
  if (HAL_RCC_OscConfig(&RCC_OscInitStruct) != HAL_OK)
  {
    Error_Handler();
  }
  /**Initializes the CPU, AHB and APB busses clocks 
  */
  RCC_ClkInitStruct.ClockType = RCC_CLOCKTYPE_HCLK|RCC_CLOCKTYPE_SYSCLK
                              |RCC_CLOCKTYPE_PCLK1;
  RCC_ClkInitStruct.SYSCLKSource = RCC_SYSCLKSOURCE_PLLCLK;
  RCC_ClkInitStruct.AHBCLKDivider = RCC_SYSCLK_DIV1;
  RCC_ClkInitStruct.APB1CLKDivider = RCC_HCLK_DIV1;

  if (HAL_RCC_ClockConfig(&RCC_ClkInitStruct, FLASH_LATENCY_1) != HAL_OK)
  {
    Error_Handler();
  }
  PeriphClkInit.PeriphClockSelection = RCC_PERIPHCLK_USB;
  PeriphClkInit.UsbClockSelection = RCC_USBCLKSOURCE_PLL;

  if (HAL_RCCEx_PeriphCLKConfig(&PeriphClkInit) != HAL_OK)
  {
    Error_Handler();
  }
}

/**
  * @brief NVIC Configuration.
  * @retval None
  */
static void MX_NVIC_Init(void)
{
  /* TSC_IRQn interrupt configuration */
  HAL_NVIC_SetPriority(TSC_IRQn, 2, 0);
  HAL_NVIC_EnableIRQ(TSC_IRQn);
  /* DMA1_Channel1_IRQn interrupt configuration */
  HAL_NVIC_SetPriority(DMA1_Channel1_IRQn, 1, 0);
  HAL_NVIC_EnableIRQ(DMA1_Channel1_IRQn);
  /* TIM2_IRQn interrupt configuration */
  HAL_NVIC_SetPriority(TIM2_IRQn, 2, 0);
  HAL_NVIC_EnableIRQ(TIM2_IRQn);
  /* TIM7_IRQn interrupt configuration */
  HAL_NVIC_SetPriority(TIM7_IRQn, 2, 0);
  HAL_NVIC_EnableIRQ(TIM7_IRQn);
}

/**
  * @brief ADC Initialization Function
  * @param None
  * @retval None
  */
static void MX_ADC_Init(void)
{

  /* USER CODE BEGIN ADC_Init 0 */

  /* USER CODE END ADC_Init 0 */

  ADC_ChannelConfTypeDef sConfig = {0};

  /* USER CODE BEGIN ADC_Init 1 */

  /* USER CODE END ADC_Init 1 */
  /**Configure the global features of the ADC (Clock, Resolution, Data Alignment and number of conversion) 
  */
  hadc.Instance = ADC1;
  hadc.Init.ClockPrescaler = ADC_CLOCK_SYNC_PCLK_DIV4;
  hadc.Init.Resolution = ADC_RESOLUTION_12B;
  hadc.Init.DataAlign = ADC_DATAALIGN_RIGHT;
  hadc.Init.ScanConvMode = ADC_SCAN_DIRECTION_FORWARD;
  hadc.Init.EOCSelection = ADC_EOC_SINGLE_CONV;
  hadc.Init.LowPowerAutoWait = DISABLE;
  hadc.Init.LowPowerAutoPowerOff = DISABLE;
  hadc.Init.ContinuousConvMode = ENABLE;
  hadc.Init.DiscontinuousConvMode = DISABLE;
  hadc.Init.ExternalTrigConv = ADC_SOFTWARE_START;
  hadc.Init.ExternalTrigConvEdge = ADC_EXTERNALTRIGCONVEDGE_NONE;
  hadc.Init.DMAContinuousRequests = ENABLE;
  hadc.Init.Overrun = ADC_OVR_DATA_PRESERVED;
  if (HAL_ADC_Init(&hadc) != HAL_OK)
  {
    Error_Handler();
  }
  /**Configure for the selected ADC regular channel to be converted. 
  */
  sConfig.Channel = ADC_CHANNEL_0;
  sConfig.Rank = ADC_RANK_CHANNEL_NUMBER;
  sConfig.SamplingTime = ADC_SAMPLETIME_239CYCLES_5;
  if (HAL_ADC_ConfigChannel(&hadc, &sConfig) != HAL_OK)
  {
    Error_Handler();
  }
  /**Configure for the selected ADC regular channel to be converted. 
  */
  sConfig.Channel = ADC_CHANNEL_1;
  if (HAL_ADC_ConfigChannel(&hadc, &sConfig) != HAL_OK)
  {
    Error_Handler();
  }
  /**Configure for the selected ADC regular channel to be converted. 
  */
  sConfig.Channel = ADC_CHANNEL_TEMPSENSOR;
  if (HAL_ADC_ConfigChannel(&hadc, &sConfig) != HAL_OK)
  {
    Error_Handler();
  }
  /**Configure for the selected ADC regular channel to be converted. 
  */
  sConfig.Channel = ADC_CHANNEL_VREFINT;
  if (HAL_ADC_ConfigChannel(&hadc, &sConfig) != HAL_OK)
  {
    Error_Handler();
  }
  /* USER CODE BEGIN ADC_Init 2 */

  /* USER CODE END ADC_Init 2 */

}

/**
  * @brief DAC Initialization Function
  * @param None
  * @retval None
  */
static void MX_DAC_Init(void)
{

  /* USER CODE BEGIN DAC_Init 0 */

  /* USER CODE END DAC_Init 0 */

  DAC_ChannelConfTypeDef sConfig = {0};

  /* USER CODE BEGIN DAC_Init 1 */

  /* USER CODE END DAC_Init 1 */
  /**DAC Initialization 
  */
  hdac.Instance = DAC;
  if (HAL_DAC_Init(&hdac) != HAL_OK)
  {
    Error_Handler();
  }
  /**DAC channel OUT1 config 
  */
  sConfig.DAC_Trigger = DAC_TRIGGER_NONE;
  sConfig.DAC_OutputBuffer = DAC_OUTPUTBUFFER_ENABLE;
  if (HAL_DAC_ConfigChannel(&hdac, &sConfig, DAC_CHANNEL_1) != HAL_OK)
  {
    Error_Handler();
  }
  /* USER CODE BEGIN DAC_Init 2 */

  /* USER CODE END DAC_Init 2 */

}

/**
  * @brief IWDG Initialization Function
  * @param None
  * @retval None
  */
static void MX_IWDG_Init(void)
{

  /* USER CODE BEGIN IWDG_Init 0 */

  /* USER CODE END IWDG_Init 0 */

  /* USER CODE BEGIN IWDG_Init 1 */

  /* USER CODE END IWDG_Init 1 */
  hiwdg.Instance = IWDG;
  hiwdg.Init.Prescaler = IWDG_PRESCALER_256;
  hiwdg.Init.Window = 900;
  hiwdg.Init.Reload = 900;
  if (HAL_IWDG_Init(&hiwdg) != HAL_OK)
  {
    Error_Handler();
  }
  /* USER CODE BEGIN IWDG_Init 2 */

  /* USER CODE END IWDG_Init 2 */

}

/**
  * @brief TIM1 Initialization Function
  * @param None
  * @retval None
  */
static void MX_TIM1_Init(void)
{

  /* USER CODE BEGIN TIM1_Init 0 */

  /* USER CODE END TIM1_Init 0 */

  TIM_ClockConfigTypeDef sClockSourceConfig = {0};
  TIM_MasterConfigTypeDef sMasterConfig = {0};
  TIM_OC_InitTypeDef sConfigOC = {0};
  TIM_BreakDeadTimeConfigTypeDef sBreakDeadTimeConfig = {0};

  /* USER CODE BEGIN TIM1_Init 1 */

  /* USER CODE END TIM1_Init 1 */
  htim1.Instance = TIM1;
  htim1.Init.Prescaler = 0;
  htim1.Init.CounterMode = TIM_COUNTERMODE_UP;
  htim1.Init.Period = 2000;
  htim1.Init.ClockDivision = TIM_CLOCKDIVISION_DIV1;
  htim1.Init.RepetitionCounter = 0;
  htim1.Init.AutoReloadPreload = TIM_AUTORELOAD_PRELOAD_DISABLE;
  if (HAL_TIM_Base_Init(&htim1) != HAL_OK)
  {
    Error_Handler();
  }
  sClockSourceConfig.ClockSource = TIM_CLOCKSOURCE_INTERNAL;
  if (HAL_TIM_ConfigClockSource(&htim1, &sClockSourceConfig) != HAL_OK)
  {
    Error_Handler();
  }
  if (HAL_TIM_PWM_Init(&htim1) != HAL_OK)
  {
    Error_Handler();
  }
  sMasterConfig.MasterOutputTrigger = TIM_TRGO_RESET;
  sMasterConfig.MasterSlaveMode = TIM_MASTERSLAVEMODE_DISABLE;
  if (HAL_TIMEx_MasterConfigSynchronization(&htim1, &sMasterConfig) != HAL_OK)
  {
    Error_Handler();
  }
  sConfigOC.OCMode = TIM_OCMODE_PWM1;
  sConfigOC.Pulse = 500;
  sConfigOC.OCPolarity = TIM_OCPOLARITY_HIGH;
  sConfigOC.OCNPolarity = TIM_OCNPOLARITY_HIGH;
  sConfigOC.OCFastMode = TIM_OCFAST_DISABLE;
  sConfigOC.OCIdleState = TIM_OCIDLESTATE_RESET;
  sConfigOC.OCNIdleState = TIM_OCNIDLESTATE_RESET;
  if (HAL_TIM_PWM_ConfigChannel(&htim1, &sConfigOC, TIM_CHANNEL_1) != HAL_OK)
  {
    Error_Handler();
  }
  if (HAL_TIM_PWM_ConfigChannel(&htim1, &sConfigOC, TIM_CHANNEL_2) != HAL_OK)
  {
    Error_Handler();
  }
  if (HAL_TIM_PWM_ConfigChannel(&htim1, &sConfigOC, TIM_CHANNEL_3) != HAL_OK)
  {
    Error_Handler();
  }
  sBreakDeadTimeConfig.OffStateRunMode = TIM_OSSR_DISABLE;
  sBreakDeadTimeConfig.OffStateIDLEMode = TIM_OSSI_DISABLE;
  sBreakDeadTimeConfig.LockLevel = TIM_LOCKLEVEL_OFF;
  sBreakDeadTimeConfig.DeadTime = 0;
  sBreakDeadTimeConfig.BreakState = TIM_BREAK_DISABLE;
  sBreakDeadTimeConfig.BreakPolarity = TIM_BREAKPOLARITY_HIGH;
  sBreakDeadTimeConfig.AutomaticOutput = TIM_AUTOMATICOUTPUT_DISABLE;
  if (HAL_TIMEx_ConfigBreakDeadTime(&htim1, &sBreakDeadTimeConfig) != HAL_OK)
  {
    Error_Handler();
  }
  /* USER CODE BEGIN TIM1_Init 2 */

  /* USER CODE END TIM1_Init 2 */
  HAL_TIM_MspPostInit(&htim1);

}

/**
  * @brief TIM2 Initialization Function
  * @param None
  * @retval None
  */
static void MX_TIM2_Init(void)
{

  /* USER CODE BEGIN TIM2_Init 0 */

  /* USER CODE END TIM2_Init 0 */

  TIM_ClockConfigTypeDef sClockSourceConfig = {0};
  TIM_MasterConfigTypeDef sMasterConfig = {0};

  /* USER CODE BEGIN TIM2_Init 1 */

  /* USER CODE END TIM2_Init 1 */
  htim2.Instance = TIM2;
  htim2.Init.Prescaler = 47;
  htim2.Init.CounterMode = TIM_COUNTERMODE_UP;
  htim2.Init.Period = 999;
  htim2.Init.ClockDivision = TIM_CLOCKDIVISION_DIV1;
  htim2.Init.AutoReloadPreload = TIM_AUTORELOAD_PRELOAD_DISABLE;
  if (HAL_TIM_Base_Init(&htim2) != HAL_OK)
  {
    Error_Handler();
  }
  sClockSourceConfig.ClockSource = TIM_CLOCKSOURCE_INTERNAL;
  if (HAL_TIM_ConfigClockSource(&htim2, &sClockSourceConfig) != HAL_OK)
  {
    Error_Handler();
  }
  sMasterConfig.MasterOutputTrigger = TIM_TRGO_RESET;
  sMasterConfig.MasterSlaveMode = TIM_MASTERSLAVEMODE_DISABLE;
  if (HAL_TIMEx_MasterConfigSynchronization(&htim2, &sMasterConfig) != HAL_OK)
  {
    Error_Handler();
  }
  /* USER CODE BEGIN TIM2_Init 2 */

  /* USER CODE END TIM2_Init 2 */

}

/**
  * @brief TIM7 Initialization Function
  * @param None
  * @retval None
  */
static void MX_TIM7_Init(void)
{

  /* USER CODE BEGIN TIM7_Init 0 */

  /* USER CODE END TIM7_Init 0 */

  TIM_MasterConfigTypeDef sMasterConfig = {0};

  /* USER CODE BEGIN TIM7_Init 1 */

  /* USER CODE END TIM7_Init 1 */
  htim7.Instance = TIM7;
  htim7.Init.Prescaler = 47999;
  htim7.Init.CounterMode = TIM_COUNTERMODE_UP;
  htim7.Init.Period = 999;
  htim7.Init.AutoReloadPreload = TIM_AUTORELOAD_PRELOAD_DISABLE;
  if (HAL_TIM_Base_Init(&htim7) != HAL_OK)
  {
    Error_Handler();
  }
  sMasterConfig.MasterOutputTrigger = TIM_TRGO_RESET;
  sMasterConfig.MasterSlaveMode = TIM_MASTERSLAVEMODE_DISABLE;
  if (HAL_TIMEx_MasterConfigSynchronization(&htim7, &sMasterConfig) != HAL_OK)
  {
    Error_Handler();
  }
  /* USER CODE BEGIN TIM7_Init 2 */

  /* USER CODE END TIM7_Init 2 */

}

/**
  * @brief TSC Initialization Function
  * @param None
  * @retval None
  */
static void MX_TSC_Init(void)
{

  /* USER CODE BEGIN TSC_Init 0 */

  /* USER CODE END TSC_Init 0 */

  /* USER CODE BEGIN TSC_Init 1 */

  /* USER CODE END TSC_Init 1 */
  /**Configure the TSC peripheral 
  */
  htsc.Instance = TSC;
  htsc.Init.CTPulseHighLength = TSC_CTPH_2CYCLES;
  htsc.Init.CTPulseLowLength = TSC_CTPL_2CYCLES;
  htsc.Init.SpreadSpectrum = DISABLE;
  htsc.Init.SpreadSpectrumDeviation = 1;
  htsc.Init.SpreadSpectrumPrescaler = TSC_SS_PRESC_DIV1;
  htsc.Init.PulseGeneratorPrescaler = TSC_PG_PRESC_DIV4;
  htsc.Init.MaxCountValue = TSC_MCV_8191;
  htsc.Init.IODefaultMode = TSC_IODEF_OUT_PP_LOW;
  htsc.Init.SynchroPinPolarity = TSC_SYNC_POLARITY_FALLING;
  htsc.Init.AcquisitionMode = TSC_ACQ_MODE_NORMAL;
  htsc.Init.MaxCountInterrupt = DISABLE;
  htsc.Init.ChannelIOs = TSC_GROUP2_IO3;
  htsc.Init.ShieldIOs = TSC_GROUP3_IO2;
  htsc.Init.SamplingIOs = TSC_GROUP2_IO4|TSC_GROUP3_IO3;
  if (HAL_TSC_Init(&htsc) != HAL_OK)
  {
    Error_Handler();
  }
  /* USER CODE BEGIN TSC_Init 2 */

  /* USER CODE END TSC_Init 2 */

}

/** 
  * Enable DMA controller clock
  */
static void MX_DMA_Init(void) 
{
  /* DMA controller clock enable */
  __HAL_RCC_DMA1_CLK_ENABLE();

}

/**
  * @brief GPIO Initialization Function
  * @param None
  * @retval None
  */
static void MX_GPIO_Init(void)
{
  GPIO_InitTypeDef GPIO_InitStruct = {0};

  /* GPIO Ports Clock Enable */
  __HAL_RCC_GPIOF_CLK_ENABLE();
  __HAL_RCC_GPIOA_CLK_ENABLE();
  __HAL_RCC_GPIOB_CLK_ENABLE();

  /*Configure GPIO pin Output Level */
  HAL_GPIO_WritePin(GPIOB, Transmitter_Enable_Pin|StepUp_Enable_Pin, GPIO_PIN_RESET);

  /*Configure GPIO pin : PA2 */
  GPIO_InitStruct.Pin = GPIO_PIN_2;
  GPIO_InitStruct.Mode = GPIO_MODE_INPUT;
  GPIO_InitStruct.Pull = GPIO_PULLUP;
  HAL_GPIO_Init(GPIOA, &GPIO_InitStruct);

  /*Configure GPIO pin : Button0_Pin */
  GPIO_InitStruct.Pin = Button0_Pin;
  GPIO_InitStruct.Mode = GPIO_MODE_IT_FALLING;
  GPIO_InitStruct.Pull = GPIO_PULLUP;
  HAL_GPIO_Init(Button0_GPIO_Port, &GPIO_InitStruct);

  /*Configure GPIO pins : Transmitter_Enable_Pin StepUp_Enable_Pin */
  GPIO_InitStruct.Pin = Transmitter_Enable_Pin|StepUp_Enable_Pin;
  GPIO_InitStruct.Mode = GPIO_MODE_OUTPUT_PP;
  GPIO_InitStruct.Pull = GPIO_PULLDOWN;
  GPIO_InitStruct.Speed = GPIO_SPEED_FREQ_LOW;
  HAL_GPIO_Init(GPIOB, &GPIO_InitStruct);

  /* EXTI interrupt init*/
  HAL_NVIC_SetPriority(EXTI4_15_IRQn, 0, 0);
  HAL_NVIC_EnableIRQ(EXTI4_15_IRQn);

}

/* USER CODE BEGIN 4 */

/* USER CODE END 4 */

/**
  * @brief  This function is executed in case of error occurrence.
  * @retval None
  */
void Error_Handler(void)
{
  /* USER CODE BEGIN Error_Handler_Debug */
  /* User can add his own implementation to report the HAL error return state */

  /* USER CODE END Error_Handler_Debug */
}

#ifdef  USE_FULL_ASSERT
/**
  * @brief  Reports the name of the source file and the source line number
  *         where the assert_param error has occurred.
  * @param  file: pointer to the source file name
  * @param  line: assert_param error line source number
  * @retval None
  */
void assert_failed(char *file, uint32_t line)
{ 
  /* USER CODE BEGIN 6 */
  /* User can add his own implementation to report the file name and line number,
     tex: printf("Wrong parameters value: file %s on line %d\r\n", file, line) */
  /* USER CODE END 6 */
}
#endif /* USE_FULL_ASSERT */

/************************ (C) COPYRIGHT STMicroelectronics *****END OF FILE****/
