using CadEditor;
using System;
using System.Collections.Generic;

public static class NinjaCatUtils 
{
    public static List<ObjectList> getObjects(int levelNo)
    {
        LevelRec lr = ConfigScript.getLevelRec(levelNo);
        int objCount = lr.objCount;
        int baseAddr = lr.objectsBeginAddr;
        var objects = new List<ObjectRec>();
        for (int i = 0; i < objCount; i++)
        {
            byte x    = Globals.romdata[baseAddr + objCount*0 + i + 1];
            byte y    = Globals.romdata[baseAddr + objCount*1 + i + 2];
            byte v    = Globals.romdata[baseAddr + objCount*2 + i + 2];
            int scrx  = x >> 4; scrx &= 0x7; //if bit 8 set, that something happen
            int realx = (x &0x0F)*16;
            int realy = y;
            var obj = new ObjectRec(v, scrx, 0, realx, realy);
            objects.Add(obj);
        }
        return new List<ObjectList> { new ObjectList { objects = objects, name = "Objects" } };
    }

  public static bool setObjects(int levelNo, List<ObjectList> objLists)
  {
      LevelRec lr = ConfigScript.getLevelRec(levelNo);
      int objCount = lr.objCount;
      int baseAddr = lr.objectsBeginAddr;
      var objects = objLists[0].objects;
      for (int i = 0; i < objects.Count; i++)
      {
          var obj = objects[i];
          byte x = (byte)((obj.x >> 4) | (obj.sx << 4));
          byte y = (byte)(obj.y & 0xF0);  //first bits can demand enemy creation
          Globals.romdata[baseAddr + objCount*0 + i + 1] = x;
          Globals.romdata[baseAddr + objCount*1 + i + 2] = y;
          Globals.romdata[baseAddr + objCount*2 + i + 2] = (byte)obj.type;
      }
      for (int i = objects.Count; i < objCount; i++)
      {
          Globals.romdata[baseAddr + objCount*0 + i + 1] = 0xFF;
          Globals.romdata[baseAddr + objCount*1 + i + 2] = 0xFF;
          Globals.romdata[baseAddr + objCount*2 + i + 2] = 0xFF;
      }
      return true;
  }
  
  public static ObjRec[] getBlocks(int tileId)
  {
      int addr = ConfigScript.getTilesAddr(tileId);
      int count = ConfigScript.getBlocksCount();
      var blocks = Utils.readBlocksLinear(Globals.romdata, addr, 2, 2, count, false);
      return blocks;
  }
  
  public static void setBlocks(int tileId, ObjRec[] blocksData)
  {
      int addr = ConfigScript.getTilesAddr(tileId);
      int count = ConfigScript.getBlocksCount();
      Utils.writeBlocksLinear(blocksData, Globals.romdata, addr, count, false);
  }
  
  public static BigBlock[] getBigBlocks(int bigTileIndex)
  {
      var data = Utils.readLinearBigBlockData(0, bigTileIndex);
      var bb = Utils.unlinearizeBigBlocks<BigBlockWithPal>(data, 2, 2);
      for (int i = 0; i < bb.Length; i++)
      {
          int palByte = Globals.romdata[ConfigScript.getPalBytesAddr() + i];
          bb[i].palBytes[0] = palByte >> 0 & 0x3;
          bb[i].palBytes[1] = palByte >> 2 & 0x3;
          bb[i].palBytes[2] = palByte >> 4 & 0x3;
          bb[i].palBytes[3] = palByte >> 6 & 0x3;
      }
      return bb;
  }
  
  public static void setBigBlocks(int bigTileIndex, BigBlock[] bigBlockIndexes)
  {
      var data = Utils.linearizeBigBlocks(bigBlockIndexes);
      Utils.writeLinearBigBlockData(0, bigTileIndex, data);
      //save pal bytes
      for (int i = 0; i < bigBlockIndexes.Length; i++)
      {
          var bb = bigBlockIndexes[i] as BigBlockWithPal;
          int palByte = bb.palBytes[0] | bb.palBytes[1] << 2 | bb.palBytes[2]<<4 | bb.palBytes[3]<< 6;
          Globals.romdata[ConfigScript.getPalBytesAddr() + i] = (byte)palByte;
      }
  }
  
  public static GetPalFunc readPalFromBin(string fname)
  {
      return (int _)=> { return Utils.readBinFile(fname); };
  }
  
  public static GetVideoPageAddrFunc fakeVideoAddr()
  {
      return (int _)=> { return -1; };
  }
  
  public static GetVideoChunkFunc getVideoChunk(string fname)
  {
     return (int _)=> { return Utils.readVideoBankFromFile(fname, 0); };
  }
}