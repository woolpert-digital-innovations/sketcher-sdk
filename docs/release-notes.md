# Release Notes

## 2.0.0

### 2025-12-04

#### ⚠️ BREAKING CHANGES

- #421: Support for SDS version < 2.0 dropped
  - required `$schema` is `https://schemas.opencamadata.org/2.0/data.schema.json`
  - refer to [SDS 2.0 release notes](https://github.com/woolpert-digital-innovations/sketcher-sdk/blob/main/docs/sds/release-notes.md#20)
- #422: Support for config version < 2.0 dropped
  - required `version` is `2.0`
- Export SVG API
  - renamed type `{ type: 'get-image-svg'; data: { data: <sketch data>; config: <config data> } }` -> `{ type: 'getImageSvg'; data: { data: <sketch data>; config: <config data> } }`
  - renamed type `{ type: 'get-image-svg'; data: { error: string } }` -> `{ type: 'getImageSvg'; data: { error: string } }`
  - renamed type `{ type: 'get-image-svg'; data: { sketches: { id: number; page: number; name: string; data: string; }[] } }` -> `{ type: 'getImageSvg'; data: { sketches: { id: number; page: number; name: string; data: string; }[] } }`
- Export PNG API
  - renamed type `{ type: 'get-image-png'; data: { data: <sketch data>; config: <config data> } }` -> `{ type: 'getImagePng', data: { data: <sketch data>; config: <config data> } }`
  - renamed type `{ type: 'get-image-png'; data: { error: string } }` -> `{ type: 'getImagePng', data: { error: string } }`
  - renamed type `{ type: 'get-image-png'; data: { sketches: { id: number; page: number; name: string; data: string; }[] } }` -> `{ type: 'getImagePng'; data: { sketches: { id: number; page: number; name: string; data: string; }[] } }`
- Sketch Select API
  - renamed type and changed data `{ type: 'buildingSelect'; data: { buildingID: number; } }` -> `{ type: 'sketchSelect'; data: { sketchId: number; } }`
- Page Select API
  - changed data `{ type: 'pageSelect'; data: { sketchID: number; pageID: number; } }` -> `{ type: 'pageSelect'; data: { sketchId: number; pageId: number; } }`
- Sketch Create API
  - renamed type and changed data `{ type: 'addSketch'; data: string }` -> `{ type: 'sketchCreate'; data: { sketchId: number; label: string; } }`
- Sketch Delete API
  - renamed type and changed data `{ type: 'deleteSketch'; data: string }` -> `{ type: 'sketchDelete'; data: { sketchId: number; } }`
- Polygon Create API
  - renamed type and changed data `{ type: 'polycreate'; data: { id: number; segmentID: number; area: number; perimeter: number; labelcode: string; } }` -> `{ type: 'polygonCreate'; data: { sketchId: number; vectorId: number; area: number; perimeter: number; lookupCode: string; } }`
- Polygon Select API
  - renamed type and changed data `{ type: 'polyclick'; data: { id: number; polyid: number; area: number; perimeter: number; labelcode: string; } }` -> `{ type: 'polygonSelect'; data: { sketchId: number; vectorId: number; area: number; perimeter: number; lookupCode: string; } }`
- Select Polygon API
  - changed data `{ type: 'selectPolygon'; data: { data: { sketchID: number; segmentID: number; pageID?: number; } } }` -> `{ type: 'selectPolygon'; data: { sketchId: number; vectorId: number; pageId?: number; } }`
  - vectorId is the unique id
- Select Polygon Failure API
  - renamed type and changed data `{ type: 'Error'; data: string; }` -> `{ type: 'selectPolygon'; data: { error: string } }`

#### 🐞 Fixes

- 86abxze4r: Starting node does not follow when moving segment line
- 86abuk324: Unable to use angle tool in key pad
- 86ac47y95: Linked area calculations not displaying correctly in area breakdown table
- 86a81y95u: Quick shape sometimes produces inaccurate dimensions
- 86ac2qwa6: Quick shape total SQFT calculation is off
- 86abq8qe5: Segment lines/walls lost after pressing the + sign on keyboard
- 86abx2grn: Ellipse/circles do not close

#### ✨ Features

- #422: Support SDS version 2.0
- #421: Support config version 2.0

## 1.1.1

### 2024-11-15

- New Feature: New Event Driven APIs added. REF: 86a2j66tp
- New Feature: Floor From and Floor To values for Commercial sketching. REF: 86a1bzpym
  *Configuration Update
- New Feature: When selected, an overview of the segment is sent over the broadcast channel. REF: 86a1k3tkc
- New Feature: A ‘hideCloseButton’ capability has been added to the configuration. REF: 86a1m0p7n
  *Configuration Update
- New Feature: Configuration updates to select which sketch labels are available by property type. REF: 86a21wwh1
  *Configuration Update
- New Feature: Right click menu has been added to bring selections forward and send back. REF: 86a25amtj
- New Feature: Ability to select multiple segments or multiple lines using ctrl + left click. REF: 86a2j3870
- The keypad is disabled when adding a placed outbuilding is selected. REF: 86a1b5m6z
  *Configuration Update
- Undo/Redo is available for arcs. REF: 86a1bvc2z
- Existing outbuilding label’s position to be changed when flipping. REF: 86a1dvqau
- Prompt added to confirm segment deletion when using the “delete” key. REF: 86a1dw0dc
- Warning message will appear when no more nodes can be added. REF: 86a1dw16p
- On a new segment with a line deleted, auto closing the segment will function correctly. REF: 86a1dw37b
- Buildings with no sketch will save without error. REF: 86a1e14m3
- Floor numbers accept only whole numbers. REF: 86a2hzbqh
- Vector sorting updated to improve display. REF: 86a2hzdy9
- Segment ID added to ‘Add Label’ and ‘Edit Label’ display. REF: 86a2j3bfy
- Label, dimensions, and area labels of the segment will adjust with zoom. REF: 86a2j643h
- Linked segments calculate SQFT correctly on area breakdown display. REF: 86a4djjw5

## 1.0.4

### 2023-09-06

- New Feature: Added API to export a base64 encoded PNG of a sketch. REF: 24133 
- New Feature: Added ability to sketch existing CAMA objects. REF: 23634 
- Decimal values now work when typing line lengths. REF: 24989 
- Deleting a line adjacent to the start node no longer causes a line to be missing. REF: 23409 
- Polygon tool no longer loses a vertex when drawing. REF: 22819 
- Arcs no longer invert when the Flip-V or Flip-H function is used. REF: 23667 
- "No Changes Found" message no longer appears after a successful save action. REF: 23800 
- Error no longer occurs when the sketch is saved when there is an undrawn segment. REF: 23799 
- Auto close now works with segments with arcs. REF: 23569 

## 1.0.3

### 2023-06-01

- New Feature: When the move sketch function is activated, the arrow keys move the sketch. REF: 23114
- New Feature: Polygon Tool now highlights yellow to indicate that it is active. REF: 22661
- Delete line tool now works for arcs. REF: 23322
- Anchor/Origin reference no longer moves when the Flip-V or Flip-H tool is used. REF: 23321
- Dotted line of previously drawn shape is no longer created when clicking Delete Segment before second segment is ready to be drawn. REF: 23320
- Duplication of first segment no longer occurs when next segment is ready to be drawn. REF: 23319
- Can no longer auto-complete a single node or line. REF: 23318
- CTRL+Z no longer errors when undoing a segment on an ellipse drawn with the keys tool. REF: 23317
- Zoom slider's zoom out button now zooms down to 5%. REF: 23316
- First arc is no longer flattened to a line when second arc is drawn with the ARC Polygon Tool. REF: 23315
- Polygon sides selection list no longer duplicates when sketcher is reloaded. REF: 23004
- The current padding value and location of the dimension labels now show in the Label Edit Tool. REF: 23003
- 90 Degree Angle Lock no longer remains active after another tool is selected. REF: 22818

## 0.10.0

### 2022-07-14

- Add export API.
- Add support for SDS document version 0.10.
- Drop support for SDS document version 0.9.
- Validate unique identifiers.

## 0.9.0

### 2021-11-30

- First release.
