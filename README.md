<div>

# [VUdon](https://github.com/Varneon/VUdon) - Seats [![GitHub](https://img.shields.io/github/license/Varneon/VUdon-Seats?color=blue&label=License&style=flat)](https://github.com/Varneon/VUdon-Seats/blob/main/LICENSE) [![GitHub Repo stars](https://img.shields.io/github/stars/Varneon/VUdon-Seats?style=flat&label=Stars)](https://github.com/Varneon/VUdon-Seats/stargazers) [![GitHub all releases](https://img.shields.io/github/downloads/Varneon/VUdon-Seats/total?color=blue&label=Downloads&style=flat)](https://github.com/Varneon/VUdon-Seats/releases) [![GitHub tag (latest SemVer)](https://img.shields.io/github/v/tag/Varneon/VUdon-Seats?color=blue&label=Release&sort=semver&style=flat)](https://github.com/Varneon/VUdon-Seats/releases/latest)

</div>

Self-calibrating synced abstract seats with callbacks and API

# Usage

* Add reference to `Varneon.VUdon.Seats.Runtime` assembly definition
* Create your own class that inherits from `Varneon.VUdon.Seats.Abstract.Seat`
* Invoke the `base.Initialize()` method once at start to initialize the base seat class
```csharp
using Varneon.VUdon.Seats.Abstract;

namespace Varneon.VUdon.Seats.Samples
{
    public class MySeat : Seat
    {
        private void Start()
        {
            base.Initialize();
        }
    }
}
```

### Callbacks:
```csharp
protected virtual void OnCalibrationStarted() { }

protected virtual void OnCalibrationFinished() { }

protected virtual void OnPlayerEnteredSeat(VRCPlayerApi player) { }

protected virtual void OnPlayerExitedSeat(VRCPlayerApi player) { }
```

### Public API Methods:
```csharp
public virtual void _Eject()
```

# Installation

<details><summary>

### Import with [VRChat Creator Companion](https://vcc.docs.vrchat.com/vpm/packages#user-packages):</summary>

> 1. Download `com.varneon.vudon.seats.zip` from [here](https://github.com/Varneon/VUdon-Seats/releases/latest)
> 2. Unpack the .zip somewhere
> 3. In VRChat Creator Companion, navigate to `Settings` > `User Packages` > `Add`
> 4. Navigate to the unpacked folder, `com.varneon.vudon.seats` and click `Select Folder`
> 5. `VUdon - Seats` should now be visible under `Local User Packages` in the project view in VRChat Creator Companion
> 6. Click `Add`

</details><details><summary>

### Import with [Unity Package Manager (git)](https://docs.unity3d.com/2019.4/Documentation/Manual/upm-ui-giturl.html):</summary>

> 1. In the Unity toolbar, select `Window` > `Package Manager` > `[+]` > `Add package from git URL...` 
> 2. Paste the following link: `https://github.com/Varneon/VUdon-Seats.git?path=/Packages/com.varneon.vudon.seats`

</details><details><summary>

### Import from [Unitypackage](https://docs.unity3d.com/2019.4/Documentation/Manual/AssetPackagesImport.html):</summary>

> 1. Download latest `VUdon - Seats` from [here](https://github.com/Varneon/VUdon-Seats/releases/latest)
> 2. Import the downloaded .unitypackage into your Unity project

</details>

<div align="center">

## Developed by Varneon with :hearts:

![Twitter Follow](https://img.shields.io/twitter/follow/Varneon?color=%231c9cea&label=%40Varneon&logo=Twitter&style=for-the-badge)
![YouTube Channel Subscribers](https://img.shields.io/youtube/channel/subscribers/UCKTxeXy7gyaxr-YA9qGWOYg?color=%23FF0000&label=Varneon&logo=YouTube&style=for-the-badge)
![GitHub followers](https://img.shields.io/github/followers/Varneon?color=%23303030&label=Varneon&logo=GitHub&style=for-the-badge)

</div>
