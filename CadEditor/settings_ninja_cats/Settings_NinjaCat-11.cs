using CadEditor;
using System;
using System.Collections.Generic;
//css_include settings_ninja_cats/NinjaCatUtils.cs;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(9685, 12 , 8*5);   }
  public int getScreenWidth()          { return 5; }
  public int getScreenHeight()         { return 8; }
  public bool getScreenVertical()      { return true; }
  
  public bool isBigBlockEditorEnabled() { return true; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public int getPalBytesAddr()          { return 0x4d15; }
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0x47ee , 8   , 0x4000); }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x5B2a  , 8   , 0x440); }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc()          { return NinjaCatUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()             { return NinjaCatUtils.getVideoChunk("chr11.bin");   }
  public SetVideoChunkFunc    setVideoChunkFunc()             { return null; }
  public GetBlocksFunc        getBlocksFunc()                 { return NinjaCatUtils.getBlocks;}
  public SetBlocksFunc        setBlocksFunc()                 { return NinjaCatUtils.setBlocks;}
  public GetBigBlocksFunc     getBigBlocksFunc()              { return NinjaCatUtils.getBigBlocks;}
  public SetBigBlocksFunc     setBigBlocksFunc()              { return NinjaCatUtils.setBigBlocks;}
  public GetPalFunc           getPalFunc()                    { return NinjaCatUtils.readPalFromBin("pal11.bin"); }
  public SetPalFunc           setPalFunc()                    { return null;}
  public GetObjectsFunc getObjectsFunc()                      { return NinjaCatUtils.getObjects;  }
  public SetObjectsFunc setObjectsFunc()                      { return NinjaCatUtils.setObjects;  }
}