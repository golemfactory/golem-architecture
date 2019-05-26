# Rendering Service 
This napespace includes properties used to describe visualization and rendering services.

## Common Properties

* [golem.activity](../../../../0-commons/golem/activity.md)

## Specific Properties

## `golem.srv.app.media.transcode.input.resolution.x : Number (int32)` 

X resolution of input stream.

### **Examples**

* `golem.srv.app.media.transcode.input.resolution.x=1024` - declared horizontal resolution of input stream is 1024 pixels.


## `golem.srv.app.media.transcode.input.resolution.y : Number (int32)` 

Y resolution of input stream.

### **Examples**

* `golem.srv.app.media.transcode.input.resolution.y=768` - declared vertical resolution of input stream is 768 pixels.

## `golem.srv.app.media.transcode.input.container : List of String` 

Supported container formats of input stream.

### Value enum
|Value| Description |
|---|---|
|"avi"| AVI format |
|"mp4"| MP4 format |

### **Examples**

* `golem.srv.app.media.transcode.input.container=["avi", "mp4"]` - Provider declares to support AVI and MP4 input stream formats.

## `golem.srv.app.media.transcode.input.video.codecs : List of String` 

Supported video codecs for input processing.

### Value enum
|Value| Description |
|---|---|
|"H264"| H.264 codec |
|"H265"| H.265 codes |

### **Examples**

* `golem.srv.app.media.transcode.input.video.codecs=["H264", "H265"]` - Provider declares to support H.264 and H.265 video codecs on input.


## `golem.srv.app.media.transcode.output.video.codecs : List of String` 

Supported video codecs for output generation.

### Value enum
|Value| Description |
|---|---|
|"vp6"| VP6 codec |
|"DivX"| DivX codes |

### **Examples**

* `golem.srv.app.media.transcode.output.video.codecs=["vp6", "DivX"]` - Provider declares to support Vp6 and DivX video codecs on output.


## `golem.srv.app.media.transcode.key_frames_number : Number (int32)` 

Number of key frames in input stream.

### **Examples**

* `golem.srv.app.media.transcode.key_frames_number=307` - declared number of key frames in input stream is 307.


## `golem.srv.app.media.transcode.bitrate_kibs : Number (int32)` 

Requested output stream bitrate (in kbit/sec)

### **Examples**

* `golem.srv.app.media.transcode.bitrate_kibs=1522` - expected output stream bitrate is 1522 kbit/sec.

