# Normal Map Mirror

This is a small tool that provides the ability to mirror the 3D information of normal map textures.

Mirroring the texture in image editing software will not work correctly, as all this does is reverses the order of the 3D vectors encoded in the texture. This tool works by translating the pixel information back into 3D vectors, mirroring the vectors on the chosen axis, then converting the new vectors back to pixel information.  
&nbsp;

|Original|Mirrored on X|
|---|---|
|![original](https://github.com/Kizari/NormalMapMirror/assets/25322543/1f9b8b94-1f62-4978-87b0-88414bbfa1bb)|![xmirror](https://github.com/Kizari/NormalMapMirror/assets/25322543/c03f4c63-ce9c-45b7-a23c-dad09556a061)|

|Mirrored on Y|Mirrored on Z|
|---|---|
|![ymirror](https://github.com/Kizari/NormalMapMirror/assets/25322543/fc79c7c6-e6cb-46d7-922d-888afac7948d)|![zmirror](https://github.com/Kizari/NormalMapMirror/assets/25322543/652f13cf-75b8-4598-8176-cc5abaac6bf4)|

&nbsp;

### Texture Support

This tool supports png, tga, dds, tif, tiff, jpg, jpeg, gif, bmp, wmp, and ico.  
&nbsp;

### Basic Usage

Download the tool on the [releases](https://github.com/Kizari/NormalMapMirror/releases/latest) page and extract the downloaded zip anywhere you like.

To mirror the normals on a texture, drag the image file and drop it onto `NormalMapMirror.exe`.

A file will be created in the same location as the original file for each axis. For example, if you drop `normal_map.png` onto the executable, you will get:
* `normal_map_XMirror.png` (this is the original texture with vector information mirrored across the X axis)
* `normal_map_YMirror.png` (this is the original texture with vector information mirrored across the Y axis)
* `normal_map_ZMirror.png` (this is the original texture with vector information mirrored across the Z axis)  
&nbsp;

### Configuration

If you want to change which files are generated, you can alter `config.ini`.

```
[Outputs]
XMirror = true
YMirror = true
ZMirror = true
```

The default configuration is shown above. For example, if you only want a copy of the image with the vectors mirrored along the X axis, you would set the other two axes to false:
```
[Outputs]
XMirror = true
YMirror = false
ZMirror = false
```

Save the file once you have made your changes. From now on, when you drop files onto the executable, only the XMirror variant will be generated.
