using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec( 0x0cd60, 6 , 8*6);   }
  public int getScreenWidth()          { return 8; }
  public int getScreenHeight()         { return 6; }
  public string getBlocksFilename()    { return "goonies_2_5.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
}