# Machine Maintenance Monitoring System

An IoT-based machine condition monitoring system using **ESP32**, **AHT20** (temperature & humidity), and **MPU6050** (3-axis vibration). Designed for predictive maintenance in smart factory environments with ultra-low power consumption for long-term battery operation.

---

## Features

- **Real-time vibration monitoring** — 3-axis acceleration (X/Y/Z) via MPU6050
- **Environmental monitoring** — Temperature & humidity via AHT20
- **Deep Sleep + Motion Interrupt** — ESP32 sleeps at ~55µA, wakes only on vibration or timer
- **Battery life ~260 days** using 2× 18650 cells (6000mAh)
- **WiFi TCP transmission** — JSON data streamed to C# dashboard
- **Anomaly detection** — Configurable WARN / ALARM thresholds
- **Adaptive sampling** — Burst mode on motion event, periodic status on timer

---

## System Architecture

```
┌─────────────────────────────────────────────────────┐
│              ESP32-WROOM-32U                        │
│                                                     │
│  AHT20 ──I2C──► Temp / Humidity                    │
│  MPU6050 ─I2C──► Accel X/Y/Z  ──► Motion INT       │
│                                        │            │
│  Deep Sleep ◄──────────────────────────┘            │
│       │                                             │
│       └──► WiFi TCP ──► JSON ──► C# Dashboard       │
└─────────────────────────────────────────────────────┘
```

---

## Hardware

| Component | Description |
|---|---|
| ESP32-WROOM-32U | Main MCU with external antenna |
| AHT20 | Temperature (±0.3°C) & Humidity (±2% RH), I2C 0x38 |
| MPU6050 | 3-axis accelerometer + gyroscope, I2C 0x68 |
| 2× 18650 Li-ion | 3.7V / 6000mAh (parallel), ~260 days battery life |

---

## Wiring

```
AHT20 / MPU6050          ESP32-WROOM-32U
────────────────         ───────────────
VCC             ──────►  3.3V
GND             ──────►  GND
SDA             ──────►  GPIO 21
SCL             ──────►  GPIO 22
MPU6050 INT     ──────►  GPIO 33    ← Motion wakeup
MPU6050 AD0     ──────►  GND       ← I2C address 0x68
```

---

## JSON Output Format

### Vibration packet (on motion event, 20 samples burst)
```json
{
  "type": "vib",
  "boot": 12,
  "idx": 1,
  "wake": "MOTION",
  "ax": 1.234,
  "ay": 0.521,
  "az": 9.812,
  "mag": 9.957,
  "status": "OK",
  "temp": 28.50,
  "hum": 65.30
}
```

### Status packet (every 60 seconds via timer)
```json
{
  "type": "status",
  "boot": 12,
  "totalWakes": 45,
  "motionWakes": 8,
  "temp": 28.50,
  "hum": 65.30
}
```

`status` field values: `OK` · `WARN` (≥ 2 m/s²) · `ALARM` (≥ 5 m/s²)

---

## Power Consumption

| Mode | Current |
|---|---|
| ESP32 deep sleep | ~10 µA |
| MPU6050 cycle mode | ~20 µA |
| AHT20 idle | ~25 µA |
| **Total sleep** | **~55 µA** |
| Active (wake) | ~80 mA |

**Estimated battery life** with 2× 18650 (6000mAh), assuming 10 motion events/hour:
- Timer wakes: 1/min × 0.4s active
- Motion wakes: 10/hour × 1.7s active
- **~260 days (~8.6 months)**

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
Edit `include/config.h` and fill in your WiFi SSID, password, and C# server IP.

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
| Adafruit AHTX0 | ^2.0.5 | AHT20 sensor driver |
| Adafruit Unified Sensor | ^1.1.14 | Sensor abstraction layer |
| ArduinoJson | ^7.0.0 | JSON serialization |

> MPU6050 communicates directly via `Wire.h` (no extra library needed).

---

## Configuration

Key parameters in `src/main.cpp`:

| Parameter | Default | Description |
|---|---|---|
| `TIMER_SLEEP_SEC` | 60 | Timer wakeup interval (seconds) |
| `VIB_BURST_SAMPLES` | 20 | Samples per motion wakeup |
| `VIB_BURST_DELAY` | 100 | ms between burst samples |
| `MOTION_THRESHOLD` | 15 | MPU6050 motion sensitivity (1 unit ≈ 2mg) |
| `THR_WARN` | 2.0 | Warning threshold (m/s²) |
| `THR_ALARM` | 5.0 | Alarm threshold (m/s²) |

---

## Project Structure

```
Machine-Maintenance-Monitoring-System/
├── src/
│   └── main.cpp              # Main firmware (ESP32 deep sleep logic)
├── include/
│   ├── config.example.h      # WiFi & TCP config template (safe to push)
│   └── config.h              # Your actual credentials (gitignored)
├── platformio.ini            # PlatformIO build configuration
└── README.md
```

---

## License

MIT License — feel free to use and modify for your own projects.
