using CadEditor;
using System;
using System.Collections.Generic;
//css_include settings_gun_smoke/GunSmokeUtils.cs;

public class Data
{ 
  public OffsetRec getScreensOffset()     { return new OffsetRec(0x410, 1, 8*128);  }
  public OffsetRec getBigBlocksOffset()   { return new OffsetRec(0x10 , 1, 0x4000); }
  public int getScreenWidth()    { return 8; }
  public int getScreenHeight()   { return 128; }
  public int getBigBlocksCount() { return 77; }
  public int getBlocksCount()    { return 64; }
  
  public GetBigTileNoFromScreenFunc getBigTileNoFromScreenFunc() { return getBigTileNoFromScreen; }
  public SetBigTileToScreenFunc     setBigTileToScreenFunc()     { return setBigTileToScreen; }
  
  public bool isBigBlockEditorEnabled() { return true; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc()          { return GunSmokeUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()             { return GunSmokeUtils.getVideoChunk("chr1.bin");   }
  public SetVideoChunkFunc    setVideoChunkFunc()             { return null; }
  public GetBlocksFunc        getBlocksFunc()                 { return GunSmokeUtils.getBlocks;}
  public SetBlocksFunc        setBlocksFunc()                 { return null; }
  public GetBigBlocksFunc     getBigBlocksFunc()              { return GunSmokeUtils.getBigBlocks;}
  public SetBigBlocksFunc     setBigBlocksFunc()              { return GunSmokeUtils.setBigBlocks;}
  public GetPalFunc           getPalFunc()                    { return GunSmokeUtils.readPalFromBin("pal1.bin"); }
  public SetPalFunc           setPalFunc()                    { return null;}
  
  public int getBigTileNoFromScreen(int[] screenData, int index)
  {
    int w = getScreenWidth();
    int h = getScreenHeight();
    int noY = index / w;
    noY = h - noY - 1;
    int noX = index % w;
    return screenData[noY*w + noX];
  }

  public void setBigTileToScreen(int[] screenData, int index, int value)
  {
    int w = getScreenWidth();
    int h = getScreenHeight();
    int noY = index / w;
    noY = h - noY - 1;
    int noX = index % w;
    screenData[noY*w + noX] = value;
  }
}