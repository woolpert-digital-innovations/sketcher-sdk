# 0.10

## 2022-07-14

- Add export API.

## 2022-01-26

- Add support for SDS document version 0.10.
- Drop support for SDS document version 0.9.
- Validate unique identifiers.

# 0.9

## 2021-11-30

- First release.

# 1.0.3

## 2023-06-01

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

# 1.0.4

## 2023-09-06

- New Feature: Added API to export a base64 encoded PNG of a sketch. REF: 24133 
- New Feature: Added ability to sketch existing CAMA objects. REF: 23634 
- Decimal values now work when typing line lengths. REF: 24989 
- Deleting a line adjacent to the start node no longer causes a line to be missing. REF: 23409 
- Polygon tool no longer loses a vertex when drawing. REF: 22819 
- Arcs no longer invert when the Flip-V or Flip-H function is used. REF: 23667 
- "No Changes Found" message no longer appears after a successful save action. REF: 23800 
- Error no longer occurs when the sketch is saved when there is an undrawn segment. REF: 23799 
- Auto close now works with segments with arcs. REF: 23569 
