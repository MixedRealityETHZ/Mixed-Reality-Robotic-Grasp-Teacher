 # Mixed Reality Robotic Grasp Teacher
 
 The **Mixed Reality Robotic Grasp Teacher** is a software developed for the mixed reality headset Microsoft HoloLens 2. It uses the built-in hand-tracking feature of the goggles to allow the user to teach with their hands, a trajectory and grasping motion of an object with a six-degrees-of-freedom robot equipped with a gripper. 
A digital twin of the object is used for the teaching. The operator can interact with it in the virtual environment of the HoloLens. It is comfortably placed for the user’s hand to show the grasping motion.
The trajectory is also taught on a digital twin of the scanned environment. This makes object avoidance more intuitive and independent of any path planner as well as collision detection algorithms.
 
 The demonstration video is available on Youtube under: https://youtu.be/HQ4btf-cnmc

## Setup

The app was tested on Unity 2021.3.11f1 and built with Visual Studio 2022.

First, clone this repository to a local folder.
```
git clone https://github.com/MixedRealityETHZ/Mixed-Reality-Robotic-Grasp-Teacher.git
```
Open the Unity Hub > Open > Add new project from disk

The app was developped with the MRTK 2.8.2 and OpenXR 1.5.1

Install the [MixedRealityFeatureTool](https://learn.microsoft.com/en-us/windows/mixed-reality/develop/unity/welcome-to-mr-feature-tool) in the folder containing the cloned repository.

In order to enhance the experience with the app, the enviornment mesh overlay, as well as the hand overlay can be removed:
While Mixed Realty Toolkit is selected, in the inspector
 - Under Input > Articulated Hand Tracking > Visualization Settings: both Hand Mesh Vizualisation Modes and Hand Joint Vizualisation Modes shall be turned off to Nothing.
 - Under Spatial Awarness, untick Enable Spatial Awareness System.

The rest of the standard export setting for HoloLens are working.

## Building the app

In order to build the app, go to File > Build Settings... Then Universal Windows Platform, change Architecture to ARM 64-bit and clock Build.
Open the generated .sln file in visual Studio. in the top menu, change Debug to Release; the architecture to ARM64; Select Run on Device while the Hololens are connected to the computer.
