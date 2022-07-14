# Sketcher Web Browser Example

This example demonstrates how to integrate a web browser client with the Sketcher.

## Getting Started

Integrating the Sketcher with your application is easy.

1. Add the client script to your application:

   ```html
   <script src="https://app.sketcher.camacloud.com/0.10/assets/ccsketcher.js"></script>
   ```

1. From your application, create the Sketcher client:

   ```javascript
   const sketcher = new CAMACloud.Sketcher({ url: 'https://app.sketcher.camacloud.com/0.10' });
   ```

1. Open a sketch:

   ```javascript
   sketcher.open({
     data, // document to open
     config, // configuration to use
     onSave: (saveData, onSuccess, onFailure) => {
       try {
         // Persist saveData...

         onSuccess();
       } catch (error) {
         onFailure(error);
       }
     }
   });
   ```

   Where `data` is an [SDS document][sds-document] and `config` the [Sketcher configuration][sketcher-configuration].

To open a new document, a new [SDS document][sds-document] object must be created.  A minimal document must include a valid [SDS][sds] schema reference and at least one sketch for the user can draw to:

```javascript
const data = {
  $schema: "https://schemas.opencamadata.org/0.10/data.schema.json",
  sketches: [
    {
      id: 1,
      label: "Building #1",
      segments: []
    }
  ]
};
```

Sketcher requires a configuration object.  A minimal configuration must specify the config version and some lookup data:

```javascript
const config = {
  version: "1.0",
  lookupCollection: {
    standard: {
      unspecified: {
        "*": {
          name: "ALL TYPES"
        }
      }
    }
  }
};
```

# API

Create the Sketcher client interface:

```javascript
const sketcher = new CAMACloud.Sketcher({ url: 'https://app.sketcher.camacloud.com/0.10' });
```

Parameters:

| Name      | Description              |
| --------- | ------------------------ |
| `options` | Options for the Sketcher |

Options:

| Name     | Description                                               |
| -------- | --------------------------------------------------------- |
| `url`    | Sketcher appliction URL                                   |

## Properties

### data

Read-only property that returns an `Object` representing the last [SDS document][sds-document] opened or saved by the Sketcher.

### isClosed

Read-only property that returns a `boolean` indicating whether the Sketcher is closed.

## Methods

### open

Opens the Sketcher.

Parameters:

| Name      | Description              |
| --------- | ------------------------ |
| `options` | Options to open Sketcher |

Options:

| Name        | Description                                                     |
| ----------- | --------------------------------------------------------------- |
| `data`      | An [SDS document][sds-document] object                          |
| `config`    | A [Sketcher configuration][sketcher-configuration] object       |
| `onReady`   | A ready callback to handle when the Sketcher is open and loaded |
| `onClosed`  | A closed callback to handle when the Sketcher is closed         |
| `onLog`     | A log callback to handle messages from the Sketcher             |
| `onSave`    | A save callback to handle save requests from the Sketcher       |
| `svgExport` | SVG export options                                              |

SVG Export Options:

| Name        | Description                                                       |
| ----------- | ----------------------------------------------------------------- |
| `onSuccess` | A callback to handle when export requests to the Sketcher succeed |
| `onFailure` | A callback to handle when export requests to the Sketcher fail    |

The `onLog` callback allows the client application to handle log information emitted by the Sketcher. The log message severity can be one of "info", "warn", "error", or "critical".

The `onSave` callback allows the client application to handle save requests emitted by the Sketcher. The callback receives the save data (the [SDS document][sds-document] to save), a success callback, and a failure callback. **The success or failure callback _MUST_ be called by your application before the Sketcher will allow the user to continue.**

Example:

```javascript
sketcher.open({
  data, // SDS document object
  config, // Sketcher configuration object
  onLog, // log handler
  onSave // save handler
});

/**
 * @param {{ severity: "info" | "warn" | "error" | "critical", code: string, description: string }} message
 * @returns {void}
 */
function onLog(message) {
  // Log message
}

/**
 * @param {Object} saveData
 * @param {() => void} onSuccess
 * @param {(error?: Error) => void} onFailure
 * @returns {void}
 */
function onSave(saveData, onSuccess, onFailure) {
  try {
    // Persist saveData...

    onSuccess();
  } catch (error) {
    onError(error);
  }
}
```

### close

Closes the Sketcher.

Example:

```javascript
sketcher.close();
```

### exportSvg

Requests SVG exports from the Sketcher.

The Sketcher must be open and ready to make this call.  The request's reesponse is received by the callbacks defined on the "svgExport" property of the [open options](#open).

Example:

```javascript
/**
 * @param {{ id: number, page: number, name: string, data: string }[]} sketches
 * @returns {void}
 */
function onSvgExportSuccess(sketches) {
  ...
}

/**
 * @param {string} error
 * @returns {void}
 */
function onSvgExportFailure(error) {
  ...
}

sketcher.open({
  data, // SDS document object
  config, // Sketcher configuration object
  svgExport: {
    onSuccess: onSvgExportSuccess,
    onFailure: onSvgExportFailure,
  },
  ...
});

sketcher.exportSvg({
  data, // SDS document object
  config, // Sketcher configuration object
});
```

# Data Extraction

The Sketcher works with JSON data that conforms to the [SDS][sds]. The following examples show how data can be extracted or aggregated from script.

## Total Area

```javascript
let totalArea = 0;
for (const sketch of data.sketches) {
  for (const segment of sketch.segments) {
    totalArea += segment.area.value;
  }
}
```

## Total Perimeter

```javascript
let totalPerimeter = 0;
for (const sketch of data.sketches) {
  for (const segment of sketch.segments) {
    totalPerimeter += segment.perimeter.value;
  }
}
```

# Demo

The demo consists of a static web application that opens sample SDS data and configuration files into the Sketcher as outlined in the [Getting Started](#getting-started) section.

## Setup

### Prerequisites

- Node.js v14+

### Configuration

- PORT=_port to run on_

### Run

```sh
npm install
npm start
```

Navigate to `http://localhost:3001` from a web browser.

or

```sh
npm install
PORT=<port> npm start
```

Navigate to `http://localhost:<port>` from a web browser.

[sds]: https://schemas.opencamadata.org
[sds-document]: https://woolpert.gitlab.io/product-engineering/cama2/sketch/sketch-data-schema/data.html
[sketcher-configuration]: ../../docs/integration-guide.md#configuration
