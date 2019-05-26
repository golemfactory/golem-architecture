# Rendering Service 
This namespace includes properties used to describe visualization and rendering services.

## Common Properties

* [golem.activity](../../../../0-commons/golem/activity.md)

## Specific Properties

## `golem.srv.app.media.render.input_file_size_kib : Number (int32)` 

Size of input scene file, in KB.

### **Examples**

* `golem.srv.app.media.render.input_file_size_kib=7233` - declared size of input file (scene) as 7233 KB.


## `golem.srv.app.media.render.output.resolution.x : Number (int32)` 

X resolution of output rendered image (in pixels).

### **Examples**

* `golem.srv.app.media.render.output.resolution.x=1920` - declared horizontal resolution of output image is 1920 px.


## `golem.srv.app.media.render.output.resolution.y : Number (int32)` 

Y resolution of output rendered image (in pixels).

### **Examples**

* `golem.srv.app.media.render.output.resolution.y=1080` - declared vertical resolution of output image is 1080 px.


## `golem.srv.app.media.render.engine : List of String` 

Indicates rendering engines supported by the provider. 

### **Examples**

* `golem.srv.app.media.render.engine=["blender"]` - declares a rendering service using Blender engine.


## `golem.srv.app.media.render.blender.benchmark : Number (int32)` 

Indicates benchmark value of Provider tested by rendering a standardized benchmark scene. 
_(**Note:** (TODO) the standard must define the standardized input scene to be used as benchmark.)_

### **Examples**

* `golem.srv.app.media.render.blender.benchmark=123` - declares a benchmark value .

