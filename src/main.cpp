#include<Arduino.h>
#include<Adafruit_AHTX0.h>
#include<Wire.h>
#include<math.h>
#include<WiFi.h>
#include<ArduinoJson.h>
#include<esp_sleep.h>

// cau hinh chan ket noi
#define I2C_SDA           21
#define I2C_SCL           22
#define MPU_INT_PIN       GPIO_NUM_33
#define SERIAL_BAUD       115200

// cấu hình mạng wifi
const char* ssid      = "TeoHamChoi";       // Tên mạng
const char* password  = "hatmebietbay";     // mật khẩu mạng
const char* hostIP    = "172.20.10.2";      // địa chỉ IP của laptop khi đã kết nối vào mạng
const int   tcpPort   = 8888;               // cổng tcp để gửi dữ liệu qua C#

// cau hinh thoi gian ngu va thuc
#define TIMER_SLEEP_SEC   60            // cứ 60 giây esp đọc dữ liệu JSON 1 lần, có thể tăng lên khoảng 120 đến 300 để tiết kiệm pin (biểu đồ thưa hơn)
#define VIB_BURST_SAMPLES 20            // khi có rung, esp sẽ đọc 20 mẫu
#define VIB_BURST_DELAY   100           // tốn 2 giây để mpu đọc 20 mẫu, tức có rung, esp sẽ on 20 x 100ms = 2 giây để đọc và off lại.
#define SERIAL_FLUSH_MS   50

// cau hinh do nhay mpu6050
#define MOTION_THRESHOLD  15        // có thể tăng lên 20-30 để giảm nhạy trong trường hợp do nhiễu, hoặc giảm xuống 10-15 để tăng nhạy
#define MOTION_DURATION   2

// nguong canh bao rung
#define THR_WARN          2.0f
#define THR_ALARM         5.0f

// danh sach thanh ghi mpu6050
#define MPU_ADDR_LOW      0x68
#define MPU_ADDR_HIGH     0x69
#define REG_WHO_AM_I      0x75
#define REG_PWR_MGMT_1    0x6B
#define REG_PWR_MGMT_2    0x6C
#define REG_CONFIG        0x1A
#define REG_ACCEL_CFG     0x1C
#define REG_GYRO_CFG      0x1B
#define REG_ACCEL_XOUT    0x3B
#define REG_INT_ENABLE    0x38
#define REG_INT_PIN_CFG   0x37
#define REG_INT_STATUS    0x3A
#define REG_MOT_THR       0x1F
#define REG_MOT_DUR       0x20
#define REG_MOT_DETECT    0x69

#define ACCEL_SCALE       4096.0f
#define GYRO_SCALE        65.5f

// bien rtc luu tru giu lieu qua cac lan deep sleep
RTC_DATA_ATTR uint32_t bootCount   = 0;
RTC_DATA_ATTR float    rtcTemp     = 0.0f;
RTC_DATA_ATTR float    rtcHumi     = 0.0f;
RTC_DATA_ATTR bool     rtcEnvValid = false;
RTC_DATA_ATTR uint32_t totalWakes  = 0;
RTC_DATA_ATTR uint32_t motionWakes = 0;

// doi tuong cam bien
Adafruit_AHTX0 aht;
uint8_t mpuAddr = 0;

// cac ham giao tiep mpu6050
void mpuWriteReg(uint8_t reg, uint8_t val) {
  Wire.beginTransmission(mpuAddr);
  Wire.write(reg);
  Wire.write(val);
  Wire.endTransmission();
}

uint8_t mpuReadReg(uint8_t reg) {
  Wire.beginTransmission(mpuAddr);
  Wire.write(reg);
  Wire.endTransmission(false);
  Wire.requestFrom(mpuAddr, (uint8_t)1);
  return Wire.available() ? Wire.read() : 0xFF;
}

bool mpuReadAccel(float &ax, float &ay, float &az) {
  Wire.beginTransmission(mpuAddr);
  Wire.write(REG_ACCEL_XOUT);
  if (Wire.endTransmission(false) != 0) return false;
  Wire.requestFrom(mpuAddr, (uint8_t)6);
  if (Wire.available() < 6) return false;
  
  int16_t rx = (Wire.read() << 8) | Wire.read();
  int16_t ry = (Wire.read() << 8) | Wire.read();
  int16_t rz = (Wire.read() << 8) | Wire.read();
  
  const float G = 9.80665f;
  ax = (rx / ACCEL_SCALE) * G;
  ay = (ry / ACCEL_SCALE) * G;
  az = (rz / ACCEL_SCALE) * G;
  return true;
}

bool findMPU() {
  for (uint8_t addr : {MPU_ADDR_LOW, MPU_ADDR_HIGH}) {
    Wire.beginTransmission(addr);
    if (Wire.endTransmission() == 0) {
      mpuAddr = addr;
      return true;
    }
  }
  return false;
}

// khoi tao mpu6050 o che do binh thuong
void initMPU_Normal() {
  mpuWriteReg(REG_PWR_MGMT_1, 0x00);  delay(100);
  mpuWriteReg(REG_PWR_MGMT_1, 0x01);  delay(10);
  mpuWriteReg(REG_CONFIG,     0x03);
  mpuWriteReg(REG_ACCEL_CFG,  0x10);
  mpuWriteReg(REG_GYRO_CFG,   0x08);
  mpuWriteReg(REG_INT_ENABLE, 0x00);
  delay(50);
}

// khoi tao mpu6050 de danh thuc he thong bang chuyen dong
void initMPU_MotionWake() {
  mpuWriteReg(REG_PWR_MGMT_1, 0x00);  delay(50);
  mpuWriteReg(REG_PWR_MGMT_1, 0x01);  delay(10);

  mpuWriteReg(REG_PWR_MGMT_2, 0x47);

  mpuWriteReg(REG_MOT_THR,    MOTION_THRESHOLD);
  mpuWriteReg(REG_MOT_DUR,    MOTION_DURATION);
  mpuWriteReg(REG_MOT_DETECT, 0x15);

  mpuWriteReg(REG_INT_PIN_CFG, 0x20);

  mpuWriteReg(REG_INT_ENABLE,  0x40);

  mpuWriteReg(REG_PWR_MGMT_1, 0x29);

  delay(20);
}

// ham gui du lieu qua mang wifi 
void broadcastJson(String jsonStr) {
  WiFiClient client; // ESP32 la Client
  
  // Thu ket noi den IP cua Laptop
  if (client.connect(hostIP, tcpPort)) {
    client.println(jsonStr); // Gui JSON
    client.stop();           // Gui xong ngat ket noi ngay
  } else {
    Serial.println("Khong tim thay Server!!!");
  }
}

// dong goi va gui json qua serial
void sendVibJson(float ax, float ay, float az, float temp, float humi, const char* wakeReason, uint32_t sampleIdx) {
  float mag = sqrtf(ax*ax + ay*ay + az*az);
  const char* status = (mag >= THR_ALARM) ? "ALARM" : (mag >= THR_WARN)  ? "WARN" : "OK";

  JsonDocument doc;
  doc["type"]   = "vib";
  doc["boot"]   = bootCount;
  doc["idx"]    = sampleIdx;
  doc["wake"]   = wakeReason;
  doc["ax"]     = serialized(String(ax, 3));
  doc["ay"]     = serialized(String(ay, 3));
  doc["az"]     = serialized(String(az, 3));
  doc["mag"]    = serialized(String(mag, 3));
  doc["status"] = status;
  doc["temp"]   = temp;
  doc["hum"]    = humi;

  String output;
  serializeJson(doc, output);
  broadcastJson(output);
}

void sendStatusJson(float temp, float humi) {
  JsonDocument doc;
  doc["type"]        = "status";
  doc["boot"]        = bootCount;
  doc["totalWakes"]  = totalWakes;
  doc["motionWakes"] = motionWakes;
  doc["temp"]        = temp;
  doc["hum"]         = humi;

  String output;
  serializeJson(doc, output);
  broadcastJson(output);
}

// dua he thong vao trang thai ngu sau
void goToSleep() {
  mpuReadReg(REG_INT_STATUS);
  initMPU_MotionWake();
  WiFi.disconnect(true);
  WiFi.mode(WIFI_OFF);

  Wire.end();
  pinMode(I2C_SDA, INPUT);
  pinMode(I2C_SCL, INPUT);


  esp_sleep_enable_ext0_wakeup(MPU_INT_PIN, 1);
  esp_sleep_enable_timer_wakeup((uint64_t)TIMER_SLEEP_SEC * 1000000ULL);
  esp_deep_sleep_start();
}

// khoi dong va xu ly su kien
void setup() {
  // khoi dong cong serial de theo doi loi
  Serial.begin(115200);
  delay(200);

  bootCount++;
  totalWakes++;
  
  esp_sleep_wakeup_cause_t wakeReason = esp_sleep_get_wakeup_cause();
  const char* wakeStr = (wakeReason == ESP_SLEEP_WAKEUP_EXT0) ? "MOTION" : (wakeReason == ESP_SLEEP_WAKEUP_TIMER) ? "TIMER" : "RESET";
  if (wakeReason == ESP_SLEEP_WAKEUP_EXT0) motionWakes++;

  // bat wifi va doi ket noi
  Serial.println("dang ket noi wifi...");
  WiFi.begin(ssid, password);
  uint32_t timeout = millis();
  
  // tang thoi gian cho len 10000ms (10 giay) de kip bat hotspot
  while (WiFi.status() != WL_CONNECTED && millis() - timeout < 10000) {
    delay(100); 
    Serial.print(".");
  }
  Serial.println();

  // kiem tra xem co vao duoc mang khong
  if (WiFi.status() == WL_CONNECTED) {
    Serial.println("ket noi wifi thanh cong!");
  } else {
    Serial.println("loi mang! di ngu day!");
  }

Wire.begin(I2C_SDA, I2C_SCL);

if (!findMPU()) {
    esp_sleep_enable_timer_wakeup((uint64_t)TIMER_SLEEP_SEC * 1000000ULL);
    esp_deep_sleep_start();
  }
  initMPU_Normal();

  if (wakeReason == ESP_SLEEP_WAKEUP_EXT0) {
    for (uint32_t i = 0; i < VIB_BURST_SAMPLES; i++) {
      float ax, ay, az;
      if (mpuReadAccel(ax, ay, az)) {
        sendVibJson(ax, ay, az, rtcTemp, rtcHumi, wakeStr, i + 1);
      }
      delay(VIB_BURST_DELAY);
    }
  } else {
    bool ahtOK = aht.begin();
    if (ahtOK) {
      sensors_event_t humi_evt, temp_evt;
      aht.getEvent(&humi_evt, &temp_evt);
      rtcTemp    = temp_evt.temperature;
      rtcHumi    = humi_evt.relative_humidity;
      rtcEnvValid = true;
    }
    sendStatusJson(rtcTemp, rtcHumi);
    float ax, ay, az;
    if (mpuReadAccel(ax, ay, az)) {
      sendVibJson(ax, ay, az, rtcTemp, rtcHumi, wakeStr, 1);
    }
  }

  delay(200); 
  goToSleep();
}

// ham loop bi bo qua do dung che do deep sleep
void loop() {
}