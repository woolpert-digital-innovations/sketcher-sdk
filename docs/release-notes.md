# Release Notes

## 3.0.0 (2026-05-15)

### ⚠️ BREAKING CHANGES

* Update underlying application code to React allowing application to be used as hosted app (as it was prior), an HTML Widget, or a React Component

#### APIs

* Select Polygon API
  * Change data

    ```diff
    {
      type: 'selectPolygon';
      data: {
    -   sketchId: number;
        vectorId: number;
    -   pageId?: number;
      }
    }
    ```
  
  * Requires vectorId (other data properties will be ignored)

* Delete Polygon API
  * Change data

    ```diff
    {
      type: 'deletePolygon';
      data: {
    -   sketchId: number;
        vectorId: number;
    -   pageId?: number;
      }
    }
    ```

  * Requires vectorId (other data properties will be ignored)

### ✨ Features

* **#343:** Update Undo/Redo to be consistently located in the topmost toolbar as icons
* **#125:** Update Undo/Redo to include more actions (such as keyboard input)
* **#126:** Update Keys interface to accept keyboard input in fields
* **#126:** Update Keys interface to visually disable unavailable options (options are still clickable and provide prompts on why they are unavailable as they did previously)
* Update `Pan Control` to consistently pan the sketch (previously would also act as micro movement in some app states)
* **#166:** Move micro movement functionality from `Pan Control` to default movement of arrows under `Keys`
* Update toolbar buttons to show as disabled when they are unavailable
* **#345:** Update dialogs to preselect the first field or primary option allowing for quick keyboard input
* Update `Delete` key interaction so it applies to the currently selected object (Previously selecting a line and pressing delete would remove the segment instead of the selected line)
* **#202:** Update `Area Breakdown` to be draggable
* **#200:** Update `CAMA Data` to be draggable
* **#170:** Update so that keyboard `+` can add the initial node
* **#346:** Add text input to background image sliders
* Update Background Image dialog to allow for manual input in addition to sliders
* **#195:** Allow adding sketch when no sketches available (after deleting all sketches)
* Move zoom buttons to topmost toolbar, separate from slider
* **#263:** Update mousewheel zoom to zoom with respect to the mouse cursor
* **#263:** Update zoom buttons to zoom on center of viewport

### 🐞 Fixes

* Prevent ctrl+click pulling up context menu on mac
* **#198:** Enable multi select in Firefox for mac
* Custom fields initialize/update only with explicit form save
* **#216:** Use higher precision math for quick shapes
* Anchor quick shapes to the grid
* Limit dimension label formatting to 1 decimal place
* **#246:** Display "N/A" when angle's not available in drawing mode
* **#283:** Validate on save

## 2.2.2 (2026-03-30)

### 🐞 Fixes

* **#472:** disable add/delete sketch by default ([0fb7041](https://github.com/woolpert-cama-cloud/sketch-app/commit/0fb7041f41c04f26440cdce10c894d4bb1d1cebb)), closes [#472](https://github.com/woolpert-cama-cloud/sketch-app/issues/472)

## 2.2.1 (2026-03-04)

### 🐞 Fixes

* **#463:** Calculate area/perimeter with scaled integers ([b6a74d5](https://github.com/woolpert-cama-cloud/sketch-app/commit/b6a74d5d62a126491586b5692d1ae84a085341e2)), closes [#463](https://github.com/woolpert-cama-cloud/sketch-app/issues/463)
* **#466:** Use reduced tolerance for Vector load ([f3843fe](https://github.com/woolpert-cama-cloud/sketch-app/commit/f3843fe479d888ebb9a6de662999643fe24826b6)), closes [#466](https://github.com/woolpert-cama-cloud/sketch-app/issues/466)
* **#468:** Replace Math.round w/ sRound for addNode fns ([b154a64](https://github.com/woolpert-cama-cloud/sketch-app/commit/b154a64222b50972ef9ef61a7caf737556782241)), closes [#468](https://github.com/woolpert-cama-cloud/sketch-app/issues/468) [#468](https://github.com/woolpert-cama-cloud/sketch-app/issues/468)
* Make negative area type vector area unsigned, consistent with SDS spec ([55a8a46](https://github.com/woolpert-cama-cloud/sketch-app/commit/55a8a46deb1574729c8a38f903524a0e69005c3d))
* Update UI `Vector` references to `Segment` ([9059016](https://github.com/woolpert-cama-cloud/sketch-app/commit/9059016f8983e51b2abd6a1a4c05021bea9ca2de))
* Update vector reference in the segment dropdown ([619d588](https://github.com/woolpert-cama-cloud/sketch-app/commit/619d588b2de56c74887f04871fdf006b16a3d9f7))
* Use correct font scale for export SVG API ([b38ab91](https://github.com/woolpert-cama-cloud/sketch-app/commit/b38ab9188565cae31dd101e5b02c67945279eaab))

## 2.2.0 (2026-01-12)

### 🐞 Fixes

* Render dimension labels on load ([aec1254](https://github.com/woolpert-cama-cloud/sketch-app/commit/aec12541ef05bf3401971c58c9ce35a22dfe0700))
* Use 'centered' default label placement ([2fe83df](https://github.com/woolpert-cama-cloud/sketch-app/commit/2fe83df6a136a9c21907ccfa4ee30e3e37b9b897))

## 2.1.0 (2025-12-16)

### ✨ Features

* **#435:** Add deletePolygon and updateConfig APIs ([66e8aa7](https://github.com/woolpert-cama-cloud/sketch-app/commit/66e8aa71c40e7dc8baf5539e7028eda764fdd955)), closes [#435](https://github.com/woolpert-cama-cloud/sketch-app/issues/435)
* **#443:** Replace CAMA Cloud ℠ references with SketchPro™ ([50dcc7e](https://github.com/woolpert-cama-cloud/sketch-app/commit/50dcc7ebc414b892d782eb29e679bef7b965170c)), closes [#443](https://github.com/woolpert-cama-cloud/sketch-app/issues/443)

### 🐞 Fixes

* **#435:** Only allow one initialization of a sketch in an application instance ([98f1a4b](https://github.com/woolpert-cama-cloud/sketch-app/commit/98f1a4ba705ceb45b606a416eddb0340beb8878a)), closes [#435](https://github.com/woolpert-cama-cloud/sketch-app/issues/435)
* **#442:** Update selectPolygon to match its documented implementation ([0fd6f16](https://github.com/woolpert-cama-cloud/sketch-app/commit/0fd6f160fc5dbc83edaced91f602f7a63c137b79)), closes [#442](https://github.com/woolpert-cama-cloud/sketch-app/issues/442)
* **#447:** Repair selectPolygon API ([5846d77](https://github.com/woolpert-cama-cloud/sketch-app/commit/5846d7745a207928b3c285fd8ab243c1f59b0406)), closes [#447](https://github.com/woolpert-cama-cloud/sketch-app/issues/447)
* Don't materialize floorFrom/floorTo on load ([c90a52a](https://github.com/woolpert-cama-cloud/sketch-app/commit/c90a52a29731de10c5ffb283e5a13b6c31cd93ca))

## 2.0.0 (2025-12-04)

### ⚠️ BREAKING CHANGES

#### SDS

* #421: Support for SDS version < 2.0 dropped
  * Required `$schema` is `https://schemas.opencamadata.org/2.0/data.schema.json`
  * Refer to [SDS 2.0 release notes](https://github.com/woolpert-digital-innovations/sketcher-sdk/blob/main/docs/sds/release-notes.md#20)

#### Config

* #422: Support for config version < 2.0 dropped
  * Required `version` is `2.0`

* Rename sketchCustomFields 

  ```diff
  {
    sketchCustomFields: {
  -   sketchCustomFields: {
  +   items: {
        cField: string;
        cValue: unknown;
        cDataType: string;
      }[];
    };
  }
  ```

* `sketchCustomFields.items` does not accept additional properties

#### APIs
  
* Export SVG API
  * Rename type 

    ```diff
    {
    - type: 'get-image-svg';
    + type: 'getImageSvg';
      data: {
        data: <sketch data>;
        config: <config data>;
      };
    }
    ```

  * Rename type 

    ```diff
    {
    - type: 'get-image-svg';
    + type: 'getImageSvg';
      data: {
        error: string;
      };
    }
    ```

  * Rename type 

    ```diff
    {
    - type: 'get-image-svg';
    + type: 'getImageSvg';
      data: {
        sketches: {
          id: number;
          page: number;
          name: string;
          data: string;
        }[];
      };
    }
    ```

* Export PNG API
  * Rename type 

    ```diff
    {
    - type: 'get-image-png';
    + type: 'getImagePng';
      data: {
        data: <sketch data>;
        config: <config data>;
      };
    }
    ```

  * Rename type 

    ```diff
    {
    - type: 'get-image-png';
    + type: 'getImagePng';
      data: {
        error: string;
      };
    }
    ```

  * Rename type 

    ```diff
    {
    - type: 'get-image-png';
    + type: 'getImagePng';
      data: {
        sketches: {
          id: number;
          page: number;
          name: string;
          data: string;
        }[];
      };
    }
    ```

* Sketch Select API
  * Rename type and change data 

    ```diff
    {
    - type: 'buildingSelect';
    + type: 'sketchSelect';
      data: {
    -   buildingID: number;
    +   sketchId: number;
      };
    }
    ```

* Page Select API
  * Change data 

    ```diff
    {
      type: 'pageSelect';
      data: {
    -   sketchID: number;
    -   pageID: number;
    +   sketchId: number;
    +   pageId: number;
      };
    }
    ```

* Sketch Create API
  * Rename type and change data 

    ```diff
    {
    - type: 'addSketch';
    + type: 'sketchCreate';
    - data: string;
    + data: {
    +   sketchId: number;
    +   label: string;
    + };
    }
    ```

* Sketch Delete API
  * Rename type and change data 

    ```diff
    {
    - type: 'deleteSketch';
    + type: 'sketchDelete';
    - data: string;
    + data: {
    +   sketchId: number;
    + };
    }
    ```

* Polygon Create API
  * Rename type and change data 

    ```diff
    {
    - type: 'polycreate';
    + type: 'polygonCreate';
      data: {
    -   id: number;
    +   sketchId: number;
    -   segmentID: number;
    +   vectorId: number;
        area: number;
        perimeter: number;
    -   labelcode: string;
    +   lookupCode: string;
      };
    }
    ```

* Polygon Select API
  * Rename type and change data 

    ```diff
    {
    - type: 'polyclick';
    + type: 'polygonSelect';
      data: {
    -   id: number;
    +   sketchId: number;
    -   polyid: number;
    +   vectorId: number;
        area: number;
        perimeter: number;
    -   labelcode: string;
    +   lookupCode: string;
      };
    }
    ```

* Select Polygon API
  * Change data 

    ```diff
    {
      type: 'selectPolygon';
      data: {
    -   data: {
    -     sketchID: number;
    -     segmentID: number;
    -     pageID?: number;
    -   };
    +   sketchId: number;
    +   vectorId: number;
    +   pageId?: number;
      };
    }
    ```

  * vectorId is the unique id
* Select Polygon Failure API
  * Rename type and change data 

    ```diff
    {
    - type: 'Error';
    + type: 'selectPolygon';
    - data: string;
    + data: {
    +   error: string;
    + };
    }
    ```

### 🐞 Fixes

* 86abxze4r: Starting node does not follow when moving segment line
* 86abuk324: Unable to use angle tool in key pad
* 86ac47y95: Linked area calculations not displaying correctly in area breakdown table
* 86a81y95u: Quick shape sometimes produces inaccurate dimensions
* 86ac2qwa6: Quick shape total SQFT calculation is off
* 86abq8qe5: Segment lines/walls lost after pressing the + sign on keyboard
* 86abx2grn: Ellipse/circles do not close

### ✨ Features

* #422: Support SDS version 2.0
* #421: Support config version 2.0

## 1.1.1 (2024-11-15)

* New Feature: New Event Driven APIs added. REF: 86a2j66tp
* New Feature: Floor From and Floor To values for Commercial sketching. REF: 86a1bzpym
  *Configuration Update
* New Feature: When selected, an overview of the segment is sent over the broadcast channel. REF: 86a1k3tkc
* New Feature: A ‘hideCloseButton’ capability has been added to the configuration. REF: 86a1m0p7n
  *Configuration Update
* New Feature: Configuration updates to select which sketch labels are available by property type. REF: 86a21wwh1
  *Configuration Update
* New Feature: Right click menu has been added to bring selections forward and send back. REF: 86a25amtj
* New Feature: Ability to select multiple segments or multiple lines using ctrl + left click. REF: 86a2j3870
* The keypad is disabled when adding a placed outbuilding is selected. REF: 86a1b5m6z
  *Configuration Update
* Undo/Redo is available for arcs. REF: 86a1bvc2z
* Existing outbuilding label’s position to be changed when flipping. REF: 86a1dvqau
* Prompt added to confirm segment deletion when using the “delete” key. REF: 86a1dw0dc
* Warning message will appear when no more nodes can be added. REF: 86a1dw16p
* On a new segment with a line deleted, auto closing the segment will function correctly. REF: 86a1dw37b
* Buildings with no sketch will save without error. REF: 86a1e14m3
* Floor numbers accept only whole numbers. REF: 86a2hzbqh
* Segment ID added to ‘Add Label’ and ‘Edit Label’ display. REF: 86a2j3bfy
* Vector sorting updated to improve display. REF: 86a2hzdy9
* Linked segments calculate SQFT correctly on area breakdown display. REF: 86a4djjw5
* Label, dimensions, and area labels of the segment will adjust with zoom. REF: 86a2j643h

## 1.0.4 (2023-09-06)

* New Feature: Added API to export a base64 encoded PNG of a sketch. REF: 24133 
* New Feature: Added ability to sketch existing CAMA objects. REF: 23634 
* Decimal values now work when typing line lengths. REF: 24989 
* Deleting a line adjacent to the start node no longer causes a line to be missing. REF: 23409 
* Polygon tool no longer loses a vertex when drawing. REF: 22819 
* Arcs no longer invert when the Flip-V or Flip-H function is used. REF: 23667 
* "No Changes Found" message no longer appears after a successful save action. REF: 23800 
* Error no longer occurs when the sketch is saved when there is an undrawn segment. REF: 23799 
* Auto close now works with segments with arcs. REF: 23569 

## 1.0.3 (2023-06-01)

* New Feature: When the move sketch function is activated, the arrow keys move the sketch. REF: 23114
* New Feature: Polygon Tool now highlights yellow to indicate that it is active. REF: 22661
* Delete line tool now works for arcs. REF: 23322
* Anchor/Origin reference no longer moves when the Flip-V or Flip-H tool is used. REF: 23321
* Dotted line of previously drawn shape is no longer created when clicking Delete Segment before second segment is ready to be drawn. REF: 23320
* Duplication of first segment no longer occurs when next segment is ready to be drawn. REF: 23319
* Can no longer auto-complete a single node or line. REF: 23318
* CTRL+Z no longer errors when undoing a segment on an ellipse drawn with the keys tool. REF: 23317
* Zoom slider's zoom out button now zooms down to 5%. REF: 23316
* First arc is no longer flattened to a line when second arc is drawn with the ARC Polygon Tool. REF: 23315
* Polygon sides selection list no longer duplicates when sketcher is reloaded. REF: 23004
* The current padding value and location of the dimension labels now show in the Label Edit Tool. REF: 23003
* 90 Degree Angle Lock no longer remains active after another tool is selected. REF: 22818

## 0.10.0 (2022-07-14)

* Add export API
* Add support for SDS document version 0.10
* Drop support for SDS document version 0.9
* Validate unique identifiers

## 0.9.0 (2021-11-30)

* First release
