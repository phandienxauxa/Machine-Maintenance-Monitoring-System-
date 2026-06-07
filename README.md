# Machine Maintenance Monitoring System

An IIoT-based machine condition monitoring node using **ESP32**, **AHT30** (temperature & humidity), and **MPU6050** (3-axis vibration). Designed for predictive maintenance in smart factory environments with ultra-low power consumption for long-term battery operation.

The Fast Fourier Transform (FFT) serves as the mathematical core of the system, transforming complex time-domain vibration data into a clear frequency spectrum to act as a diagnostic "microscope" for pinpointing specific mechanical failures.

The node eliminates on-device displays entirely — all monitoring is handled by a **C# DevExpress dashboard** over WiFi TCP, following industrial IoT conventions.

---

## Final Product
https://github.com/user-attachments/assets/50478319-17dd-43f2-b733-473b464219a0

## Demo verview
https://github.com/user-attachments/assets/dbac4ba8-4298-4473-b97f-ee65607bbbc8

---

## Features

- **Real-time vibration monitoring** — 3-axis acceleration (X/Y/Z) via MPU6050
- **Environmental monitoring** — Temperature & humidity via AHT30
- **Battery monitoring** — Voltage and percentage via ADC voltage divider (GPIO 34)
- **Deep Sleep + Motion Interrupt** — ESP32 wakes only on vibration (EXT0) or 5-minute timer
- **LED status indicators** — Red / Yellow / Green LEDs for WiFi connection feedback
- **Magnetic mounting** — Neodymium magnets embedded in 3D-printed enclosure for tool-free attachment to metal surfaces
- **WiFi TCP transmission** — JSON data streamed to C# DevExpress dashboard
- **Anomaly detection** — Configurable WARN / ALARM thresholds on vibration magnitude
- **Wake statistics** — Tracks total wakes vs. motion-triggered wakes for anomaly frequency analysis

---

## System Architecture

```
┌─────────────────────────────────────────────────────────┐
│                  ESP32-WROOM-32U                        │
│                                                         │
│  AHT30   ──I2C──► Temp / Humidity                       │
│  MPU6050 ──I2C──► Accel X/Y/Z  ──► Motion INT (EXT0)   │
│  ADC Divider ───► GPIO 34  ──► Battery Voltage/Percent  │
│                                          │              │
│  Deep Sleep ◄────────────────────────────┘              │
│       │                                                 │
│       └──► WiFi TCP ──► JSON ──► C# Dashboard           │
└─────────────────────────────────────────────────────────┘
```

---

## Hardware

| Component | Description |
|---|---|
| ESP32-WROOM-32U | Main MCU with external antenna |
| AHT30 | Temperature (±0.3°C) & Humidity (±2% RH), I2C 0x38 |
| MPU6050 | 3-axis accelerometer + gyroscope, I2C 0x68/0x69 (auto-detected) |
| 2× 18650 Li-ion (2S) | Series pack, ~7.4V, ~3000mAh |
| 2-Series Charging | Type-C, 6.0V(0%) - 8.4V(100%) | 
| MP1584EN buck converter | Steps 2S pack down to stable 3.3V |
| Voltage divider (22kΩ / 10kΩ) | Scales battery voltage to ADC-safe range |
| 3× status LEDs | Red (fail) GPIO 27 / Yellow (connecting) GPIO 26 / Green (success) GPIO 14 |

---

## Wiring

```
AHT30 / MPU6050          ESP32-WROOM-32U
────────────────         ───────────────
VCC             ──────►  3.3V
GND             ──────►  GND
SDA             ──────►  GPIO 21
SCL             ──────►  GPIO 22
MPU6050 INT     ──────►  GPIO 33    ← Motion wakeup (EXT0)
MPU6050 AD0     ──────►  GND        ← I2C address 0x68

Battery divider output ──► GPIO 34  ← ADC battery sensing
                                       (R1=22kΩ, R2=10kΩ, scale ×3.2)

LED Red    ──────────────► GPIO 27  ← WiFi fail
LED Yellow ──────────────► GPIO 26  ← WiFi connecting
LED Green  ──────────────► GPIO 14  ← WiFi success
```

---

## JSON Output Format

The system transmits three distinct packet types: vib for burst motion data, fft for spectral analysis, and status for routine environment updates.

### Vibration packet (motion wakeup — 20-sample burst)
```json
{
  "type": "vib",
  "boot": 12,
  "idx": 1,
  "wake": "MOTION",
  "ax": "1.234",
  "ay": "0.521",
  "az": "9.812",
  "mag": "9.957",
  "status": "OK",
  "temp": 28.50,
  "hum": 65.30,
  "vbat": "8.24",
  "pbat": "93.3",
  "totalWakes": 13,
  "motionWakes": 3
  "peakFreq": 10.50
}
```

### FFT packet (frequency spectrum data)
```json
{
  "type": "fft",
  "fft_data": [
    "0.0",
    "1.5",
    "4.2",
    "12.8",
    "..." 
  ]
}
```
### Status packet (timer wakeup — every 5 minutes)
```json
{
  "type": "status",
  "boot": 12,
  "totalWakes": 12,
  "motionWakes": 2,
  "temp": 28.50,
  "hum": 65.30,
  "vbat": "8.24",
  "pbat": "93.3"
}
```

`status` field values: `OK` · `WARN` (≥ 2.0 m/s²) · `ALARM` (≥ 5.0 m/s²)

`wake` field values: `MOTION` · `TIMER` · `RESET`

---

## Battery Monitoring

The 2S Li-ion pack (6.0V – 8.4V) is measured via a resistor divider that scales the voltage to the ESP32 ADC input range (0 – 3.3V).

```
Battery+ ──► R1 (22kΩ) ──┬──► R2 (10kΩ) ──► GND
                          └──► GPIO 34 (ADC)
```

| Pack voltage | ADC pin voltage | Reported percentage |
|---|---|---|
| 8.4V (full) | ~2.625V | 100% |
| 7.2V (nominal) | ~2.25V | 50% |
| 6.0V (empty) | ~1.875V | 0% |

The firmware averages 10 ADC samples per reading to reduce noise.

---

## Power Consumption & Battery Life 

| Mode | Current |
|---|---|
| ESP32 deep sleep | ~10 µA |
| MPU6050 motion-detect mode | ~20 µA |
| AHT30 idle | ~25 µA |
| MP1584EN + charging circuit (sleep overhead) | ~1–2 mA |
| Active (wake + WiFi TX) | ~120 mA |

**Battery life calculation** — 2S 18650 pack, ~3000mAh at 7.4V nominal:

| Parameter | Value |
|---|---|
| Gross capacity | 3000 mAh |
| MP1584EN efficiency | ~85% |
| Usable capacity (at 3.3V rail) | **2550 mAh** |
| Active current (WiFi TX) | ~120 mA |
| Active duration per wake | ~3 s |
| Energy per wake | 120 mA × (3/3600) h = **0.1 mAh** |
| **Max wake cycles** | 2550 / 0.1 = **~25,500 wakes** |

**At `TIMER_SLEEP_SEC = 300` (1 wake per 5 min = 288 wakes/day):**

> **~88 days (~3 months)** before recharge

> Note: The MP1584EN buck converter and charging circuit draw ~1–2 mA even during deep sleep, reducing real-world runtime by roughly 10–15% compared to a theoretical bare-chip calculation.

---

## Getting Started

### 1. Clone the repository
```bash
git clone https://github.com/phandienxauxa/Machine-Maintenance-Monitoring-System-.git
cd Machine-Maintenance-Monitoring-System-
```

### 2. Configure credentials
```bash
cp include/config.example.h include/config.h
```
Edit `include/config.h` and fill in your WiFi SSID, password, host IP, and TCP port.

### 3. Open in VS Code with PlatformIO
```bash
code .
```
PlatformIO will automatically install required libraries on first build.

### 4. Build & Upload
Click **Build** (✓) then **Upload** (→) in the PlatformIO toolbar, or:
```bash
pio run --target upload
```

### 5. Monitor output
```bash
pio device monitor
```

---

## Libraries

| Library | Version | Purpose |
|---|---|---|
| Adafruit AHTX0 | ^2.0.5 | AHT30 sensor driver |
| Adafruit Unified Sensor | ^1.1.14 | Sensor abstraction layer |
| ArduinoJson | ^7.0.0 | JSON serialization |
| ArduinoFFT | ^3.x.x | Fast-Fourier-Transform Processing |

> MPU6050 communicates directly via `Wire.h` with raw register access — no additional library required.

---

## Configuration

Key parameters in [src/main.cpp](src/main.cpp):

| Parameter | Default | Description |
|---|---|---|
| `TIMER_SLEEP_SEC` | 300 | Timer wakeup interval (seconds) |
| `VIB_BURST_SAMPLES` | 20 | Samples per motion wakeup |
| `VIB_BURST_DELAY` | 100 | ms between burst samples |
| `MOTION_THRESHOLD` | 15 | MPU6050 motion sensitivity (1 unit ≈ 2mg) |
| `MOTION_DURATION` | 2 | MPU6050 motion duration register |
| `THR_WARN` | 2.0 | Warning threshold (m/s²) |
| `THR_ALARM` | 5.0 | Alarm threshold (m/s²) |
| `BATTERY_MAX_VOLTAGE` | 8.4 | 2S full voltage (V) |
| `BATTERY_MIN_VOLTAGE` | 6.0 | 2S cutoff voltage (V) |

---

## Project Structure

```
aht30_mpu6050/
├── src/
│   └── main.cpp              # Firmware — deep sleep, sensors, JSON TX
├── include/
│   ├── config.example.h      # WiFi & TCP config template (safe to push)
│   └── config.h              # Your actual credentials (gitignored)
├── platformio.ini            # PlatformIO build configuration
└── README.md
```

---

## License

MIT License — feel free to use and modify for your own projects.
