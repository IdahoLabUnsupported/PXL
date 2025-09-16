# PXL: Interactive Images in Unity and MRTK3
[![Made with Unity](https://img.shields.io/badge/Made%20with-Unity-57b9d3.svg?style=flat&logo=unity)](https://unity3d.com)
[![License](https://img.shields.io/badge/License-BSD_3--Clause-blue.svg)](https://opensource.org/licenses/BSD-3-Clause)
<br>
<b> Pixel eXtraction via Location tracking </b> (PXL) is an additional Unity package built on Microsoft's Mixed Reality Toolkit 3 framework for extended reality (XR) applications.
<br><br>
The code fetches an image, creates a button behind the image that detects user touch via Unity cues, and converts 3D input to pixel space and estimates the corresponding pixel position. Images displayed as sprites aren't interactive by default, and this package aims to address that issue. 

# To Install
<b><i> Make sure that your Unity project has MRTK 3 and required dependencies all installed before proceeding.</i></b> <br> 
If you are new to Unity, follow Microsoft's setup instructions here: https://learn.microsoft.com/en-us/windows/mixed-reality/mrtk-unity/mrtk3-overview/getting-started/setting-up/setup-new-project<br><br>
<b><i>Make sure that you have your Unity scene configured for MRTK 3 as well </i></b> using the instructions at this link: https://learn.microsoft.com/en-us/windows/mixed-reality/mrtk-unity/mrtk3-overview/getting-started/setting-up/setup-new-scene <br><br>
Download this repo and unzip it. Navigate to the package manager under "Window" -> "Package Manager" in Unity. Select "Add package from disk". Find where you unzipped your package, and click the "manifest.json" file. 

# To Set Up
This package comes with various scripts that work together to get the pixel coordinate.

Start by finding the starter prefab "Interactive Image System" under Packages/PXL/Assets/Prefabs. Place that prefab into your scene.

<b>ImageGetter.cs</b> is the script that makes the web request for your image. <b><ins>By default it is attached to the child game object under the Interactive Image System prefab</ins></b>. Specify the URL in the public field. You can also specify where you want the image and how you want it positioned in the other public fields "Image Position" and "Image Rotation". <i> This class fetches the image in the Start() method. </i> <br><br> This class also will scale your image as higher quality images can be quite large in a Unity scene.
<b>ButtonManager.cs</b> is the script that creates the interactive panel behind the image. In the private method Notify(), the specific pixel that was touched is calculated (stored in the Vector2 "touchedPixel"). You don't have to do anything with this script. <br><br>
<b>HandTracker.cs</b> is the script that gets the hand position when the interaction is triggered. Attach this script to the <i>Camera offset</i> game object in the hierarchy (under MRTK XR Rig).<br><br>
Feel free to customize these scripts to fit your needs like changing how you fetch the image if you need some sort of authentication, want to change how the image is scaled, or if you need the pixel value for a certain application. 

# License
This code is provided under a BSD-3-Clause license and is <b><ins>not</ins></b> maintained. Feel free to alter this code. <br> See more under the <i>License</i> tab of this repo.
