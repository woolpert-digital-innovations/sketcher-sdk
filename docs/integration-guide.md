# Integration Guide

# Introduction

Sketcher is a hosted web application. Everything is handled for you, all you need to do is:

1. Launch the Sketcher
1. Load your data
1. Listen for updates

## SDS

Sketcher uses the open [Sketch Data Schema](https://opencamadata.org) (SDS) specification to communicate sketch data.

## Technology

Sketcher uses the web standard Broadcast Channel API to communicate with your application. To learn more about this mechanism please refer to the following resources:

- [BroacastChannel](https://developer.mozilla.org/en-US/docs/Web/API/BroadcastChannel)
- [Window.postMessage()](https://developer.mozilla.org/en-US/docs/Web/API/Window/postMessage)
- [Window: message event](https://developer.mozilla.org/en-US/docs/Web/API/Window/message_event)

# Application Flow

Following is a typical application lifecycle flow. Details of steps are outlined below.

```mermaid
sequenceDiagram
    participant ya as Your App
    participant sk as Sketcher
    ya->>sk: open
    sk->>ya: ready message
    ya->>sk: load
    loop Editing
        sk->>sk: Editing
        sk->>ya: save messages (multiple)
    end
    sk ->> ya: log messages (multiple)
    ya ->> sk: close (optional)
    sk ->> ya: closed message
```

## Launch

On launch the Sketcher will check for a valid license. If one is not found the user will be redirected to the licensing page, and then redirected back once authentication is completed.

```mermaid
graph LR;
    app[Your App]-- launches -->auth{Licensed?};
    sa[Sketcher]-- message event: ready -->app;
    auth-- yes -->sa;
    auth-- no -->lisc[Licensing];
    lisc-->sa;
```

### Ready Response Payload:

```javascript
{
  type: "ready";
}
```

## Load

Once the application is opened a "ready" message will be sent. Once received the app should load the sketch document by sending a load command with a valid SDS document.

```mermaid
graph RL;
    sa[Sketcher]-- message event: ready -->app;
    app[Your App]-- postMessage: load -->sa[Sketcher];

```

When a ready message is received, this is your opportunity to load data.

### Ready Response Payload:

```javascript
{
  type: "ready";
}
```

### Load Payload:

```javascript
{
  type: "load",
  data: {
    data: <sketch data>,
    config: <config data>
  }
}
```

## Save

When a request to save occurs in the Sketcher a save event will be sent. Your app should respond with a "save" message to confirm completion. Multiple save events can occur as the user is given the choice to close or continue upon save.

```mermaid
graph RL;
    sa[Sketcher]-- message event: save -->app;
    app[Your App]-- postMessage: save -->sa[Sketcher];
```

`

### Save Payload:

```javascript
{
  type: "save",
  data: {
    data: <sketch data>
  }
}
```

### Save Success Confirmation:

```javascript
{
  type: "save";
}
```

### Save Failure Confirmation:

```javascript
{
  type: "save",
  data: <string>
}
```

## Log

The Sketcher will log messages when appropriate. These messages are all for informational purposes.

```mermaid
graph RL;
    sa[Sketcher]-- message event: log -->app[Your App];
```

### Log Payload:

```ts
{
  severity: "info" | "warn" | "error" | "critical",
  code: string,
  description: string
}
```

## Close

If the Sketcher is closed a closed message will be sent. If your app initiates the close, the Sketcher will send the closed event when it has completed.

```mermaid
graph LR;
    app[Your App]-- "Window.close()" -->sa[Sketcher];
    sa[Sketcher]-- message event: closed -->app;
```

### Closed Payload:

```javascript
{
  type: "closed";
}
```

# Example Integrations

## Web Browser

Sketcher integrates easily via JavaScript in a browser-based app. Guidance and a working example can be found [here](../examples/browser).

## Windows

Sketcher can integrate with your Windows app through a control such as [WebView2](https://docs.microsoft.com/en-us/microsoft-edge/webview2/). Guidance and a working example for a WPF app can be found [here](../examples/dotnet-webview2).

# Sketch

Sketcher requires an (SDS)[https://opencamadata.org] document object to open. A minimal document includes the schema and sketches. In addition to that, Sketcher requires at least one sketch for a user to get started:

```javascript
{
  $schema: "https://schemas.opencamadata.org/0.10/data.schema.json",
  sketches: [
    {
      label: "Building #1"
    }
  ]
}
```

# Configuration

Sketcher requires a configuration object to open. The configuration object should specify the config version and a non-empty lookup collection that can be used to create segments.  Sketcher and config versions are independent.  A minimal configuration object:

```javascript
{
  version: "1.0",
  lookupCllection: {
    standard: {
      unspecified: {
        "*": {
          name: "ALL TYPES",
          description: "ALL TYPES",
          ordinal: 1,
          styles: {
            vector: {
              color: "#F52817",
              fill: "#D6FCAB",
              stroke: "#1B0C9C"
            }
          }
        }
      }
    }
  }
}
```

## Interface

A variety of options may be specified by a configuration object that you provide when opening the Sketcher.

```typescript
interface Configuration {
  /** Config version in x.y semantic version format. */
  version: string;
  sketchSettings?: {
    /** Coordinate precision.  Value must be a positive integer or 0. */
    coordinatePrecision?: number;
    /** Unit of measure.  Default is "feet". */
    unitOfMeasure?: "feet" | "meters";
  };
  capabilities?: {
    paging?: {
      /** Controls whether sketches can have multiple pages. */
      flag: "enabled" | "disabled";
      /** Maximum number of pages.  Minimum is 1.  Default is 1. */
      maximumPageCount: number;
    };
    outbuildings?: {
      /** Controls support of an "outbuildings" domain. */
      flag: "enabled" | "disabled";
      /** Domain outbuildings are associated with.  Default is "outbuilding". */
      associatedDomain?: "unspecified" | "outbuilding";
    };
    arcDrawing?: {
      /** Controls the type of arc drawing. */
      flag: "enabled" | "disabled";
      /** Supported arc types.  Default is "ANY". */
      supportedArcTypes?: "ELL" | "ANY";
    };
    boundaryConstraints?: {
      flag: "enabled" | "disabled";
      /** Size of each side the sketch boundary.  Boundary is square.  Minimum is 1.  Default is 100. */
      defaultBoundarySize?: number;
    };
    colorCustomization?: {
      /** Controls whether segments are drawn using styles defined by an associated lookup. */
      flag: "enabled" | "disabled";
    };
    sketchOriginPosition?: {
      flag: "enabled" | "disabled";
      /** Sketches exist on a cartesian plane with a (0, 0) origin. The plane has [quadrants name according to conventions](https://en.wikipedia.org/wiki/Cartesian_coordinate_system#Quadrants_and_octants). The default is quadrant I or the space of (+, +) coordinates or "bottomLeft". The "bottomLeft" reflects what the user of a sketching app would interpret as the origin is in the bottom-left of the sketch. This capability can be used to change the default quadrant to `topRight` (quadrant III) or `topLeft` (quadrant IV). */
      origin?: "bottomLeft" | "topLeft" | "topRight";
    };
    multipleLabels?: {
      /** Controls whether multiple labels can be associated with segments and vectors. */
      flag: "enabled" | "disabled";
      /** Delimeter used to combine the multiple values into a single text.  Default is "/". */
      delimeter?: string;
    };
  };
  lookupCollection?: {
    /** Recognized values are "standard" and "outbuilding" */
    [lookupName: string]: {
      /** The domain-specific lookups */
      [domain: "unspecified" | "outbuilding"]: {
        [lookupCode: string]: {
          name: string;
          description?: string;
          /** Sort order */
          ordinal?: number;
          /** Styles to apply to associated components */
          styles?: {
            label?: Style;
            dimensionLabel?: Style;
            vector?: Style;
          };
          attributes?: {
            key: string;
            value:
              | string
              | number
              | boolean
              | { [key: string]: unknown }
              | unknown[];
          }[];
        };
      }
    }
  };
  /**
   * Fill options.  Valid values must be a hexadecimal color code or "none".
   * @example
   * [
   *   { name: "None", value: "none" },
   *   { name: "Red", value: "#ff0000" },
   *   { name: "Blue", value: "#0000ff" },
   *   { name: "Green", value: "#0f0" }
   * ]
   */
  fills?: { name: string; value: string }[];
  /**
   * Stroke options.  Valid values must be a hexadecimal color code or "none".
   * @example
   * [
   *   { name: "None", value: "none" },
   *   { name: "Red", value: "#ff0000" },
   *   { name: "Blue", value: "#0000ff" },
   *   { name: "Green", value: "#0f0" }
   * ]
   */
  strokes?: { name: string; value: string }[];
  /**
   * Stroke width options.  Valid values must be a positive integer or 0.
   * @example
   * [
   *   { name: "None", value: 0 },
   *   { name: "Thin", value: 1 },
   *   { name: "Thick", value: 2 }
   * ]
   */
  strokeWidths?: { name: string; value: number }[];
  /**
   * Stroke dash array options.  Values must be an array of positive integer or 0 that represents a pattern of dashses and gaps.  Follows the dash array convension shared in common with SVG/HTML/CSS.
   * @example
   * [
   *   { name: "None", value: [] },
   *   { name: "Dash", value: [4, 4] },
   *   { name: "Dot Dot Dash", value: [2, 2, 2, 2, 4, 2] }
   * ]
   */
  strokeDashArrays?: { name: string; value: number[] }[];
  /**
   * Fill pattern options.  Values must be a pattern object.
   * @example
   * [
   *   { name: "None", value: "none" },
   *   {
   *     name: "Diagonal",
   *     value: {
   *       height: 10,
   *       width: 10,
   *       elements: [
   *         {
   *           path: "M-1,1 l2,-2 M0,10 l10,-10 M9,11 l2,-2",
   *           style: { stroke: "#000", strokeWidth: 2 }
   *         }
   *       ]
   *     }
   *   }
   * ]
   */
  fillPatterns?: { name: string; value: ("none" | Pattern)[] }[];
}
```

Style is an (SDS)[https://opencamadata.org] _style_.

```typescript
interface Style {
  /** Foreground color. Value is a hexadecimal color code or "none" */
  color?: string;
  /** Stroke color. Value is a hexadecimal color code or "none" */
  stroke?: string;
  /** Stroke width. Value is a positive integer or 0 */
  strokeWidth?: number;
  /**
   * An array of positive integer or 0 that represents a pattern of dashses and gaps.
   * Follows the dash array convension shared in common with SVG/HTML/CSS.
   */
  strokeDashArray?: number[];
  /** Fill color. Value is a hexadecimal color code or "none" */
  fill?: string;
  fillPattern?: "none" | Pattern;
}
```

Pattern is an (SDS)[https://opencamadata.org] _pattern_.

```typescript
interface Pattern {
  /** Positive integer or 0. */
  height: number;
  /** Positive integer or 0. */
  width: number;
  /** Drawing elements of the pattern */
  elements?: {
    /** Sequence of commands that follow the [SVG standard](https://svgwg.org/svg2-draft/paths.html#PathData) for path data. */
    path: string;
    /** Style to apply to this element */
    style?: Style;
  }[];
}
```

## Examples

```javascript
{
  capabilities: {
    colorCustomization: {
      flag: "enabled"
    }
  },
  lookupCollection: {
    standard: {
      unspecified: {
        "*": {
          name: "ALL TYPES",
          description: "ALL TYPES",
          ordinal: 1
        },
        AC: {
          name: "CANOPY ALUM.",
          description: "CANOPY ALUM.",
          ordinal: 2,
          styles: {
            vector: {
              color: "#F52817",
              fill: "#D6FCAB",
              stroke: "#1B0C9C"
            }
          }
        }
      }
    }
  },
  strokes: [
    {
      name: "green",
      value: "#9FE2BF"
    },
    {
      name: "aqua",
      value: "#40E0D0"
    },
    {
      name: "blue",
      value: "#6495ED"
    },
    {
      name: "purple",
      value: "#CCCCFF"
    },
    {
      name: "red",
      value: "#DE3163"
    },
    {
      name: "orange",
      value: "#FF7F50"
    }
  ],
  strokeWidths: [
    {
      name: "1",
      value: 1
    },
    {
      name: "2",
      value: 2
    },
    {
      name: "3",
      value: 3
    },
    {
      name: "4",
      value: 4
    },
    {
      name: "5",
      value: 5
    },
    {
      name: "6",
      value: 6
    }
  ],
  strokeDashArrays: [
    {
      name: "none",
      value: []
    },
    {
      name: "one",
      value: [
        10,
        10
      ]
    },
    {
      name: "two",
      value: [
        10,
        2,
        2
      ]
    },
    {
      name: "three",
      value: [
        20,
        5
      ]
    }
  ],
  fills: [
    {
      name: "green",
      value: "#9FE2BF"
    },
    {
      name: "aqua",
      value: "#40E0D0"
    },
    {
      name: "blue",
      value: "#6495ED"
    },
    {
      name: "purple",
      value: "#CCCCFF"
    },
    {
      name: "red",
      value: "#DE3163"
    },
    {
      name: "orange",
      value: "#FF7F50"
    }
  ],
  fillPatterns: [
    {
      name: "None",
      value: "none"
    },
    {
      name: "Diagonal",
      value: {
        height: 10,
        width: 10,
        elements: [
          {
            path: "M-1,1 l2,-2 M0,10 l10,-10 M9,11 l2,-2",
            style: {
              stroke: "#000",
              strokeWidth: 2
            }
          }
        ]
      }
    },
    {
      name: "Cross Hatch",
      value: {
        height: 15,
        width: 15,
        elements: [
          {
            path: "M0,0 l15,15 M15,0 l-15,15",
            style: {
              stroke: "#000",
              strokeWidth: 2
            }
          }
        ]
      }
    }
  ]
}
```
