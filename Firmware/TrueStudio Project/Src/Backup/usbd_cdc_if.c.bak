/**
  ******************************************************************************
  * @file           : usbd_cdc_if.c
  * @version        : v2.0_Cube
  * @brief          : Usb device for Virtual Com Port.
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

/* Includes ------------------------------------------------------------------*/
#include "usbd_cdc_if.h"

/* USER CODE BEGIN INCLUDE */
#include "backlight.h"
#include "main.h"
#include "memSave.h"
/* USER CODE END INCLUDE */

/* Private typedef -----------------------------------------------------------*/
/* Private define ------------------------------------------------------------*/
/* Private macro -------------------------------------------------------------*/

/* USER CODE BEGIN PV */
/* Private variables ---------------------------------------------------------*/

/* USER CODE END PV */

/** @addtogroup STM32_USB_OTG_DEVICE_LIBRARY
  * @brief Usb device library.
  * @{
  */

/** @addtogroup USBD_CDC_IF
  * @{
  */

/** @defgroup USBD_CDC_IF_Private_TypesDefinitions USBD_CDC_IF_Private_TypesDefinitions
  * @brief Private types.
  * @{
  */

/* USER CODE BEGIN PRIVATE_TYPES */

/* USER CODE END PRIVATE_TYPES */

/**
  * @}
  */

/** @defgroup USBD_CDC_IF_Private_Defines USBD_CDC_IF_Private_Defines
  * @brief Private defines.
  * @{
  */

/* USER CODE BEGIN PRIVATE_DEFINES */
/* Define size for the receive and transmit buffer over CDC */
/* It's up to user to redefine and/or remove those define */
#define APP_RX_DATA_SIZE  1024
#define APP_TX_DATA_SIZE  1024
/* USER CODE END PRIVATE_DEFINES */

/**
  * @}
  */

/** @defgroup USBD_CDC_IF_Private_Macros USBD_CDC_IF_Private_Macros
  * @brief Private macros.
  * @{
  */

/* USER CODE BEGIN PRIVATE_MACRO */

/* USER CODE END PRIVATE_MACRO */

/**
  * @}
  */

/** @defgroup USBD_CDC_IF_Private_Variables USBD_CDC_IF_Private_Variables
  * @brief Private variables.
  * @{
  */
/* Create buffer for reception and transmission           */
/* It's up to user to redefine and/or remove those define */
/** Received data over USB are stored in this buffer      */
uint8_t UserRxBufferFS[APP_RX_DATA_SIZE];

/** Data to send over USB CDC are stored in this buffer   */
uint8_t UserTxBufferFS[APP_TX_DATA_SIZE];

/* USER CODE BEGIN PRIVATE_VARIABLES */

/* USER CODE END PRIVATE_VARIABLES */

/**
  * @}
  */

/** @defgroup USBD_CDC_IF_Exported_Variables USBD_CDC_IF_Exported_Variables
  * @brief Public variables.
  * @{
  */

extern USBD_HandleTypeDef hUsbDeviceFS;

/* USER CODE BEGIN EXPORTED_VARIABLES */

/* USER CODE END EXPORTED_VARIABLES */

/**
  * @}
  */

/** @defgroup USBD_CDC_IF_Private_FunctionPrototypes USBD_CDC_IF_Private_FunctionPrototypes
  * @brief Private functions declaration.
  * @{
  */

static int8_t CDC_Init_FS(void);
static int8_t CDC_DeInit_FS(void);
static int8_t CDC_Control_FS(uint8_t cmd, uint8_t* pbuf, uint16_t length);
static int8_t CDC_Receive_FS(uint8_t* pbuf, uint32_t *Len);

/* USER CODE BEGIN PRIVATE_FUNCTIONS_DECLARATION */
inline float byteToFloat(const uint8_t *bytes)
{
    return *((float *)bytes);
}
/* USER CODE END PRIVATE_FUNCTIONS_DECLARATION */

/**
  * @}
  */

USBD_CDC_ItfTypeDef USBD_Interface_fops_FS =
{
  CDC_Init_FS,
  CDC_DeInit_FS,
  CDC_Control_FS,
  CDC_Receive_FS
};

/* Private functions ---------------------------------------------------------*/
/**
  * @brief  Initializes the CDC media low layer over the FS USB IP
  * @retval USBD_OK if all operations are OK else USBD_FAIL
  */
static int8_t CDC_Init_FS(void)
{
  /* USER CODE BEGIN 3 */
  /* Set Application Buffers */
  USBD_CDC_SetTxBuffer(&hUsbDeviceFS, UserTxBufferFS, 0);
  USBD_CDC_SetRxBuffer(&hUsbDeviceFS, UserRxBufferFS);
  return (USBD_OK);
  /* USER CODE END 3 */
}

/**
  * @brief  DeInitializes the CDC media low layer
  * @retval USBD_OK if all operations are OK else USBD_FAIL
  */
static int8_t CDC_DeInit_FS(void)
{
  /* USER CODE BEGIN 4 */
  return (USBD_OK);
  /* USER CODE END 4 */
}

/**
  * @brief  Manage the CDC class requests
  * @param  cmd: Command code
  * @param  pbuf: Buffer containing command data (request parameters)
  * @param  length: Number of data to be sent (in bytes)
  * @retval Result of the operation: USBD_OK if all operations are OK else USBD_FAIL
  */
static int8_t CDC_Control_FS(uint8_t cmd, uint8_t* pbuf, uint16_t length)
{
  /* USER CODE BEGIN 5 */
  switch(cmd)
  {
    case CDC_SEND_ENCAPSULATED_COMMAND:

    break;

    case CDC_GET_ENCAPSULATED_RESPONSE:

    break;

    case CDC_SET_COMM_FEATURE:

    break;

    case CDC_GET_COMM_FEATURE:

    break;

    case CDC_CLEAR_COMM_FEATURE:

    break;

  /*******************************************************************************/
  /* Line Coding Structure                                                       */
  /*-----------------------------------------------------------------------------*/
  /* Offset | Field       | Size | Value  | Description                          */
  /* 0      | dwDTERate   |   4  | Number |Data terminal rate, in bits per second*/
  /* 4      | bCharFormat |   1  | Number | Stop bits                            */
  /*                                        0 - 1 Stop bit                       */
  /*                                        1 - 1.5 Stop bits                    */
  /*                                        2 - 2 Stop bits                      */
  /* 5      | bParityType |  1   | Number | Parity                               */
  /*                                        0 - None                             */
  /*                                        1 - Odd                              */
  /*                                        2 - Even                             */
  /*                                        3 - Mark                             */
  /*                                        4 - Space                            */
  /* 6      | bDataBits  |   1   | Number Data bits (5, 6, 7, 8 or 16).          */
  /*******************************************************************************/
    case CDC_SET_LINE_CODING:

    break;

    case CDC_GET_LINE_CODING:

    break;

    case CDC_SET_CONTROL_LINE_STATE:

    break;

    case CDC_SEND_BREAK:

    break;

  default:
    break;
  }

  return (USBD_OK);
  /* USER CODE END 5 */
}

/**
  * @brief  Data received over USB OUT endpoint are sent over CDC interface
  *         through this function.
  *
  *         @note
  *         This function will block any OUT packet reception on USB endpoint
  *         untill exiting this function. If you exit this function before transfer
  *         is complete on CDC interface (ie. using DMA controller) it will result
  *         in receiving more data while previous ones are still not sent.
  *
  * @param  Buf: Buffer of data to be received
  * @param  Len: Number of data received (in bytes)
  * @retval Result of the operation: USBD_OK if all operations are OK else USBD_FAIL
  */
static int8_t CDC_Receive_FS(uint8_t* Buf, uint32_t *Len)
{
  /* USER CODE BEGIN 6 */
  USBD_CDC_SetRxBuffer(&hUsbDeviceFS, &Buf[0]);

  const uint32_t cb0 = ('l' << 24) | ('c' << 16) | ('-' << 8) | '-'; //colour
  const uint32_t cb1 = ('t' << 24) | ('b' << 16) | ('-' << 8) | '-'; //brightness
  const uint32_t cb2 = ('d' << 24) | ('m' << 16) | ('-' << 8) | '-'; //mode
  const uint32_t cb3 = ('s' << 24) | ('c' << 16) | ('-' << 8) | '-'; //custom mode setup block (count)
  const uint32_t cb4 = ('d' << 24) | ('c' << 16) | ('-' << 8) | '-'; //custom mode data block (step, r, g, b, brt)
  const uint32_t cb5 = ('c' << 24) | ('c' << 16) | ('-' << 8) | '-'; //custom mode data copy (from, to)
  const uint32_t cb6 = ('v' << 24) | ('s' << 16) | ('-' << 8) | '-'; //save profile
  const uint32_t cb7 = ('m' << 24) | ('b' << 16) | ('-' << 8) | '-'; //brightness mul
  const uint32_t cb8 = ('t' << 24) | ('s' << 16) | ('r' << 8) | '-'; //reload
  const uint32_t cb9 = ('l' << 24) | ('a' << 16) | ('c' << 8) | '-'; //calibration
  const uint32_t cb10 = ('x' << 24) | ('t' << 16) | ('-' << 8) | '-'; //etx control
  const uint32_t cb11 = ('r' << 24) | ('d' << 16) | ('-' << 8) | '-'; //data send request

//  const uint32_t c0 = ('b' << 24) | ('g' << 16) | ('r' << 8) | '-';
//  const uint32_t c1 = ('t' << 24) | ('r' << 16) | ('b' << 8) | '-';
//  const uint32_t c2 = ('d' << 24) | ('o' << 16) | ('m' << 8) | '-';
//
//  const uint32_t c3 = ('b' << 24) | ('s' << 16) | ('c' << 8) | '-'; //custom mode setup block (count)
//  const uint32_t c4 = ('b' << 24) | ('d' << 16) | ('c' << 8) | '-'; //custom mode data block (step, r, g, b, brt)
//  const uint32_t c5 = ('c' << 24) | ('d' << 16) | ('c' << 8) | '-'; //custom mode data copy (from, to)
//  const uint32_t c6 = ('v' << 24) | ('s' << 16) | ('p' << 8) | '-'; //save profile

  USBD_CDC_ReceivePacket(&hUsbDeviceFS);

  if (*(uint32_t *)Buf == cb0)
  {
	setRGBColour(byteToFloat(Buf + 4), byteToFloat(Buf + 8), byteToFloat(Buf + 12));
	return USBD_OK;
  }else if (*(uint32_t *)Buf == cb1)
  {
	setRGBBrightness(byteToFloat(Buf + 4));
	return USBD_OK;
  }else if (*(uint32_t *)Buf == cb2)
  {
	initBacklightMode(*(uint8_t *)(Buf + 4));
	return USBD_OK;
  }else if (*(uint32_t *)Buf == cb3)
  {
	  backlight_userdata_steps = Buf[4];
	return USBD_OK;
  }else if (*(uint32_t *)Buf == cb4)
  {
	backlight_userdata[Buf[4]].target_brightness = byteToFloat(Buf + 8);
	backlight_userdata[Buf[4]].step_brightness = byteToFloat(Buf + 12);
	backlight_userdata[Buf[4]].target_red = byteToFloat(Buf + 16);
	backlight_userdata[Buf[4]].step_red = byteToFloat(Buf + 20);
	backlight_userdata[Buf[4]].target_green = byteToFloat(Buf + 24);
	backlight_userdata[Buf[4]].step_green = byteToFloat(Buf + 28);
	backlight_userdata[Buf[4]].target_blue = byteToFloat(Buf + 32);
	backlight_userdata[Buf[4]].step_blue = byteToFloat(Buf + 36);
	return USBD_OK;
  }else if (*(uint32_t *)Buf == cb5)
  {
	backlightUserDataCopy(Buf[4], Buf[5]);
	return USBD_OK;
  }else if (*(uint32_t *)Buf == cb6)
  {
	  if (*Len > 4)
	  {
		  saveSettings(*(Buf + 4));
	  }else saveSettings(-1);
	  return USBD_OK;
  }else if (*(uint32_t *)Buf == cb7)
  {
	  setBrightnessMultiplier(byteToFloat(Buf + 4));
	  return USBD_OK;
  }else if (*(uint32_t *)Buf == cb8)
  {
	  NVIC_SystemReset();
	  return USBD_OK;
  }else if (*(uint32_t *)Buf == cb9)
  {
	  if (*Len > 4)
	  {
		  setCalibrationValue(byteToFloat(Buf + 4));
	  }else runCalibration();
	  return USBD_OK;
  }else if (*(uint32_t *)Buf == cb10)
  {
	setETXMode(Buf[4]);
	return USBD_OK;
  }else if (*(uint32_t *)Buf == cb11)
  {
	dataSendRequest = 1;
	return USBD_OK;
  }

//  uint8_t str[128];
//  str[0] = 0;
//  memcpy(str, Buf, *Len);
//  str[*Len] = 0;
//
//  if (*(uint32_t *)str == c0) //colour
//  {
//
//	uint16_t r, g, b;
//	uint8_t *cs = strstr(str, " ");
//	uint8_t *ce = strstr(str, ",");
//	if ((cs == NULL) || (ce == NULL)) return USBD_OK;
//	ce[0] = 0;
//	r = atoi(cs + 1);
//	if (r > 255) r = 255;
//	cs = ce + 1;
//	*ce = strstr(cs, ",");
//	if ((cs == NULL) || (ce == NULL)) return USBD_OK;
//	ce[0] = 0;
//	g = atoi(cs);
//	if (g > 255) g = 255;
//	cs = ce + 1;
//	if ((cs == NULL)) return USBD_OK;
//	b = atoi(cs);
//	if (b > 255) b = 255;
//  	setRGBColour((float)r / 2.55, (float)g / 2.55, (float)b / 2.55);
//
//  }else if (*(uint32_t *)str == c1) //brightness
//  {
//
//	uint16_t b = 0;
//	uint8_t *cs = strstr(str, " ");
//	if (cs == NULL) return USBD_OK;
//	b = atoi(cs + 1);
//	if (b > 255) b = 255;
//	setRGBBrightness((float)b / 25.5);
//
//  }else if (*(uint32_t *)str == c2) //mode
//  {
//
//	uint16_t m = 0;
//	uint8_t *cs = strstr(str, " ");
//	if (cs == NULL) return USBD_OK;
//	m = atoi(cs + 1);
//	if (m) backlightSetMode(PresetMode);
//	else backlightSetMode(DirectMode);
//
//  }else if (*(uint32_t *)str == c3) //custom mode setup block (count)
//  {
//
//	uint16_t b = 0;
//	uint8_t *cs = strstr(str, " ");
//	if (cs == NULL) return USBD_OK;
//	b = atoi(cs + 1);
//	if (b > 16) b = 16;
//	backlight_userdata_steps = b;
//
//  }else if (*(uint32_t *)str == c4) //custom mode data block (step, brtt, brts, rt, rs, gt, gs, bt, bs )
//  {
//
//	uint8_t st;
//	float fdata[9];
//	uint8_t *cs = strstr(str, " ");
//	uint8_t *ce = strstr(str, ",");
//	if ((cs == NULL) || (ce == NULL)) return USBD_OK;
//	*ce = 0;
//	st = atoi(cs + 1);
//	if (st > 16) st = 16;
//	for (uint8_t i = 1; i < 8; i++)
//	{
//		cs = ce + 1;
//		*ce = strstr(cs, ",");
//		if ((cs == NULL) || (ce == NULL)) return USBD_OK;
//		*ce = 0;
//		if ((i & 0b1)) fdata[i] = (float)atoi(cs);
//		else fdata[i] = atof(cs);
//		if (fdata[i] > 255) fdata[i] = 255;
//	}
//	fdata[8] = atof(ce + 1);
//	if (fdata[8] > 255) fdata[8] = 255;
//
//	backlight_userdata[st].target_brightness = fdata[1] * 0.03921568627;
//	backlight_userdata[st].step_brightness = fdata[2] * 0.03921568627;
//	backlight_userdata[st].target_red = fdata[3] * 0.392156862745;
//	backlight_userdata[st].step_red = fdata[4] * 0.392156862745;
//	backlight_userdata[st].target_green = fdata[5] * 0.392156862745;
//	backlight_userdata[st].step_green = fdata[6] * 0.392156862745;
//	backlight_userdata[st].target_blue = fdata[7] * 0.392156862745;
//	backlight_userdata[st].step_blue = fdata[8] * 0.392156862745;
//
//  }else if (*(uint32_t *)str == c5) //custom mode data copy (from, to)
//  {
//
//	uint8_t pf, pt;
//	uint8_t *cs = strstr(str, " ");
//	uint8_t *ce = strstr(str, ",");
//	if ((cs == NULL) || (ce == NULL)) return USBD_OK;
//	*ce = 0;
//	pf = atoi(cs + 1);
//	if (pf > 16) pf = 16;
//	cs = ce + 1;
//	if ((cs == NULL) || (ce == NULL)) return USBD_OK;
//	pt = atoi(cs);
//	if (pt > 16) pt = 16;
//
//	backlight_userdata[pt] = backlight_userdata[pf];
//
//  }else if (*(uint32_t *)str == c6) //save profile
//  {
//	backlightSaveUserPreset();
//  }

  return (USBD_OK);
  /* USER CODE END 6 */
}

/**
  * @brief  CDC_Transmit_FS
  *         Data to send over USB IN endpoint are sent over CDC interface
  *         through this function.
  *         @note
  *
  *
  * @param  Buf: Buffer of data to be sent
  * @param  Len: Number of data to be sent (in bytes)
  * @retval USBD_OK if all operations are OK else USBD_FAIL or USBD_BUSY
  */
uint8_t CDC_Transmit_FS(uint8_t* Buf, uint16_t Len)
{
  uint8_t result = USBD_OK;
  /* USER CODE BEGIN 7 */
  USBD_CDC_HandleTypeDef *hcdc = (USBD_CDC_HandleTypeDef*)hUsbDeviceFS.pClassData;
  if (hcdc->TxState != 0){
    return USBD_BUSY;
  }
  USBD_CDC_SetTxBuffer(&hUsbDeviceFS, Buf, Len);
  result = USBD_CDC_TransmitPacket(&hUsbDeviceFS);
  /* USER CODE END 7 */
  return result;
}

/* USER CODE BEGIN PRIVATE_FUNCTIONS_IMPLEMENTATION */

/* USER CODE END PRIVATE_FUNCTIONS_IMPLEMENTATION */

/**
  * @}
  */

/**
  * @}
  */

/************************ (C) COPYRIGHT STMicroelectronics *****END OF FILE****/
