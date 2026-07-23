# Feature Version Guide

## Quick Note

App Versions 2.0.0 forward do not support SDS or Config versions below 2.0 
- [SDS](https://github.com/woolpert-digital-innovations/sketcher-sdk/blob/main/docs/release-notes.md#sds)
- [Config](https://github.com/woolpert-digital-innovations/sketcher-sdk/blob/main/docs/release-notes.md#config)

## Features

- [ArcDrawing](#arcdrawing)
- [ColorCustomization](#colorcustomization)
- [CreateSketch](#createsketch)
- [CustomFields](#customfields)
- [FloorTracking](#floortracking)
- [HideCloseButton](#hideclosebutton)
- [InnerRings](#innerrings)
- [LookupCollection-Styles-DimensionLabel-Styles](#lookupcollection-styles-dimensionlabel-styles)
- [MultipleLabels](#multiplelabels)
- [OutBuilding](#outbuilding)
- [Paging](#paging)
- [ReadOnly Mode](#readonly-mode)
- [saveSDSVersion](#savesdsversion)
- [SegmentLinking](#segmentlinking)
- [SharedWalls](#sharedwalls)
- [SketchNotes](#sketchnotes)
- [SketchOriginPosition](#sketchoriginposition)

### LookupCollection-Styles-DimensionLabel-Styles 

Ability to set `Visibility`, `Placement` and `Padding` styles on DimensionLabels in config lookup

- Min App Version: 3.1.0
- Min SDS Version: 2.1
```diff
sds = {
  ...
  lookupCollection: {
    ['standard'|'outbuildings']: {
      ['unspecified'|'outbuilding']: {
        [lookupKey: String]: {
          ...
          styles: {
            ...
            dimensionLabel: {
+             placement?: 'inside' | 'outside'
+             visibility?: 'hidden' | 'shown'
+             padding?: number
            }
          }
        }
      }
    }
  }
}
```
- Min Config Version: 2.1
```diff
config = {
  ...
  lookupCollection: {
    ['standard'|'outbuildings']: {
      ['unspecified'|'outbuilding']: {
        [lookupKey: String]: {
          ...
          styles: {
            ...
            dimensionLabel: {
+             placement?: 'inside' | 'outside'
+             visibility?: 'hidden' | 'shown'
+             padding?: number
            }
          }
        }
      }
    }
  }
}
```

### saveSDSVersion

Ability to save SDS to a newer version

- Min App Version: 3.1.0
- Min SDS Version: 2.0
- Min Config Version: 2.1
```diff
config = {
  ...
  capabilites: {
    ...
+    sdsSaveVersion?: {
+      flag: 'enabled'|'disabled'
+      minVersion?: string
+    }
  }
}
```


### ReadOnly Mode

Ability to load an SDS and remove the ability to modify it through the Sketch App

- Min App Version: 3.1.0
```diff
api = {
  ...
+  readOnly?: 'true'
}
```
- Min SDS Version: 2.0
- Min Config Version: 2.0

### SharedWalls

Ability to mark a wall as shared so it is not included in calculations

- Min App Version: 2.0.0
- Min SDS Version: 2.0
- Min Config Version: 2.0
```diff
config = {
  ...
  capabilites: {
    ...
+    sharedWalls?: {
+      flag: 'enabled'|'disabled'
+    }
  }
}
```

### CreateSketch

Ability to `Add New` and `Remove Existing` Sketches to/from an SDS document

- Min App Version: 2.0.0
- Min SDS Version: 2.0
- Min Config Version: 2.0
```diff
config = {
  ...
  capabilites: {
    ...
+    createSketch?: {
+      flag: 'enabled'|'disabled'
+    }
  }
}
```

### MultipleLabels

Ability to provide multiple lookup codes to a segment

- Min App Version: 2.0.0
- Min SDS Version: 2.0
- Min Config Version: 2.0
```diff
config = {
  ...
  capabilites: {
    ...
+    multipleLabels?: {
+      flag: 'enabled'|'disabled'
+      delimiter: '/'
+    }
  }
}
```

### CustomFields

Ability to collect data in the SDS as specified by hosted application

- Min App Version: 2.0.0
- Min SDS Version: 2.0
- Min Config Version: 2.0
```diff
config = {
  ...
  capabilites: {
    ...
+    customFields?: {
+      flag: 'enabled'|'disabled'
+      items: {
+        cField: string
+        cValue: string
+        cDataType: 'boolean' | 'string' | 'number'
+      }[]
+    }
  }
}
```

### InnerRings

- Min App Version: 2.0.0
- Min SDS Version: 2.0
- Min Config Version: 2.0
```diff
config = {
  ...
  capabilites: {
    ...
+    innerRings?: {
+      flag: 'enabled'|'disabled'
+    }
  }
}
```
### Paging

Ability to create pages inside of a Sketch

- Min App Version: 2.0.0
- Min SDS Version: 2.0
- Min Config Version: 2.0
```diff
config = {
  ...
  capabilites: {
    ...
+    paging?: {
+      flag: 'enabled'|'disabled'
+    }
  }
}
```

### OutBuilding

Ability to add segments identified as `OutBuilding`

- Min App Version: 2.0.0
- Min SDS Version: 2.0
- Min Config Version: 2.0
```diff
config = {
  ...
  capabilites: {
    ...
+    outbuildings?: {
+      flag: 'enabled'|'disabled'
+      associatedDomain?: "unspecified" | "outbuilding"
+    }
  }
}
```

### ArcDrawing

Ability to draw segments with Arcs

- Min App Version: 2.0.0
- Min SDS Version: 2.0
- Min Config Version: 2.0
```diff
config = {
  ...
  capabilites: {
    ...
+    arcDrawing?: {
+      flag: 'enabled'|'disabled'
+      supportedArcTypes?: "ELL" | "ANY"
+    }
  }
}
```

### ColorCustomization

Ability to Style segments

- Min App Version: 2.0.0
- Min SDS Version: 2.0
- Min Config Version: 2.0
```diff
config = {
  ...
  capabilites: {
    ...
+    colorCustomization?: {
+      flag: 'enabled'|'disabled'
+    }
  }
+  strokes?: {
+    name: string
+    value: string
+  }[]
+  strokeWidths?: {
+    name: string
+    value: string   
+  }[]
+  strokeDashArrays?: {
+    name: string
+    value: string   
+  }
+  fills?: {
+    name: string
+    value: string   
+  }
+  fillPatternss?: {
+    name: string
+    value: 'none' | {
+      height: number
+      width: number
+      elements: {
+        path: string
+        style: {
+          stroke: string
+          strokeWidth: number
+        }
+      }[]
+    }   
+  }
}
```

### SketchNotes

Ability to add Notes to a Sketch

- Min App Version: 2.0.0
- Min SDS Version: 2.0
- Min Config Version: 2.0
```diff
config = {
  ...
  capabilites: {
    ...
+    sketchNotes?: {
+      flag: 'enabled'|'disabled'
+      maxNoteLength?: number
+    }
  }
}
```

### SketchOriginPosition

Ability to change the origin of Sketches in a document 

- Min App Version: 2.0.0
- Min SDS Version: 2.0
- Min Config Version: 2.0
```diff
config = {
  ...
  capabilites: {
    ...
+    sketchOriginPosition?: {
+      flag: 'enabled'|'disabled'
+      origin?: 'bottomLeft' | 'topLeft' | 'topRight' | 'bottomRight'
+    }
  }
}
```

### HideCloseButton

Ability to hide the close button in the Sketch App

- Min App Version: 2.0.0
- Min SDS Version: 2.0
- Min Config Version: 2.0
```ts
config = {
  ...
  capabilities: {
    ...
+    hideCloseButton?: {
+      flag: 'enabled'|'disabled'
+    }
  }
}
```

### FloorTracking

Ability to specify a segment as more than a single floor

- Min App Version: 2.0.0
- Min SDS Version: 2.0
- Min Config Version: 2.0
```diff
config = {
  ...
  capabilities: {
    ...
+    floorTracking?: {
+      flag: 'enabled'|'disabled'
+    }
  }
}
```
### SegmentLinking

Ability to link segments

- Min App Version: 2.0.0
- Min SDS Version: 2.0
- Min Config Version: 2.0
```diff
config = {
  ...
  capabilities: {
    ...
+    segmentLinking?: {
+      flag: 'enabled'|'disabled'
+    }
  }
}
```