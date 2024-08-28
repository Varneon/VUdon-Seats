[0.2.0-alpha.2] - 2024-08-28
Fixes
* Player ID will now be reset to 0 instead of -1 when player exits the seat
* Fixed continuous automatic calibration caused by avatar event migration regression

[0.2.0-alpha.1] - 2024-04-16
Added
* Runtime manager and abstract event receivers
* Manual calibration
* Null checks to callback receiver invokation
* Protected OnDeserialization callback
* Added calibration persistence

Changes
* Exposed calibration options publicly
* Overhauled the build process
* New VRChat events are now used for detecting avatar and height changes

Fixes
* Manual calibration doesn't end anymore on input use
* Preserved calibration is now reset on auto calibration
* Package dependency issues caused by UdonSharp relocation
