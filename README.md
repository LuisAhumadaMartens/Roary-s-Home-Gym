# Roary's Home Gym  
AR/VR Sports 

![Roary's Home Gym logo](ReadMeAssets/logo.svg)

## Table of Contents  
- [Introduction](#introduction)  
- [Why a VR game?](#why-a-vr-game)  
- [Our structure](#our-structure)  
- [Requirements](#requirements)  
- [Implementation](#implementation)  
- [Installation](#installation)  
- [Summary](#summary)  

---

## Introduction  
**Roary's Home Gym** is a virtual reality-based fitness platform that provides an immersive and engaging home workout experience. Featuring our school mascot, Roary, as a personal trainer, the system combines VR technology and gamification to address common challenges in home fitness. 

---

## Why a VR game?  
Many people struggle with maintaining a consistent home workout routine due to:
- Lack of motivation and proper guidance
- Perceived ineffectiveness of home workouts  
- Risk of injury from incorrect exercise form  

Roary's Home Gym addresses these challenges by providing a structured, fun, and interactive VR workout environment designed for small spaces, featuring engaging mini-games.

---

## Our structure   
- **Interactive Virtual Trainer**: Roary, the FIU mascot, guides users through their workouts.
- **Workout Modes**: Includes exercises like Squats, Windmills, Push-Ups, Calf Raises, Side-Bends, and Elbow Raises.
- **Gamification Elements**: The workouts are presented as mini-games, inspired by Microsoft Xbox Kinect and Nintendo Wii Sports.
- **Space Optimization**: Designed for a compact 2 m x 1.5 m area, ideal for small rooms.

---

## Requirements  
To use Roary's Home Gym, users need:  
1. A VR headset (e.g., Meta Quest 3, HTC Vive) with motion controllers
2. A workout area of at least 2 m x 1.5 m (6 ft 6 in x 5 ft)
3. Basic familiarity with VR hardware and software  

No additional equipment is required, as virtual tools handle all exercises.

---

## Implementation  
Roary's Home Gym is built using **Unity** and includes:  
- **Compatibility** with Meta Quest headsets  
- **Motion tracking** for precise excersice tracking.  
- **Pre-designed workouts** for all fitness levels with **gamification features**

---

## Installation 

### Option 1: Using the Pre-Built APK  

#### 1. **Download the APK**  
   - Go to the [Releases](/Releases) section of this repository.  
   - Locate the latest version of the APK compatible with Meta Quest 3.  
   - Download the APK file to your computer.  

#### 2. **Install the APK on Your VR Headset**  

##### **For Windows Users (Using Meta Quest Link):**  
1. **Enable Developer Mode**:  
   - Open the **Meta Quest app** on your smartphone.  
   - Navigate to **Settings > Headset Settings > Developer Mode** and toggle it on.  

2. **Set Up Meta Quest Link**:  
   - Connect your Meta Quest 3 headset to your Windows PC using a USB-C cable.  
   - Enable **Link Mode** on your headset when prompted.  

3. **Install Using ADB (Android Debug Bridge)**:  
   - Ensure you have **ADB** installed on your Windows PC.  
   - Open a terminal or command prompt and navigate to the folder containing the APK file.  
   - Run the following command:  
     ```bash  
     adb install RoarysHomeGym.apk  
     ```  

4. **Verify Installation**:  
   - Put on your headset, navigate to the **Unknown Sources** section in your apps, and check for **Roary's Home Gym**.

##### **For Mac Users (Using Meta Developer Hub):**  
1. **Enable Developer Mode**:  
   - Open the **Meta Quest app** on your smartphone.  
   - Navigate to **Settings > Headset Settings > Developer Mode** and toggle it on.  

2. **Download and Install Meta Developer Hub**:  
   - Go to the [Meta Developer Hub](https://developer.oculus.com/downloads/) page and download the application for macOS.  
   - Install and open Meta Developer Hub on your Mac.  

3. **Connect Your Headset**:  
   - Use a USB-C cable to connect your Meta Quest 3 headset to your Mac.  
   - Meta Developer Hub will detect your device (ensure the headset is unlocked and trust the computer when prompted).  

4. **Install the APK**:  
   - In Meta Developer Hub, click on **Device Manager**.  
   - Drag and drop the downloaded APK file into the interface or use the **Install APK** option.  

5. **Verify Installation**:  
   - Put on your headset, navigate to the **Unknown Sources** section in your apps, and check for **Roary's Home Gym**.

#### 3. **Launch the App**  
   - Put on your Meta Quest 3 headset.  
   - Navigate to the **Unknown Sources** section in your apps.  
   - Select **Roary's Home Gym** to start your workout journey!

### Option 2: Building from Source Code  

1. **Clone the Repository**:  
   - Open a terminal or command prompt.  
   - In the directory where you want to clone the project, run the following command:  
     ```bash  
     git clone https://github.com/LuisAhumadaMartens/Roary-s-Home-Gym  
     ```  

2. **Set Up Unity**:  
   - Install **Unity Hub** and the Unity Editor version specified in the repository's documentation (e.g., Unity 2021.3.x or later).  
   - Open **Unity Hub**, navigate to the **Projects** tab, and click **Open Project**.  
   - Select the cloned repository folder.  

3. **Configure the Build Settings**:  
   - In Unity, go to **File > Build Settings**.  
   - Select **Android** as the target platform.  
   - Click **Switch Platform** if it isn’t already set to Android.  
   - Connect your Meta Quest 3 headset to your computer.  
   - Enable Developer Mode on your headset (see instructions in Option 1).  
   - Ensure the **XR Plugin Management** is correctly configured for VR:  
     - Go to **Edit > Project Settings > XR Plugin Management**.  
     - Enable **Oculus** under the Android tab.  

4. **Build the APK**:  
   - In the **Build Settings** window, click **Build** or **Build and Run**.
   - Once the build completes, the APK file will be saved to the selected location.  

5. **Launch the App**:  
   - Put on your headset, navigate to the **Unknown Sources** section, and select **Roary's Home Gym** to start.  

---

## Summary  
**Roary's Home Gym** bridges the gap between accessibility and motivation for home fitness. By leveraging VR technology, gamification, and real-time feedback, it provides users with an innovative and engaging fitness solution, even in limited spaces.  
