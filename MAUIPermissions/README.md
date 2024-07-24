# MAUI Permissions

This is a prototype of a .NET MAUI app that can be used to check the permissions of an app.
For that, we will have an app to add Attachments of three types:

- Take a Photo.
- Select an Image from your gallery.
- Select a File from your device.

## NugetPackages



## Android Setup
Enable these 3 permissions in your AndroidManifest.xml file:

CAMERA
READ_MEDIA_IMAGES
READ_MEDIA_VIDEO
READ_MEDIA_AUDIO

Add this to the <manifest> node of your AndroidManifest.xml file:
```
<queries>
    <intent>
        <action android:name="android.media.action.IMAGE_CAPTURE" />
    </intent>
</queries>
```