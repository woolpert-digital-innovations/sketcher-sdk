# Features Guide

## Features

- [LookupCollection](#lookupcollection)
- [ReadOnly Mode](#readonly-mode)

## Capabilities

- [arcDrawing](#arcdrawing)
- [boundaryConstraints](#boundaryconstraints)
- [colorCustomization](#colorcustomization)
- [createSketch](#createsketch)
- [customFields](#customfields)
- [floorTracking](#floortracking)
- [hideCloseButton](#hideclosebutton)
- [innerRings](#innerrings)
- [multipleLabels](#multiplelabels)
- [outBuildings](#outbuildings)
- [paging](#paging)
- [sdsSaveVersion](#sdssaveversion)
- [segmentLinking](#segmentlinking)
- [sharedWalls](#sharedwalls)
- [sketchNotes](#sketchnotes)
- [sketchOriginPosition](#sketchoriginposition)

### LookupCollection

#### 3.1.0 update

Ability to set `Visibility`, `Placement` and `Padding` styles on DimensionLabels in config lookup

- Min App Version: [3.1.0](release-notes.md#310-2026-07-15)
- Min SDS Version: [2.1](sds/release-notes.md#21-2026-07-15)

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
              ...
            }
          }
        }
      }
    }
  }
}
```

- Min Config Version: [2.1](config/release-notes.md#21-2026-07-15)

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
              ...
            }
          }
        }
      }
    }
  }
}
```

### 1.1.1 Release

- Min App Version: [1.1.1](docs/release-notes.md#111-2024-1115)
- Min SDS Version: [0.10](sds/release-notes.md#010-2022-01-26)
  - [schema](sds/schemas/0.9.md#lookupCollection)
- Min Config Version: [1.0](config/release-notes.md#10--2021-11-30)

```diff
config = {
  ...
+  lookupCollection: {
+    ['standard'|'outbuildings']: {
+      ['unspecified'|'outbuilding']: {
+        [lookupKey: String]: {
+          ...
+          name: string
+          description?: string
+          ordinal?: number
+          styles: {
+            label: {
+              color?: Color
+              fontFamily?: FontFamily
+              fontSize?: FontSize
+              fontStyle?: FontStyle
+              fontWeight?: FontWeight
+              stroke?: Stroke
+              strokeWidth?: StrokeWidth
+              strokeDashArray?: StrokeDashArray
+              fill?: Fill
+              fillPattern?: FillPattern
+            }
+            dimensionLabel: {
+              color?: Color
+              fontFamily?: FontFamily
+              fontSize?: FontSize
+              fontStyle?: FontStyle
+              fontWeight?: FontWeight
+              stroke?: Stroke
+              strokeWidth?: StrokeWidth
+              strokeDashArray?: StrokeDashArray
+              fill?: Fill
+              fillPattern?: FillPattern
+            }
+            vector: {
+              color?: Color
+              fontFamily?: FontFamily
+              fontSize?: FontSize
+              fontStyle?: FontStyle
+              fontWeight?: FontWeight
+              stroke?: Stroke
+              strokeWidth?: StrokeWidth
+              strokeDashArray?: StrokeDashArray
+              fill?: Fill
+              fillPattern?: FillPattern
+            }
+          }
+          attributes?: {
+            key: string
+            value:
+              | string
+              | number
+              | {
+                  [k: string]: unknown
+                }
+              | boolean
+              | unknown[]
+           }[]
+          propertyTypes?: string
+        }
+      }
+    }
+  }
}
```

### sdsSaveVersion

Ability to save SDS to a newer version.

- Min App Version: [3.1.0](release-notes.md#310-2026-07-15)
- Min SDS Version: N/A
- Min Config Version: [2.1](config/release-notes.md#21-2026-07-15)

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

Ability to load an SDS document and remove the ability to modify it through the editor. 

- Min App Version: [3.1.0](release-notes.md#310-2026-07-15)

```diff
api = {
  ...
+  readOnly?: 'true'
}
```

- Min SDS Version: N/A
- Min Config Version: N/A

### sharedWalls

Ability to mark a wall as shared so it is not included in calculations

- Min App Version: [2.0.0](release-notes.md#200-2025-12-04)
- Min SDS Version: [2.0](sds/release-notes.md#20-2025-11-10)
- Min Config Version: [2.0](config/release-notes.md#20-2025-12-04)
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

### createSketch

Ability to add/remove sketches to/from an SDS document by enabling `Add Sketch` and `Delete Sketch` buttons in the editor. 

- Min App Version: [1.1.1](docs/release-notes.md#111-2024-11-15)
- Min SDS Version: N/A
- Min Config Version: [1.0](config/release-notes.md#10--2021-11-30)
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

### multipleLabels

Ability to provide multiple lookup codes to a segment.

- Min App Version: [1.1.1](docs/release-notes.md#111-2024-11-15)
- Min SDS Version: [0.10](sds/release-notes.md#010-2022-01-26)
- Min Config Version: [1.0](config/release-notes.md#10--2021-11-30)
```diff
config = {
  ...
  capabilites: {
    ...
+    multipleLabels?: {
+      flag: 'enabled'|'disabled'
+      delimiter: string
+    }
  }
}
```

### customFields

Ability to collect data in the SDS as specified by your application.

- Min App Version: [1.1.1](docs/release-notes.md#111-2024-11-15)
- Min SDS Version: [0.10](sds/release-notes.md#010-2022-01-26)
- Min Config Version: [1.0](config/release-notes.md#10--2021-11-30)

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

### innerRings

Ability to create holes by enabling the Cutter tool 

- Min App Version: [1.1.1](docs/release-notes.md#111-2024-11-15)
- Min SDS Version: [0.10](sds/release-notes.md#010-2022-01-26)
- Min Config Version: [1.0](config/release-notes.md#10--2021-11-30)

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

### paging

Ability to create pages inside of a sketch.

- Min App Version: [1.1.1](docs/release-notes.md#111-2024-11-15)
- Min SDS Version: [0.10]
- Min Config Version: [1.0](config/release-notes.md#10--2021-11-30)

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

### outBuildings

Ability to add segments identified as `outBuilding`

- Min App Version: [1.1.1](docs/release-notes.md#111-2024-11-15)
- Min SDS Version: [0.10](sds/release-notes.md#010-2022-01-26)
- Min Config Version: [1.0](config/release-notes.md#10--2021-11-30)

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

### arcDrawing

Ability to draw segments with arcs.

- Min App Version: [1.1.1](docs/release-notes.md#111-2024-11-15)
- Min SDS Version: [0.10](sds/release-notes.md#010-2022-01-26)
- Min Config Version: [1.0](config/release-notes.md#10--2021-11-30)

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

### colorCustomization

Ability to style segments.

- Min App Version: [1.1.1](docs/release-notes.md#111-2024-11-15)
- Min SDS Version: [0.10](sds/release-notes.md#010-2022-01-26)
- Min Config Version: [1.0](config/release-notes.md#10--2021-11-30)

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

### sketchNotes

Ability to add Notes to a Sketch.

- Min App Version: [1.1.1](docs/release-notes.md#111-2024-11-15)
- Min SDS Version: [0.10](sds/release-notes.md#010-2022-01-26)
- Min Config Version: [1.0](config/release-notes.md#10--2021-11-30)

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

### sketchOriginPosition

Ability to change where the origin appears in the editor.

- Min App Version: 
- Min SDS Version: [0.10](sds/release-notes.md#010-2022-01-26)
- Min Config Version: [1.0](config/release-notes.md#10--2021-11-30)

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

### hideCloseButton

Ability to hide the close button in the editor.

- Min App Version: [1.1.1](docs/release-notes.md#111-2024-11-15)
- Min SDS Version: No Restriction
- Min Config Version: [1.0](config/release-notes.md#10--2021-11-30)

```diff
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

### floorTracking

Ability to specify a segment as more than a single floor.

- Min App Version: [1.1.1](docs/release-notes.md#111-2024-11-15)
- Min SDS Version: [0.10](sds/release-notes.md#010-2022-01-26)
- Min Config Version: [1.0](config/release-notes.md#10--2021-11-30)

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

### segmentLinking

Ability to link segments.

- Min App Version: <= 1.1.1
- Min SDS Version: < 2
- Min Config Version: [1.0](config/release-notes.md#10--2021-11-30)

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

### boundaryConstraints

Ability to set a constraint across all sketches in a document to be within the specified boundary.

- Min App Version: [1.1.1](docs/release-notes.md#111-2024-11-15)
- Min SDS Version: [0.10](sds/release-notes.md#010-2022-01-26)
- Min Config Version: [1.0](config/release-notes.md#10--2021-11-30)

``` diff
config = {
  ...
  capabilities: {
    ...
+    "boundaryConstraints": {
+      "flag": "enabled",
+      "defaultBoundarySize": 250
+    }
  }
}